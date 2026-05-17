using System.Collections.Generic;
using WCSharp.Api;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public class SummonLimitPair
{
    public string Name { get; set; }
    public int Limit { get; set; }

    public SummonLimitPair(string InName, int InLimit)
    {
        Name = InName;
        Limit = InLimit;
    }
}

public static class SummonLimiter
{
    private static readonly List<SummonLimitPair> SummonLimits = new();

    private static readonly List<player> Players = new()
    {
        Player(0), Player(1), Player(2), Player(3), Player(4),
        Player(6), Player(7), Player(8), Player(9), Player(10)
    };

    public static void Init()
    {
        SummonLimits.Add(new SummonLimitPair("Hawk", 1));
        SummonLimits.Add(new SummonLimitPair("Walter Lemontal", 3));
        SummonLimits.Add(new SummonLimitPair("Bear", 1));
        SummonLimits.Add(new SummonLimitPair("Quilbeast", 3));
        SummonLimits.Add(new SummonLimitPair("Doom Guard", 2));
        SummonLimits.Add(new SummonLimitPair("Sapphiron", 2));
        SummonLimits.Add(new SummonLimitPair("Sea Elemental", 2));

        trigger SummonTrigger = trigger.Create();

        foreach (player Owner in Players)
        {
            TriggerRegisterPlayerUnitEvent(SummonTrigger, Owner, EVENT_PLAYER_UNIT_SUMMON, null);
            TriggerRegisterPlayerUnitEvent(SummonTrigger, Owner, EVENT_PLAYER_UNIT_CHANGE_OWNER, null);
        }

        TriggerAddCondition(SummonTrigger, Condition(Conditions));
        TriggerAddAction(SummonTrigger, Actions);
    }

    private static bool Conditions()
    {
        unit Summoner = GetSummoningUnit();
        if (Summoner == null)
        {
            return false;
        }

        player Owner = Summoner.Owner;
        if (Owner.Controller != mapcontrol.User)
        {
            return false;
        }

        unit Summoned = GetSummonedUnit();
        string SummonName = Summoned.Name;

        foreach (SummonLimitPair Pair in SummonLimits)
        {
            if (SummonName.StartsWith(Pair.Name))
            {
                int SummonCount = CountLivingPlayerUnitsOfTypeId(GetUnitTypeId(Summoned), Owner);
                if (SummonCount > Pair.Limit)
                {
                    return true;
                }

                break;
            }
        }

        return false;
    }

    private static void Actions()
    {
        player Owner = GetTriggerPlayer();
        unit Summoned = GetSummonedUnit();
        int SummonId = Summoned.UnitType;

        group SummonGroup = CreateGroup();
        GroupEnumUnitsOfPlayer(SummonGroup, Owner, null);
        SummonGroup.Dispose();

        SummonGroup = GetUnitsOfPlayerAndTypeId(Owner, SummonId);

        unit Victim = null;
        float LowestHp = float.MaxValue;

        ForGroup(SummonGroup, () =>
        {
            unit Summon = GetEnumUnit();
            if (Summon.Alive && Summon != Summoned)
            {
                float Hp = Summon.Life;
                if (Hp < LowestHp)
                {
                    LowestHp = Hp;
                    Victim = Summon;
                }
            }
        });

        if (Victim == null)
        {
            Victim = GroupPickRandomUnit(SummonGroup);
        }

        if (Victim != null)
        {
            KillUnit(Victim);
        }

        SummonGroup.Dispose();
    }
}
