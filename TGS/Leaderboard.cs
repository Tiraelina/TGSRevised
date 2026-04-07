using WCSharp.Api;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public class Leaderboard
{
    private static int TotalPlayers;
    public static int SlotCount;
    public static multiboard Multiboard;

    public static void Init()
    {
        for (int i = 0; i <= 11; i++)
        {
            player CurrentPlayer = player.Create(i);
            if (CurrentPlayer.SlotState != playerslotstate.Playing)
            {
                continue;
            }

            TotalPlayers += 1;
        }

        CreateMultiboardBJ(3, TotalPlayers + 1, "Score Board");
        Multiboard = GetLastCreatedMultiboard();
        MultiboardSetItemStyleBJ(Multiboard, 0, 0, true, false);
        MultiboardSetItemWidthBJ(Multiboard, 1, 0, 8.00f);
        MultiboardSetItemWidthBJ(Multiboard, 2, 0, 5.00f);
        MultiboardSetItemValueBJ(Multiboard, 2, 0, "0");
        MultiboardSetItemWidthBJ(Multiboard, 3, 0, 5.00f);
        MultiboardSetItemValueBJ(Multiboard, 3, 0, "0");

        MultiboardSetItemValueBJ(Multiboard, 1, 1, "Player");
        MultiboardSetItemColorBJ(Multiboard, 1, 1, 100.00f, 80.00f, 0.00f, 0.00f);
        MultiboardSetItemValueBJ(Multiboard, 2, 1, "Kills");
        MultiboardSetItemColorBJ(Multiboard, 2, 1, 100.00f, 40.00f, 0.00f, 0.00f);
        MultiboardSetItemValueBJ(Multiboard, 3, 1, "Hero Kills");
        MultiboardSetItemColorBJ(Multiboard, 3, 1, 100.00f, 0.00f, 0.00f, 0.00f);

        MultiboardDisplayBJ(true, Multiboard);

        trigger KillCounter = trigger.Create();
        TriggerRegisterAnyUnitEventBJ(KillCounter, playerunitevent.Death);
        KillCounter.AddCondition(Condition(KillCounterAction));
    }

    public static bool KillCounterAction()
    {
        if (GetDyingUnit().Owner == player.Create(PLAYER_NEUTRAL_AGGRESSIVE) &&
            GetDyingUnit().Owner == player.Create(PLAYER_NEUTRAL_PASSIVE) &&
            GetKillingUnit().Owner == player.Create(PLAYER_NEUTRAL_PASSIVE) &&
            GetKillingUnit().Owner == player.Create(PLAYER_NEUTRAL_PASSIVE) &&
            GetKillingUnit() == null &&
            !GetDyingUnit().Owner.IsEnemy(GetKillingUnit().Owner) &&
            !GetDyingUnit().IsUnitType(unittype.Structure) &&
            !GetDyingUnit().IsUnitType(unittype.Summoned))
        {
            return false;
        }

        if (GetDyingUnit().IsUnitType(unittype.Hero))
        {
            if (GetKillingUnit().Owner.Controller == mapcontrol.Computer)
            {
                if (GetKillingUnit().Owner.IsAlly(player.Create(5)))
                {
                    Globals.Players[player.Create(5)].AddHeroKill();
                    return true;
                }

                Globals.Players[player.Create(11)].AddHeroKill();
                return true;
            }

            Globals.Players[GetKillingUnit().Owner].AddHeroKill();
            return true;
        }

        if (GetKillingUnit().Owner.Controller == mapcontrol.Computer)
        {
            if (GetKillingUnit().Owner.IsAlly(player.Create(5)))
            {
                Globals.Players[player.Create(5)].AddUnitKill();
                return true;
            }

            Globals.Players[player.Create(11)].AddUnitKill();
            return true;
        }

        Globals.Players[GetKillingUnit().Owner].AddUnitKill();
        return true;
    }
}
