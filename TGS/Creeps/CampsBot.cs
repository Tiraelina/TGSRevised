using static Constants;
using static TGS.Creeps.CreepsCore;
using static TGS.Util;

namespace TGS.Creeps;

public static class CampsBot
{
    public static void Init()
    {
        CampGroup TempGroup;

        CreepCamp RuinsWest = new(Camp.RuinsWest, CreepRespawnTime5m,
            _ => { SetShopState(Globals.RuinsShopWest, true); },
            _ => { SetShopState(Globals.RuinsShopWest, false); });
        RuinsWest.AddUnit(GetUnitAt(-4608.0f, -7129.5f));
        RuinsWest.AddUnit(GetUnitAt(-4597.2f, -7313.0f), DropID.HealRune);
        RuinsWest.AddUnit(GetUnitAt(-4479.2f, -7219.2f), DropID.Gold25Candy);

        RuinsWest.AddUnit(GetUnitAt(-6610.8f, -7237.2f));
        RuinsWest.AddUnit(GetUnitAt(-6564.8f, -7381.8f));
        RuinsWest.AddUnit(GetUnitAt(-6564.0f, -7085.2f));
        RuinsWest.AddUnit(GetUnitAt(-6491.8f, -7233.5f), DropID.Gold25Candy);

        RuinsWest.AddUnit(GetUnitAt(-8055.0f, -6961.2f), DropID.HealRune);
        RuinsWest.AddUnit(GetUnitAt(-7983.0f, -7081.8f), DropID.Gold25Candy);
        RuinsWest.AddUnit(GetUnitAt(-7900.2f, -7152.0f));
        RuinsWest.AddUnit(GetUnitAt(-7870.5f, -6774.0f), DropID.QuadDamage);
        RuinsWest.AddUnit(GetUnitAt(-7766.9f, -6941.0f), DropID.Gold100Candy);

        RuinsWest.AddUnit(GetUnitAt(-8975.5f, -6590.0f));
        RuinsWest.AddUnit(GetUnitAt(-9160.8f, -6608.0f));
        RuinsWest.AddUnit(GetUnitAt(-8918.5f, -6467.2f), DropID.Gold50Candy);
        RuinsWest.AddUnit(GetUnitAt(-9247.0f, -6467.2f), DropID.Gold100Candy);
        RuinsWest.AddUnit(GetUnitAt(-9092.8f, -6400.2f), DropID.Aura);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRS_BEAST_TAMER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRS_BEAST_TAMER, DropID.HealRune));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRT_BLOOD_GILL, DropID.Gold50Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NJGA_ELDER_YETI_FISTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NJGA_ELDER_YETI_FISTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NJGA_ELDER_YETI_FISTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NJGB_ANGRY_YETI_SLAMMER, DropID.Gold50Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLDS_MAKRURA_REAPER, DropID.HealRune));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLSN_WALRUS_BONEGRINDER, DropID.Gold50Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLDS_MAKRURA_REAPER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLKL_MAKRURA_LURKER, DropID.QuadDamage));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLKL_MAKRURA_LURKER, DropID.Gold100Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSEL_SEA_MISTRESS));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSEL_SEA_MISTRESS));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSGB_URCHIN_LORD, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSGB_URCHIN_LORD, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NFOD_FACELESS_SIEGE_BREAKER, DropID.Aura));
        RuinsWest.AddUnitSet(TempGroup);

        CreepCamp TortalWest = new(Camp.TortalWest, CreepRespawnTime4m);
        TortalWest.AddUnit(GetUnitAt(-1689.0f, -4172.8f));
        TortalWest.AddUnit(GetUnitAt(-1524.8f, -4322.2f));
        TortalWest.AddUnit(GetUnitAt(-1656.5f, -4455.5f), DropID.HealRune50Candy);
        TortalWest.AddUnit(GetUnitAt(-1843.5f, -4345.2f), DropID.Gold25Candy);

        CreepCamp IcerogWest = new(Camp.IcerogWest, CreepRespawnTime5m);
        IcerogWest.AddUnit(GetUnitAt(-5058.5f, -8942.2f));
        IcerogWest.AddUnit(GetUnitAt(-5162.8f, -9389.0f));
        IcerogWest.AddUnit(GetUnitAt(-5178.0f, -8773.8f), DropID.TomeOfPower);
        IcerogWest.AddUnit(GetUnitAt(-5356.5f, -9462.8f), DropID.QuadDamage);
        IcerogWest.AddUnit(GetUnitAt(-5022.5f, -9174.5f), DropID.WoodBundle);

        IcerogWest.AddUnit(GetUnitAt(-9426.8f, -8975.0f), DropID.Gear);

        CreepCamp KoboldWest = new(Camp.KoboldWest, CreepRespawnTime5m);
        KoboldWest.AddUnit(GetUnitAt(-2336.2f, -8170.2f));
        KoboldWest.AddUnit(GetUnitAt(-2162.5f, -8152.5f), DropID.Swiftness);
        KoboldWest.AddUnit(GetUnitAt(-2145.0f, -8322.2f));
        KoboldWest.AddUnit(GetUnitAt(-2316.8f, -8372.5f), DropID.Healthstone);

        KoboldWest.AddUnit(GetUnitAt(-3400.5f, -9376.8f));
        KoboldWest.AddUnit(GetUnitAt(-3376.0f, -9051.5f));
        KoboldWest.AddUnit(GetUnitAt(-3465.2f, -9208.0f), DropID.WoodBundle);
        KoboldWest.AddUnit(GetUnitAt(-3523.0f, -9058.5f));

        CreepCamp NightlordWest = new(Camp.NightlordWest, CreepRespawnTime5m);
        NightlordWest.AddUnit(GetUnitAt(-10141.5f, -12488.0f));
        NightlordWest.AddUnit(GetUnitAt(-9491.5f, -13053.8f));
        NightlordWest.AddUnit(GetUnitAt(-9901.5f, -12546.0f));
        NightlordWest.AddUnit(GetUnitAt(-9571.0f, -12867.0f), DropID.Ultravision);
        NightlordWest.AddUnit(GetUnitAt(-9792.0f, -12806.0f), DropID.WoodBundle);

        CreepCamp DragonspawnWest = new(Camp.DragonspawnWest, CreepRespawnTime5m);
        DragonspawnWest.AddUnit(GetUnitAt(-3158.5f, -12905.8f), DropID.HealRune);

        CreepCamp RuinsEast = new(Camp.RuinsEast, CreepRespawnTime5m,
            _ => { SetShopState(Globals.RuinsShopEast, true); },
            _ => { SetShopState(Globals.RuinsShopEast, false); });
        RuinsEast.AddUnit(GetUnitAt(4531.0f, -7100.8f));
        RuinsEast.AddUnit(GetUnitAt(4500.0f, -7273.2f), DropID.HealRune);
        RuinsEast.AddUnit(GetUnitAt(4380.2f, -7189.5f), DropID.Gold25Candy);

        RuinsEast.AddUnit(GetUnitAt(7339.8f, -7188.0f));
        RuinsEast.AddUnit(GetUnitAt(7381.8f, -7339.0f));
        RuinsEast.AddUnit(GetUnitAt(7354.5f, -7486.0f));
        RuinsEast.AddUnit(GetUnitAt(7248.0f, -7312.0f), DropID.Gold25Candy);

        RuinsEast.AddUnit(GetUnitAt(7567.8f, -5874.2f));
        RuinsEast.AddUnit(GetUnitAt(7730.0f, -6111.2f), DropID.HealRune);
        RuinsEast.AddUnit(GetUnitAt(7674.0f, -5969.2f), DropID.Gold25Candy);
        RuinsEast.AddUnit(GetUnitAt(7338.5f, -6019.2f), DropID.QuadDamage);
        RuinsEast.AddUnit(GetUnitAt(7511.5f, -6168.8f), DropID.Gold100Candy);

        RuinsEast.AddUnit(GetUnitAt(9027.0f, -6557.8f));
        RuinsEast.AddUnit(GetUnitAt(9201.1f, -6554.8f));
        RuinsEast.AddUnit(GetUnitAt(8943.2f, -6429.0f), DropID.Gold50Candy);
        RuinsEast.AddUnit(GetUnitAt(9262.2f, -6425.8f), DropID.Gold100Candy);
        RuinsEast.AddUnit(GetUnitAt(9107.8f, -6409.0f), DropID.Aura);

        TempGroup = new CampGroup();
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRS_BEAST_TAMER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRS_BEAST_TAMER, DropID.HealRune));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NTRT_BLOOD_GILL, DropID.Gold25Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NJGA_ELDER_YETI_FISTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NJGA_ELDER_YETI_FISTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NJGA_ELDER_YETI_FISTER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NJGB_ANGRY_YETI_SLAMMER, DropID.Gold100Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLDS_MAKRURA_REAPER));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLSN_WALRUS_BONEGRINDER, DropID.HealRune));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLDS_MAKRURA_REAPER, DropID.Gold50Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLKL_MAKRURA_LURKER, DropID.QuadDamage));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NLKL_MAKRURA_LURKER, DropID.Gold100Candy));

        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSEL_SEA_MISTRESS));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSEL_SEA_MISTRESS));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSGB_URCHIN_LORD, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NSGB_URCHIN_LORD, DropID.Gold100Candy));
        TempGroup.CampUnitIDs.Add(new CampUnits(UNIT_NFOD_FACELESS_SIEGE_BREAKER, DropID.Aura));
        RuinsEast.AddUnitSet(TempGroup);

        CreepCamp TortalEast = new(Camp.TortalEast, CreepRespawnTime4m);
        TortalEast.AddUnit(GetUnitAt(1668.0f, -4429.8f));
        TortalEast.AddUnit(GetUnitAt(1727.5f, -4604.0f));
        TortalEast.AddUnit(GetUnitAt(1578.5f, -4751.5f), DropID.HealRune50Candy);
        TortalEast.AddUnit(GetUnitAt(1438.8f, -4591.5f), DropID.Gold25Candy);

        CreepCamp IcerogEast = new(Camp.IcerogEast, CreepRespawnTime5m);
        IcerogEast.AddUnit(GetUnitAt(5515.8f, -8605.0f));
        IcerogEast.AddUnit(GetUnitAt(5538.2f, -9060.8f));
        IcerogEast.AddUnit(GetUnitAt(5651.8f, -8546.5f), DropID.TomeOfPower);
        IcerogEast.AddUnit(GetUnitAt(5633.0f, -9247.0f), DropID.QuadDamage);
        IcerogEast.AddUnit(GetUnitAt(5463.0f, -8894.8f), DropID.WoodBundle);

        IcerogEast.AddUnit(GetUnitAt(10172.5f, -8448.5f), DropID.Gear);

        CreepCamp KoboldEast = new(Camp.KoboldEast, CreepRespawnTime5m);
        KoboldEast.AddUnit(GetUnitAt(2089.2f, -8077.5f));
        KoboldEast.AddUnit(GetUnitAt(1942.8f, -8133.2f), DropID.Swiftness);
        KoboldEast.AddUnit(GetUnitAt(2048.2f, -8325.2f));
        KoboldEast.AddUnit(GetUnitAt(2177.8f, -8309.0f), DropID.Healthstone);

        KoboldEast.AddUnit(GetUnitAt(3662.5f, -9393.8f));
        KoboldEast.AddUnit(GetUnitAt(3649.5f, -9087.0f));
        KoboldEast.AddUnit(GetUnitAt(3726.5f, -9269.5f), DropID.WoodBundle);
        KoboldEast.AddUnit(GetUnitAt(3809.5f, -9072.8f));

        CreepCamp NightlordEast = new(Camp.NightlordEast, CreepRespawnTime5m);
        NightlordEast.AddUnit(GetUnitAt(9086.0f, -12208.5f));
        NightlordEast.AddUnit(GetUnitAt(8433.8f, -12734.5f));
        NightlordEast.AddUnit(GetUnitAt(8876.8f, -12256.0f));
        NightlordEast.AddUnit(GetUnitAt(8532.8f, -12527.5f), DropID.Ultravision);
        NightlordEast.AddUnit(GetUnitAt(8751.0f, -12471.0f), DropID.WoodBundle);

        CreepCamp DragonspawnEast = new(Camp.DragonspawnEast, CreepRespawnTime5m);
        DragonspawnEast.AddUnit(GetUnitAt(3086.2f, -12118.8f), DropID.HealRune);

        CreepCamp HeroBot = new(Camp.HeroBot, CreepRespawnTime5m);
        HeroBot.AddUnit(GetUnitAt(-453.5f, 13308.5f));
        HeroBot.AddUnit(GetUnitAt(-312.2f, 13148.5f));
    }
}