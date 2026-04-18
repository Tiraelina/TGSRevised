using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Api.Enums;
using WCSharp.Events;
using static TGS.Util;
using static TGS.Globals;
using static Constants;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

internal enum Lane
{
    Top = 0,
    Middle = 1,
    Bottom = 2
}

public static class Army
{
    private static int TechStage;
    public static int TechGroupOne;
    public static int TechGroupTwo;
    public static int HumGoldSplitTotal;
    public static int HumGoldSplitEach;
    public static int OrcGoldSplitTotal;
    public static int OrcGoldSplitEach;
    public static int GoldSplitMod = 10;

    public static List<Factory> HumFactories1st = new();
    public static List<Factory> HumFactories2nd = new();
    public static List<Factory> HumFactories3rd = new();
    public static List<Factory> HumFactories4th = new();
    public static List<Factory> OrcFactories1st = new();
    public static List<Factory> OrcFactories2nd = new();
    public static List<Factory> OrcFactories3rd = new();
    public static List<Factory> OrcFactories4th = new();

    public static List<Factory> HumFactoriesTop = new();
    public static List<Factory> HumFactoriesMid = new();
    public static List<Factory> HumFactoriesBot = new();
    public static List<Factory> OrcFactoriesTop = new();
    public static List<Factory> OrcFactoriesMid = new();
    public static List<Factory> OrcFactoriesBot = new();

    public static Dictionary<Factory, Factory> OpposingFactory = new();
    public static Dictionary<unit, Factory> FactoryLookup = new();

    public static void InitHumArmy()
    {
        Factory NewFactory;
        NewFactory = new Factory(player.Create(12), HumBarracksTop);
        List<FactorySpawn> Spawns = new List<FactorySpawn>();
        Spawns.Add(new FactorySpawn(UNIT_HFOO_FOOTMAN, UNIT_HCTH_CRUSADER, false, 1));
        Spawns.Add(new FactorySpawn(UNIT_HRIF_RIFLEMAN, UNIT_HHES_SENTINEL, false, 1));
        Spawns.Add(new FactorySpawn(UNIT_HKNI_HUNTRESS, UNIT_HHDL_PALADIN, true, 0));
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories1st.Add(NewFactory);
        HumFactoriesTop.Add(NewFactory);

        NewFactory = new Factory(player.Create(13), HumBarracksMid);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories1st.Add(NewFactory);
        HumFactoriesMid.Add(NewFactory);

        NewFactory = new Factory(player.Create(14), HumBarracksBot);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories1st.Add(NewFactory);
        HumFactoriesBot.Add(NewFactory);

        NewFactory = new Factory(player.Create(12), HumAviaryTop);
        Spawns.Clear();
        Spawns.Add(new FactorySpawn(UNIT_HDHW_DRAGONHAWK_RIDER, UNIT_NWS1_ELDER_DRAGONHAWK_RIDER, false, 1));
        Spawns.Add(new FactorySpawn(UNIT_HGRY_BRONZE_WHELP_GRYPHON, UNIT_NSER_BLACK_DRAKE, true, 0));
        Spawns.Add(new FactorySpawn(UNIT_HGRY_BRONZE_WHELP_GRYPHON, UNIT_NBEE_FLYING_FORTRESS, false, 0));
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories2nd.Add(NewFactory);
        HumFactoriesTop.Add(NewFactory);

        NewFactory = new Factory(player.Create(13), HumAviaryMid);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories2nd.Add(NewFactory);
        HumFactoriesMid.Add(NewFactory);

        NewFactory = new Factory(player.Create(14), HumAviaryBot);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories2nd.Add(NewFactory);
        HumFactoriesBot.Add(NewFactory);

        NewFactory = new Factory(player.Create(12), HumSanctumTop);
        Spawns.Clear();
        Spawns.Add(new FactorySpawn(UNIT_HMPR_DRUID, UNIT_NEMI_BISHOP, false, 1));
        Spawns.Add(new FactorySpawn(UNIT_HSOR_SORCERESS, UNIT_NHYM_ARCHMAGE, true, 0));
        Spawns.Add(new FactorySpawn(UNIT_HSPT_SUMMONER_UPDATED_FROM_SPELLBREAKER_6_14_2025, UNIT_NBEL_MAGE_SLAYER, true, 0));
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories3rd.Add(NewFactory);
        HumFactoriesTop.Add(NewFactory);

        NewFactory = new Factory(player.Create(13), HumSanctumMid);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories3rd.Add(NewFactory);
        HumFactoriesMid.Add(NewFactory);

        NewFactory = new Factory(player.Create(14), HumSanctumBot);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories3rd.Add(NewFactory);
        HumFactoriesBot.Add(NewFactory);

        NewFactory = new Factory(player.Create(12), HumWorkshopTop);
        Spawns.Clear();
        Spawns.Add(new FactorySpawn(UNIT_HMTM_MORTAR_TEAM, UNIT_HBEW_BALLISTA, true, 1));
        Spawns.Add(new FactorySpawn(UNIT_HMTT_SIEGE_ENGINE, UNIT_HRDH_FORTIFIED_SIEGE_ENGINE, false, 0));
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories4th.Add(NewFactory);
        HumFactoriesTop.Add(NewFactory);

        NewFactory = new Factory(player.Create(13), HumWorkshopMid);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories4th.Add(NewFactory);
        HumFactoriesMid.Add(NewFactory);

        NewFactory = new Factory(player.Create(14), HumWorkshopBot);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        HumFactories4th.Add(NewFactory);
        HumFactoriesBot.Add(NewFactory);
    }

    public static void InitOrcArmy()
    {
        Factory NewFactory;
        NewFactory = new Factory(player.Create(15), OrcBarracksTop);
        List<FactorySpawn> Spawns = new List<FactorySpawn>();
        Spawns.Add(new FactorySpawn(UNIT_OGRU_GRUNT, UNIT_NCHG_CHAOS_GRUNT, false, 1));
        Spawns.Add(new FactorySpawn(UNIT_OHUN_CRYPT_FIEND, UNIT_NFTB_TROLL_AXE_BERSERKER, false, 1));
        Spawns.Add(new FactorySpawn(UNIT_ORAI_ABOMINATION, UNIT_NCHR_CHAOS_RAIDER, true, 0));
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories1st.Add(NewFactory);
        OrcFactoriesTop.Add(NewFactory);

        NewFactory = new Factory(player.Create(16), OrcBarracksMid);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories1st.Add(NewFactory);
        OrcFactoriesMid.Add(NewFactory);

        NewFactory = new Factory(player.Create(17), OrcBarracksBot);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories1st.Add(NewFactory);
        OrcFactoriesBot.Add(NewFactory);

        NewFactory = new Factory(player.Create(15), OrcBeastiaryTop);
        Spawns.Clear();
        Spawns.Add(new FactorySpawn(UNIT_OWYV_WINDRACER, UNIT_NRDR_RED_DRAKE, true, 1));
        Spawns.Add(new FactorySpawn(UNIT_OTBR_BUTTRIDER, UNIT_NZEP_ZEPPELIN_ORC, false, 0));
        Spawns.Add(new FactorySpawn(UNIT_OKOD_KODO_BEAST, UNIT_NCKB_SUCCUBUS, false, 0));
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories2nd.Add(NewFactory);
        OrcFactoriesTop.Add(NewFactory);

        NewFactory = new Factory(player.Create(16), OrcBeastiaryMid);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories2nd.Add(NewFactory);
        OrcFactoriesMid.Add(NewFactory);

        NewFactory = new Factory(player.Create(17), OrcBeastiaryBot);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories2nd.Add(NewFactory);
        OrcFactoriesBot.Add(NewFactory);

        NewFactory = new Factory(player.Create(15), OrcLodgeTop);
        Spawns.Clear();
        Spawns.Add(new FactorySpawn(UNIT_OSHM_SCHWAMAN, UNIT_NCHW_WITCH_ARROW, true, 1));
        Spawns.Add(new FactorySpawn(UNIT_ODOC_MEDICINE_MAN, UNIT_ODKT_NECROMANCER, true, 0));
        Spawns.Add(new FactorySpawn(UNIT_OSPW_SPIRIT_WALKER, UNIT_NOMG_OGRE_MAGI, false, 0));
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories3rd.Add(NewFactory);
        OrcFactoriesTop.Add(NewFactory);

        NewFactory = new Factory(player.Create(16), OrcLodgeMid);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories3rd.Add(NewFactory);
        OrcFactoriesMid.Add(NewFactory);

        NewFactory = new Factory(player.Create(17), OrcLodgeBot);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories3rd.Add(NewFactory);
        OrcFactoriesBot.Add(NewFactory);

        NewFactory = new Factory(player.Create(15), OrcTotemTop);
        Spawns.Clear();
        Spawns.Add(new FactorySpawn(UNIT_OCAT_DEMOLISHER, UNIT_NINC_INFERNAL_CANNON, false, 1));
        Spawns.Add(new FactorySpawn(UNIT_OTAU_BLADEMASTER, UNIT_NOGL_OGRE_LORD, true, 0));
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories4th.Add(NewFactory);
        OrcFactoriesTop.Add(NewFactory);

        NewFactory = new Factory(player.Create(16), OrcTotemMid);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories4th.Add(NewFactory);
        OrcFactoriesMid.Add(NewFactory);

        NewFactory = new Factory(player.Create(17), OrcTotemBot);
        foreach (FactorySpawn SpawnedUnit in Spawns)
        {
            NewFactory.AddSpawn(SpawnedUnit);
        }

        OrcFactories4th.Add(NewFactory);
        OrcFactoriesBot.Add(NewFactory);
    }

    public static void Init()
    {
        InitHumArmy();
        InitOrcArmy();
        int Index = 0;
        // Human
        foreach (Factory Spawner in HumFactoriesTop)
        {
            OpposingFactory.Add(Spawner, OrcFactoriesTop[Index]);
            OpposingFactory.Add(OrcFactoriesTop[Index], Spawner);
            FactoryLookup.Add(Spawner.Unit, Spawner);
            FactoryLookup.Add(OrcFactoriesTop[Index].Unit, OrcFactoriesTop[Index]);
            Index += 1;
        }

        Index = 0;
        foreach (Factory Spawner in HumFactoriesMid)
        {
            OpposingFactory.Add(Spawner, OrcFactoriesMid[Index]);
            OpposingFactory.Add(OrcFactoriesMid[Index], Spawner);
            FactoryLookup.Add(Spawner.Unit, Spawner);
            FactoryLookup.Add(OrcFactoriesMid[Index].Unit, OrcFactoriesMid[Index]);
            Index += 1;
        }

        Index = 0;
        foreach (Factory Spawner in HumFactoriesBot)
        {
            OpposingFactory.Add(Spawner, OrcFactoriesBot[Index]);
            OpposingFactory.Add(OrcFactoriesBot[Index], Spawner);
            FactoryLookup.Add(Spawner.Unit, Spawner);
            FactoryLookup.Add(OrcFactoriesBot[Index].Unit, OrcFactoriesBot[Index]);
            Index += 1;
        }

        trigger TechStages = trigger.Create();
        TriggerRegisterTimerEvent(TechStages, 450.0f, true);
        TriggerAddAction(TechStages, TechStagesAction);

        trigger GoldSharing = trigger.Create();
        TriggerRegisterTimerEvent(GoldSharing, 0.5f, true);
        TriggerAddAction(GoldSharing, GoldShare);
        
        trigger GoldTickRate = trigger.Create();
        TriggerRegisterTimerEvent(GoldTickRate, 60.0f, true);
        TriggerAddAction(GoldTickRate, () =>
        {
            if (player.Create(5).Gold > 10000 && player.Create(5).Gold < 100000)
            {
                UpdateHumGoldSplit(50);
            }
            else if (player.Create(5).Gold > 100000)
            {
                UpdateHumGoldSplit(100);
            }
            else
            {
                UpdateHumGoldSplit(10);
            }
            if (player.Create(11).Gold > 10000 && player.Create(11).Gold < 100000)
            {
                UpdateOrcGoldSplit(50);
            }
            else if (player.Create(11).Gold > 100000)
            {
                UpdateOrcGoldSplit(100);
            }
            else
            {
                UpdateOrcGoldSplit(10);
            }
        });

        PlayerUnitEvents.Register(UnitTypeEvent.Dies, DeathEvents);

        PlayerUnitEvents.Register(UnitEvent.SellsItem, HumSalvage, HumTagawa);
        PlayerUnitEvents.Register(UnitEvent.SellsItem, OrcSalvage, OrcTagawa);

        PlayerUnitEvents.Register(UnitTypeEvent.FinishesConstruction, () =>
        {
            player TriggeringPlayer = GetTriggerPlayer();
            if (TriggeringPlayer == player.Create(5) || TriggeringPlayer == player.Create(11))
            {
                unit NewFactory = GetTriggerUnit();
                if (NewFactory.IsUnitType(unittype.Structure)
                    && NewFactory.IsUnitType(unittype.Ancient)
                    && NewFactory.IsUnitType(unittype.Sapper))
                {
                    FactoryLookup[GetTriggerUnit()].State = FactoryState.Alive;
                }
            }
        });
        PlayerUnitEvents.Register(UnitTypeEvent.StartsConstruction, () =>
        {
            player TriggeringPlayer = GetTriggerPlayer();
            if (TriggeringPlayer == player.Create(5) || TriggeringPlayer == player.Create(11))
            {
                unit NewFactory = GetTriggerUnit();
                if (NewFactory.IsUnitType(unittype.Structure)
                    && NewFactory.IsUnitType(unittype.Ancient)
                    && NewFactory.IsUnitType(unittype.Sapper))
                {
                    if (TriggeringPlayer == player.Create(5))
                    {
                        foreach (Factory Spawner in HumFactoriesTop)
                        {
                            if (Spawner.Location.X == NewFactory.X && Spawner.Location.Y == NewFactory.Y)
                            {
                                Spawner.SetNewFactory(NewFactory);
                                return;
                            }
                        }
                        foreach (Factory Spawner in HumFactoriesMid)
                        {
                            if (Spawner.Location.X == NewFactory.X && Spawner.Location.Y == NewFactory.Y)
                            {
                                Spawner.SetNewFactory(NewFactory);
                                return;
                            }
                        }
                        foreach (Factory Spawner in HumFactoriesBot)
                        {
                            if (Spawner.Location.X == NewFactory.X && Spawner.Location.Y == NewFactory.Y)
                            {
                                Spawner.SetNewFactory(NewFactory);
                                return;
                            }
                        }
                    }
                    if (TriggeringPlayer == player.Create(11))
                    {
                        foreach (Factory Spawner in OrcFactoriesTop)
                        {
                            if (Spawner.Location.X == NewFactory.X && Spawner.Location.Y == NewFactory.Y)
                            {
                                Spawner.SetNewFactory(NewFactory);
                                return;
                            }
                        }
                        foreach (Factory Spawner in OrcFactoriesMid)
                        {
                            if (Spawner.Location.X == NewFactory.X && Spawner.Location.Y == NewFactory.Y)
                            {
                                Spawner.SetNewFactory(NewFactory);
                                return;
                            }
                        }
                        foreach (Factory Spawner in OrcFactoriesBot)
                        {
                            if (Spawner.Location.X == NewFactory.X && Spawner.Location.Y == NewFactory.Y)
                            {
                                Spawner.SetNewFactory(NewFactory);
                                return;
                            }
                        }
                    }
                }
            }
        });
        PlayerUnitEvents.Register(UnitTypeEvent.Summons, ArmySummonAttack);

        trigger ArmyRing1 = trigger.Create();
        TriggerRegisterTimerEvent(ArmyRing1, 60.0f, true);
        TriggerRegisterTimerEvent(ArmyRing1, 5.0f, false);
        TriggerAddAction(ArmyRing1, SpawnRing1);

        trigger ArmyRing2 = trigger.Create();
        TriggerRegisterTimerEvent(ArmyRing2, 120.0f, true);
        TriggerAddAction(ArmyRing2, SpawnRing2);

        trigger ArmyRing3 = trigger.Create();
        TriggerRegisterTimerEvent(ArmyRing3, 120.0f, true);
        TriggerRegisterTimerEvent(ArmyRing3, 5.0f, false);
        TriggerAddAction(ArmyRing3, SpawnRing3);

        trigger ArmyRing4 = trigger.Create();
        TriggerRegisterTimerEvent(ArmyRing4, 180.0f, true);
        TriggerAddAction(ArmyRing4, SpawnRing4);
    }

    private static void UpdateHumGoldSplit(int SplitMod)
    {
        HumGoldSplitTotal = Math.Max(CountPlayersInForceBJ(Human) - 4, 1) * SplitMod;
        HumGoldSplitEach = HumGoldSplitTotal / Math.Max(CountPlayersInForceBJ(Human) - 4, 1);
    }

    private static void UpdateOrcGoldSplit(int SplitMod)
    {
        OrcGoldSplitTotal = Math.Max(CountPlayersInForceBJ(Orc) - 4, 1) * SplitMod;
        OrcGoldSplitEach = OrcGoldSplitTotal / Math.Max(CountPlayersInForceBJ(Orc) - 4, 1);
    }

    private static void SpawnRing1()
    {
        foreach (Factory Spawner in HumFactories1st)
        {
            Spawner.Spawn();
        }

        foreach (Factory Spawner in OrcFactories1st)
        {
            Spawner.Spawn();
        }
    }

    private static void SpawnRing2()
    {
        foreach (Factory Spawner in HumFactories2nd)
        {
            Spawner.Spawn();
        }

        foreach (Factory Spawner in OrcFactories2nd)
        {
            Spawner.Spawn();
        }
    }

    private static void SpawnRing3()
    {
        foreach (Factory Spawner in HumFactories3rd)
        {
            Spawner.Spawn();
        }

        foreach (Factory Spawner in OrcFactories3rd)
        {
            Spawner.Spawn();
        }
    }

    private static void SpawnRing4()
    {
        foreach (Factory Spawner in HumFactories4th)
        {
            Spawner.Spawn();
        }

        foreach (Factory Spawner in OrcFactories4th)
        {
            Spawner.Spawn();
        }
    }

    private static void ArmySummonAttack()
    {
        if (HumConditionFilter.Contains(GetTriggerPlayer()))
        {
            GetSummonedUnit().IssueOrder(ORDER_ATTACK, OrcCastle.X, OrcCastle.Y);
        }

        if (OrcConditionFilter.Contains(GetTriggerPlayer()))
        {
            GetSummonedUnit().IssueOrder(ORDER_ATTACK, HumCastle.X, HumCastle.Y);
        }
    }

    private static void HumSalvage()
    {
        if (GetSoldItem().TypeId == ITEM_TBAR_SALVAGE_STRUCTURE_NORTH)
        {
            foreach (Factory Spawner in HumFactoriesTop)
            {
                if (Spawner.State < FactoryState.UnderConstruction)
                {
                    BuildFactory(Spawner, Human);
                    return;
                }
            }

            GetBuyingUnit().Owner.Gold += 2000;
        }

        if (GetSoldItem().TypeId == ITEM_I0AB_SALVAGE_STRUCTURE_CENTRE)
        {
            foreach (Factory Spawner in HumFactoriesMid)
            {
                if (Spawner.State < FactoryState.UnderConstruction)
                {
                    BuildFactory(Spawner, Human);
                    return;
                }
            }

            GetBuyingUnit().Owner.Gold += 2000;
        }

        if (GetSoldItem().TypeId == ITEM_I0AA_SALVAGE_STRUCTURE_SOUTH)
        {
            foreach (Factory Spawner in HumFactoriesBot)
            {
                if (Spawner.State < FactoryState.UnderConstruction)
                {
                    BuildFactory(Spawner, Human);
                    return;
                }
            }

            GetBuyingUnit().Owner.Gold += 2000;
        }
    }

    private static void OrcSalvage()
    {
        if (GetSoldItem().TypeId == ITEM_TBAR_SALVAGE_STRUCTURE_NORTH)
        {
            foreach (Factory Spawner in OrcFactoriesTop)
            {
                if (Spawner.State < FactoryState.UnderConstruction)
                {
                    BuildFactory(Spawner, Orc);
                    return;
                }
            }

            GetBuyingUnit().Owner.Gold += 2000;
        }

        if (GetSoldItem().TypeId == ITEM_I0AB_SALVAGE_STRUCTURE_CENTRE)
        {
            foreach (Factory Spawner in OrcFactoriesMid)
            {
                if (Spawner.State < FactoryState.UnderConstruction)
                {
                    BuildFactory(Spawner, Orc);
                    return;
                }
            }

            GetBuyingUnit().Owner.Gold += 2000;
        }

        if (GetSoldItem().TypeId == ITEM_I0AA_SALVAGE_STRUCTURE_SOUTH)
        {
            foreach (Factory Spawner in OrcFactoriesBot)
            {
                if (Spawner.State < FactoryState.UnderConstruction)
                {
                    BuildFactory(Spawner, Orc);
                    return;
                }
            }

            GetBuyingUnit().Owner.Gold += 2000;
        }
    }

    private static void BuildFactory(Factory Spawner, force Force)
    {
        QuestMessageBJ(Force, bj_QUESTMESSAGE_HINT,
            $"{GetBuyingUnit().Owner.Name} bought |cffff8000{GetSoldItem().Name}|cffffffff to rebuild |cffff8000{Spawner.UnitName}");
        PingMinimapLocForForce(Force, Spawner.Location, 5.0f);
        unit Tony;
        if (Spawner.Owner.IsAlly(player.Create(5)))
        {
            Tony = unit.Create(player.Create(5), UNIT_UFBD_ARCHITECT, HumCastle.X, HumCastle.Y);
        }
        else
        {
            Tony = unit.Create(player.Create(11), UNIT_UFBD_ARCHITECT, OrcCastle.X, OrcCastle.Y);
        }
        
        Tony.IssueBuildOrder(Spawner.FactoryId, Spawner.Location.X, Spawner.Location.Y);
        Tony.ApplyTimedLife(FourCC("BTLF"), 120.0f);
        Spawner.State = FactoryState.Pending;
    }

    private static void GoldShare()
    {
        player.Create(5).Gold += player.Create(12).Gold;
        player.Create(12).Gold = 0;
        player.Create(5).Gold += player.Create(13).Gold;
        player.Create(13).Gold = 0;
        player.Create(5).Gold += player.Create(14).Gold;
        player.Create(14).Gold = 0;

        if (player.Create(5).Gold >= HumGoldSplitTotal)
        {
            player.Create(0).Gold += HumGoldSplitEach;
            player.Create(1).Gold += HumGoldSplitEach;
            player.Create(2).Gold += HumGoldSplitEach;
            player.Create(3).Gold += HumGoldSplitEach;
            player.Create(4).Gold += HumGoldSplitEach;
            player.Create(5).Gold -= HumGoldSplitTotal;
        }

        player.Create(11).Gold += player.Create(15).Gold;
        player.Create(15).Gold = 0;
        player.Create(11).Gold += player.Create(16).Gold;
        player.Create(16).Gold = 0;
        player.Create(11).Gold += player.Create(17).Gold;
        player.Create(17).Gold = 0;

        if (player.Create(11).Gold >= OrcGoldSplitTotal)
        {
            player.Create(6).Gold += OrcGoldSplitEach;
            player.Create(7).Gold += OrcGoldSplitEach;
            player.Create(8).Gold += OrcGoldSplitEach;
            player.Create(9).Gold += OrcGoldSplitEach;
            player.Create(10).Gold += OrcGoldSplitEach;
            player.Create(11).Gold -= HumGoldSplitTotal;
        }
    }

    private static void DeathEvents()
    {
        // Army kills
        if (!HumConditionFilter.Contains(GetTriggerPlayer()) && !OrcConditionFilter.Contains(GetTriggerPlayer()))
        {
            return;
        }

        if (GetTriggerUnit().UnitClassification == UnitClassifications.Ancient
            || GetTriggerUnit().UnitType == UNIT_OTOT_STASIS_TRAP
            || GetTriggerUnit().UnitType == UNIT_OHWD_HEALING_WARD)
        {
            return;
        }

        Drops.DropFromPool(GetTriggerUnit(), Drops.IP_Coin6);
        if (GetKillingUnit().Owner.Controller == mapcontrol.User)
        {
            if (Human.Contains(GetKillingUnit().Owner))
            {
                player.Create(5).Gold += 3;
            }

            if (Orc.Contains(GetKillingUnit().Owner))
            {
                player.Create(11).Gold += 3;
            }
        }
    }

    public static void AddCreepToArmy(unit TargetUnit, unit KillingUnit)
    {
#if DEBUG
        Console.WriteLine(TargetUnit.Name + " added to army.");
#endif
        if (IsMidCreep(TargetUnit))
        {
            if (IsHumUnit(KillingUnit))
            {
                HumFactories4th[(int)Lane.Middle].AddCreep(GetUnitTypeId(TargetUnit));
            }
            else
            {
                OrcFactories4th[(int)Lane.Middle].AddCreep(GetUnitTypeId(TargetUnit));
            }
        }
        else if (IsTopCreep(TargetUnit))
        {
            if (IsHumUnit(KillingUnit))
            {
                HumFactories4th[(int)Lane.Top].AddCreep(GetUnitTypeId(TargetUnit));
            }
            else
            {
                OrcFactories4th[(int)Lane.Top].AddCreep(GetUnitTypeId(TargetUnit));
            }
        }
        else
        {
            if (IsHumUnit(KillingUnit))
            {
                HumFactories4th[(int)Lane.Bottom].AddCreep(GetUnitTypeId(TargetUnit));
            }
            else
            {
                OrcFactories4th[(int)Lane.Bottom].AddCreep(GetUnitTypeId(TargetUnit));
            }
        }
    }

    private static void TechStagesAction()
    {
        TechStage += 1;
        switch (TechStage)
        {
            // 5m L1
            case 1:
                TechGroupOne += 1;
                break;
            // 10m L1
            case 2:
                TechGroupTwo += 1;
                break;
            // 15m L2
            case 3:
                TechGroupOne += 1;
                break;
            // 20m L2
            case 4:
                TechGroupTwo += 1;
                break;
            // 25m L3
            case 5:
                TechGroupOne += 1;
                break;
            // 30m L3
            case 6:
                TechGroupTwo += 1;
                break;
        }
    }
}
