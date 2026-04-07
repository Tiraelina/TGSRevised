using WCSharp.Api;
using static Constants;
using static TGS.Creeps.CreepsCore;
using static TGS.Util;
using static TGS.Globals;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS.Creeps;

public static class CampsMid
{
    public static void Init()
    {
        CampGroup TempGroup;

        CreepCamp SiegeWest = new(Camp.SiegeWest, CreepRespawnTime4m);
        SiegeWest.AddUnit(GetUnitAt(-3530.4f, 1826.5f));
        SiegeWest.AddUnit(GetUnitAt(-3570.0f, 1665.2f), DropID.Gold100Candy);
        SiegeWest.AddUnit(GetUnitAt(-3713.0f, 1739.5f), DropID.Keg);

        CreepCamp GoblinWest = new(Camp.GoblinWest, CreepRespawnTime3m);
        GoblinWest.AddUnit(GetUnitAt(-1615.0f, -2122.5f));
        GoblinWest.AddUnit(GetUnitAt(-1488.0f, -2192.0f));
        GoblinWest.AddUnit(GetUnitAt(-1588.5f, -2382.0f), DropID.Swiftness);
        GoblinWest.AddUnit(GetUnitAt(-1495.0f, -2263.5f), DropID.Gold25Candy);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRH_STORMREAVER_SHELLMAGE));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRH_STORMREAVER_SHELLMAGE));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRH_STORMREAVER_SHELLMAGE, DropID.Swiftness));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRN_STORMREAVER_WORM_CASTER, DropID.Gold50Candy));
        GoblinWest.AddUnitSet(TempGroup);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRN_STORMREAVER_WORM_CASTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRN_STORMREAVER_WORM_CASTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRN_STORMREAVER_WORM_CASTER, DropID.Swiftness));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRW_STORMREAVER_WORM_WRANGLER, DropID.Gold100Candy));
        GoblinWest.AddUnitSet(TempGroup);

        CreepCamp PigeonWest = new(Camp.PigeonWest, CreepRespawnTime5m);
        PigeonWest.AddUnit(GetUnitAt(-10288.2f, -3513.5f), DropID.Candy5WoodBundles3);

        CreepCamp GnollsSouthwest = new(Camp.GnollsSouthwest, CreepRespawnTime5m);
        GnollsSouthwest.AddUnit(GetUnitAt(-5783.5f, -12693.2f));
        GnollsSouthwest.AddUnit(GetUnitAt(-5871.8f, -12705.0f));
        GnollsSouthwest.AddUnit(GetUnitAt(-5798.8f, -12784.0f));
        GnollsSouthwest.AddUnit(GetUnitAt(-5538.5f, -12994.5f), DropID.Healthstone);
        GnollsSouthwest.AddUnit(GetUnitAt(-5626.8f, -13006.2f));
        GnollsSouthwest.AddUnit(GetUnitAt(-5553.5f, -13085.0f));
        GnollsSouthwest.AddUnit(GetUnitAt(-6045.5f, -13066.5f), DropID.TomeOfPower);
        GnollsSouthwest.AddUnit(GetUnitAt(-6215.0f, -13101.2f));
        GnollsSouthwest.AddUnit(GetUnitAt(-6217.0f, -12896.0f), DropID.Gear2);

        CreepCamp SiegeEast = new(Camp.SiegeEast, CreepRespawnTime4m);
        SiegeEast.AddUnit(GetUnitAt(3697.0f, 1825.5f));
        SiegeEast.AddUnit(GetUnitAt(3685.5f, 1648.0f), DropID.Gold100Candy);
        SiegeEast.AddUnit(GetUnitAt(3833.0f, 1738.8f), DropID.Keg);

        CreepCamp GoblinEast = new(Camp.GoblinEast, CreepRespawnTime3m);
        GoblinEast.AddUnit(GetUnitAt(1531.0f, 2702.0f));
        GoblinEast.AddUnit(GetUnitAt(1408.0f, 2515.5f));
        GoblinEast.AddUnit(GetUnitAt(1500.0f, 2411.8f), DropID.Swiftness);
        GoblinEast.AddUnit(GetUnitAt(1418.2f, 2630.5f), DropID.Gold25Candy);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRH_STORMREAVER_SHELLMAGE));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRH_STORMREAVER_SHELLMAGE));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRH_STORMREAVER_SHELLMAGE, DropID.Swiftness));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRN_STORMREAVER_WORM_CASTER, DropID.Gold50Candy));
        GoblinEast.AddUnitSet(TempGroup);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRN_STORMREAVER_WORM_CASTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRN_STORMREAVER_WORM_CASTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRN_STORMREAVER_WORM_CASTER, DropID.Swiftness));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRW_STORMREAVER_WORM_WRANGLER, DropID.Gold100Candy));
        GoblinEast.AddUnitSet(TempGroup);

        CreepCamp PigeonEast = new(Camp.PigeonEast, CreepRespawnTime5m);
        PigeonEast.AddUnit(GetUnitAt(10213.5f, -3308.0f), DropID.Candy5WoodBundles3);

        CreepCamp GnollsSoutheast = new(Camp.GnollsSoutheast, CreepRespawnTime5m);
        GnollsSoutheast.AddUnit(GetUnitAt(5865.0f, -12979.5f));
        GnollsSoutheast.AddUnit(GetUnitAt(5857.8f, -13059.2f));
        GnollsSoutheast.AddUnit(GetUnitAt(5938.5f, -13080.0f));
        GnollsSoutheast.AddUnit(GetUnitAt(5594.5f, -12961.5f), DropID.Healthstone);
        GnollsSoutheast.AddUnit(GetUnitAt(5505.2f, -12973.5f));
        GnollsSoutheast.AddUnit(GetUnitAt(5579.2f, -13052.5f));
        GnollsSoutheast.AddUnit(GetUnitAt(5225.2f, -12843.0f), DropID.TomeOfPower);
        GnollsSoutheast.AddUnit(GetUnitAt(5073.5f, -12912.5f));
        GnollsSoutheast.AddUnit(GetUnitAt(5084.2f, -12688.5f), DropID.Gear2);

        CreepCamp Urtle = new(Camp.Urtle, CreepRespawnTime5m);
        Urtle.AddUnit(GetUnitAt(1.8f, 10045.5f));

        CreepCamp Crabnar = new(Camp.Crabnar, CreepRespawnTime5m);
        Crabnar.AddUnit(GetUnitAt(11.5f, -8568.0f));

        CreepCamp FurbolgFarSouth = new(Camp.FurbolgFarSouth, CreepRespawnTime5m);
        FurbolgFarSouth.AddUnit(GetUnitAt(419.5f, -12647.5f), DropID.HealRune);
        FurbolgFarSouth.AddUnit(GetUnitAt(223.5f, -12798.2f));
        FurbolgFarSouth.AddUnit(GetUnitAt(578.0f, -12847.0f), DropID.Gear3);
        FurbolgFarSouth.AddUnit(GetUnitAt(395.0f, -13021.0f));

        CreepCamp HeroMid = new(Camp.HeroMid, CreepRespawnTime5m);
        HeroMid.AddUnit(GetUnitAt(-834.2f, 12880.2f));
        HeroMid.AddUnit(GetUnitAt(-672.8f, 12742.0f));
        HeroMid.AddUnit(GetUnitAt(572.5f, 13072.2f));
        HeroMid.AddUnit(GetUnitAt(862.2f, 12907.5f));

        CreepCamp MoonkinEast = new(Camp.MoonkinEast, CreepRespawnTime4m);
        MoonkinEast.AddUnit(GetUnitAt(3186.0f, -2030.5f));
        MoonkinEast.AddUnit(GetUnitAt(3258.5f, -1851.0f));
        MoonkinEast.AddUnit(GetUnitAt(3383.0f, -2031.0f));

        CreepCamp MoonkinWest = new(Camp.MoonkinWest, CreepRespawnTime4m);
        MoonkinWest.AddUnit(GetUnitAt(-3263.2f, -1732.2f));
        MoonkinWest.AddUnit(GetUnitAt(-3398.8f, -1615.8f));
        MoonkinWest.AddUnit(GetUnitAt(-3439.5f, -1833.5f));

        CreepCamp GateNorth = new(Camp.GateNorth, CreepRespawnTime4m,
            _ => { SetNorthGate(true); },
            _ => { SetNorthGate(false); });
        GateNorth.AddUnit(GetUnitAt(-153.0f, 4059.8f), DropID.Swiftness);
        GateNorth.AddUnit(GetUnitAt(133.5f, 4066.8f));
        GateNorth.AddUnit(GetUnitAt(1.8f, 3878.5f), DropID.Gold25Candy);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRV_REVENANT_OF_THE_SEAS, DropID.Swiftness));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRV_REVENANT_OF_THE_SEAS));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.Gold50Candy));
        GateNorth.AddUnitSet(TempGroup);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.Swiftness));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLRV_ELDRITCH_MONSTROSITY, DropID.Gold100Candy));
        GateNorth.AddUnitSet(TempGroup);

        CreepCamp GateSouth = new(Camp.GateSouth, CreepRespawnTime4m,
            _ => { SetSouthGate(true); },
            _ => { SetSouthGate(false); });
        GateSouth.AddUnit(GetUnitAt(-175.0f, -4408.2f), DropID.Swiftness);
        GateSouth.AddUnit(GetUnitAt(181.8f, -4411.5f));
        GateSouth.AddUnit(GetUnitAt(2.5f, -4246.5f), DropID.Gold25Candy);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRV_REVENANT_OF_THE_SEAS, DropID.Swiftness));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSRV_REVENANT_OF_THE_SEAS));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.Gold50Candy));
        GateSouth.AddUnitSet(TempGroup);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.Swiftness));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLRV_ELDRITCH_MONSTROSITY, DropID.Gold100Candy));
        GateSouth.AddUnitSet(TempGroup);
    }

    public static void SetNorthGate(bool Open)
    {
        if (Open)
        {
            foreach (destructable Blocker in NorthBlockers)
            {
                KillDestructable(Blocker);
            }

            foreach (destructable Gate in NorthGates)
            {
                ModifyGateBJ(bj_GATEOPERATION_OPEN, Gate);
            }
        }
        else
        {
            foreach (destructable Blocker in NorthBlockers)
            {
                DestructableRestoreLife(Blocker, GetDestructableMaxLife(Blocker), false);
            }

            foreach (destructable Gate in NorthGates)
            {
                ModifyGateBJ(bj_GATEOPERATION_CLOSE, Gate);
            }
        }
    }

    public static void SetSouthGate(bool Open)
    {
        if (Open)
        {
            foreach (destructable Blocker in SouthBlockers)
            {
                KillDestructable(Blocker);
            }

            foreach (destructable Gate in SouthGates)
            {
                ModifyGateBJ(bj_GATEOPERATION_OPEN, Gate);
            }
        }
        else
        {
            foreach (destructable Blocker in SouthBlockers)
            {
                DestructableRestoreLife(Blocker, GetDestructableMaxLife(Blocker), false);
            }

            foreach (destructable Gate in SouthGates)
            {
                ModifyGateBJ(bj_GATEOPERATION_CLOSE, Gate);
            }
        }
    }
}