using static Constants;
using static TGS.Creeps.TGSCreeps;
using static TGS.Util;

namespace TGS.Creeps;

public static class CampsTop
{
    public static void Init()
    {
        CampGroup TempGroup;
        CreepCamp FrenchmansWest = new(Camp.FrenchmansWest, CreepRespawnTime5m,
            _ => { SetShopState(Globals.FrenchmansEndWest, true); },
            _ => { SetShopState(Globals.FrenchmansEndWest, false); });
        FrenchmansWest.AddUnit(GetUnitAt(-4740.8f, 5742.8f));
        FrenchmansWest.AddUnit(GetUnitAt(-4688.5f, 5826.5f), DropID.HealLesser);
        FrenchmansWest.AddUnit(GetUnitAt(-4824.2f, 5881.5f), DropID.Gold25Candy);

        FrenchmansWest.AddUnit(GetUnitAt(-7284.0f, 5216.0f), DropID.Gold25Candy);
        FrenchmansWest.AddUnit(GetUnitAt(-6999.5f, 5051.2f), DropID.Gold100Candy);
        FrenchmansWest.AddUnit(GetUnitAt(-7342.0f, 4905.8f), DropID.HealRune);

        FrenchmansWest.AddUnit(GetUnitAt(-6466.5f, 6500.5f));
        FrenchmansWest.AddUnit(GetUnitAt(-6344.0f, 6501.8f));
        FrenchmansWest.AddUnit(GetUnitAt(-6247.5f, 6437.5f));
        FrenchmansWest.AddUnit(GetUnitAt(-6376.0f, 6330.8f), DropID.Gold25Candy);

        FrenchmansWest.AddUnit(GetUnitAt(-5488.8f, 7698.8f), DropID.Gold25Candy);
        FrenchmansWest.AddUnit(GetUnitAt(-5311.2f, 7578.8f), DropID.Gold25Candy);

        FrenchmansWest.AddUnit(GetUnitAt(-9271.5f, 4530.2f), DropID.HealRune);
        FrenchmansWest.AddUnit(GetUnitAt(-8949.5f, 3861.0f));
        FrenchmansWest.AddUnit(GetUnitAt(-9095.2f, 4446.2f), DropID.Gold100Candy);
        FrenchmansWest.AddUnit(GetUnitAt(-8839.0f, 3994.5f), DropID.Gold50Candy);
        FrenchmansWest.AddUnit(GetUnitAt(-8942.0f, 4204.0f), DropID.Aura);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSC2_DRAGONSPAWN));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSC2_DRAGONSPAWN, DropID.HealLesser));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSC3_SPIDERCRAB_MANCATCHER, DropID.Gold50Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.HealRune));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMRV_MUR_GUL_OVERSEER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMSN_MUR_GUL_WHIPLASHER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMSN_MUR_GUL_WHIPLASHER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMSC_MUR_GUL_SHADOWCASTER, DropID.Gold100Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMFS_COLD_ONE, DropID.Gold50Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMFS_COLD_ONE, DropID.Gold50Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NHYD_HYDRA, DropID.HealRune));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NHYD_HYDRA));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NAHY_ANCIENT_HYDRA_LEAD_HYDRA, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NAHY_ANCIENT_HYDRA_LEAD_HYDRA, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLRV_ELDRITCH_MONSTROSITY, DropID.Aura));
        FrenchmansWest.AddUnitSet(TempGroup);

        CreepCamp GolemWest = new(Camp.GolemWest, CreepRespawnTime4m);
        GolemWest.AddUnit(GetUnitAt(-1457.2f, 3749.5f));
        GolemWest.AddUnit(GetUnitAt(-1299.2f, 3799.0f));
        GolemWest.AddUnit(GetUnitAt(-1307.5f, 3638.8f), DropID.Gold100Candy);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSOC_CRYSTAL_GOLEM));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSOC_CRYSTAL_GOLEM));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLRV_ELDRITCH_MONSTROSITY, DropID.Gold100Candy));
        GolemWest.AddUnitSet(TempGroup);

        CreepCamp OcculordWest = new(Camp.OcculordWest, CreepRespawnTime5m);
        OcculordWest.AddUnit(GetUnitAt(-8629.0f, 9672.0f));
        OcculordWest.AddUnit(GetUnitAt(-8516.5f, 9847.8f));
        OcculordWest.AddUnit(GetUnitAt(-8316.0f, 9645.5f), DropID.Swiftness);

        OcculordWest.AddUnit(GetUnitAt(-6385.5f, 9669.0f));
        OcculordWest.AddUnit(GetUnitAt(-6131.5f, 9671.5f));
        OcculordWest.AddUnit(GetUnitAt(-6249.8f, 9452.5f), DropID.QuadDamage);
        OcculordWest.AddUnit(GetUnitAt(-6274.5f, 9896.2f), DropID.WoodBundle);

        OcculordWest.AddUnit(GetUnitAt(-4274.2f, 10218.5f));
        OcculordWest.AddUnit(GetUnitAt(-4172.8f, 10085.0f), DropID.TomeOfPower);
        OcculordWest.AddUnit(GetUnitAt(-4195.5f, 9859.5f));
        OcculordWest.AddUnit(GetUnitAt(-4289.5f, 9765.2f));
        OcculordWest.AddUnit(GetUnitAt(-4198.5f, 9708.5f));
        OcculordWest.AddUnit(GetUnitAt(-4294.0f, 9620.0f));
        OcculordWest.AddUnit(GetUnitAt(-4192.2f, 9557.8f), DropID.HealRune);
        OcculordWest.AddUnit(GetUnitAt(-4146.2f, 9398.2f));
        OcculordWest.AddUnit(GetUnitAt(-4252.0f, 9279.5f));
        OcculordWest.AddUnit(GetUnitAt(-4013.0f, 9714.0f), DropID.WoodBundle);

        CreepCamp FrenchmansEast = new(Camp.FrenchmansEast, CreepRespawnTime5m,
            _ => { SetShopState(Globals.FrenchmansEndEast, true); },
            _ => { SetShopState(Globals.FrenchmansEndEast, false); });
        FrenchmansEast.AddUnit(GetUnitAt(4693.0f, 5706.5f), DropID.HealLesser);
        FrenchmansEast.AddUnit(GetUnitAt(4818.2f, 5719.2f));
        FrenchmansEast.AddUnit(GetUnitAt(4817.0f, 5843.0f), DropID.Gold25Candy);

        FrenchmansEast.AddUnit(GetUnitAt(6860.8f, 5079.0f), DropID.Gold100Candy);
        FrenchmansEast.AddUnit(GetUnitAt(6680.5f, 4866.5f), DropID.Gold25Candy);
        FrenchmansEast.AddUnit(GetUnitAt(6918.2f, 4800.8f), DropID.HealRune);

        FrenchmansEast.AddUnit(GetUnitAt(6092.8f, 6072.8f));
        FrenchmansEast.AddUnit(GetUnitAt(6002.5f, 6060.2f));
        FrenchmansEast.AddUnit(GetUnitAt(5915.5f, 5951.0f));
        FrenchmansEast.AddUnit(GetUnitAt(6053.5f, 5922.5f), DropID.Gold25Candy);

        FrenchmansEast.AddUnit(GetUnitAt(4471.5f, 7612.0f), DropID.Gold25Candy);
        FrenchmansEast.AddUnit(GetUnitAt(4626.5f, 7753.5f), DropID.Gold25Candy);

        FrenchmansEast.AddUnit(GetUnitAt(8767.5f, 4525.2f), DropID.HealRune);
        FrenchmansEast.AddUnit(GetUnitAt(8393.5f, 4023.0f));
        FrenchmansEast.AddUnit(GetUnitAt(8614.0f, 4599.5f), DropID.Gold100Candy);
        FrenchmansEast.AddUnit(GetUnitAt(8324.5f, 4165.0f), DropID.Gold50Candy);
        FrenchmansEast.AddUnit(GetUnitAt(8354.0f, 4412.5f), DropID.Aura);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSC2_DRAGONSPAWN, DropID.HealLesser));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSC2_DRAGONSPAWN));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSC3_SPIDERCRAB_MANCATCHER, DropID.Gold25Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRD_FACELESS_GARGANTUAN, DropID.HealRune));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMSN_MUR_GUL_WHIPLASHER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMSN_MUR_GUL_WHIPLASHER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMRV_MUR_GUL_OVERSEER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMSC_MUR_GUL_SHADOWCASTER, DropID.Gold100Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMFS_COLD_ONE, DropID.Gold50Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NMFS_COLD_ONE, DropID.Gold50Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NHYD_HYDRA));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NHYD_HYDRA));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NAHY_ANCIENT_HYDRA_LEAD_HYDRA, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NAHY_ANCIENT_HYDRA_LEAD_HYDRA, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLRV_ELDRITCH_MONSTROSITY, DropID.Aura));
        FrenchmansEast.AddUnitSet(TempGroup);

        CreepCamp GolemEast = new(Camp.GolemEast, CreepRespawnTime4m);
        GolemEast.AddUnit(GetUnitAt(1255.0f, 3859.5f));
        GolemEast.AddUnit(GetUnitAt(1412.0f, 3774.8f));
        GolemEast.AddUnit(GetUnitAt(1290.5f, 3668.0f), DropID.Gold100Candy);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSOC_CRYSTAL_GOLEM));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSOC_CRYSTAL_GOLEM));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLRV_ELDRITCH_MONSTROSITY, DropID.Gold100Candy));
        GolemEast.AddUnitSet(TempGroup);

        CreepCamp OcculordEast = new(Camp.OcculordEast, CreepRespawnTime5m);
        OcculordEast.AddUnit(GetUnitAt(7571.0f, 9699.0f));
        OcculordEast.AddUnit(GetUnitAt(7422.0f, 9562.0f));
        OcculordEast.AddUnit(GetUnitAt(7227.0f, 9813.2f), DropID.Swiftness);

        OcculordEast.AddUnit(GetUnitAt(5314.0f, 9326.0f));
        OcculordEast.AddUnit(GetUnitAt(5567.8f, 9328.5f));
        OcculordEast.AddUnit(GetUnitAt(5407.5f, 9158.2f), DropID.QuadDamage);
        OcculordEast.AddUnit(GetUnitAt(5431.8f, 9622.5f), DropID.WoodBundle);

        OcculordEast.AddUnit(GetUnitAt(3373.5f, 10075.5f));
        OcculordEast.AddUnit(GetUnitAt(3306.5f, 9954.5f), DropID.TomeOfPower);
        OcculordEast.AddUnit(GetUnitAt(3358.0f, 9727.5f));
        OcculordEast.AddUnit(GetUnitAt(3448.2f, 9643.0f));
        OcculordEast.AddUnit(GetUnitAt(3370.5f, 9568.2f));
        OcculordEast.AddUnit(GetUnitAt(3457.8f, 9485.2f));
        OcculordEast.AddUnit(GetUnitAt(3376.2f, 9424.0f), DropID.HealRune);
        OcculordEast.AddUnit(GetUnitAt(3333.0f, 9268.0f));
        OcculordEast.AddUnit(GetUnitAt(3395.8f, 9136.5f));
        OcculordEast.AddUnit(GetUnitAt(3222.5f, 9555.2f), DropID.WoodBundle);

        CreepCamp HeroTop = new(Camp.HeroTop, CreepRespawnTime5m);
        HeroTop.AddUnit(GetUnitAt(-966.5f, 12722.0f));
        HeroTop.AddUnit(GetUnitAt(802.0f, 13192.0f));
        HeroTop.AddUnit(GetUnitAt(439.5f, 13230.2f));
        HeroTop.AddUnit(GetUnitAt(1097.2f, 13098.2f));
    }
}