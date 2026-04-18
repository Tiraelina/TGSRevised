using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Events;
using static TGS.Util;
using static Constants;
using static TGS.Globals;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public enum OrbType
{
    None,
    Feedback,
    // Spawner
    BlackArrow,
    Ooze,
    Shred,
    Damage,
    Slow,
    Poison,
    Incinerate,
    Pillage,
    Purge,
}

public class TGSPlayer
{
    public static unit PassiveEnabler;
    public static timer PassiveBusterTimer;
    public effect BladestormModel;
    public timer BladestormTimer;
    public PlayerColor Color;
    public timer DeathTimer;
    public timerdialog DeathTimerDialog;
    public unit Hero;
    public TGSHero TGSHeroData;
    public int HeroId;
    public int HeroKills;
    public string Name;
    public player Player;
    public int Row;
    public int UnitKills;

    public TGSPlayer(player InPlayer)
    {
        Player = InPlayer;
        if (Player.Controller == mapcontrol.Computer)
        {
            Name = Player.Name;
        }
        else
        {
            // Strip tag
            Name = Player.Name.Remove(Player.Name.Length - 5, 5);
        }

        Color = PlayerColors[Player.Id];
        Row = Leaderboard.SlotCount + 2;
        Leaderboard.SlotCount += 1;
        MultiboardSetItemValueBJ(Leaderboard.Multiboard, 1, Row, Name);
        MultiboardSetItemColorBJ(Leaderboard.Multiboard, 1, Row, Color.R, Color.G, Color.B, 0);
        MultiboardSetItemValueBJ(Leaderboard.Multiboard, 3, Row, UnitKills.ToString());
        MultiboardSetItemValueBJ(Leaderboard.Multiboard, 3, Row, HeroKills.ToString());
    }

    public void SetHero(unit InHero)
    {
        Hero = InHero;
        TGSHeroData = new TGSHero(Hero, Player);
        HeroId = Hero.UnitType;
        Hero.AddAbility(ABILITY_A0JU_RETURN);
        Player.Lumber += 6;
        PassiveEnabler = unit.Create(Player, UNIT_O003_PASSIVE_BUSTER_TO_WEAR_OFF, Corner.X, Corner.Y);

        trigger ReviveTrigger = trigger.Create();
        ReviveTrigger.RegisterDeathEvent(Hero);
        ReviveTrigger.AddAction(Died);

        trigger HeroKill = trigger.Create();
        TriggerRegisterUnitEvent(HeroKill, Hero, EVENT_UNIT_DEATH);
        TriggerAddAction(HeroKill, DeathMessage);
    }

    public void SetName(string InName, PlayerColor InPlayerColor)
    {
        Name = InName;
        MultiboardSetItemValueBJ(Leaderboard.Multiboard, 1, Row, Name);
        MultiboardSetItemColorBJ(Leaderboard.Multiboard, 1, Row, InPlayerColor.R, InPlayerColor.G, InPlayerColor.B, 0);
    }

    public void AddUnitKill()
    {
        UnitKills += 1;
        MultiboardSetItemValueBJ(Leaderboard.Multiboard, 2, Row, UnitKills.ToString());
    }

    public void AddHeroKill()
    {
        HeroKills += 1;
        MultiboardSetItemValueBJ(Leaderboard.Multiboard, 3, Row, HeroKills.ToString());
    }

    public void Died()
    {
        BladestormModel.Dispose();
        PolledWait(1.5f);
        DeathTimer = timer.Create();
        DeathTimer.Start(10.0f + Math.Min(15.0f, Hero.Level) * 3.0f, false, Revive);
        DeathTimerDialog = timerdialog.Create(DeathTimer);
        DeathTimerDialog.SetTitle(Name);
        DeathTimerDialog.IsDisplayed = true;
    }

    public void Bladestorm()
    {
        BladestormModel = AddSpecialEffectTargetUnitBJ("origin", GetTriggerUnit(), @"war3mapImported\bladestorm.mdx");
        BladestormTimer = timer.Create();
        BladestormTimer.Start(7.0f, false, () => { BladestormModel.Dispose(); });
    }

    private static void DeathMessage()
    {
        if (GetDyingUnit().IsUnitType(unittype.Hero)
            && GetDyingUnit().IsEnemyTo(GetKillingUnit().Owner))
        {
            if (GetKillingUnit().Owner != Player(PLAYER_NEUTRAL_AGGRESSIVE))
            {
                if (GetKillingUnit().Owner.Controller == mapcontrol.Computer)
                {
                    if (GetKillingUnit().IsAllyTo(player.Create(5)))
                    {
                        QuestMessageBJ(GetPlayersAll(), bj_QUESTMESSAGE_HINT,
                            "|cffff0000" + Players[GetDyingUnit().Owner].Name + "'s hero was killed by |cffffff00" + Players[player.Create(5)].Name);
                        AdjustPlayerStateBJ(GetDyingUnit().Level * 15 / (CountPlayersInForceBJ(Orc) - 3), GetEnumPlayer(), PLAYER_STATE_RESOURCE_GOLD);
                    }
                    else
                    {
                        QuestMessageBJ(GetPlayersAll(), bj_QUESTMESSAGE_HINT,
                            "|cffff0000" + Players[GetDyingUnit().Owner].Name + "'s hero was killed by |cffffff00" + Players[player.Create(11)].Name);
                        AdjustPlayerStateBJ(GetDyingUnit().Level * 15 / (CountPlayersInForceBJ(Human) - 3), GetEnumPlayer(), PLAYER_STATE_RESOURCE_GOLD);
                    }
                }
                else
                {
                    QuestMessageBJ(GetPlayersAll(), bj_QUESTMESSAGE_HINT,
                        "|cffff0000" + Players[GetDyingUnit().Owner].Name + "'s hero was killed by |cffffff00" + Players[GetKillingUnit().Owner].Name);
                    AdjustPlayerStateBJ(GetDyingUnit().Level * 15, GetKillingUnit().Owner, PLAYER_STATE_RESOURCE_GOLD);
                }
            }
            else
            {
                QuestMessageBJ(GetPlayersAll(), bj_QUESTMESSAGE_HINT,
                    "|cffff0000" + Players[GetDyingUnit().Owner].Name + "'s hero got dunked by |cffffff00" + GetKillingUnit().Name + "|cfff00f00 in an alley.");
            }

            return;
        }

        if (GetDyingUnit().IsUnitType(unittype.Hero)
            && GetDyingUnit().IsAllyTo(GetKillingUnit().Owner))
        {
            QuestMessageBJ(GetPlayersAll(), bj_QUESTMESSAGE_HINT,
                "|cffff0000" + Players[GetDyingUnit().Owner].Name + "'s hero tasted the towel whip by |cffffff00" + Players[GetKillingUnit().Owner].Name);
        }
    }

    public void Revive()
    {
        DeathTimerDialog.Dispose();
        if (Hero.IsAllyTo(player.Create(5)))
        {
            Hero.Revive(HumFountain.X, HumFountain.Y);
            PanCameraToTimedLocForPlayer(Hero.Owner, GetUnitLoc(HumFountain), 0.50f);
        }
        else
        {
            Hero.Revive(OrcFountain.X, OrcFountain.Y);
            PanCameraToTimedLocForPlayer(Hero.Owner, GetUnitLoc(OrcFountain), 0.50f);
        }

        SelectUnitForPlayerSingle(Hero, Hero.Owner);
    }

    public void PassiveBusterActivate()
    {
        RemoveUnit(PassiveEnabler);
        if (PassiveBusterTimer == null)
        {
            PassiveBusterTimer = timer.Create();
        }
        else
        {
            PassiveBusterTimer.Dispose();
            PassiveBusterTimer = timer.Create();
        }

        PassiveBusterTimer.Start(15.0f, false, PassiveBusterDeactivate);
    }

    private void PassiveBusterDeactivate()
    {
        PassiveEnabler = unit.Create(Player, UNIT_O003_PASSIVE_BUSTER_TO_WEAR_OFF, Corner.X, Corner.Y);
        PassiveBusterTimer.Dispose();
    }
}
