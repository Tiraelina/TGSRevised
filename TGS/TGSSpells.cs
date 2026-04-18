using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TGS.Spells;
using WCSharp.Api;
using WCSharp.Dummies;
using WCSharp.Events;
using WCSharp.Missiles;
using WCSharp.Shared;
using WCSharp.Shared.Extensions;
using static TGS.Globals;
using static Constants;
using static TGS.Util;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public enum AttributeType
{
    None = -1,
    Str = 0,
    Agi = 1,
    Int = 2,
    All = 3
}

public class SpellScaling
{
    private string SpellComponentName { get; set; }
    public int Index { get; init; }
    private int ItemId { get; set; }
    private AttributeType ScalingAttribute { get; set; }
    private float DamageBase { get; set; }
    private float DamageBonus { get; set; }
    private float Scalar { get; set; }

    public SpellScaling(string InName, int InIndex, int InItemId, AttributeType InAttribute, float InBase = 1.0f, float InBonus = 1.0f, float InScalar = 1.0f)
    {
        SpellComponentName = InName;
        Index = InIndex - 3000;
        ItemId = InItemId;
        ScalingAttribute = InAttribute;
        DamageBase = InBase;
        DamageBonus = InBonus;
        Scalar = InScalar;
        TGSSpells.SpellScalingList[Index] = this;
    }

    public float GetDamage(unit InHero)
    {
        if (TGSAbilities.ByUnit.TryGetValue(InHero, out TGSHero Value))
        {
            int Attribute = Value.GetAttribute(ScalingAttribute);
            int AbilLevel = Value.GetAbilityLevel(ItemId);

            float LevelDamage = DamageBase + (DamageBonus * AbilLevel);
            float Scaling = Attribute * Scalar;
            return LevelDamage + (LevelDamage * Scaling);
        }

        return 0.0f;
    }
}

public static class TGSSpells
{
    public static Dictionary<int, int> PocketFactoryLookup = new();
    public static Dictionary<int, int> ClockwerkGoblinLookup = new();
    public static List<SpellScaling> SpellScalingList = new();

    public static void Init()
    {
        trigger AbilityScaling = trigger.Create();
        TriggerRegisterAnyUnitEventBJ(AbilityScaling, EVENT_PLAYER_UNIT_DAMAGING);
        AbilityScaling.AddCondition(Condition(DamageHandling));

        PlayerUnitEvents.Register(SpellEvent.Effect, HeroSpellEffect);
        PlayerUnitEvents.Register(SpellEvent.Channel, ChannelingRemoveDivine);

        PocketFactoryLookup.Add(1, UNIT_NFAC_POCKET_FACTORY_L1);
        PocketFactoryLookup.Add(2, UNIT_NFA1_POCKET_FACTORY_L2);
        PocketFactoryLookup.Add(3, UNIT_NFA2_POCKET_FACTORY_L3);
        PocketFactoryLookup.Add(4, UNIT_N01Z_POCKET_FACTORY_L4);
        PocketFactoryLookup.Add(5, UNIT_N020_POCKET_FACTORY_L5);
        PocketFactoryLookup.Add(6, UNIT_N021_POCKET_FACTORY_L6);
        PocketFactoryLookup.Add(7, UNIT_N022_POCKET_FACTORY_L7);
        PocketFactoryLookup.Add(8, UNIT_N023_POCKET_FACTORY_L8);
        PocketFactoryLookup.Add(9, UNIT_N024_POCKET_FACTORY_L9);
        PocketFactoryLookup.Add(10, UNIT_N01Y_ULTRA_POCKET_FACTORY_L10);

        ClockwerkGoblinLookup.Add(1, UNIT_NCGB_CLOCKWERK_GOBLIN_L1);
        ClockwerkGoblinLookup.Add(2, UNIT_NCG1_CLOCKWERK_GOBLIN_L2);
        ClockwerkGoblinLookup.Add(3, UNIT_NCG2_CLOCKWERK_GOBLIN_L3);
        ClockwerkGoblinLookup.Add(4, UNIT_N027_CLOCKWERK_GOBLIN_L4);
        ClockwerkGoblinLookup.Add(5, UNIT_N028_CLOCKWERK_GOBLIN_L5);
        ClockwerkGoblinLookup.Add(6, UNIT_N029_CLOCKWERK_GOBLIN_L6);
        ClockwerkGoblinLookup.Add(7, UNIT_N02A_CLOCKWERK_GOBLIN_L7);
        ClockwerkGoblinLookup.Add(8, UNIT_N02B_CLOCKWERK_GOBLIN_L8);
        ClockwerkGoblinLookup.Add(9, UNIT_N025_CLOCKWERK_GOBLIN_L9);
        ClockwerkGoblinLookup.Add(10, UNIT_N026_CLOCKWERK_GOBLIN_L10);
    }

    private static void InitSpellTable()
    {
        // new SpellScaling("Bash", AttributeType.Str);
        new SpellScaling("Blizzard", 3001, ITEM_I004_BLIZZARD_ACTIVE, AttributeType.Int, 10.0f, 10.0f, 0.4f);
        new SpellScaling("Blizzard DoT", 3002, ITEM_I004_BLIZZARD_ACTIVE, AttributeType.Int, 0.0f, 5.0f, 0.4f);
        new SpellScaling("Flame Strike Full", 3003, ITEM_I008_FLAME_STRIKE_ACTIVE, AttributeType.Int, 15.0f, 10.0f, 0.4f);
        new SpellScaling("Flame Strike Half", 3004, ITEM_I008_FLAME_STRIKE_ACTIVE, AttributeType.Int, 0.0f, 7.0f, 0.4f);
        // new SpellScaling("Siphon Mana", AttributeType.Int);
        new SpellScaling("Thunder Clap Target", 3005, ITEM_I003_THUNDER_CLAP_ACTIVE, AttributeType.Str, 0.0f, 60.0f, 0.4f);
        new SpellScaling("Thunder Clap AOE", 3006, ITEM_I003_THUNDER_CLAP_ACTIVE, AttributeType.Str, 15.0f, 45.0f, 0.4f);
        // new SpellScaling("Mirror Image", AttributeType.Agi);
        // new SpellScaling("Shockwave", AttributeType.Str);
        // new SpellScaling("War Stomp", AttributeType.Str);
        // new SpellScaling("Wind Walk", AttributeType.Agi);
        // new SpellScaling("Carrion Swarm", AttributeType.Int);
        // new SpellScaling("Soul Harvest", AttributeType.Int);
        // new SpellScaling("Death Pact", AttributeType.Int);
        // new SpellScaling("Feedback", AttributeType.Agi);
        // new SpellScaling("Frost Nova", AttributeType.Int);
        // new SpellScaling("Impale", AttributeType.Str, 50.0f, 30.0f, 2.0f);
        // new SpellScaling("Spiked Carapace", AttributeType.Str);
        // new SpellScaling("Entangling Roots", AttributeType.Int);
        // new SpellScaling("Diffusion Flare", AttributeType.Int);
        // new SpellScaling("Fan of Knives", AttributeType.Agi);
        // new SpellScaling("Immolation", AttributeType.Agi);
        // new SpellScaling("Searing Arrows", AttributeType.Agi);
        // new SpellScaling("Shadow Strike", AttributeType.Agi);
        // new SpellScaling("Thorns Aura", AttributeType.Int);
        // new SpellScaling("Acid Bomb", AttributeType.Int);
        // new SpellScaling("Breath of Fire", AttributeType.Str);
        // new SpellScaling("Cleaving Attack", AttributeType.Str);
        // new SpellScaling("Cluster Rockets", AttributeType.Int);
        // new SpellScaling("Cold Arrows", AttributeType.Agi);
        // new SpellScaling("Drunken Haze", AttributeType.Str);
        // new SpellScaling("Forked Lightning", AttributeType.Int);
        // new SpellScaling("Healing Spray", AttributeType.Int);
        // new SpellScaling("Essence Drain", AttributeType.Int);
        // new SpellScaling("Rain of Fire", AttributeType.Int);
        // new SpellScaling("Chemical Rage", AttributeType.Str);
        // new SpellScaling("Fire Nova", AttributeType.Int);
        // new SpellScaling("Frostbolt", AttributeType.Int);
        // new SpellScaling("Berserk", AttributeType.Str);
        // new SpellScaling("Ion Shield", AttributeType.Int);
        // new SpellScaling("Monsoon", AttributeType.Int);
        // new SpellScaling("Glacial Spike", AttributeType.Int);
        // new SpellScaling("Goblin Land Mine", AttributeType.Agi);
        // new SpellScaling("Finger of Death", AttributeType.Int);
        // new SpellScaling("Frost Missile", AttributeType.Int);
        // new SpellScaling("Poison Sting", AttributeType.Agi);
        // new SpellScaling("Slow Poison", AttributeType.Agi);
        // new SpellScaling("Big Game Hunter", AttributeType.Str);
        // new SpellScaling("Hurl Boulder", AttributeType.Str);
        // new SpellScaling("Falling Sword", AttributeType.Int);
        // new SpellScaling("Incinerate", AttributeType.Agi);
        new SpellScaling("Bladestorm", 3007, ITEM_I00V_BLADESTORM_ULTIMATE, AttributeType.Agi, 25.0f, 75.0f, 0.8f);
        // new SpellScaling("Earthquake", AttributeType.Int);
        // new SpellScaling("Death And Decay", AttributeType.Int);
        // new SpellScaling("Inferno", AttributeType.All);
        // new SpellScaling("Locust Swarm", AttributeType.Str);
        // new SpellScaling("Starfall", AttributeType.Agi);
        // new SpellScaling("Tranquility", AttributeType.Int);
        // new SpellScaling("Charm", AttributeType.All);
        // new SpellScaling("Doom", AttributeType.Int);
        // new SpellScaling("Stampede", AttributeType.Str);
        // new SpellScaling("Storm, Earth, And Fire", AttributeType.All);
        // new SpellScaling("Tornado", AttributeType.Int);
    }

    private static bool HeroAbilityScaling()
    {
        unit DamageSource = GetEventDamageSource();
        if (BlzGetEventDamageType() != DAMAGE_TYPE_SPIRIT_LINK
            && DamageSource.IsUnitType(unittype.Hero)
            && !BlzGetEventIsAttack())
        {
            float EventDamage = GetEventDamage();
            if (UnitHasBuffBJ(BlzGetEventDamageTarget(), BUFF_B00F_DEPRESSION_AURA))
            {
                BlzSetEventDamage((EventDamage + EventDamage * DamageSource.Intelligence / 4.0f / 100.0f) * 0.75f);
            }
            else
            {
                BlzSetEventDamage(EventDamage + EventDamage * DamageSource.Intelligence / 4.0f / 100.0f);
            }
            //BlzSetAbilityExtendedTooltip(ABILITY_AHDS_DIVINE_SHIELD_Q, "CAT", 1);
            return true;
        }

        return false;
    }

    // TODO: replace orbs, spell vamp, spell crits?, cleave?
    private static bool DamageHandling()
    {
        unit DamageSource = GetEventDamageSource();
        unit DamageTarget = BlzGetEventDamageTarget();
        float EventDamage = GetEventDamage();
        bool bEnemy = DamageSource.Owner.IsEnemy(DamageTarget.Owner);
        
        // Channeling is showing 0 tags too
        if (EventDamage <= 0.0f)
        {
            return false;
        }

        // SPELL DAMAGE
        if (BlzGetEventDamageType() != DAMAGE_TYPE_SPIRIT_LINK
            && DamageSource.IsUnitType(unittype.Hero)
            && !BlzGetEventIsAttack())
        {
            TagType OutTag = TagType.Spell;
            float SpellReductionFinal = 1.0f;
            if (UnitHasBuffBJ(DamageTarget, BUFF_B00F_DEPRESSION_AURA))
            {
                SpellReductionFinal = 0.75f;
            }

            TGSAbilities.ByUnit.TryGetValue(DamageSource, out var SourceHero);
            int Index = (int)Math.Round(EventDamage - 3000.0);
            if (Index > 0)
            {
                float ArmorReduction = GetArmorSpellReduction(DamageTarget.DefenseType);

                float DamageOut = (SpellScalingList[Index].GetDamage(DamageSource) * ArmorReduction) * SpellReductionFinal;
            
                // SPELL CRIT
                if (GetRandomReal(0f, 1f) < SourceHero.SpellCritChance)
                {
                    DamageOut *= SourceHero.SpellCritMult;
                    OutTag = TagType.SpellCrit;
                }
                BlzSetEventDamage(DamageOut);
                MakeTag(DamageOut, DamageTarget, OutTag);
                
                // LIFE STEAL
                if (SourceHero.SpellLifeSteal > 0.0f)
                {
                    float HealAmount = DamageOut * SourceHero.SpellLifeSteal;
                    MakeTag(HealAmount, DamageSource, TagType.Heal);
                    DamageSource.Heal(HealAmount);
                    BlzSetEventDamageType(DAMAGE_TYPE_ENHANCED);
                }
                return true;
            }
            
            float ScaleDamage = (EventDamage + EventDamage * DamageSource.Intelligence * 0.4f / 100.0f) * SpellReductionFinal;
            // SPELL CRIT
            if (GetRandomReal(0f, 1f) < SourceHero.SpellCritChance)
            {
                ScaleDamage *= SourceHero.SpellCritMult;
                OutTag = TagType.SpellCrit;
            }

            BlzSetEventDamage(ScaleDamage);
            MakeTag(ScaleDamage, DamageTarget, OutTag);
                
            // LIFE STEAL
            if (SourceHero.SpellLifeSteal > 0.0f)
            {
                float HealAmount = ScaleDamage * SourceHero.SpellLifeSteal;
                MakeTag(HealAmount, DamageSource, TagType.Heal);
                DamageSource.Heal(HealAmount);
                BlzSetEventDamageType(DAMAGE_TYPE_ENHANCED);
            }
            return true;
        }

        // ATTACK DAMAGE
        if (BlzGetEventDamageType() != DAMAGE_TYPE_SPIRIT_LINK
            && DamageSource.IsUnitType(unittype.Hero)
            && BlzGetEventIsAttack()
            && BlzGetEventDamageType() != DAMAGE_TYPE_ENHANCED)
        {
            TGSAbilities.ByUnit.TryGetValue(DamageSource, out var SourceHero);
            
            // MISS
            if (GetRandomReal(0f,1f) < SourceHero.AttackMissChance)
            {
                BlzSetEventDamage(0.0f);
                BlzSetEventDamageType(DAMAGE_TYPE_UNKNOWN);
                MakeTag(EventDamage, DamageTarget, TagType.Miss);
                return true;
            }

            TGSHero TargetTGSHero;
            if (DamageTarget.IsUnitType(unittype.Hero))
            {
                if (TGSAbilities.ByUnit.TryGetValue(DamageTarget, out TargetTGSHero))
                {
                    // EVASION - Clamped at 50%
                    float EvasionBonus = Math.Min(TargetTGSHero.AttackEvasionChance + TargetTGSHero.ItemMods.EvasionChance, 0.5f);
                    if (GetRandomReal(0f, 1f) < EvasionBonus)
                    {
                        BlzSetEventDamage(0.0f);
                        BlzSetEventDamageType(DAMAGE_TYPE_UNKNOWN);
                        MakeTag(EventDamage, DamageTarget, TagType.Evade);
                        return true;
                    }
                }
            }
            TagType OutTag = TagType.Normal;

            // CRIT
            if (GetRandomReal(0f,1f) < SourceHero.AttackCritChance)
            {
                EventDamage *= SourceHero.AttackCritMult;
                OutTag = TagType.Crit;
            }

            // BASIC ATTACK
            if (bEnemy)
            {
                foreach (IOrbEffect Orb in SourceHero.Orbs)
                {
                    Orb.OnHit(DamageSource, DamageTarget, ref EventDamage);
                }
            }
            BlzSetEventDamage(EventDamage);
            MakeTag(EventDamage, DamageTarget, OutTag);
            
            // CLEAVE
            if (DamageSource.AttackRange1 < 150)
            {
                int HitTargets = 0;
                group Excluded = group.Create();
                Excluded.Add(DamageTarget);
                group Targets = group.Create();
                GroupEnumUnitsInRange(Targets, DamageTarget.X, DamageTarget.Y, 200.0f, Condition(null));
                foreach (unit NearestUnit in Targets.ToList())
                {
                    if (!IsUnitInGroup(NearestUnit, Excluded)
                        && NearestUnit.IsEnemyTo(DamageSource.Owner)
                        && IsValidTarget(NearestUnit))
                    {
                        Excluded.Add(NearestUnit);
                        NearestUnit.Damage(DamageSource, EventDamage / 2, ATTACK_TYPE_HERO, DAMAGE_TYPE_ENHANCED);
                        float CleaveBonus = SourceHero.AttackMultiMult + SourceHero.ItemMods.CleaveBonus;
                        if (OutTag == TagType.Crit)
                        {
                            MakeTag(EventDamage * CleaveBonus, NearestUnit, TagType.CleaveCrit);
                        }
                        else
                        {
                            MakeTag(EventDamage * CleaveBonus, NearestUnit, TagType.Cleave);
                        }
                        HitTargets += 1;
                        if (HitTargets > SourceHero.AttackMultiTargets)
                        {
                            Excluded.Dispose();
                            Targets.Dispose();
                            break;
                        }
                    }
                }
            }
            
            // LIFE STEAL
            if (SourceHero.AttackLifeSteal > 0.0f)
            {
                float HealAmount = EventDamage * SourceHero.AttackLifeSteal;
                DamageSource.Heal(HealAmount);
                MakeTag(HealAmount, DamageSource, TagType.Heal);
            }
        }
        return false;
    }

    private static void ChannelingRemoveDivine()
    {
        if (ConditionFilter.Contains(GetTriggerPlayer()))
        {
            unit TriggerUnit = GetTriggerUnit();
            UnitRemoveBuffBJ(FourCC("BHds"), TriggerUnit);
            TriggerUnit.UserData = 1;
        }
    }

    private static void HeroSpellEffect()
    {
        if (ConditionFilter.Contains(GetTriggerPlayer()))
        {
            unit TriggerUnit = GetTriggerUnit();
            int AbilityId = GetSpellAbilityId();
            // Divine Shield stop channeling
            if (TriggerUnit.Owner != player.Create(PLAYER_NEUTRAL_AGGRESSIVE)
                && TriggerUnit.UserData == 1
                && (AbilityId == ABILITY_AHDS_DIVINE_SHIELD_Q
                    || AbilityId == ABILITY_A00D_DIVINE_SHIELD_W
                    || AbilityId == ABILITY_A00E_DIVINE_SHIELD_E
                    || AbilityId == ABILITY_A00F_DIVINE_SHIELD_R))
            {
                TriggerUnit.IssueOrder(ORDER_STOP);
                TriggerUnit.UserData = 0;
                return;
            }

            // Bladestorm model
            if ((TriggerUnit.UnitType != UNIT_OGRH_FERAL_DRUID_HERO
                 || TriggerUnit.UnitType != UNIT_OPGH_BLADEMASTER_HERO)
                && AbilityId == ABILITY_AOWW_BLADESTORM_T)
            {
                Players[GetTriggerPlayer()].Bladestorm();
                return;
            }

            HeroAbilityCaster(AbilityId);
        }
    }

    private static void HeroAbilityCaster(int AbilityId)
    {
        switch (AbilityId)
        {
            case ABILITY_A07U_RAIN_OF_FIRE_Q_NEW:
            case ABILITY_A07X_RAIN_OF_FIRE_W_NEW:
            case ABILITY_A07Y_RAIN_OF_FIRE_E_NEW:
            case ABILITY_A07Z_RAIN_OF_FIRE_R_NEW:
            {
                new WaveSpell(GetTriggerUnit(), 10, ABILITY_A07V_RAIN_OF_FIRE_DUMMY, ORDER_RAIN_OF_FIRE, GetSpellTargetLoc()).Cast();
                break;
            }
            case ABILITY_A081_BLIZZARD_Q:
            case ABILITY_A082_BLIZZARD_W:
            case ABILITY_A083_BLIZZARD_E:
            case ABILITY_A084_BLIZZARD_R:
            {
                new WaveSpell(GetTriggerUnit(), 10, ABILITY_A080_BLIZZARD_DUMMY, ORDER_BLIZZARD, GetSpellTargetLoc()).Cast();
                break;
            }
            case ABILITY_A00R_ENTANGLING_ROOTS_Q:
            case ABILITY_A00P_ENTANGLING_ROOTS_W:
            case ABILITY_A07M_ENTANGLING_ROOTS_E:
            case ABILITY_A07O_ENTANGLING_ROOTS_R:
            {
                EntanglingRoots Missile = new(GetTriggerUnit(), GetSpellTargetLoc().X, GetSpellTargetLoc().Y, AbilityId);
                MissileSystem.Add(Missile);
                break;
            }
            case ABILITY_A07H_STORM_BOLT_Q:
            case ABILITY_A07G_STORM_BOLT_W:
            case ABILITY_A07I_STORM_BOLT_E:
            case ABILITY_A07J_STORM_BOLT_R:
            {
                StormBolt Missile = new(GetTriggerUnit(), GetSpellTargetLoc().X, GetSpellTargetLoc().Y, AbilityId);
                MissileSystem.Add(Missile);
                break;
            }
            case ABILITY_A07N_HURL_BOULDER_X:
            {
                HurlBoulder Missile = new(GetTriggerUnit(), GetSpellTargetLoc().X, GetSpellTargetLoc().Y, AbilityId);
                MissileSystem.Add(Missile);
                break;
            }
            case ABILITY_A065_CHAIN_DEATH_COIL_Q:
            case ABILITY_A06A_CHAIN_DEATH_COIL_W:
            case ABILITY_A06B_CHAIN_DEATH_COIL_E:
            case ABILITY_A06C_CHAIN_DEATH_COIL_R:
            {
                ChainDeathCoil Missile = new(GetTriggerUnit(), GetSpellTargetUnit(), AbilityId);
                MissileSystem.Add(Missile);
                break;
            }
            case ABILITY_A0LI_CHAIN_LIGHTNING_Q:
            case ABILITY_A0LM_CHAIN_LIGHTNING_W:
            case ABILITY_A0LN_CHAIN_LIGHTNING_E:
            case ABILITY_A0LO_CHAIN_LIGHTNING_R:
            {
                ChainLightning Missile = new(GetTriggerUnit(), GetSpellTargetUnit(), AbilityId);
                MissileSystem.Add(Missile);
                break;
            }
            case ABILITY_A01A_HEALING_WAVE_Q:
            case ABILITY_A01B_HEALING_WAVE_W:
            case ABILITY_A01C_HEALING_WAVE_E:
            case ABILITY_A01P_HEALING_WAVE_R:
            {
                HealingWave Missile = new(GetTriggerUnit(), GetSpellTargetUnit(), AbilityId);
                MissileSystem.Add(Missile);
                break;
            }
            case ABILITY_A066_HOLY_LIGHT_NOVA_Q:
            case ABILITY_A067_HOLY_LIGHT_NOVA_W:
            case ABILITY_A068_HOLY_LIGHT_NOVA_E:
            case ABILITY_A069_HOLY_LIGHT_NOVA_R:
            {
                new HolyLightNova(GetTriggerUnit(), GetSpellTargetUnit(), GetSpellAbilityId()).Cast();
                break;
            }
            case ABILITY_A0FL_DIFFUSION_FLARE_Q:
            case ABILITY_A03H_DIFFUSION_FLARE_W:
            case ABILITY_A03I_DIFFUSION_FLARE_E:
            case ABILITY_A03J_DIFFUSION_FLARE_R:
            {
                DiffusionFlare Missile = new(GetTriggerUnit(), GetSpellTargetLoc().X, GetSpellTargetLoc().Y, AbilityId);
                MissileSystem.Add(Missile);
                break;
            }
            //case ABILITY_A0K4_WAR_CRY_Z:
            //{
            //    GetSpellAbilityUnit().AddItem(ITEM_I08Z_WARCRY_ARMOR_BONUS);
            //    break;
            //}
            case ABILITY_A08M_BLESSED_HAMMER_Z_MURADIN:
            {
                BlessedHammer Missile = new(GetTriggerUnit(), GetTriggerUnit(), AbilityId);
                MissileSystem.Add(Missile);
                break;
            }
            case ABILITY_A0KI_CONFUSE_Z_EVILSYLVANAS_DEFUNCT:
            {
                Confuse();
                break;
            }
            case ABILITY_A0CY_DARK_CONVERSION_Z_TICHONDRIUS:
            {
                DarkConversion();
                break;
            }
            case ABILITY_A0JX_LEAP_ATTACK_Z_GOR:
            {
                LeapAttack();
                break;
            }
            case ABILITY_A005_PASSIVE_BUSTER_X:
            {
                Players.TryGetValue(GetSpellTargetUnit().Owner, out TGSPlayer Data);
                if (Data != null)
                {
                    Data.PassiveBusterActivate();
                }

                break;
            }
            case ABILITY_ANFN_FIRE_NOVA_Q:
            case ABILITY_A062_FIRE_NOVA_W:
            case ABILITY_A063_FIRE_NOVA_E:
            case ABILITY_A064_FIRE_NOVA_R:
            {
                new FireNova(GetTriggerUnit(), GetSpellTargetUnit(), GetSpellAbilityId()).Cast();
                break;
            }
            case ABILITY_ANDS_DISARM_X:
            {
                new Disarm(GetTriggerUnit(), GetSpellTargetUnit(), GetSpellAbilityId()).Cast();
                break;
            }
            case ABILITY_A0M5_BERSERK_X:
            {
                new Berserk(GetTriggerUnit()).Cast();
                break;
            }
            case ABILITY_A0AO_POCKET_FACTORY_Q:
            case ABILITY_A0FM_POCKET_FACTORY_W:
            case ABILITY_A0FN_POCKET_FACTORY_E:
            case ABILITY_A0FO_POCKET_FACTORY_R:
            {
                PocketFactory Missile = new(GetTriggerUnit(), GetSpellTargetLoc().X, GetSpellTargetLoc().Y, AbilityId);
                MissileSystem.Add(Missile);
                break;
            }
        }
    }

    private static void Confuse()
    {
        unit Target = GetSpellTargetUnit();
        effect.Create(@"Abilities\Spells\Human\MarkOfChaos\MarkOfChaosTarget.mdl", Target, "overhead").Dispose();
        Target.SetOwner(player.Create(PLAYER_NEUTRAL_AGGRESSIVE));
        Target.IssueOrder(ORDER_ATTACK, Target.X, Target.Y);
    }

    private static void DarkConversion()
    {
        unit Target = GetSpellTargetUnit();
        location UnitLocation = GetUnitLoc(Target);
        effect.Create(@"Abilities\Spells\Human\HolyBolt\HolyBoltSpecialArt.mdl", Target.X, Target.Y).Dispose();
        RemoveUnit(Target);
        unit Zombie = unit.Create(GetSpellAbilityUnit().Owner, UNIT_N000_ZOMBIE_DARK_CONVERSION, UnitLocation.X, UnitLocation.Y);
        Summons.ScaleSummon(GetSpellAbilityUnit(), Zombie);
        UnitLocation.Dispose();
        Target.IssueOrder(ORDER_ATTACK, Zombie.X, Zombie.Y);
    }

    private static void LeapAttack()
    {
        LeapAttackUnit = GetSpellAbilityUnit();
        PlaySoundOnUnitBJ(LeapAttackSound[GetRandomInt(0, 2)], 100, LeapAttackUnit);
        LeapAttackUnit.AddAbility(FourCC("Arav"));
        LeapAttackUnit.RemoveAbility(FourCC("Arav"));
        LeapAttackStart = GetUnitLoc(LeapAttackUnit);
        LeapAttackTarget = GetSpellTargetLoc();
        LeapAttackSpeed = FastUtil.DistanceBetweenPoints(LeapAttackStart.X, LeapAttackStart.Y, LeapAttackTarget.X, LeapAttackTarget.Y) / 20.0f;
        LeapAttackUnit.SetAnimation("slam");
        LeapAttackUnit.SetTimeScale(0.8826f);
        LeapAttackUnit.SetPathing(false);
        LeapAttackUnit.AddAbility(FourCC("Aeth"));
        LeapAttackMove = timer.Create();
        LeapAttackMove.Start(0.05f, true, LeapAttackMovement);
    }

    private static void LeapAttackMovement()
    {
        location LeapUnit = GetUnitLoc(LeapAttackUnit);
        if (FastUtil.DistanceBetweenPoints(LeapUnit.X, LeapUnit.Y, LeapAttackTarget.X, LeapAttackTarget.Y) <= LeapAttackSpeed)
        {
            unit Dummy = DummySystem.GetDummy(LeapAttackTarget.X, LeapAttackTarget.Y, LeapAttackUnit.Owner);
            Dummy.AddAbility(ABILITY_A0JV_CLAP_FOR_LEAP_ATTACK);
            Dummy.IssueOrder("thunderclap");
            DummySystem.RecycleDummy(Dummy, 0.25f);
            effect LeapEffect = effect.Create(@"Abilities\Spells\Orc\WarStomp\WarStompCaster.mdl", LeapAttackTarget.X, LeapAttackTarget.Y);
            LeapEffect.Dispose();
            LeapAttackUnit.SetPathing(true);
            LeapAttackUnit.RemoveAbility(FourCC("Aeth"));
            EnumDestructablesInCircleBJ(250.00f, LeapAttackTarget, SmokeTrees);
            LeapAttackUnit.SetPosition(LeapAttackTarget.X, LeapAttackTarget.Y);
            LeapUnit.Dispose();
            LeapAttackStart.Dispose();
            LeapAttackTarget.Dispose();
            LeapAttackUnit.SetTimeScale(1.0f);
            LeapAttackUnit.SetFlyHeight(0.0f, 100000.0f);
            ResetUnitAnimation(LeapAttackUnit);
            LeapAttackMove.Dispose();
        }
        else
        {
            location LerpDestination = PolarProjectionBJ(LeapUnit, LeapAttackSpeed, AngleBetweenPoints(LeapUnit, LeapAttackTarget));
            LeapAttackUnit.SetPosition(LerpDestination.X, LerpDestination.Y);
            LeapUnit.Dispose();
            LerpDestination.Dispose();
            LeapUnit = GetUnitLoc(LeapAttackUnit);
            Ratio = DistanceBetweenPoints(LeapUnit, LeapAttackTarget) / DistanceBetweenPoints(LeapAttackStart, LeapAttackTarget);
            LeapAttackUnit.SetFlyHeight(600.00f * (1 - Pow(Ratio * 2.00f - 1, 2.00f)), 100000.00f);
            LeapUnit.Dispose();
        }
    }

    public static void SmokeTrees()
    {
        KillDestructable(GetEnumDestructable());
    }

    public static bool IsValidTarget(unit Target)
    {
        //Console.WriteLine($"|cff000000Target:|r |cffffff00Immune:|r {IsUnitType(Target, UNIT_TYPE_MAGIC_IMMUNE)}, |cffffff00Building:|r {IsUnitType(Target, UNIT_TYPE_STRUCTURE)}, |cffffff00Mechanical:|r {IsUnitType(Target, UNIT_TYPE_MECHANICAL)}, |cffffff00Alive:|r {IsUnitAliveBJ(Target)}");
        return !Target.IsUnitType(unittype.MagicImmune)
               && !Target.IsUnitType(unittype.Structure)
               && !Target.IsUnitType(unittype.Mechanical)
               && Target.Alive;
    }
}

public class AbilityMissile : BasicMissile
{
    public int AbilityId;
    public int AbilityLevel;
    public float Damage;
    public int DummyAbilityId;
    public bool Expired = false;
    public float Range;

    public AbilityMissile(unit caster, float targetX, float targetY, int abilityId) : base(caster, targetX, targetY)
    {
        AbilityId = abilityId;
        AbilityLevel = Caster.GetAbilityLevel(AbilityId);
        Speed = 800.0f;
        IsArcing = false;
        CollisionRadius = 50.0f;
        EffectScale = 1.0f;
        CasterLaunchZ = 50.0f;
    }

    public override void OnImpact()
    {
        if (!Expired)
        {
            Active = true;
        }
    }

    public override void OnPeriodic()
    {
        Expired = true;
        Active = false;
    }

    public override void OnCollision(unit unit)
    {
        if (unit.IsEnemyTo(Caster.Owner))
        {
            unit Dummy = DummySystem.GetDummy(unit.X, unit.Y, Caster.Owner);
            Dummy.AddAbility(DummyAbilityId);
            Dummy.SetAbilityLevel(DummyAbilityId, Caster.GetAbilityLevel(AbilityId));
            Dummy.IssueOrder(ORDER_SOUL_BURN, unit);
            DummySystem.RecycleDummy(Dummy, 0.25f);
        }
    }
}
