using System;
using System.Collections.Generic;
using TGS.Spells;
using WCSharp.Api;
using WCSharp.Events;
using static Constants;
using static TGS.Globals;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS
{
    public class ItemData
    {
        public ItemData(int ItemId)
        {
            Items.ItemLookup.Add(ItemId, this);
        }

        public ItemData()
        {
            AttackSpeed = 0.0f;
            HealthRegenBonus = 0.0f;
            HealthRegenPercentage = 0.0f;
            ManaRegenFactor = 0.0f;
            ManaRegenBonus = 0.0f;
            ManaRegenPercentage = 0.0f;
            BaseDamage = 0.0f;
            SpellBonus = 0.0f;
            CleaveCount = 0;
            CleaveBonus = 0.0f;
            EvasionChance = 0.0f;
            DamageTakenModifier = 0.0f;
            UnavoidableDamage = 0.0f;
        }

        public float AttackSpeed { get; set; }
        public float HealthRegenBonus { get; set; }
        public float HealthRegenPercentage { get; set; }
        public float ManaRegenFactor { get; set; }
        public float ManaRegenBonus { get; set; }
        public float ManaRegenPercentage { get; set; }
        public float BaseDamage { get; set; }
        public float SpellBonus { get; set; }
        public int CleaveCount { get; set; }
        public float CleaveBonus { get; set; }
        public float EvasionChance { get; set; }
        public float DamageTakenModifier { get; set; }
        public float UnavoidableDamage { get; set; }
        public List<OrbType> OrbEffects { get; set; } = new();
    }

    public static class Items
    {
        public static Dictionary<int, ItemData> ItemLookup = new();

        public static void InitItemData()
        {
            ItemData Item;
            Item = new ItemData(ITEM_PRVT_PERIAPT_OF_VITALITY);
            Item.HealthRegenBonus = 1.0f;
            Item = new ItemData(ITEM_I0B2_CHEESEBLASTER);
            Item.BaseDamage = 3.0f;
            Item = new ItemData(ITEM_GCEL_GLOVES_OF_HASTE);
            Item.AttackSpeed = 25.0f;
            Item.BaseDamage = 3.0f;
            //Item = new ItemData(ITEM_RWIZ_SOBI_MASK, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
            Item = new ItemData(ITEM_CNHN_HORN_OF_CENARIUS);
            Item.HealthRegenBonus = 2.0f;
            Item = new ItemData(ITEM_I0AT_DRAGONLANCE);
            Item.BaseDamage = 75.0f;
            Item.UnavoidableDamage = 30.0f;
            Item = new ItemData(ITEM_I0AO_SHROUDBLADE_OF_AZZINOTH);
            Item.AttackSpeed = 25.0f;
            Item.BaseDamage = 20.0f;
            Item = new ItemData(ITEM_I0AR_SUMMONER_S_STAFF);
            Item.AttackSpeed = 20.0f;
            Item = new ItemData(ITEM_I09O_BALANCED_AXE);
            Item.BaseDamage = 50.0f;
            Item = new ItemData(ITEM_I09Q_HEAD_BARREL);
            Item.BaseDamage = 10.0f;
            Item = new ItemData(ITEM_I04S_WIRT_S_THIRD_LEG);
            Item.BaseDamage = 40.0f;
            Item.CleaveCount = 1;
            Item = new ItemData(ITEM_I0B0_NIGHTFURY_THE_BLOOD_DRINKER);
            Item.BaseDamage = 50.0f;
            Item = new ItemData(ITEM_I0AQ_GILDED_ARMOR);
            Item.AttackSpeed = -25.0f;
            Item = new ItemData(ITEM_I09P_CAT_PAW);
            Item.AttackSpeed = 70.0f;
            Item.EvasionChance = 0.1f;
            List<OrbType> OrbEffects = new();
            OrbEffects.Add(OrbType.Pillage);
            Item = new ItemData(ITEM_I0AW_SHATTER_BLADE_OF_STORMWIND);
            Item.HealthRegenBonus = 16.0f;
            Item.BaseDamage = 20.0f;
            Item.CleaveCount = 2;
            Item.CleaveBonus = 0.25f;
            Item.OrbEffects = OrbEffects;
            Item = new ItemData(ITEM_JDRN_YARN_BALL);
            Item.AttackSpeed = 50.0f;
            Item = new ItemData(ITEM_SRTL_WHIPLASH);
            Item.AttackSpeed = 25.0f;
            Item.BaseDamage = 40.0f;
            Item = new ItemData(ITEM_FRGD_FROSTGUARD);
            Item.HealthRegenBonus = 4.0f;
            Item.BaseDamage = 20.0f;
            Item = new ItemData(ITEM_KLMM_OFF_BALANCE_HALBERD);
            Item.HealthRegenBonus = 4.0f;
            Item.BaseDamage = 50.0f;
            Item = new ItemData(ITEM_I09L_SURGICAL_MASK);
            Item.ManaRegenFactor = 1.0f;
            Item = new ItemData(ITEM_RLIF_RING_OF_REGENERATION);
            Item.HealthRegenBonus = 4.0f;
            Item = new ItemData(ITEM_RDE1_RING_OF_OPULENCE);
            Item.HealthRegenBonus = 1.0f;
            Item = new ItemData(ITEM_RAT6_LONGSWORD_5_2);
            Item.BaseDamage = 5.0f;
            Item = new ItemData(ITEM_I0AN_BLOODY_KEY);
            Item.HealthRegenBonus = 16.0f;
            Item = new ItemData(ITEM_RATF_NETHERSTRAND_LONGBOW);
            Item.BaseDamage = 30.0f;
            Item = new ItemData(ITEM_KGAL_KEG_OF_ALE);
            Item.HealthRegenBonus = 1.0f;
            Item.ManaRegenBonus = 1.0f;
            Item = new ItemData(ITEM_KTRM_RAG_NAR_O_S_THE_FIBER_HOARD);
            Item.HealthRegenBonus = 50.0f;
            Item = new ItemData(ITEM_CLFM_MANAFUSED_CLOAK);
            Item.ManaRegenFactor = 4.0f;
            Item = new ItemData(ITEM_RATC_QUEL_THALAS_GREATSWORD_12_4);
            Item.BaseDamage = 12.0f;
            Item = new ItemData(ITEM_I0AZ_EMBERSTONE_STAFF);
            Item.ManaRegenFactor = 0.25f;
            Item = new ItemData(ITEM_PHLT_POWER_CRYSTAL);
            Item.HealthRegenBonus = 4.0f;
            Item.ManaRegenBonus = 1.0f;
            Item = new ItemData(ITEM_VPUR_CLAYMORE);
            Item.HealthRegenBonus = 8.0f;
            Item = new ItemData(ITEM_KYSN_LONGBOW);
            Item.AttackSpeed = 25.0f;
            Item = new ItemData(ABILITY_A0KP_SCORCHING_RAY_PYRO_STAFF);
            Item.DamageTakenModifier = 0.5f;
            OrbEffects.Clear();
            OrbEffects.Add(OrbType.Ooze);
            Item = new ItemData(ITEM_ODEF_ORB_OF_SHADOWS);
            Item.OrbEffects = OrbEffects;
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
            if (GetManipulatedItem().TypeId == ITEM_BZBF_QUESTIONABLE_PHILTER)
            {
                if (GetManipulatingUnit().Owner.Name == "Honejasi#1172")
                {
                    int French = GetRandomInt(1, 10);
                    if (French == 10)
                    {
                        PlaySoundOnUnitBJ(CrJaraxxusSpecial01, 100, GetSpellAbilityUnit());
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
                        GetOrderedUnit().AddItem(ITEM_BZBF_QUESTIONABLE_PHILTER);
#if DEBUG
                    Console.WriteLine("Vial swapped.");
#endif
                    }
                }
            }
        }
    }
}
