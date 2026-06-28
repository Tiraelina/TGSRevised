using System;
using System.Collections.Generic;
using WCSharp.Api;
using static Constants;
using static TGS.Globals;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS
{
    public static class Drops
    {
        private static itempool IpAura { get; } = CreateItemPool();
        private static itempool IpGear { get; } = CreateItemPool();
        private static itempool IpHealthstone { get; } = CreateItemPool();
        private static itempool IpWatcherRune { get; } = CreateItemPool();
        private static itempool IpHealRune60 { get; } = CreateItemPool();
        private static itempool IpKeg { get; } = CreateItemPool();
        private static itempool IpGold100 { get; } = CreateItemPool();
        private static itempool IpGold50 { get; } = CreateItemPool();
        private static itempool IpGold25 { get; } = CreateItemPool();
        public static itempool IpCoin6 { get; } = CreateItemPool();
        private static itempool IpCandy25 { get; } = CreateItemPool();
        private static itempool IpCandy50 { get; } = CreateItemPool();
        private static itempool IpCandy100 { get; } = CreateItemPool();
        private static itempool IpWood100 { get; } = CreateItemPool();
        private static itempool IpWood50 { get; } = CreateItemPool();
        private static itempool IpUltravision { get; } = CreateItemPool();
        private static itempool IpTomeOfPower { get; } = CreateItemPool();
        private static itempool IpGear2 { get; } = CreateItemPool();
        private static itempool IpGear3 { get; } = CreateItemPool();
        private static itempool IpHealRune { get; } = CreateItemPool();
        private static itempool IpQuadDamage { get; } = CreateItemPool();
        private static itempool IpSwiftness { get; } = CreateItemPool();
        private static itempool IpHealRune50Candy { get; } = CreateItemPool();
        private static itempool IpHealLesser { get; } = CreateItemPool();

        private static Dictionary<DropID, List<itempool>> GetDropPool = new();

        public static void Init()
        {
            ItemPoolAddItemType(IpAura, ITEM_I09N_BLAST_STAFF, 1); // Chance: 1/6
            ItemPoolAddItemType(IpAura, ITEM_AJEN_ANCIENT_JUGGALO_OF_ENDURANCE, 1); // Chance: 1/6
            ItemPoolAddItemType(IpAura, ITEM_LGDH_LEGION_DOOM_HORN, 1); // Chance: 1/6
            ItemPoolAddItemType(IpAura, ITEM_AFAC_ALLERIA_S_FLUTE_OF_ACCURACY, 1); // Chance: 1/6
            ItemPoolAddItemType(IpAura, ITEM_LHST_THE_LION_HORN_OF_STORMWIND, 1); // Chance: 1/6
            ItemPoolAddItemType(IpAura, ITEM_WARD_WARSONG_BATTLE_DRUMS, 1); // Chance: 1/6

            ItemPoolAddItemType(IpGear, ITEM_I09L_SURGICAL_MASK, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear, ITEM_I09N_BLAST_STAFF, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear, ITEM_RATF_NETHERSTRAND_LONGBOW, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear, ITEM_CRDT_CROWN_OF_THE_DERPLORD, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear, ITEM_RDE3_RING_OF_THE_WALL, 1); // Chance: 1/5

            ItemPoolAddItemType(IpGear2, ITEM_SHCW_FURBOLG_S_FOCUS, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear2, ITEM_FWSS_FROST_WYRM_SKULL_SHIELD, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear2, ITEM_ROTS_SCEPTER_OF_THE_OCEAN, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear2, ITEM_RDE3_RING_OF_THE_WALL, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear2, ITEM_BRAC_BRAWLER_S_BRACERS, 1); // Chance: 1/5

            ItemPoolAddItemType(IpGear3, ITEM_SRBD_VAN_CLEEF_S_DAGGERS, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear3, ITEM_I09N_BLAST_STAFF, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear3, ITEM_MODT_LICH_S_MASK, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear3, ITEM_SRTL_WHIPLASH, 1); // Chance: 1/5
            ItemPoolAddItemType(IpGear3, ITEM_FRGD_FROSTGUARD, 1); // Chance: 1/5

            ItemPoolAddItemType(IpHealthstone, ITEM_HLST_EMPOWERED_HEALTH_STONE, 1); // Chance: 1/1

            ItemPoolAddItemType(IpKeg, ITEM_KGAL_KEG_OF_ALE, 1); // Chance: 1/5
            ItemPoolAddItemType(IpKeg, FourCC("COCK"), 1); // Chance: 4/5

            ItemPoolAddItemType(IpWatcherRune, ITEM_RWAT_RUNE_OF_THE_WATCHER, 1); // Chance: 1/1

            ItemPoolAddItemType(IpHealRune60, ITEM_RHE3_MEAT, 5); // Chance: 5/10
            ItemPoolAddItemType(IpHealRune60, ITEM_GOLD_75_GOLD, 1); // Chance: 1/10
            ItemPoolAddItemType(IpHealRune60, FourCC("COCK"), 4); // Chance: 4/10

            ItemPoolAddItemType(IpGold100, ITEM_GOLD_75_GOLD, 1); // Chance: 1/1

            ItemPoolAddItemType(IpGold25, ITEM_GOLD_75_GOLD, 1); // Chance: 1/4
            ItemPoolAddItemType(IpGold25, FourCC("COCK"), 2); // Chance: 3/4

            ItemPoolAddItemType(IpGold50, ITEM_GOLD_75_GOLD, 1); // Chance: 1/2
            ItemPoolAddItemType(IpGold50, FourCC("COCK"), 1); // Chance: 1/2

            ItemPoolAddItemType(IpCoin6, ITEM_GOLD_75_GOLD, 1); // Chance: 1/11
            ItemPoolAddItemType(IpCoin6, FourCC("COCK"), 10); // Chance: 10/11

            ItemPoolAddItemType(IpCandy25, ITEM_MANH_CANDY, 1); // Chance: 1/4
            ItemPoolAddItemType(IpCandy25, FourCC("COCK"), 3); // Chance: 3/4

            ItemPoolAddItemType(IpCandy50, ITEM_MANH_CANDY, 1); // Chance: 1/2
            ItemPoolAddItemType(IpCandy50, FourCC("COCK"), 1); // Chance: 1/2

            ItemPoolAddItemType(IpCandy100, ITEM_MANH_CANDY, 1); // Chance: 1/1

            ItemPoolAddItemType(IpWood50, ITEM_I0AE_WOOD_BUNDLE_3, 1); // Chance: 1/2
            ItemPoolAddItemType(IpWood50, FourCC("COCK"), 1); // Chance: 1/2

            ItemPoolAddItemType(IpWood100, ITEM_I0AE_WOOD_BUNDLE_3, 1); // Chance: 1/1

            ItemPoolAddItemType(IpUltravision, ITEM_GUVI_GLYPH_OF_ULTRAVISION, 1); // Chance: 1/1

            ItemPoolAddItemType(IpTomeOfPower, ITEM_TKNO_A_REALLY_HEAVY_BOOK, 1); // Chance: 1/1

            ItemPoolAddItemType(IpHealRune, ITEM_RHE3_MEAT, 1); // Chance: 1/1

            ItemPoolAddItemType(IpQuadDamage, ITEM_RREB_ORB_OF_SHIELDING, 1); // Chance: 1/1

            ItemPoolAddItemType(IpSwiftness, ITEM_RSPD_ICE_CREAM_BAR, 1); // Chance: 1/1

            ItemPoolAddItemType(IpHealRune50Candy, ITEM_RHE3_MEAT, 5); // Chance: 5/10
            ItemPoolAddItemType(IpHealRune50Candy, ITEM_MANH_CANDY, 1); // Chance: 1/10
            ItemPoolAddItemType(IpHealRune50Candy, FourCC("COCK"), 4); // Chance: 4/10

            ItemPoolAddItemType(IpHealLesser, ITEM_RHE1_RUNE_OF_LESSER_HEALING, 1); // Chance: 1/1

            GetDropPool[DropID.Nothing] = null;
            GetDropPool[DropID.Gold25Candy] = new List<itempool> { IpGold25, IpCandy25 };
            GetDropPool[DropID.Gold50Candy] = new List<itempool> { IpGold50, IpCandy50 };
            GetDropPool[DropID.Gold100Candy] = new List<itempool> { IpGold100, IpCandy100 };
            GetDropPool[DropID.WoodBundle] = new List<itempool> { IpWood100 };
            GetDropPool[DropID.HealRune60] = new List<itempool> { IpHealRune60 };
            GetDropPool[DropID.Healthstone] = new List<itempool> { IpHealthstone };
            GetDropPool[DropID.Keg] = new List<itempool> { IpKeg };
            GetDropPool[DropID.WatcherRune] = new List<itempool> { IpWatcherRune };
            GetDropPool[DropID.Aura] = new List<itempool> { IpAura };
            GetDropPool[DropID.Gear] = new List<itempool> { IpGear };
            GetDropPool[DropID.Candy5WoodBundles3] = new List<itempool> { IpCandy100, IpCandy100, IpCandy100, IpCandy100, IpCandy100, IpWood100, IpWood100, IpWood100 };
            GetDropPool[DropID.Ultravision] = new List<itempool> { IpUltravision };
            GetDropPool[DropID.TomeOfPower] = new List<itempool> { IpTomeOfPower };
            GetDropPool[DropID.Gear2] = new List<itempool> { IpGear2 };
            GetDropPool[DropID.Gear2] = new List<itempool> { IpGear3 };
            GetDropPool[DropID.HealRune] = new List<itempool> { IpHealRune };
            GetDropPool[DropID.QuadDamage] = new List<itempool> { IpQuadDamage };
            GetDropPool[DropID.Swiftness] = new List<itempool> { IpSwiftness };
            GetDropPool[DropID.HealRune50Candy] = new List<itempool> { IpHealRune50Candy };
            GetDropPool[DropID.HealLesser] = new List<itempool> { IpHealLesser };
        }

        public static void DropItem(this unit InUnit)
        {
            DropID Drop = (DropID)InUnit.UserData;
#if DEBUG
        Console.WriteLine(InUnit.UnitType.Id2String() + " dropping " + Drop.ToString());
#endif
            List<itempool> Itempools = GetDropPool[Drop];
            if (Itempools == null)
            {
                return;
            }

            foreach (itempool Pool in Itempools)
            {
                InUnit.DropFromPool(Pool);
            }
        }

        public static void DropFromPool(this unit InUnit, itempool InPool)
        {
            location Offset = PolarProjectionBJ(GetUnitLoc(InUnit), GetRandomReal(0, BlzGetUnitCollisionSize(InUnit)), GetRandomDirectionDeg());
            item Drop = PlaceRandomItem(InPool, GetLocationX(Offset), GetLocationY(Offset));
            if (Drop != null)
            {
                if (Drop.TypeId == ITEM_GOLD_75_GOLD)
                {
                    Coins75.Add(Drop);
                }
            }
            Offset.Dispose();
        }
    }

    public enum DropID
    {
        Nothing = 0,
        Gold25Candy = 1,
        Gold100Candy = 2,
        WoodBundle = 3,
        HealRune60 = 4,
        Healthstone = 5,
        Keg = 6,
        WatcherRune = 7,
        Aura = 8,
        Gear = 9,
        Candy5WoodBundles3 = 10,
        Ultravision = 11,
        TomeOfPower = 12,
        Gear2 = 13,
        Gear3 = 14,
        HealRune = 15,
        QuadDamage = 16,
        Swiftness = 17,
        HealRune50Candy = 18,
        HealLesser = 19,
        Gold50Candy = 20,
    }
}
