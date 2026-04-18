using System;
using System.Collections.Generic;
using WCSharp.Api;
using static Constants;
using static TGS.Globals;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public static class Drops
{
    private static itempool IP_Aura = CreateItemPool();
    private static itempool IP_Gear = CreateItemPool();
    private static itempool IP_Healthstone = CreateItemPool();
    private static itempool IP_WatcherRune = CreateItemPool();
    private static itempool IP_HealRune60 = CreateItemPool();
    private static itempool IP_Keg = CreateItemPool();
    private static itempool IP_Gold100 = CreateItemPool();
    private static itempool IP_Gold50 = CreateItemPool();
    private static itempool IP_Gold25 = CreateItemPool();
    public static itempool IP_Coin6 = CreateItemPool();
    private static itempool IP_Candy25 = CreateItemPool();
    private static itempool IP_Candy50 = CreateItemPool();
    private static itempool IP_Candy100 = CreateItemPool();
    private static itempool IP_Wood100 = CreateItemPool();
    private static itempool IP_Wood50 = CreateItemPool();
    private static itempool IP_Ultravision = CreateItemPool();
    private static itempool IP_TomeOfPower = CreateItemPool();
    private static itempool IP_Gear2 = CreateItemPool();
    private static itempool IP_Gear3 = CreateItemPool();
    private static itempool IP_HealRune = CreateItemPool();
    private static itempool IP_QuadDamage = CreateItemPool();
    private static itempool IP_Swiftness = CreateItemPool();
    private static itempool IP_HealRune50Candy = CreateItemPool();
    private static itempool IP_HealLesser = CreateItemPool();

    private static Dictionary<DropID, List<itempool>> GetDropPool = new();

    public static void Init()
    {
        ItemPoolAddItemType(IP_Aura, ITEM_I09N_BLAST_STAFF, 1); // Chance: 1/6
        ItemPoolAddItemType(IP_Aura, ITEM_AJEN_ANCIENT_JUGGALO_OF_ENDURANCE, 1); // Chance: 1/6
        ItemPoolAddItemType(IP_Aura, ITEM_LGDH_LEGION_DOOM_HORN, 1); // Chance: 1/6
        ItemPoolAddItemType(IP_Aura, ITEM_AFAC_ALLERIA_S_FLUTE_OF_ACCURACY, 1); // Chance: 1/6
        ItemPoolAddItemType(IP_Aura, ITEM_LHST_THE_LION_HORN_OF_STORMWIND, 1); // Chance: 1/6
        ItemPoolAddItemType(IP_Aura, ITEM_WARD_WARSONG_BATTLE_DRUMS, 1); // Chance: 1/6

        ItemPoolAddItemType(IP_Gear, ITEM_I09L_SURGICAL_MASK, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear, ITEM_I09N_BLAST_STAFF, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear, ITEM_RATF_NETHERSTRAND_LONGBOW, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear, ITEM_CRDT_CROWN_OF_THE_DERPLORD, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear, ITEM_RDE3_RING_OF_THE_WALL, 1); // Chance: 1/5

        ItemPoolAddItemType(IP_Gear2, ITEM_SHCW_FURBOLG_S_FOCUS, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear2, ITEM_FWSS_FROST_WYRM_SKULL_SHIELD, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear2, ITEM_ROTS_SCEPTER_OF_THE_OCEAN, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear2, ITEM_RDE3_RING_OF_THE_WALL, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear2, ITEM_BRAC_BRAWLER_S_BRACERS, 1); // Chance: 1/5

        ItemPoolAddItemType(IP_Gear3, ITEM_SRBD_VAN_CLEEF_S_DAGGERS, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear3, ITEM_I09N_BLAST_STAFF, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear3, ITEM_MODT_LICH_S_MASK, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear3, ITEM_SRTL_WHIPLASH, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Gear3, ITEM_FRGD_FROSTGUARD, 1); // Chance: 1/5

        ItemPoolAddItemType(IP_Healthstone, ITEM_HLST_EMPOWERED_HEALTH_STONE, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_Keg, ITEM_KGAL_KEG_OF_ALE, 1); // Chance: 1/5
        ItemPoolAddItemType(IP_Keg, FourCC("COCK"), 1); // Chance: 4/5

        ItemPoolAddItemType(IP_WatcherRune, ITEM_RWAT_RUNE_OF_THE_WATCHER, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_HealRune60, ITEM_RHE3_MEAT, 5); // Chance: 5/10
        ItemPoolAddItemType(IP_HealRune60, ITEM_GOLD_75_GOLD, 1); // Chance: 1/10
        ItemPoolAddItemType(IP_HealRune60, FourCC("COCK"), 4); // Chance: 4/10

        ItemPoolAddItemType(IP_Gold100, ITEM_GOLD_75_GOLD, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_Gold25, ITEM_GOLD_75_GOLD, 1); // Chance: 1/4
        ItemPoolAddItemType(IP_Gold25, FourCC("COCK"), 2); // Chance: 3/4

        ItemPoolAddItemType(IP_Gold50, ITEM_GOLD_75_GOLD, 1); // Chance: 1/2
        ItemPoolAddItemType(IP_Gold50, FourCC("COCK"), 1); // Chance: 1/2

        ItemPoolAddItemType(IP_Coin6, ITEM_GOLD_75_GOLD, 1); // Chance: 1/11
        ItemPoolAddItemType(IP_Coin6, FourCC("COCK"), 10); // Chance: 10/11

        ItemPoolAddItemType(IP_Candy25, ITEM_MANH_CANDY, 1); // Chance: 1/4
        ItemPoolAddItemType(IP_Candy25, FourCC("COCK"), 3); // Chance: 3/4

        ItemPoolAddItemType(IP_Candy50, ITEM_MANH_CANDY, 1); // Chance: 1/2
        ItemPoolAddItemType(IP_Candy50, FourCC("COCK"), 1); // Chance: 1/2

        ItemPoolAddItemType(IP_Candy100, ITEM_MANH_CANDY, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_Wood50, ITEM_I0AE_WOOD_BUNDLE_3, 1); // Chance: 1/2
        ItemPoolAddItemType(IP_Wood50, FourCC("COCK"), 1); // Chance: 1/2

        ItemPoolAddItemType(IP_Wood100, ITEM_I0AE_WOOD_BUNDLE_3, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_Ultravision, ITEM_GUVI_GLYPH_OF_ULTRAVISION, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_TomeOfPower, ITEM_TKNO_A_REALLY_HEAVY_BOOK, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_HealRune, ITEM_RHE3_MEAT, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_QuadDamage, ITEM_RREB_ORB_OF_SHIELDING, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_Swiftness, ITEM_RSPD_ICE_CREAM_BAR, 1); // Chance: 1/1

        ItemPoolAddItemType(IP_HealRune50Candy, ITEM_RHE3_MEAT, 5); // Chance: 5/10
        ItemPoolAddItemType(IP_HealRune50Candy, ITEM_MANH_CANDY, 1); // Chance: 1/10
        ItemPoolAddItemType(IP_HealRune50Candy, FourCC("COCK"), 4); // Chance: 4/10

        ItemPoolAddItemType(IP_HealLesser, ITEM_RHE1_RUNE_OF_LESSER_HEALING, 1); // Chance: 1/1

        GetDropPool[DropID.Nothing] = null;
        GetDropPool[DropID.Gold25Candy] = new List<itempool> { IP_Gold25, IP_Candy25 };
        GetDropPool[DropID.Gold50Candy] = new List<itempool> { IP_Gold50, IP_Candy50 };
        GetDropPool[DropID.Gold100Candy] = new List<itempool> { IP_Gold100, IP_Candy100 };
        GetDropPool[DropID.WoodBundle] = new List<itempool> { IP_Wood100 };
        GetDropPool[DropID.HealRune60] = new List<itempool> { IP_HealRune60 };
        GetDropPool[DropID.Healthstone] = new List<itempool> { IP_Healthstone };
        GetDropPool[DropID.Keg] = new List<itempool> { IP_Keg };
        GetDropPool[DropID.WatcherRune] = new List<itempool> { IP_WatcherRune };
        GetDropPool[DropID.Aura] = new List<itempool> { IP_Aura };
        GetDropPool[DropID.Gear] = new List<itempool> { IP_Gear };
        GetDropPool[DropID.Candy5WoodBundles3] = new List<itempool> { IP_Candy100, IP_Candy100, IP_Candy100, IP_Candy100, IP_Candy100, IP_Wood100, IP_Wood100, IP_Wood100 };
        GetDropPool[DropID.Ultravision] = new List<itempool> { IP_Ultravision };
        GetDropPool[DropID.TomeOfPower] = new List<itempool> { IP_TomeOfPower };
        GetDropPool[DropID.Gear2] = new List<itempool> { IP_Gear2 };
        GetDropPool[DropID.Gear2] = new List<itempool> { IP_Gear3 };
        GetDropPool[DropID.HealRune] = new List<itempool> { IP_HealRune };
        GetDropPool[DropID.QuadDamage] = new List<itempool> { IP_QuadDamage };
        GetDropPool[DropID.Swiftness] = new List<itempool> { IP_Swiftness };
        GetDropPool[DropID.HealRune50Candy] = new List<itempool> { IP_HealRune50Candy };
        GetDropPool[DropID.HealLesser] = new List<itempool> { IP_HealLesser };
    }

    public static void DropItem(unit InUnit)
    {
        DropID Drop = (DropID)InUnit.UserData;
#if DEBUG
        Console.WriteLine(Util.Id2String(InUnit.UnitType) + " dropping " + Drop.ToString());
#endif
        List<itempool> Itempools = GetDropPool[Drop];
        if (Itempools == null)
        {
            return;
        }

        foreach (itempool Pool in Itempools)
        {
            DropFromPool(InUnit, Pool);
        }
    }

    public static void DropFromPool(unit InUnit, itempool InPool)
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
