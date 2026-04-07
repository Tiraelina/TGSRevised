using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Events;
using static Constants;
using static TGS.Globals;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public class ItemData
{
    public ItemData(int ItemId, float attackSpeed, float healthRegen, float manaRegen, float baseDamage, float spellBonus, int cleaveCount = 0, float cleaveBonus = 0.0f, float evasionChance = 0.0f)
    {
        AttackSpeed = attackSpeed;
        HealthRegen = healthRegen;
        ManaRegen = manaRegen;
        BaseDamage = baseDamage;
        SpellBonus = spellBonus;
        CleaveCount = cleaveCount;
        CleaveBonus = cleaveBonus;
        EvasionChance = evasionChance;
        Items.ItemLookup.Add(ItemId, this);
    }
    public ItemData()
    {
        AttackSpeed = 0.0f;
        HealthRegen = 0.0f;
        ManaRegen = 0.0f;
        BaseDamage = 0.0f;
        SpellBonus = 0.0f;
        CleaveCount = 0;
        CleaveBonus = 0.0f;
        EvasionChance = 0.0f;
    }

    public float AttackSpeed { get; set; }
    public float HealthRegen { get; set; }
    public float ManaRegen { get; set; }
    public float BaseDamage { get; set; }
    public float SpellBonus { get; set; }
    public int CleaveCount { get; set; }
    public float CleaveBonus { get; set; }
    public float EvasionChance { get; set; }
}

public static class Items
{
    public static Dictionary<int, ItemData> ItemLookup = new();

    public static void InitItemData()
    {
        new ItemData(ITEM_PRVT_PERIAPT_OF_VITALITY
            , 0.0f, 1.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_I0B2_CHEESEBLASTER
            , 0.0f, 0.0f, 0.0f, 3.0f, 0.0f);
        new ItemData(ITEM_GCEL_GLOVES_OF_HASTE
            , 25.0f, 0.0f, 0.0f, 3.0f, 0.0f);
        //new ItemData(ITEM_RWIZ_SOBI_MASK, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_CNHN_HORN_OF_CENARIUS
            , 0.0f, 2.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_I0AT_DRAGONLANCE
            , 0.0f, 0.0f, 0.0f, 75.0f, 0.0f);
        new ItemData(ITEM_I0AO_SHROUDBLADE_OF_AZZINOTH
            , 25.0f, 0.0f, 0.0f, 20.0f, 0.0f);
        new ItemData(ITEM_I0AR_SUMMONER_S_STAFF
            , 20.0f, 0.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_I09O_BALANCED_AXE
            , 0.0f, 0.0f, 0.0f, 50.0f, 0.0f);
        new ItemData(ITEM_I09Q_HEAD_BARREL
            , 0.0f, 0.0f, 0.0f, 10.0f, 0.0f);
        new ItemData(ITEM_I04S_WIRT_S_THIRD_LEG
            , 0.0f, 0.0f, 0.0f, 40.0f, 0.0f, 1);
        new ItemData(ITEM_I0B0_NIGHTFURY_THE_BLOOD_DRINKER
            , 0.0f, 0.0f, 0.0f, 50.0f, 0.0f);
        new ItemData(ITEM_I0AQ_GILDED_ARMOR
            , -25.0f, 0.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_I09P_CAT_PAW
            , 70.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0, 0.0f, 0.1f);
        new ItemData(ITEM_I0AW_SHATTER_BLADE_OF_STORMWIND
            , 0.0f, 16.0f, 0.0f, 20.0f, 0.0f, 2, 0.25f);
        new ItemData(ITEM_JDRN_YARN_BALL
            , 50.0f, 0.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_SRTL_WHIPLASH
            , 25.0f, 0.0f, 0.0f, 40.0f, 0.0f);
        new ItemData(ITEM_FRGD_FROSTGUARD
            , 0.0f, 4.0f, 0.0f, 20.0f, 0.0f);
        new ItemData(ITEM_KLMM_OFF_BALANCE_HALBERD
            , 0.0f, 4.0f, 0.0f, 50.0f, 0.0f);
        new ItemData(ITEM_I09L_SURGICAL_MASK
            , 0.0f, 0.0f, 100.0f, 0.0f, 0.0f);
        new ItemData(ITEM_RLIF_RING_OF_REGENERATION
            , 0.0f, 4.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_RDE1_RING_OF_OPULENCE
            , 0.0f, 1.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_RAT6_LONGSWORD_5_2
            , 0.0f, 0.0f, 0.0f, 5.0f, 0.0f);
        new ItemData(ITEM_I0AN_BLOODY_KEY
            , 0.0f, 16.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_RATF_NETHERSTRAND_LONGBOW
            , 0.0f, 0.0f, 0.0f, 30.0f, 0.0f);
        new ItemData(ITEM_KGAL_KEG_OF_ALE
            , 0.0f, 1.0f, 1.0f, 0.0f, 0.0f);
        new ItemData(ITEM_KTRM_RAG_NAR_O_S_THE_FIBER_HOARD
            , 0.0f, 50.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_CLFM_MANAFUSED_CLOAK
            , 0.0f, 0.0f, 400.0f, 0.0f, 0.0f);
        new ItemData(ITEM_RATC_QUEL_THALAS_GREATSWORD_12_4
            , 0.0f, 0.0f, 0.0f, 12.0f, 0.0f);
        new ItemData(ITEM_I0AZ_EMBERSTONE_STAFF
            , 0.0f, 0.0f, 25.0f, 0.0f, 0.0f);
        new ItemData(ITEM_PHLT_POWER_CRYSTAL
            , 0.0f, 4.0f, 1.0f, 0.0f, 0.0f);
        new ItemData(ITEM_VPUR_CLAYMORE
            , 0.0f, 8.0f, 0.0f, 0.0f, 0.0f);
        new ItemData(ITEM_KYSN_LONGBOW
            , 25.0f, 0.0f, 0.0f, 0.0f, 0.0f);
    }

    public static void Init()
    {
        InitItemData();
        PlayerUnitEvents.Register(UnitTypeEvent.ReceivesTargetOrder, VialClick);
        PlayerUnitEvents.Register(UnitTypeEvent.UsesItem, VialUsed);

        PlayerUnitEvents.Register(UnitEvent.SellsItem, AbilityExchange, GoblinWest);
        PlayerUnitEvents.Register(UnitEvent.SellsItem, AbilityExchange, GoblinEast);
    }

    private static void AbilityExchange()
    {
        switch (GetSoldItem().TypeId)
        {
            case ITEM_I034_EXCHANGE_STATS_STR_FOR_GOLD:
                if (GetBuyingUnit().BaseStrength > 3)
                {
                    GetBuyingUnit().BaseStrength -= 1;
                    GetBuyingUnit().Owner.Gold += 50;
                }

                break;
            case ITEM_I02Z_EXCHANGE_2_STR_FOR_2_AGI:
                if (GetBuyingUnit().BaseStrength > 4)
                {
                    GetBuyingUnit().BaseStrength -= 2;
                    GetBuyingUnit().BaseAgility += 2;
                }
                else
                {
                    GetBuyingUnit().Owner.Gold += 200;
                }

                break;
            case ITEM_I04U_EXCHANGE_2_STR_FOR_2_INT:
                if (GetBuyingUnit().BaseStrength > 4)
                {
                    GetBuyingUnit().BaseStrength -= 2;
                    GetBuyingUnit().BaseIntelligence += 2;
                }
                else
                {
                    GetBuyingUnit().Owner.Gold += 200;
                }

                break;
            case ITEM_I035_EXCHANGE_STATS_AGI_FOR_GOLD:
                if (GetBuyingUnit().BaseAgility > 3)
                {
                    GetBuyingUnit().BaseAgility -= 1;
                    GetBuyingUnit().Owner.Gold += 50;
                }

                break;
            case ITEM_I02Y_EXCHANGE_2_AGI_FOR_2_STR:
                if (GetBuyingUnit().BaseAgility > 4)
                {
                    GetBuyingUnit().BaseAgility -= 2;
                    GetBuyingUnit().BaseStrength += 2;
                }
                else
                {
                    GetBuyingUnit().Owner.Gold += 200;
                }

                break;
            case ITEM_I033_EXCHANGE_2_AGI_FOR_2_INT:
                if (GetBuyingUnit().BaseAgility > 4)
                {
                    GetBuyingUnit().BaseAgility -= 2;
                    GetBuyingUnit().BaseIntelligence += 2;
                }
                else
                {
                    GetBuyingUnit().Owner.Gold += 200;
                }

                break;
            case ITEM_I036_EXCHANGE_STATS_INT_FOR_GOLD:
                if (GetBuyingUnit().BaseIntelligence > 3)
                {
                    GetBuyingUnit().BaseIntelligence -= 1;
                    GetBuyingUnit().Owner.Gold += 50;
                }

                break;
            case ITEM_I032_EXCHANGE_2_INT_FOR_2_STR:
                if (GetBuyingUnit().BaseIntelligence > 4)
                {
                    GetBuyingUnit().BaseIntelligence -= 2;
                    GetBuyingUnit().BaseStrength += 2;
                }
                else
                {
                    GetBuyingUnit().Owner.Gold += 200;
                }

                break;
            case ITEM_I030_EXCHANGE_2_INT_FOR_2_AGI:
                if (GetBuyingUnit().BaseIntelligence > 4)
                {
                    GetBuyingUnit().BaseIntelligence -= 2;
                    GetBuyingUnit().BaseAgility += 2;
                }
                else
                {
                    GetBuyingUnit().Owner.Gold += 200;
                }

                break;
            default:
                break;
        }
    }

    private static void VialUsed()
    {
        if (GetManipulatedItem().TypeId == ITEM_BZBF_FULL_VIAL)
        {
            if (GetManipulatingUnit().Owner.Name == "Honejasi#1172")
            {
                int French = GetRandomInt(1, 10);
                if (French == 10)
                {
                    PlaySoundOnUnitBJ(CR_Jaraxxus_Special01, 100, GetSpellAbilityUnit());
                    location RandomLoc = GetRandomLocInRect(GetEntireMapRect());
                    GetManipulatingUnit().SetPosition(RandomLoc.X, RandomLoc.Y);
                    RandomLoc.Dispose();
                }
            }

            GetManipulatedItem().Dispose();
            GetTriggerUnit().AddItem(ITEM_BZBE_EMPTY_VIAL);
        }
    }

    private static void VialClick()
    {
        if (GetTriggerUnit().IsUnitType(unittype.Hero)
            && ((GetOrderTargetUnit() == HumFountain
                 && GetTriggerUnit().IsInRange(HumFountain, 400.0f))
                || (GetOrderTargetUnit() == OrcFountain
                    && GetTriggerUnit().IsInRange(OrcFountain, 400.0f))))
        {
            for (int i = 0; i <= 5; i++)
            {
                if (GetOrderedUnit().ItemAtOrDefault(i).TypeId == ITEM_BZBE_EMPTY_VIAL)
                {
                    GetOrderedUnit().RemoveItem(i);
                    GetOrderedUnit().AddItem(ITEM_BZBF_FULL_VIAL);
#if DEBUG
                    Console.WriteLine("Vial swapped.");
#endif
                }
            }
        }
    }
}
