using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Api.Enums;
using WCSharp.Events;
using WCSharp.Shared.Data;
using static TGS.Util;
using static Constants;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public static class Globals
{
    public static Dictionary<int, PlayerColor> PlayerColors = new();
    public static Dictionary<player, TGSPlayer> Players = new();
    public static List<player> ConditionFilter = new();
    public static List<player> HumConditionFilter = new();
    public static List<player> OrcConditionFilter = new();
    public static Dictionary<unit, effect> ShopEffects = new();
    public static unit FrenchmansEndWest;
    public static unit FrenchmansEndEast;
    public static unit RuinsShopWest;
    public static unit RuinsShopEast;
    public static unit GoblinWest;
    public static unit GoblinEast;
    public static List<destructable> NorthGates = new();
    public static List<destructable> SouthGates = new();
    public static List<destructable> NorthBlockers = new();
    public static List<destructable> SouthBlockers = new();
    public static force Human;
    public static force Orc;
    public static float XPMod;
    public static float HumXPSplit;
    public static float OrcXPSplit;
    public static Point Corner;
    public static unit HumCastle;
    public static unit HumFountain;
    public static unit OrcCastle;
    public static unit OrcFountain;
    public static unit HumBarracksTop;
    public static unit HumBarracksMid;
    public static unit HumBarracksBot;
    public static unit HumAviaryTop;
    public static unit HumAviaryMid;
    public static unit HumAviaryBot;
    public static unit HumSanctumTop;
    public static unit HumSanctumMid;
    public static unit HumSanctumBot;
    public static unit HumWorkshopTop;
    public static unit HumWorkshopMid;
    public static unit HumWorkshopBot;
    public static unit HumTagawa;
    public static unit OrcBarracksTop;
    public static unit OrcBarracksMid;
    public static unit OrcBarracksBot;
    public static unit OrcBeastiaryTop;
    public static unit OrcBeastiaryMid;
    public static unit OrcBeastiaryBot;
    public static unit OrcLodgeTop;
    public static unit OrcLodgeMid;
    public static unit OrcLodgeBot;
    public static unit OrcTotemTop;
    public static unit OrcTotemMid;
    public static unit OrcTotemBot;
    public static unit OrcTagawa;
    public static location LeapAttackStart;
    public static location LeapAttackTarget;
    public static unit LeapAttackUnit;
    public static float LeapAttackSpeed;
    public static timer LeapAttackMove;
    public static List<sound> LeapAttackSound = new();
    public static float Ratio;
    public static sound HeroMountainKingYesAttack1;
    public static sound CR_Jaraxxus_Special01;
    public static timer GoldMergerT1;
    public static timer GoldMergerT2;
    public static timer GoldMergerT3;
    public static List<item> Coins75 = new();
    public static List<item> Coins150 = new();
    public static List<item> Coins300 = new();
#if DEBUG
    // It will only output ~259 characters as one string.
    public static List<string> OutputBuffer = new();
#endif

    public static void Init()
    {
        // Delay a little since some stuff can break otherwise
        timer SpawnDelay = CreateTimer();
        SpawnDelay.Start(0.25f, false, () =>
        {
            InitGlobalVariables();
            SpawnDelay.Dispose();
        });
        StartGoldMerger();

        Corner = new Point(-10000.0f, 8000.0f);
        FrenchmansEndWest = GetUnitAt(-8640.0f, 6528.0f);
        FrenchmansEndEast = GetUnitAt(8256.0f, 6848.0f);
        RuinsShopWest = GetUnitAt(-9056.0f, -7136.0f);
        RuinsShopEast = GetUnitAt(9120.0f, -7136.0f);
        SetShopState(FrenchmansEndWest, false);
        SetShopState(FrenchmansEndEast, false);
        SetShopState(RuinsShopWest, false);
        SetShopState(RuinsShopEast, false);
        GoblinWest = GetUnitAt(-1472.0f, -2752.0f);
        GoblinEast = GetUnitAt(1536.0f, 2048.0f);

        // Human
        HumCastle = GetUnitAt(-7680.0f, 0.0f);
        HumFountain = GetUnitAt(-9344.0f, 2048.0f);
        HumBarracksTop = GetUnitAt(-6144.0f, 3328.0f);
        HumBarracksMid = GetUnitAt(-4352.0f, 0.0f);
        HumBarracksBot = GetUnitAt(-6144.0f, -3328.0f);
        HumAviaryTop = GetUnitAt(-6656.0f, 2816.0f);
        HumAviaryMid = GetUnitAt(-5120.0f, 0.0f);
        HumAviaryBot = GetUnitAt(-6656.0f, -2816.0f);
        HumSanctumTop = GetUnitAt(-7168.0f, 2304.0f);
        HumSanctumMid = GetUnitAt(-5888.0f, 0.0f);
        HumSanctumBot = GetUnitAt(-7168.0f, -2304.0f);
        HumWorkshopTop = GetUnitAt(-7680.0f, 1792.0f);
        HumWorkshopMid = GetUnitAt(-6656.0f, 0.0f);
        HumWorkshopBot = GetUnitAt(-7680.0f, -1792.0f);
        HumTagawa = GetUnitAt(-8401.5f, 957.5f);

        // Orc
        OrcCastle = GetUnitAt(7680.0f, 0.0f);
        OrcFountain = GetUnitAt(9344.0f, 2048.0f);
        OrcBarracksTop = GetUnitAt(6144.0f, 3328.0f);
        OrcBarracksMid = GetUnitAt(4352.0f, 0.0f);
        OrcBarracksBot = GetUnitAt(6144.0f, -3328.0f);
        OrcBeastiaryTop = GetUnitAt(6656.0f, 2816.0f);
        OrcBeastiaryMid = GetUnitAt(5120.0f, 0.0f);
        OrcBeastiaryBot = GetUnitAt(6656.0f, -2816.0f);
        OrcLodgeTop = GetUnitAt(7168.0f, 2304.0f);
        OrcLodgeMid = GetUnitAt(5888.0f, 0.0f);
        OrcLodgeBot = GetUnitAt(7168.0f, -2304.0f);
        OrcTotemTop = GetUnitAt(7680.0f, 1792.0f);
        OrcTotemMid = GetUnitAt(6656.0f, 0.0f);
        OrcTotemBot = GetUnitAt(7680.0f, -1792.0f);
        OrcTagawa = GetUnitAt(8328.2f, 845.0f);

        ConditionFilter.Add(player.Create(0));
        ConditionFilter.Add(player.Create(1));
        ConditionFilter.Add(player.Create(2));
        ConditionFilter.Add(player.Create(3));
        ConditionFilter.Add(player.Create(4));
        ConditionFilter.Add(player.Create(6));
        ConditionFilter.Add(player.Create(7));
        ConditionFilter.Add(player.Create(8));
        ConditionFilter.Add(player.Create(9));
        ConditionFilter.Add(player.Create(10));

        HumConditionFilter.Add(player.Create(5));
        HumConditionFilter.Add(player.Create(12));
        HumConditionFilter.Add(player.Create(13));
        HumConditionFilter.Add(player.Create(14));

        OrcConditionFilter.Add(player.Create(11));
        OrcConditionFilter.Add(player.Create(15));
        OrcConditionFilter.Add(player.Create(16));
        OrcConditionFilter.Add(player.Create(17));

        PlayerColors.Add(0, new PlayerColor(100.0f, 1.0f, 1.0f));
        PlayerColors.Add(1, new PlayerColor(0.0f, 25.0f, 100.0f));
        PlayerColors.Add(2, new PlayerColor(11.0f, 90.0f, 72.0f));
        PlayerColors.Add(3, new PlayerColor(33.0f, 0.0f, 50.0f));
        PlayerColors.Add(4, new PlayerColor(100.0f, 99.0f, 0.0f));
        PlayerColors.Add(5, new PlayerColor(100.0f, 54.0f, 5.0f));
        PlayerColors.Add(6, new PlayerColor(12.0f, 75.0f, 0.0f));
        PlayerColors.Add(7, new PlayerColor(90.0f, 35.0f, 69.0f));
        PlayerColors.Add(8, new PlayerColor(58.0f, 58.0f, 59.0f));
        PlayerColors.Add(9, new PlayerColor(49.0f, 75.0f, 95.0f));
        PlayerColors.Add(10, new PlayerColor(9.0f, 38.0f, 27.0f));
        PlayerColors.Add(11, new PlayerColor(30.0f, 16.0f, 1.0f));
        EnumDestructablesInRect(Regions.NorthGates.Rect, null, () =>
        {
            destructable SightBlocker = GetEnumDestructable();
            if (GetDestructableTypeId(SightBlocker) == FourCC("YTlb"))
            {
                NorthBlockers.Add(GetEnumDestructable());
            }
            else if (GetDestructableTypeId(SightBlocker) == FourCC("ZTsx") || GetDestructableTypeId(SightBlocker) == FourCC("B000"))
            {
                NorthGates.Add(GetEnumDestructable());
            }
        });
        EnumDestructablesInRect(Regions.SouthGates.Rect, null, () =>
        {
            destructable SightBlocker = GetEnumDestructable();
            if (GetDestructableTypeId(SightBlocker) == FourCC("YTlb"))
            {
                SouthBlockers.Add(GetEnumDestructable());
            }
            else if (GetDestructableTypeId(SightBlocker) == FourCC("ZTsx") || GetDestructableTypeId(SightBlocker) == FourCC("B000"))
            {
                SouthGates.Add(GetEnumDestructable());
            }
        });
        foreach (destructable Gate in NorthGates)
        {
            Gate.IsInvulnerable = true;
        }

        foreach (destructable Gate in SouthGates)
        {
            Gate.IsInvulnerable = true;
        }

        MeleeStartingVisibility();
    }

    private static void StartGoldMerger()
    {
        GoldMergerT1 = CreateTimer();
        GoldMergerT1.Start(15.0f, true, () =>
        {
            List<item> newCoins = Coins75;
            foreach (item Coin1 in newCoins)
            {
                if (Coin1 == null) continue;
                foreach (item Coin2 in newCoins)
                {
                    if (Coin2 == null) continue;
                    if (Coin2 == Coin1) continue;

                    float dx = GetItemX(Coin1) - GetItemX(Coin2);
                    float dy = GetItemY(Coin1) - GetItemY(Coin2);
                    float dist = (float)Math.Sqrt(dx * dx + dy * dy);

                    if (dist <= 300.0f)
                    {
                        float midX = (GetItemX(Coin1) + GetItemX(Coin2)) / 2;
                        float midY = (GetItemY(Coin1) + GetItemY(Coin2)) / 2;

                        Coins150.Add(CreateItem(ITEM_I03E_150_BANANAS, midX, midY));

                        Coins75.Remove(Coin1);
                        Coin1.Dispose();
                        Coins75.Remove(Coin2);
                        Coin2.Dispose();
                        break;
                    }
                }
            }
        });
        GoldMergerT2 = CreateTimer();
        GoldMergerT2.Start(30.0f, true, () =>
        {
            List<item> newCoins = Coins150;
            foreach (item Coin1 in newCoins)
            {
                if (Coin1 == null) continue;
                foreach (item Coin2 in newCoins)
                {
                    if (Coin2 == null) continue;
                    if (Coin2 == Coin1) continue;

                    float dx = GetItemX(Coin1) - GetItemX(Coin2);
                    float dy = GetItemY(Coin1) - GetItemY(Coin2);
                    float dist = (float)Math.Sqrt(dx * dx + dy * dy);

                    if (dist <= 300.0f)
                    {
                        float midX = (GetItemX(Coin1) + GetItemX(Coin2)) / 2;
                        float midY = (GetItemY(Coin1) + GetItemY(Coin2)) / 2;

                        Coins300.Add(CreateItem(ITEM_I03F_300_GOLDEN_CHEESE_COINS, midX, midY));

                        Coins150.Remove(Coin1);
                        Coin1.Dispose();
                        Coins150.Remove(Coin2);
                        Coin2.Dispose();
                        break;
                    }
                }
            }
        });
        // GoldMergerT3 = CreateTimer();
        // GoldMergerT3.Start(60.0f, true, () =>
        // {
        //     List<item> newCoins = Coins300;
        //     foreach (item Coin1 in newCoins)
        //     {
        //         if (Coin1 == null) continue;
        //         foreach (item Coin2 in newCoins)
        //         {
        //             if (Coin2 == null) continue;
        //             if (Coin2 == Coin1) continue;
        //
        //             float dx = GetItemX(Coin1) - GetItemX(Coin2);
        //             float dy = GetItemY(Coin1) - GetItemY(Coin2);
        //             float dist = (float)Math.Sqrt(dx * dx + dy * dy);
        //
        //             if (dist <= 300.0f)
        //             {
        //                 float midX = (GetItemX(Coin1) + GetItemX(Coin2)) / 2;
        //                 float midY = (GetItemY(Coin1) + GetItemY(Coin2)) / 2;
        //
        //                 CreateItem(ITEM_I03F_300_GOLD, midX, midY);
        //
        //                 Coins150.Remove(Coin1);
        //                 Coin1.Dispose();
        //                 Coins150.Remove(Coin2);
        //                 Coin2.Dispose();
        //                 break;
        //             }
        //         }
        //     }
        // });
    }

    private static void InitGlobalVariables()
    {
        Human = GetPlayersAllies(player.Create(5));
        Orc = GetPlayersAllies(player.Create(11));
#if DEBUG
        XPMod = 10.5f;
#else
        XPMod = 1.5f;
#endif
        HumXPSplit = (CountPlayersInForceBJ(Human) - 4.0f) / 5.00f * XPMod;
        OrcXPSplit = (CountPlayersInForceBJ(Orc) - 4.0f) / 5.00f * XPMod;

        ForForce(GetPlayersAll(), InitPlayers);

        trigger SiegeScaling = trigger.Create();
        TriggerRegisterAnyUnitEventBJ(SiegeScaling, EVENT_PLAYER_UNIT_DAMAGING);
        SiegeScaling.AddCondition(Condition(DemolishSiegeScaling));

        trigger TomeFix =  trigger.Create();
        TriggerRegisterAnyUnitEventBJ(TomeFix, EVENT_PLAYER_UNIT_PICKUP_ITEM);
        TomeFix.AddCondition(Condition(FixTomes));

        PlayerUnitEvents.Register(SpellEvent.EndCast, AbilityStops);
        PlayerUnitEvents.Register(SpellEvent.Finish, AbilityStops);

        PlayerUnitEvents.Register(UnitTypeEvent.IsAttacked, HeroAttacked);

        PlayerUnitEvents.Register(UnitTypeEvent.Summons, BountyNegation);

        PlayerUnitEvents.Register(UnitTypeEvent.IsAttacked, PolymorphConcoction);

        PlayerUnitEvents.Register(UnitTypeEvent.Dies, AncestralSpirit);

        trigger PlaverLeaves = trigger.Create();
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(0));
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(1));
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(2));
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(3));
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(4));
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(6));
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(7));
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(8));
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(9));
        TriggerRegisterPlayerEventLeave(PlaverLeaves, player.Create(10));
        TriggerAddAction(PlaverLeaves, PlayerLeft);

        trigger SmokeTrees = trigger.Create();
        TriggerRegisterTimerEvent(SmokeTrees, 180.0f, true);
        TriggerAddAction(SmokeTrees, RespawnTrees);

        trigger HumWin = trigger.Create();
        TriggerRegisterUnitEvent(HumWin, OrcCastle, unitevent.Death);
        TriggerAddAction(HumWin, HumVictory);

        trigger OrcWin = trigger.Create();
        TriggerRegisterUnitEvent(OrcWin, HumCastle, unitevent.Death);
        TriggerAddAction(OrcWin, OrcVictory);

        sound GromYesAttack1 = CreateSound(@"Units\Orc\Hellscream\GromYesAttack1.wav", false, true, true, 10, 10, "HeroAcksEAX");
        SetSoundDuration(GromYesAttack1, 1282);
        SetSoundChannel(GromYesAttack1, 0);
        SetSoundVolume(GromYesAttack1, -1);
        SetSoundPitch(GromYesAttack1, 1.0f);
        SetSoundDistances(GromYesAttack1, 0.0f, 10000.0f);
        SetSoundDistanceCutoff(GromYesAttack1, 3000.0f);
        SetSoundConeAngles(GromYesAttack1, 0.0f, 0.0f, 127);
        SetSoundConeOrientation(GromYesAttack1, 0.0f, 0.0f, 0.0f);
        sound GromYesAttack2 = CreateSound(@"Units\Orc\Hellscream\GromYesAttack2.wav", false, true, true, 10, 10, "HeroAcksEAX");
        SetSoundDuration(GromYesAttack2, 1457);
        SetSoundChannel(GromYesAttack2, 0);
        SetSoundVolume(GromYesAttack2, -1);
        SetSoundPitch(GromYesAttack2, 1.0f);
        SetSoundDistances(GromYesAttack2, 0.0f, 10000.0f);
        SetSoundDistanceCutoff(GromYesAttack2, 3000.0f);
        SetSoundConeAngles(GromYesAttack2, 0.0f, 0.0f, 127);
        SetSoundConeOrientation(GromYesAttack2, 0.0f, 0.0f, 0.0f);
        sound GromYesAttack3 = CreateSound(@"Units\Orc\Hellscream\GromYesAttack3.wav", false, true, true, 10, 10, "HeroAcksEAX");
        SetSoundDuration(GromYesAttack3, 1741);
        SetSoundChannel(GromYesAttack3, 0);
        SetSoundVolume(GromYesAttack3, -1);
        SetSoundPitch(GromYesAttack3, 1.0f);
        SetSoundDistances(GromYesAttack3, 0.0f, 10000.0f);
        SetSoundDistanceCutoff(GromYesAttack3, 3000.0f);
        SetSoundConeAngles(GromYesAttack3, 0.0f, 0.0f, 127);
        SetSoundConeOrientation(GromYesAttack3, 0.0f, 0.0f, 0.0f);
        HeroMountainKingYesAttack1 = CreateSound(@"Units\Human\HeroMountainKing\HeroMountainKingYesAttack1.wav", false, true, true, 10, 10, "HeroAcksEAX");
        SetSoundDuration(HeroMountainKingYesAttack1, 975);
        SetSoundChannel(HeroMountainKingYesAttack1, 0);
        SetSoundVolume(HeroMountainKingYesAttack1, -1);
        SetSoundPitch(HeroMountainKingYesAttack1, 1.0f);
        SetSoundDistances(HeroMountainKingYesAttack1, 0.0f, 10000.0f);
        SetSoundDistanceCutoff(HeroMountainKingYesAttack1, 3000.0f);
        SetSoundConeAngles(HeroMountainKingYesAttack1, 0.0f, 0.0f, 127);
        SetSoundConeOrientation(HeroMountainKingYesAttack1, 0.0f, 0.0f, 0.0f);
        CR_Jaraxxus_Special01 = CreateSound(@"war3mapImported/CR_Jaraxxus_Special01.flac", false, false, false, 0, 0, "DefaultEAXON");
        SetSoundDuration(CR_Jaraxxus_Special01, 2446);
        SetSoundChannel(CR_Jaraxxus_Special01, 0);
        SetSoundVolume(CR_Jaraxxus_Special01, 127);
        SetSoundPitch(CR_Jaraxxus_Special01, 1.0f);
        LeapAttackSound.Add(GromYesAttack1);
        LeapAttackSound.Add(GromYesAttack2);
        LeapAttackSound.Add(GromYesAttack3);
    }

    private static void InitPlayers()
    {
        player CurrentPlayer = GetEnumPlayer();
        Players.Add(CurrentPlayer, new TGSPlayer(CurrentPlayer));
        CurrentPlayer.SetState(playerstate.GivesBounty, 1);
        if (GetPlayerController(CurrentPlayer) == mapcontrol.Computer)
        {
            return;
        }

        CurrentPlayer.Gold = 150;
        if (CurrentPlayer.IsAlly(player.Create(5)))
        {
            CurrentPlayer.HandicapXP = HumXPSplit;
        }
        else
        {
            CurrentPlayer.HandicapXP = OrcXPSplit;
        }
    }

    private static void HeroAttacked()
    {
        if (GetAttacker().UnitType == FourCC("Hdgo") && !GetAttacker().IsIllusion)
        {
            if (GetRandomInt(0, 4) == 0)
            {
                unit Bomb = unit.Create(GetAttacker().Owner, UNIT_O002_CRUSHING_BLOW_BOMB, GetAttackedUnitBJ().X, GetAttackedUnitBJ().Y);
                Bomb.Kill();
            }
        }
    }

    private static bool DemolishSiegeScaling()
    {
        if (BlzGetEventAttackType() == ATTACK_TYPE_SIEGE
            && BlzGetEventDamageTarget().IsUnitType(unittype.Structure)
            && BlzGetEventIsAttack())
        {
            BlzSetEventDamage(GetEventDamage() + GetEventDamage() * (I2R(GetPlayerTechCountSimple(FourCC("R001"), GetOwningPlayer(GetEventDamageSource()))) * 0.25f));
            return true;
        }

        return false;
    }

    private static void AncestralSpirit()
    {
        if (OrcConditionFilter.Contains(GetTriggerPlayer())
            && (GetTriggerUnit().UnitType == FourCC("otau")
                || GetTriggerUnit().UnitType == FourCC("ospw")))
        {
            TriggerSleepAction(2.5f);
            group TempGroup = group.Create();
            EnumRadius(TempGroup, GetTriggerUnit().X, GetTriggerUnit().Y, 800.0f, IsOrcSpiritwalker);
            GroupPickRandomUnit(TempGroup).IssueOrder(ORDER_ANCESTRAL_SPIRIT);
            TempGroup.Dispose();
        }
    }

    private static void PolymorphConcoction()
    {
        if (HumConditionFilter.Contains(GetTriggerPlayer()) && GetRandomInt(1, 10) == 1)
        {
            if (GetAttacker().UnitType == FourCC("ogru")
                || GetAttacker().UnitType == FourCC("otau")
                || GetAttacker().UnitType == FourCC("okod"))
            {
                group TempGroup = group.Create();
                EnumRadius(TempGroup, GetUnitX(GetAttacker()), GetUnitY(GetAttacker()), 700.0f, IsHumSorc);
                GroupPickRandomUnit(TempGroup).IssueOrder(ORDER_POLYMORPH, GetAttacker());
                TempGroup.Dispose();
            }

            return;
        }

        if (OrcConditionFilter.Contains(GetTriggerPlayer())
            && GetTriggerUnit().UnitType == UNIT_OTBR_BUTTRIDER
            && GetAttacker().IsUnitType(unittype.Flying))
        {
            GetAttackedUnitBJ().IssueOrder(ORDER_UNSTABLE_CONCOCTION, GetAttacker());
            return;
        }

        if (GetSummonedUnit().Owner.Controller == mapcontrol.Computer)
        {
            if (GetSummonedUnit().Owner.IsAlly(player.Create(5)))
            {
                GetSummonedUnit().IssueOrder(ORDER_ATTACK, OrcCastle.X, OrcCastle.Y);
            }

            if (GetSummonedUnit().Owner.IsAlly(player.Create(11)))
            {
                GetSummonedUnit().IssueOrder(ORDER_ATTACK, HumCastle.X, HumCastle.Y);
            }
        }
    }

    private static void OrcVictory()
    {
        Orc.ForEach(() => { CustomVictoryBJ(GetEnumPlayer(), true, true); });

        Human.ForEach(() => { CustomDefeatBJ(GetEnumPlayer(), "Hey buddy, the leather club is 3 blocks down."); });
    }

    private static void HumVictory()
    {
        Human.ForEach(() => { CustomVictoryBJ(GetEnumPlayer(), true, true); });

        Orc.ForEach(() => { CustomDefeatBJ(GetEnumPlayer(), "Hey buddy, the leather club is 3 blocks down."); });
    }

    private static void RespawnTrees()
    {
        EnumDestructablesInRectAll(GetPlayableMapRect(), () => { DestructableRestoreLife(GetEnumDestructable(), GetDestructableMaxLife(GetEnumDestructable()), true); });
    }

    private static void PlayerLeft()
    {
        force PlayerGroup = force.Create();
        ForForce(GetPlayersAll(), () =>
        {
            player CurrentPlayer = GetEnumPlayer();
            if (CurrentPlayer.SlotState == playerslotstate.Playing
                && CurrentPlayer.IsAlly(GetTriggerPlayer())
                && CurrentPlayer.Controller == mapcontrol.User)
            {
                PlayerGroup.Add(CurrentPlayer);
            }
        });

        int CurrentPlayers = CountPlayersInForceBJ(PlayerGroup);
        int GoldSplit = GetTriggerPlayer().Gold / CurrentPlayers;
        int LumberSplit = GetTriggerPlayer().Lumber / CurrentPlayers;
        GetTriggerPlayer().Gold = 0;
        GetTriggerPlayer().Lumber = 0;
        if (CurrentPlayers > 0)
        {
            QuestMessageBJ(PlayerGroup, bj_QUESTMESSAGE_HINT, "You got the inheritance of " + GetPlayerName(GetTriggerPlayer()));
            ForForce(PlayerGroup, () =>
            {
                GetEnumPlayer().Gold += GoldSplit;
                GetEnumPlayer().Lumber += LumberSplit;
            });
        }

        Players[GetTriggerPlayer()].SetName("Left", new PlayerColor(40.0f, 40.0f, 40.0f));
        DisplayTextToForce(GetPlayersAll(), GetTriggerPlayer().Name + " has left the game");
        ForGroupBJ(GetUnitsOfPlayerAll(GetTriggerPlayer()), () => { GetEnumUnit().Dispose(); });
    }

    private static void BountyNegation()
    {
        if (ConditionFilter.Contains(GetTriggerPlayer())
            && GetTriggerPlayer().Controller == mapcontrol.User
            && GetSummonedUnit().Owner.Color == playercolor.Orange)
        {
            NegateBounty(GetSummonedUnit());
            return;
        }

        if (OrcConditionFilter.Contains(GetTriggerPlayer())
            && GetTriggerUnit().UnitClassification == UnitClassifications.Summoned)
        {
            group TempGroup = group.Create();
            EnumRadius(TempGroup, GetAttacker().X, GetAttacker().Y, 800.0f, IsHumSpellbreaker);
            TempGroup.IssueOrder(ORDER_CONTROL_MAGIC, GetSummonedUnit());
            TempGroup.Dispose();
        }
    }

    private static void AbilityStops()
    {
        if (ConditionFilter.Contains(GetTriggerPlayer()))
        {
            if (GetTriggerUnit().Owner == player.Create(PLAYER_NEUTRAL_AGGRESSIVE))
            {
                GetTriggerUnit().UserData = 0;
                return;
            }
        }

        if (GetTriggerUnit().UnitType != UNIT_UFBD_ARCHITECT)
        {
            if (HumConditionFilter.Contains(GetTriggerPlayer()))
            {
                GetSpellAbilityUnit().IssueOrder(ORDER_ATTACK, OrcCastle.X, OrcCastle.Y);
            }
            else if (OrcConditionFilter.Contains(GetTriggerPlayer()))
            {
                GetSpellAbilityUnit().IssueOrder(ORDER_ATTACK, HumCastle.X, HumCastle.Y);
            }
        }
    }

    private static bool FixTomes()
    {
        if (GetManipulatedItem().TypeId == ITEM_GOLD_75_GOLD)
        {
            Coins75.Remove(GetManipulatedItem());
        }
        if (GetManipulatedItem().TypeId == ITEM_I03E_150_BANANAS)
        {
            Coins150.Remove(GetManipulatedItem());
        }
        if (GetManipulatedItem().TypeId == ITEM_I03F_300_GOLDEN_CHEESE_COINS)
        {
            Coins300.Remove(GetManipulatedItem());
        }
        new TomeItem(GetManipulatedItem());
        return true;
    }
}

public class TomeItem
{
    public item Item;
    private timer Cleanup;

    public TomeItem(item InItem)
    {
        Item = InItem;
        Cleanup = timer.Create();
        Cleanup.Start(1.0f, false, Remove);
    }

    private void Remove()
    {
        if (GetWidgetLife(Item) <= 0.0f)
        {
            Item.Dispose();
            Cleanup.Dispose();
        }
    }
}
