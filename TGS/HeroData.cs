using System;
using System.Collections.Generic;
using TGS.Spells;
using WCSharp.Api;
using WCSharp.Events;
using static Constants;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public class HeroData
{
    public static List<int> AbilityMax = new();
    public static Dictionary<int, int> HeroToBaseAbility = new();
    public static readonly Dictionary<unit, Hero> ByUnit = new();
    public static readonly Dictionary<player, Hero> ByPlayer = new();
    public static readonly Dictionary<int, NormalAbility> NormalByItemId = new();
    public static readonly Dictionary<int, SpecialAbility> SpecialByItemId = new();
    public static readonly Dictionary<int, UltimateAbility> UltimateByItemId = new();

    public static void Init()
    {
        AbilityMax.Add(1);
        AbilityMax.Add(1);
        AbilityMax.Add(1);
        AbilityMax.Add(2);
        AbilityMax.Add(3);
        AbilityMax.Add(3);
        AbilityMax.Add(4);
        AbilityMax.Add(5);
        AbilityMax.Add(5);
        AbilityMax.Add(6);
        AbilityMax.Add(7);
        AbilityMax.Add(7);
        AbilityMax.Add(8);
        AbilityMax.Add(9);
        AbilityMax.Add(9);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        AbilityMax.Add(10);
        Hero.Initialize();

        RegisterAbilities();
        InitAbilLookups();
    }

    private static void InitAbilLookups()
    {
        HeroToBaseAbility.Add(UNIT_OTHR_GUNNER_HERO, ABILITY_A0JA_MAGIC_LAMP_Z);
        HeroToBaseAbility.Add(UNIT_OPGH_BLADEMASTER_HERO, ABILITY_A0NU_WEASEL_S_SLASH_Z);
        HeroToBaseAbility.Add(UNIT_OGRH_FERAL_DRUID_HERO, ABILITY_A0JX_LEAP_ATTACK_Z);
        HeroToBaseAbility.Add(UNIT_NAKA_SWOLBOLD_HERO, ABILITY_A0AP_SWOL_CANDLE_SWOLBOLD_Z);
        HeroToBaseAbility.Add(UNIT_OCB2_TAUREN_CHIEFTAIN_MAIN_TANK_HERO, ABILITY_A0ES_EARTHSHATTER_Z_CAIRNE);
        HeroToBaseAbility.Add(UNIT_OGLD_WARLOCK_HERO, ABILITY_A05A_LESSER_INFERNO_Z);
        HeroToBaseAbility.Add(UNIT_ORKN_SHADOW_HUNTER_HERO, ABILITY_A0O4_SOUL_STEAL_DANCE_Z);
        HeroToBaseAbility.Add(UNIT_UTIC_DREADLORD_HERO, ABILITY_A0CY_DARK_CONVERSION_Z);
        HeroToBaseAbility.Add(UNIT_UWAR_EREDAR_WARLOCK_HERO, ABILITY_A0KU_DEVOUR_MAGIC_Z);
        HeroToBaseAbility.Add(UNIT_UEAR_DEATH_KNIGHT_HERO, ABILITY_A0KC_UNHOLY_FRENZY_ONLY_FOR_DK_Z);
        HeroToBaseAbility.Add(UNIT_UANB_HORSEMAN_OF_FAMINE_HERO, ABILITY_A0AS_AURA_OF_THE_SCOURGE_Z_LADY_BLAUMEUX);
        HeroToBaseAbility.Add(UNIT_UKTL_LICH_HERO, ABILITY_A0KO_FREEZING_WAVE_Z_KEL_THUZAD);
        HeroToBaseAbility.Add(UNIT_UVNG_DOOM_LORD_HERO, ABILITY_A0J9_CORPSE_EXPLOSION_Z);
        HeroToBaseAbility.Add(UNIT_USYL_DARK_RANGER_HERO, ABILITY_A0EW_EVENING_THE_SCALES_Z_SYLVANAS);
        HeroToBaseAbility.Add(UNIT_UMAL_EREDAR_HIERARCH_HERO, ABILITY_A0LK_ORB_OF_ANNIHILATION_Z);
        HeroToBaseAbility.Add(UNIT_NTIN_MURLOC_WARLOCK_HERO, ABILITY_A0KG_SUICIDAL_EXPLOSION_Z);
        HeroToBaseAbility.Add(UNIT_OREX_BEASTMASTER_HERO, ABILITY_A0KB_ROAR_Z);
        HeroToBaseAbility.Add(UNIT_U001_DEATHSPEAKER_HERO, ABILITY_A0ME_CRITICAL_BLOW_Z);
        HeroToBaseAbility.Add(UNIT_U000_ABOMINATION_HERO, ABILITY_A0LD_DEVOUR_Z);
        HeroToBaseAbility.Add(UNIT_ODRT_FEL_WARDEN_HERO, ABILITY_A08K_NOVA_Z);

        HeroToBaseAbility.Add(UNIT_HART_PALADIN_HERO, ABILITY_A0M4_ZEAL_Z);
        HeroToBaseAbility.Add(UNIT_HANT_ARCHMAGE_HERO, ABILITY_A0L1_SUPER_SLOW_Z);
        HeroToBaseAbility.Add(UNIT_HJAI_ARCHMAGE_HERO, ABILITY_A0BV_ARCANE_MISSILE_Z_JAINA);
        HeroToBaseAbility.Add(UNIT_HKAL_BLOOD_MAGE_HERO, ABILITY_A0KH_PSYCHO_SHIELD_Z);
        HeroToBaseAbility.Add(UNIT_HMBR_MOUNTAIN_KING_MAIN_TANK_HERO, ABILITY_A08M_BLESSED_HAMMER_Z);
        HeroToBaseAbility.Add(UNIT_HVWD_RANGER_HERO, ABILITY_A0IA_QUEL_THALAN_ARROWS_Z_SYLVANAS_WINDRUNNER_LIVING);
        HeroToBaseAbility.Add(UNIT_EMFR_KEEPER_OF_THE_GROVE_HERO, ABILITY_A0KT_REJUVENATION_ONLY_FOR_KOTG_Z);
        HeroToBaseAbility.Add(UNIT_EMNS_KEEPER_OF_THE_GROVE_HERO, ABILITY_A0JJ_MEDICINE_OF_FURION_Z);
        HeroToBaseAbility.Add(UNIT_EEVI_DEMON_HUNTER_HERO, ABILITY_A0JO_TREANT_OF_THE_DEAD_Z);
        HeroToBaseAbility.Add(UNIT_EILL_NIGHT_ELF_FERAL_DRUID_HERO, ABILITY_A0KQ_STEAL_MANA_Z);
        HeroToBaseAbility.Add(UNIT_EWRD_WARDEN_HERO, ABILITY_A0EP_MOONFIRE_Z_MAIEV);
        HeroToBaseAbility.Add(UNIT_ETYR_PRIESTESS_OF_THE_MOON_HERO, ABILITY_A0EQ_OWL_COMPANION_Z_TYRANDE);
        HeroToBaseAbility.Add(UNIT_HVSH_SEA_WITCH_HERO, ABILITY_A0L8_PARASITE_Z);
        HeroToBaseAbility.Add(UNIT_HDGO_SENTINEL_HERO, ABILITY_A0LH_CRUSHING_ARROW_Z);
        HeroToBaseAbility.Add(UNIT_NPBM_BREWMASTER_HERO, ABILITY_A0NY_GNAWING_PANDAS_Z);
        HeroToBaseAbility.Add(UNIT_HUTH_PALADIN_HERO, ABILITY_A0L2_MIND_UNIFICATION_Z);
        HeroToBaseAbility.Add(UNIT_HLGR_DARK_KNIGHT_HERO, ABILITY_A0JB_DARK_KNIGHT_AURA_Z);
        HeroToBaseAbility.Add(UNIT_HAPM_THANE_PROUDMOORE_HERO, ABILITY_A0JQ_DEADLY_STRIKE_PROUDMOORE_Z);
    }

    private static void RegisterAbilities()
    {
        new NormalAbility("Devotion Aura", ITEM_I002_DEVOTION_AURA_AURA, ABILITY_AHAD_DEVOTION_AURA_Q, ABILITY_A011_DEVOTION_AURA_W, ABILITY_A012_DEVOTION_AURA_E, ABILITY_A013_DEVOTION_AURA_R);
        new NormalAbility("Bash", ITEM_I017_BASH_PASSIVE, ABILITY_AHBH_BASH_Q, ABILITY_A015_BASH_W, ABILITY_A014_BASH_E, ABILITY_A016_BASH_R);
        NormalAbility Banish = new("Banish", ITEM_I009_BANISH_UTILITY, ABILITY_AHBN_BANISH_Q, ABILITY_A017_BANISH_W, ABILITY_A018_BANISH_E, ABILITY_A019_BANISH_R);
        Banish.MaxLevel = 3; // Banish only has 3 levels
        new NormalAbility("Blizzard", ITEM_I004_BLIZZARD_ACTIVE, ABILITY_A081_BLIZZARD_Q, ABILITY_A082_BLIZZARD_W, ABILITY_A083_BLIZZARD_E, ABILITY_A084_BLIZZARD_R);
        new NormalAbility("Divine Shield", ITEM_I001_DIVINE_SHIELD_UTILITY, ABILITY_AHDS_DIVINE_SHIELD_Q, ABILITY_A00D_DIVINE_SHIELD_W, ABILITY_A00E_DIVINE_SHIELD_E, ABILITY_A00F_DIVINE_SHIELD_R);
        new NormalAbility("Flame Strike", ITEM_I008_FLAME_STRIKE_ACTIVE, ABILITY_AHFS_FLAME_STRIKE_Q, ABILITY_A00G_FLAME_STRIKE_W, ABILITY_A00H_FLAME_STRIKE_E, ABILITY_A00I_FLAME_STRIKE_R);
        new NormalAbility("Holy Light Nova", ITEM_I02C_HOLY_LIGHT_NOVA_UTILITY, ABILITY_A066_HOLY_LIGHT_NOVA_Q, ABILITY_A067_HOLY_LIGHT_NOVA_W, ABILITY_A068_HOLY_LIGHT_NOVA_E,
            ABILITY_A069_HOLY_LIGHT_NOVA_R);
        new NormalAbility("Siphon Mana", ITEM_I00A_SIPHON_MANA_ACTIVE, ABILITY_AHDR_SIPHON_MANA_Q, ABILITY_A00M_SIPHON_MANA_W, ABILITY_A00N_SIPHON_MANA_E, ABILITY_A00O_SIPHON_MANA_R);
        new NormalAbility("Storm Bolt", ITEM_I016_STORM_BOLT_NUKE, ABILITY_A07H_STORM_BOLT_Q, ABILITY_A07G_STORM_BOLT_W, ABILITY_A07I_STORM_BOLT_E, ABILITY_A07J_STORM_BOLT_R);
        new NormalAbility("Summon Water Elemental", ITEM_I005_SUMMON_WALTER_LEMONTAL_SUMMON, ABILITY_AHWE_SUMMON_WALTER_LEMONTAL_Q, ABILITY_A00S_SUMMON_WALTER_LEMONTAL_W,
            ABILITY_A00T_SUMMON_WALTER_LEMONTAL_E, ABILITY_A00U_SUMMON_WALTER_LEMONTAL_R);
        new NormalAbility("Thunder Clap", ITEM_I003_THUNDER_CLAP_ACTIVE, ABILITY_AHTC_THUNDER_CLAP_Q, ABILITY_A00V_THUNDER_CLAP_W, ABILITY_A00W_THUNDER_CLAP_E, ABILITY_A00X_THUNDER_CLAP_R);
        new NormalAbility("Brilliance Aura", ITEM_I006_BRILLIANCE_AURA_AURA, ABILITY_AHAB_BRILLIANCE_AURA_Q, ABILITY_A00Y_BRILLIANCE_AURA_W, ABILITY_A00Z_BRILLIANCE_AURA_E,
            ABILITY_A010_BRILLIANCE_AURA_R);
        new NormalAbility("Chain Lightning", ITEM_I010_CHAIN_LIGHTNING_NUKE, ABILITY_A0LI_CHAIN_LIGHTNING_Q, ABILITY_A0LM_CHAIN_LIGHTNING_W, ABILITY_A0LN_CHAIN_LIGHTNING_E,
            ABILITY_A0LO_CHAIN_LIGHTNING_R);
        new NormalAbility("Critical Strike", ITEM_I00U_CRITICAL_STRIKE_PASSIVE, ABILITY_AOCR_CRITICAL_STRIKE_Q, ABILITY_A01D_CRITICAL_STRIKE_W, ABILITY_A01E_CRITICAL_STRIKE_E,
            ABILITY_A01F_CRITICAL_STRIKE_R);
        new NormalAbility("Endurance Aura", ITEM_I00Y_ENDURANCE_AURA_AURA, ABILITY_AOAE_ENDURANCE_AURA_Q, ABILITY_A01G_ENDURANCE_AURA_W, ABILITY_A01H_ENDURANCE_AURA_E, ABILITY_A01I_ENDURANCE_AURA_R);
        NormalAbility Farsight = new NormalAbility("Far Sight", ITEM_I011_FAR_SIGHT_UTILITY, ABILITY_AOFS_FAR_SIGHT_Q, ABILITY_A01J_FAR_SIGHT_W, ABILITY_A01K_FAR_SIGHT_E, ABILITY_A01L_FAR_SIGHT_R);
        Farsight.MaxLevel = 3; // Far Sight only has 3 levels
        new NormalAbility("Feral Spirit", ITEM_I012_FERAL_SPIRIT_SUMMON, ABILITY_AOSF_FERAL_SPIRIT_Q, ABILITY_A01M_FERAL_SPIRIT_W, ABILITY_A01N_FERAL_SPIRIT_E, ABILITY_A01O_FERAL_SPIRIT_R);
        new NormalAbility("Healing Wave", ITEM_I014_HEALING_WAVE_UTILITY, ABILITY_A01A_HEALING_WAVE_Q, ABILITY_A01B_HEALING_WAVE_W, ABILITY_A01C_HEALING_WAVE_E, ABILITY_A01P_HEALING_WAVE_R);
        new NormalAbility("Hex", ITEM_I015_HEX_UTILITY, ABILITY_AOHX_HEX_Q, ABILITY_A01S_HEX_W, ABILITY_A01T_HEX_E, ABILITY_A01U_HEX_R);
        new NormalAbility("Mirror Image", ITEM_I00T_MIRROR_IMAGE_ACTIVE, ABILITY_AOMI_MIRROR_IMAGE_Q, ABILITY_A01V_MIRROR_IMAGE_W, ABILITY_A01W_MIRROR_IMAGE_E, ABILITY_A01X_MIRROR_IMAGE_R);
        new NormalAbility("Serpent Ward", ITEM_I02A_SERPENT_WARD_SUMMON, ABILITY_AOSW_SERPENT_WARD_Q, ABILITY_A01Y_SERPENT_WARD_W, ABILITY_A01Z_SERPENT_WARD_E, ABILITY_A020_SERPENT_WARD_R);
        new NormalAbility("Shockwave", ITEM_I00W_SHOCKWAVE_ACTIVE, ABILITY_AOSH_SHOCKWAVE_Q, ABILITY_A021_SHOCKWAVE_W, ABILITY_A022_SHOCKWAVE_E, ABILITY_A023_SHOCKWAVE_R);
        new NormalAbility("War Stomp", ITEM_I00X_WAR_STOMP_ACTIVE, ABILITY_AOWS_WAR_STOMP_Q, ABILITY_A024_WAR_STOMP_W, ABILITY_A025_WAR_STOMP_E, ABILITY_A026_WAR_STOMP_R);
        new NormalAbility("Wind Walk", ITEM_I00S_WIND_WALK_UTILITY, ABILITY_AOWK_WIND_WALK_Q, ABILITY_A027_WIND_WALK_W, ABILITY_A028_WIND_WALK_E, ABILITY_A029_WIND_WALK_R);
        new NormalAbility("Carrion Beetles", ITEM_I01M_CARRION_BEETLES_SUMMON, ABILITY_AUCB_CARRION_BEETLES_Q, ABILITY_A02A_CARRION_BEETLES_W, ABILITY_A02B_CARRION_BEETLES_E,
            ABILITY_A02C_CARRION_BEETLES_R);
        new NormalAbility("Carrion Swarm", ITEM_I01C_CARRION_SWARM_ACTIVE, ABILITY_AUCS_CARRION_SWARM_Q, ABILITY_A02D_CARRION_SWARM_W, ABILITY_A02E_CARRION_SWARM_E, ABILITY_A02F_CARRION_SWARM_R);
        new NormalAbility("Soul Harvest", ITEM_I01I_SOUL_HARVEST_UTILITY, ABILITY_AUDR_SOUL_HARVEST_Q_DARK_RITUAL, ABILITY_A02G_DARK_RITUAL_W, ABILITY_A02H_DARK_RITUAL_E, ABILITY_A02I_DARK_RITUAL_R);
        new NormalAbility("Chain Death Coil", ITEM_I02D_CHAIN_DEATH_COIL_UTILITY, ABILITY_A065_CHAIN_DEATH_COIL_Q, ABILITY_A06A_CHAIN_DEATH_COIL_W, ABILITY_A06B_CHAIN_DEATH_COIL_E,
            ABILITY_A06C_CHAIN_DEATH_COIL_R);
        new NormalAbility("Death Pact", ITEM_I019_DEATH_PACT_UTILITY, ABILITY_AUDP_DEATH_PACT_Q, ABILITY_A02M_DEATH_PACT_W, ABILITY_A02N_DEATH_PACT_E, ABILITY_A02O_DEATH_PACT_R);
        new NormalAbility("Feedback", ITEM_I07Z_FEEDBACK_PASSIVE, ABILITY_A0LW_FEEDBACK_Q, ABILITY_A06G_FEEDBACK_W, ABILITY_A06H_FEEDBACK_E, ABILITY_A06I_FEEDBACK_R, OrbType.Feedback);
        new NormalAbility("Frost Armor", ITEM_I01H_FROST_ARMOR_AUTOCAST, ABILITY_AUFU_FROST_ARMOR_AUTOCAST_Q, ABILITY_A02P_FROST_ARMOR_AUTOCAST_W, ABILITY_A02Q_FROST_ARMOR_AUTOCAST_E,
            ABILITY_A02R_FROST_ARMOR_AUTOCAST_R);
        new NormalAbility("Frost Nova", ITEM_I01G_FROST_NOVA_NUKE, ABILITY_AUFN_FROST_NOVA_Q, ABILITY_A02S_FROST_NOVA_W, ABILITY_A02T_FROST_NOVA_E, ABILITY_A02U_FROST_NOVA_R);
        new NormalAbility("Impale", ITEM_I01K_IMPALE_ACTIVE, ABILITY_AUIM_IMPALE_Q, ABILITY_A02V_IMPALE_W, ABILITY_A02W_IMPALE_E, ABILITY_A02X_IMPALE_R);
        new NormalAbility("Sleep", ITEM_I01D_SLEEP_UTILITY, ABILITY_AUSL_SLEEP_Q, ABILITY_A02Y_SLEEP_W, ABILITY_A02Z_SLEEP_E, ABILITY_A030_SLEEP_R);
        new NormalAbility("Spiked Carapace", ITEM_I01L_SPIKED_CARAPACE_PASSIVE, ABILITY_AUTS_SPIKED_CARAPACE_Q, ABILITY_A031_SPIKED_CARAPACE_W, ABILITY_A032_SPIKED_CARAPACE_E,
            ABILITY_A033_SPIKED_CARAPACE_R);
        new NormalAbility("Vampiric Aura", ITEM_I01E_VAMPIRIC_AURA_AURA, ABILITY_AUAV_VAMPIRIC_AURA_Q, ABILITY_A034_VAMPIRIC_AURA_W, ABILITY_A035_VAMPIRIC_AURA_E, ABILITY_A036_VAMPIRIC_AURA_R);
        new NormalAbility("Unholy Aura", ITEM_I01A_DEPRESSION_AURA_AURA, ABILITY_A037_UNHOLY_AURA_Q, ABILITY_A038_UNHOLY_AURA_W, ABILITY_A039_UNHOLY_AURA_E, ABILITY_A03A_UNHOLY_AURA_R);
        new NormalAbility("Entangling Roots", ITEM_I00K_ENTANGLING_ROOTS_NUKE, ABILITY_A00R_ENTANGLING_ROOTS_Q, ABILITY_A00P_ENTANGLING_ROOTS_W, ABILITY_A07M_ENTANGLING_ROOTS_E,
            ABILITY_A07O_ENTANGLING_ROOTS_R);
        new NormalAbility("Evasion", ITEM_I00E_EVASION_PASSIVE, ABILITY_AEEV_EVASION_Q, ABILITY_A03E_EVASION_W, ABILITY_A03F_EVASION_E, ABILITY_A03G_EVASION_R);
        new NormalAbility("Diffusion Flare", ITEM_I091_DIFFUSION_FLARE_UTILITY, ABILITY_A0FL_DIFFUSION_FLARE_Q, ABILITY_A03H_DIFFUSION_FLARE_W, ABILITY_A03I_DIFFUSION_FLARE_E,
            ABILITY_A03J_DIFFUSION_FLARE_R);
        new NormalAbility("Fan of Knives", ITEM_I00O_FAN_OF_KNIVES_NUKE, ABILITY_AEFK_FAN_OF_KNIVES_Q, ABILITY_A03K_FAN_OF_KNIVES_W, ABILITY_A03L_FAN_OF_KNIVES_E, ABILITY_A03M_FAN_OF_KNIVES_R);
        new NormalAbility("Force of Nature", ITEM_I00L_FORCE_OF_NATURE_SUMMON, ABILITY_AEFN_FORCE_OF_NATURE_Q, ABILITY_A03N_FORCE_OF_NATURE_W, ABILITY_A03O_FORCE_OF_NATURE_E,
            ABILITY_A03P_FORCE_OF_NATURE_R);
        new NormalAbility("Immolation", ITEM_I00C_IMMOLATION_ACTIVE, ABILITY_AEIM_IMMOLATION_Q, ABILITY_A03Q_IMMOLATION_W, ABILITY_A03R_IMMOLATION_E, ABILITY_A03S_IMMOLATION_R);
        new NormalAbility("Scout", ITEM_I00G_SCOUT_SUMMON, ABILITY_AEST_SCOUT_Q, ABILITY_A03T_SCOUT_W, ABILITY_A03U_SCOUT_E, ABILITY_A03V_SCOUT_R);
        new NormalAbility("Searing Arrows", ITEM_I00H_SEARING_ARROWS_AUTOCAST, ABILITY_AHFA_SEARING_ARROWS_Q, ABILITY_A03W_SEARING_ARROWS_W, ABILITY_A03X_SEARING_ARROWS_E,
            ABILITY_A03Y_SEARING_ARROWS_R);
        new NormalAbility("Shadow Strike", ITEM_I00Q_SHADOW_STRIKE_NUKE, ABILITY_AESH_SHADOW_STRIKE_Q, ABILITY_A03Z_SHADOW_STRIKE_W, ABILITY_A040_SHADOW_STRIKE_E, ABILITY_A041_SHADOW_STRIKE_R);
        new NormalAbility("Thorns Aura", ITEM_I00M_THORNS_AURA_AURA, ABILITY_AEAH_THORNS_AURA_Q, ABILITY_A042_THORNS_AURA_W, ABILITY_A043_THORNS_AURA_E, ABILITY_A044_THORNS_AURA_R);
        new NormalAbility("Trueshot Aura", ITEM_I00I_TRUESHOT_AURA_AURA, ABILITY_AEAR_TRUESHOT_AURA_Q, ABILITY_A045_TRUESHOT_AURA_W, ABILITY_A046_TRUESHOT_AURA_E, ABILITY_A047_TRUESHOT_AURA_R);
        new NormalAbility("Acid Bomb", ITEM_I08U_ACID_BOMB_ACTIVE, ABILITY_ANAB_ACID_BOMB_Q, ABILITY_A048_ACID_BOMB_W, ABILITY_A049_ACID_BOMB_E, ABILITY_A04A_ACID_BOMB_R);
        new NormalAbility("Black Arrow", ITEM_I01T_BLACK_ARROW_AUTOCAST, ABILITY_ANBA_BLACK_ARROW_Q, ABILITY_A04B_BLACK_ARROW_W, ABILITY_A04C_BLACK_ARROW_E, ABILITY_A04D_BLACK_ARROW_R, OrbType.Spawner);
        new NormalAbility("Breath of Fire", ITEM_I01W_BREATH_OF_FIRE_ACTIVE, ABILITY_ANBF_BREATH_OF_FIRE_Q, ABILITY_A04E_BREATH_OF_FIRE_W, ABILITY_A04F_BREATH_OF_FIRE_E,
            ABILITY_A04G_BREATH_OF_FIRE_R);
        new NormalAbility("Cleaving Attack", ITEM_I027_CLEAVING_ATTACK_PASSIVE, ABILITY_ANCA_CLEAVING_ATTACK_Q, ABILITY_A04H_CLEAVING_ATTACK_W, ABILITY_A04I_CLEAVING_ATTACK_E,
            ABILITY_A04J_CLEAVING_ATTACK_R);
        new NormalAbility("Cluster Rockets", ITEM_I08M_CLUSTER_ROCKETS_NUKE, ABILITY_ANCS_CLUSTER_ROCKETS_Q, ABILITY_A04K_CLUSTER_ROCKETS_W, ABILITY_A04L_CLUSTER_ROCKETS_E,
            ABILITY_A04M_CLUSTER_ROCKETS_R);
        new NormalAbility("Cold Arrows", ITEM_I01P_COLD_ARROWS_AUTOCAST, ABILITY_AHCA_COLD_ARROWS_Q, ABILITY_A04N_COLD_ARROWS_W, ABILITY_A04O_COLD_ARROWS_E, ABILITY_A04P_COLD_ARROWS_R);
        new NormalAbility("Drunken Brawler", ITEM_I01Y_DRUNKEN_BRAWLER_PASSIVE, ABILITY_ANDB_DRUNKEN_BRAWLER_Q, ABILITY_A04Q_DRUNKEN_BRAWLER_W, ABILITY_A04R_DRUNKEN_BRAWLER_E,
            ABILITY_A04S_DRUNKEN_BRAWLER_R);
        new NormalAbility("Drunken Haze", ITEM_I01X_DRUNKEN_HAZE_ACTIVE, ABILITY_ANDH_DRUNKEN_HAZE_Q, ABILITY_A04T_DRUNKEN_HAZE_W, ABILITY_A04U_DRUNKEN_HAZE_E, ABILITY_A04V_DRUNKEN_HAZE_R);
        new NormalAbility("Engineering Upgrade", ITEM_I08N_ENGINEERING_UPGRADE_PASSIVE, ABILITY_ANEG_ENGINEERING_UPGRADE_Q, ABILITY_A04W_ENGINEERING_UPGRADE_W, ABILITY_A04X_ENGINEERING_UPGRADE_E,
            ABILITY_A04Y_ENGINEERING_UPGRADE_R);
        new NormalAbility("Forked Lightning", ITEM_I01O_FORKED_LIGHTNING_NUKE, ABILITY_ANFL_FORKED_LIGHTNING_Q, ABILITY_A04Z_FORKED_LIGHTNING_W, ABILITY_A050_FORKED_LIGHTNING_E,
            ABILITY_A051_FORKED_LIGHTNING_R);
        new NormalAbility("Healing Spray", ITEM_I08W_HEALING_SPRAY_UTILITY, ABILITY_ANHS_HEALING_SPRAY_Q, ABILITY_A052_HEALING_SPRAY_W, ABILITY_A053_HEALING_SPRAY_E, ABILITY_A054_HEALING_SPRAY_R);
        new NormalAbility("Howl of Terror", ITEM_I026_HOWL_OF_TERROR_UTILITY, ABILITY_ANHT_HOWL_OF_TERROR_Q, ABILITY_A055_HOWL_OF_TERROR_W, ABILITY_A056_HOWL_OF_TERROR_E,
            ABILITY_A057_HOWL_OF_TERROR_R);
        new NormalAbility("Essence Drain", ITEM_I01U_ESSENCE_DRAIN_ACTIVE, ABILITY_ANDR_ESSENCE_DRAIN_Q, ABILITY_A05B_ESSENCE_DRAIN_W, ABILITY_A05C_ESSENCE_DRAIN_E, ABILITY_A05D_ESSENCE_DRAIN_R);
        new NormalAbility("Mana Shield", ITEM_I020_MANA_SHIELD_ACTIVE, ABILITY_ANMS_MANA_SHIELD_Q, ABILITY_A05E_MANA_SHIELD_W, ABILITY_A05F_MANA_SHIELD_E, ABILITY_A05G_MANA_SHIELD_R);
        new NormalAbility("Pocket Factory", ITEM_I08O_POCKET_FACTORY_SUMMON, ABILITY_A0AO_POCKET_FACTORY_Q, ABILITY_A0FM_POCKET_FACTORY_W, ABILITY_A0FN_POCKET_FACTORY_E,
            ABILITY_A0FO_POCKET_FACTORY_R);
        new NormalAbility("Rain of Fire", ITEM_I024_RAIN_OF_FIRE_ACTIVE, ABILITY_A07U_RAIN_OF_FIRE_Q_NEW, ABILITY_A07X_RAIN_OF_FIRE_W_NEW, ABILITY_A07Y_RAIN_OF_FIRE_E_NEW,
            ABILITY_A07Z_RAIN_OF_FIRE_R_NEW);
        new NormalAbility("Silence", ITEM_I01S_SILENCE_UTILITY, ABILITY_ANSI_SILENCE_Q, ABILITY_A05N_SILENCE_W, ABILITY_A05O_SILENCE_E, ABILITY_A05P_SILENCE_R);
        new NormalAbility("Summon Bear", ITEM_I025_SUMMON_BEAR_SUMMON, ABILITY_ANSG_SUMMON_BEAR_Q, ABILITY_A05Q_SUMMON_BEAR_W, ABILITY_A05R_SUMMON_BEAR_E, ABILITY_A05S_SUMMON_BEAR_R);
        new NormalAbility("Summon Hawk", ITEM_I022_SUMMON_HAWK_SUMMON, ABILITY_ANSW_SUMMON_HAWK_Q, ABILITY_A05T_SUMMON_HAWK_W, ABILITY_A05U_SUMMON_HAWK_E, ABILITY_A05V_SUMMON_HAWK_R);
        new NormalAbility("Summon Quilbeast", ITEM_I021_SUMMON_QUILBEAST_SUMMON, ABILITY_ANSQ_SUMMON_QUILBEAST_Q, ABILITY_A05W_SUMMON_QUILBEAST_W, ABILITY_A05X_SUMMON_QUILBEAST_E,
            ABILITY_A05Y_SUMMON_QUILBEAST_R);
        new NormalAbility("Chemical Rage", ITEM_I08Y_CHEMICAL_RAGE_ACTIVE, ABILITY_A0P1_CHEMICAL_RAGE_Q, ABILITY_A05Z_CHEMICAL_RAGE_W, ABILITY_A060_CHEMICAL_RAGE_E, ABILITY_A061_CHEMICAL_RAGE_R);
        new NormalAbility("Fire Nova", ITEM_I092_FIRE_NOVA_NUKE, ABILITY_ANFN_FIRE_NOVA_Q, ABILITY_A062_FIRE_NOVA_W, ABILITY_A063_FIRE_NOVA_E, ABILITY_A064_FIRE_NOVA_R);
        new NormalAbility("Frostbolt", ITEM_I02N_FROSTBOLT_AUTOCAST, ABILITY_A0HA_FROSTBOLT_Q, ABILITY_A0H9_FROSTBOLT_W, ABILITY_A0BW_FROSTBOLT_E, ABILITY_A0HB_FROSTBOLT_R);
        new NormalAbility("Healing Ward", ITEM_I03C_HEALING_WARD_UTILITY_SUMMON, ABILITY_A0IG_HEALING_WARD_Q, ABILITY_A0J1_HEALING_WARD_W, ABILITY_A0J2_HEALING_WARD_E, ABILITY_A0J3_HEALING_WARD_R);
        new NormalAbility("Cone of Cold", ITEM_I0B8_CONE_OF_COLD_NUKE, ABILITY_A0N9_CONE_OF_COLD_Q, ABILITY_A0NA_CONE_OF_COLD_W, ABILITY_A0NB_CONE_OF_COLD_E, ABILITY_A0N8_CONE_OF_COLD_R);
        new NormalAbility("Plague Frogs", ITEM_I03H_PLAGUE_FROGS_NUKE, ABILITY_A0MW_PLAGUE_FROGS_Q, ABILITY_A0MX_PLAGUE_FROGS_W, ABILITY_A0MA_PLAGUE_FROGS_E, ABILITY_A0MV_PLAGUE_FROGS_R);
        new NormalAbility("Unstable Volatility", ITEM_I03P_UNSTABLE_VOLATILITY_NUKE, ABILITY_A0MZ_UNSTABLE_VOLATILITY_Q, ABILITY_A0N0_UNSTABLE_VOLATILITY_W, ABILITY_A0N1_UNSTABLE_VOLATILITY_E, ABILITY_A0MY_UNSTABLE_VOLATILITY_R);
        new SpecialAbility("Blink", ITEM_I00P_BLINK_UTILITY, ABILITY_AEBL_BLINK_X);
        new SpecialAbility("Berserk", ITEM_I07H_BERSERK_UTILITY, ABILITY_A0M5_BERSERK_X);
        new SpecialAbility("Ion Shield", ITEM_I08C_ION_SHIELD_ACTIVE, ABILITY_A0LG_ION_SHIELD_X);
        new SpecialAbility("Command Aura", ITEM_I07R_COMMAND_AURA_AURA, ABILITY_A0M6_COMMAND_AURA_X);
        new SpecialAbility("Monsoon", ITEM_I07K_MONSOON_ACTIVE, ABILITY_A0M2_MONSOON_X);
        new SpecialAbility("Glacial Spike", ITEM_I07U_GLACIAL_SPIKE_NUKE, ABILITY_A0LF_GLACIAL_SPIKE_X);
        new SpecialAbility("Goblin Land Mine", ITEM_I085_GOBLIN_LAND_MINE_WARD, ABILITY_A0JR_GOBLIN_LAND_MINE_X);
        new SpecialAbility("Magic Sentry", ITEM_I07Y_MAGIC_SENTRY_SPECIAL, ABILITY_A0LS_MAGIC_SENTRY_X);
        new SpecialAbility("Summon Sea Elemental", ITEM_I07I_SUMMON_SEA_ELEMENTAL_SUMMON, ABILITY_A0M0_SUMMON_SEA_LEMONTAL_X);
        new SpecialAbility("Book of the Dead", ITEM_I08P_BOOK_OF_THE_DEAD_SUMMON, ABILITY_AIFS_BOOK_OF_THE_DEAD_X);
        new SpecialAbility("Chain Dispel", ITEM_I083_CHAIN_DISPEL_UTILITY, ABILITY_A0JL_CHAIN_DISPEL_X);
        new SpecialAbility("Finger of Death", ITEM_I07J_FINGER_OF_DEATH_NUKE, ABILITY_A0M1_FINGER_OF_DEATH_X);
        new SpecialAbility("Frost Missile", ITEM_I080_FROST_MISSILE_NUKE, ABILITY_A0LX_FROST_MISSILE_X);
        new SpecialAbility("Poison Sting", ITEM_I08J_POISON_STING_SPECIAL, ABILITY_A0JM_POISON_STING_X);
        new SpecialAbility("Summon Sapphiron", ITEM_I08B_SUMMON_SAPPHIRON_SUMMON, ABILITY_A0M7_SUMMON_SAPPHIRON_X);
        new SpecialAbility("Elune's Grace", ITEM_I07N_ELUNE_S_GRACE_SPECIAL, ABILITY_A0LP_ELUNE_S_GRACE_X);
        new SpecialAbility("Hardened Skin", ITEM_I07O_HARDENED_SKIN_SPECIAL, ABILITY_A0LQ_HARDENED_SKIN_X);
        new SpecialAbility("Roar", ITEM_I082_ROAR_UTILITY, ABILITY_A0JH_ROAR_X);
        new SpecialAbility("Slow Poison", ITEM_I07P_SLOW_POISON_SPECIAL, ABILITY_A0LR_SLOW_POISON_X);
        new SpecialAbility("Assassin's Arcana", ITEM_I08K_ASSASSIN_S_ARCANA_SPECIAL, ABILITY_A0MG_ASSASSIN_S_ARCANA_X);
        new SpecialAbility("Attribute Bonus", ITEM_I07T_ATTRIBUTE_BONUS_SPECIAL, ABILITY_A0JC_ATTRIBUTE_BONUS_X);
        new SpecialAbility("Big Game Hunter", ITEM_I08L_BIG_GAME_HUNTER_SPECIAL, ABILITY_ANDE_BIG_GAME_HUNTER_DEMOLISH_X);
        new SpecialAbility("Disarm", ITEM_I087_DISARM_UTILITY, ABILITY_ANDS_DISARM_X);
        new SpecialAbility("Fighter's Knowledge", ITEM_I08H_FIGHTER_S_KNOWLEDGE_SPECIAL, ABILITY_A0JF_FIGHTER_S_KNOWLEDGE_X);
        new SpecialAbility("Hurl Boulder", ITEM_I07L_HURL_BOULDER_NUKE, ABILITY_A07N_HURL_BOULDER_X);
        new SpecialAbility("Arcane Intellect", ITEM_I07X_ARCANE_INTELLECT_SPECIAL, ABILITY_A0KK_ARCANE_INTELLECT_X);
        new SpecialAbility("Passive Buster", ITEM_I090_PASSIVE_BUSTER_UTILITY, ABILITY_A005_PASSIVE_BUSTER_X);
        new SpecialAbility("Falling Sword", ITEM_I08R_VOLCANO_ULTIMATE, ABILITY_A08T_VOLCANO_X);
        new SpecialAbility("Incinerate", ITEM_I08X_INCINERATE_SPECIAL, ABILITY_ANIC_INCINERATE_X);
        new SpecialAbility("Monster Hunter", ITEM_I0BA_MONSTER_HUNTER_PASSIVE, ABILITY_ANDE_BIG_GAME_HUNTER_DEMOLISH_X);
        new SpecialAbility("Devour Magic", ITEM_I03L_DEVOUR_MAGIC_UTILITY, ABILITY_A0N7_DEVOUR_MAGIC_X);
        new UltimateAbility("Bloat", ITEM_I02B_BLOAT_ULTIMATE, ABILITY_AHAV_BLOAT_T);
        new UltimateAbility("Safeguard", ITEM_I08A_SAFEGUARD_UTILITY, ABILITY_AOET_SAFEGUARD_T);
        new UltimateAbility("Mass Teleport", ITEM_I007_MASS_TELEPORT_ULTIMATE, ABILITY_AHMT_MASS_TELEPORT_LVL1_T);
        new UltimateAbility("Phoenix", ITEM_I00B_PHOENIX_ULTIMATE, ABILITY_AHPX_PHOENIX_T);
        new UltimateAbility("Resurrection", ITEM_I029_RESURRECTION_ULTIMATE, ABILITY_AHRE_RESURRECTION_T);
        new UltimateAbility("Big Bad Voodoo", ITEM_I01Q_BIG_BAD_VOODOO_ULTIMATE, ABILITY_AOVD_BIG_BAD_VOODOO_T);
        new UltimateAbility("Bladestorm", ITEM_I00V_BLADESTORM_ULTIMATE, ABILITY_AOWW_BLADESTORM_T);
        new UltimateAbility("Earthquake", ITEM_I013_EARTHQUAKE_ULTIMATE, ABILITY_AOEQ_EARTHQUAKE_T);
        new UltimateAbility("Reincarnation", ITEM_I00Z_REINCARNATION_ULTIMATE, ABILITY_AORE_REINCARNATION_T);
        new UltimateAbility("Animate Dead", ITEM_I01B_ANIMATE_DEAD_ULTIMATE, ABILITY_AUAN_ANIMATE_DEAD_T);
        new UltimateAbility("Death March", ITEM_I0AD_DEATH_MARCH_PASSIVE, ABILITY_A07W_DEATH_MARCH_Q_NEW);
        new UltimateAbility("Death And Decay", ITEM_I01J_DEATH_AND_DECAY_ULTIMATE, ABILITY_AUDD_DEATH_AND_DECAY_T);
        new UltimateAbility("Inferno", ITEM_I01F_INFERNO_ULTIMATE, ABILITY_AUIN_INFERNO_T);
        new UltimateAbility("Locust Swarm", ITEM_I01N_LOCUST_SWARM_ULTIMATE, ABILITY_AULS_LOCUST_SWARM_T);
        new UltimateAbility("Starfall", ITEM_I00J_STARFALL_ULTIMATE, ABILITY_AESF_STARFALL_T);
        new UltimateAbility("Tranquility", ITEM_I00N_TRANQUILITY_ULTIMATE, ABILITY_AETQ_TRANQUILITY_T);
        new UltimateAbility("Vengeance", ITEM_I00R_VENGEANCE_ULTIMATE, ABILITY_AESV_VENGEANCE_T);
        new UltimateAbility("Charm", ITEM_I01V_CHARM_ULTIMATE, ABILITY_ANCH_CHARM_T);
        new UltimateAbility("Doom", ITEM_I028_DOOM_ULTIMATE, ABILITY_ANDO_DOOM_T);
        new UltimateAbility("Stampede", ITEM_I023_STAMPEDE_ULTIMATE, ABILITY_ANST_STAMPEDE_T);
        new UltimateAbility("Storm, Earth, And Fire", ITEM_I01Z_BERSERKER_S_TOTEM_SUMMON_WARD, ABILITY_ANEF_STORM_EARTH_AND_FIRE_T);
        new UltimateAbility("Tornado", ITEM_I01R_TORNADO_ULTIMATE, ABILITY_ANTO_TORNADO_T);
        new UltimateAbility("Summon Felhunter", ITEM_I02W_SUMMON_FELHUNTER_ULTIMATE, ABILITY_A0HG_SUMMON_FELHUNTER_T);
        new UltimateAbility("Shadowfire", ITEM_I03K_SHADOWFIRE_ULTIMATE, ABILITY_A0N4_SHADOWFIRE_ULTIMATE_ATC_T);
        new UltimateAbility("Truestrike", ITEM_I02R_TRUESTRIKE_ULTIMATE, ABILITY_A0N2_TRUESTRIKE_T);
        new UltimateAbility("Artillery", ITEM_I03G_ARTILLERY_WARD_ULTIMATE, ABILITY_A0LT_ARTILLERY_T);
        new UltimateAbility("Heavenly Smite", ITEM_I03I_HEAVENLY_SMITE_ULTIMATE, ABILITY_A0LV_HEAVENLY_SMITE_T);
    }
}

public class Hero
{
    public Hero(unit unit, player owner)
    {
        Unit = unit;
        Owner = owner;
        HeroData.ByUnit[unit] = this;
        HeroData.ByPlayer[owner] = this;
        trigger ItemPickup = trigger.Create();
        TriggerRegisterUnitEvent(ItemPickup, Unit, EVENT_UNIT_PICKUP_ITEM);
        TriggerAddAction(ItemPickup, OnItemChange);
        PlayerUnitEvents.Register(UnitEvent.PawnsItem, OnItemChange, Unit);
        PlayerUnitEvents.Register(UnitEvent.DropsItem, OnItemChange, Unit);
    }

    public unit Unit { get; }
    public player Owner { get; }
    public int[] SlotLevels { get; } = new int[5];
    public NormalAbility[] NormalAbilities { get; } = new NormalAbility[4];
    public SpecialAbility Special { get; set; }
    public UltimateAbility Ultimate { get; set; }
    public List<IOrbEffect> Orbs = new();
    public Dictionary<OrbType, IOrbEffect> OrbLookup = new();

    // Attack
    public float AttackCritChance = 0.0f;
    public float AttackCritMult = 2.0f;
    public float AttackLifeSteal = 0.0f;
    public float AttackEvasionChance = 0.0f;
    public float AttackMissChance = 0.0f;
    public int AttackMultiTargets = 1;
    public float AttackMultiMult = 0.5f;

    // Spell
    public float SpellStrengthScaling = 0.0f;
    public float SpellAgilityScaling = 0.0f;
    public float SpellIntelligenceScaling = 0.0f;
    public float SpellLifeSteal = 0.0f;
    public float SpellCritChance = 0.0f;
    public float SpellCritMult = 1.5f;
    
    // Ability mods
    public float CritChanceCrit = 0.0f;
    public float CritChanceBrawler = 0.0f;
    public float EvasionEvade = 0.0f;
    public float EvasionBrawler = 0.0f;
    public int CleaveMultiTargets = 1;
    
    // Item mods
    public ItemData ItemMods = new();

    private static Hero Get(unit InUnit)
    {
        return HeroData.ByUnit[InUnit];
    }

    private static Hero Get(player InPlayer)
    {
        return HeroData.ByPlayer[InPlayer];
    }

    public void OnItemChange()
    {
        ResetItemStats();
        for (int i = 0; i <= 5; i++)
        {
            item CurrentItem = Unit.ItemAtOrDefault(i);
            if (CurrentItem != null)
            {
                Items.ItemLookup.TryGetValue(CurrentItem.TypeId, out ItemData itemData);
                if (itemData != null)
                {
                    ItemMods.AttackSpeed += itemData.AttackSpeed;
                    ItemMods.HealthRegen += itemData.HealthRegen;
                    ItemMods.ManaRegen += itemData.ManaRegen;
                    ItemMods.BaseDamage += itemData.BaseDamage;
                    ItemMods.SpellBonus += itemData.SpellBonus;
                    ItemMods.CleaveCount += itemData.CleaveCount;
                    ItemMods.CleaveBonus += itemData.CleaveBonus;
                    ItemMods.EvasionChance += itemData.EvasionChance;
                }
            }
        }
        // foreach (NormalAbility Ability in HeroData.NormalAbilities)
        // {
        //     if (Ability != null)
        //     {
        //         ability AbilityInstance = Hero.GetAbility(Ability.LearnedId);
        //         AbilityInstance.SetTooltipNormalExtended_aub1(Hero.GetAbilityLevel(Ability.LearnedId), "CAT");
        //     }
        // }
    }

    public void ResetItemStats()
    {
        ItemMods.AttackSpeed = 0.0f;
        ItemMods.HealthRegen = 0.0f;
        ItemMods.ManaRegen = 0.0f;
        ItemMods.BaseDamage = 0.0f;
        ItemMods.SpellBonus = 0.0f;
        ItemMods.CleaveCount = 0;
        ItemMods.CleaveBonus = 0.0f;
        ItemMods.EvasionChance = 0.0f;
    }

    public int GetAttribute(AttributeType InType)
    {
        switch (InType)
        {
            case AttributeType.None:
            {
                return 0;
            }
            case AttributeType.Str:
            {
                return Unit.Strength;
            }
            case AttributeType.Agi:
            {
                return Unit.Agility;
            }
            case AttributeType.Int:
            {
                return Unit.Intelligence;
            }
            case AttributeType.All:
            {
                return Unit.Strength + Unit.Agility + Unit.Intelligence;
            }
        }

        return 0;
    }

    public int GetAbilityLevel(int InItemId)
    {
        for (int i = 0; i <= 3; i++)
        {
            if (NormalAbilities[i].ItemId == InItemId)
            {
                return SlotLevels[i];
            }
        }

        if (Special.ItemId == InItemId)
        {
            return Unit.Level / 2;
        }

        if (Ultimate.ItemId == InItemId)
        {
            return SlotLevels[5];
        }

        return 0;
    }

    // Events
    private static void OnItemPickup()
    {
        if (GetManipulatedItem().Type != itemtype.Powerup)
        {
            return;
        }

        Hero Hero = Get(GetTriggerUnit());
        if (Hero == null)
        {
            return;
        }

        int ItemId = GetManipulatedItem().TypeId;
#if DEBUG
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, $"Picked up item ID: {ItemId} (hex {(uint)ItemId:X8}) | Registered normals: {NormalAbility.GetByItemId(ItemId).MaxLevel}");
#endif

        if (NormalAbility.GetByItemId(ItemId) is { } Normal)
        {
            Normal.AddToHero(Hero);
        }
        else if (SpecialAbility.GetByItemId(ItemId) is { } Special)
        {
            Special.AddToHero(Hero);
        }
        else if (UltimateAbility.GetByItemId(ItemId) is { } Ultimate)
        {
            Ultimate.AddToHero(Hero);
        }
    }

    public void AddOrb(OrbType InOrbType, int InOrbLevel)
    {
        if (OrbLookup.ContainsKey(InOrbType))
        {
            OrbLookup.TryGetValue(InOrbType, out IOrbEffect Orb);
            if (InOrbLevel !> Orb.Level)
            {
                return;
            }
            RemoveOrb(InOrbType);
        }

        switch (InOrbType)
        {
            case OrbType.Feedback:
            {
                Feedback Orb = new();
                Orb.Aquire(Unit, InOrbLevel);
                Orbs.Add(Orb);
                OrbLookup.Add(InOrbType, Orb);
                break;
            }
            case OrbType.Spawner:
            {
                BlackArrow Orb = new();
                Orb.Aquire(Unit, InOrbLevel);
                Orbs.Add(Orb);
                OrbLookup.Add(InOrbType, Orb);
                break;
            }
            // case OrbType.Corruption:
            // {
            //     Corruption Orb = new();
            //     Orb.Aquired(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Fire:
            // {
            //     Fire Orb = new();
            //     Orb.Aquired(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Slow:
            // {
            //     Slow Orb = new();
            //     Orb.Aquired(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Poison:
            // {
            //     Poison Orb = new();
            //     Orb.Aquired(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Incinerate:
            // {
            //     Incinerate Orb = new();
            //     Orb.Aquired(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Pillage:
            // {
            //     Pillage Orb = new();
            //     Orb.Aquired(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Purge:
            // {
            //     Purge Orb = new();
            //     Orb.Aquired(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            default:
                throw new ArgumentOutOfRangeException(nameof(InOrbType), InOrbType, null);
        }
    }

    public void RemoveOrb(OrbType InOrbType)
    {
        if (OrbLookup.ContainsKey(InOrbType))
        {
            OrbLookup.TryGetValue(InOrbType, out IOrbEffect Orb);
            OrbLookup.Remove(InOrbType);
            Orb.Remove();
            Orbs.Remove(Orb);
        }
    }

    private static void OnChatCommand()
    {
        Hero Hero = Get(GetTriggerPlayer());
        if (Hero == null)
        {
            return;
        }

        string Message = GetEventPlayerChatString();
        string Letter;

        switch (Message.Length)
        {
            case >= 10 when Message.Substring(0, 9) == "-unlearn ":
                Letter = Message.Substring(9, 1).ToUpper();
                break;
            case >= 4 when Message.Substring(0, 3) == "-u ":
                Letter = Message.Substring(3, 1).ToUpper();
                break;
            default:
                return;
        }

        //Letter = Letter.ToUpper();
        switch (Letter)
        {
            case "Q" when Hero.NormalAbilities[0] != null:
                UnlearnNormal(Hero, 0, Hero.NormalAbilities[0]);
                break;
            case "W" when Hero.NormalAbilities[1] != null:
                UnlearnNormal(Hero, 1, Hero.NormalAbilities[1]);
                break;
            case "E" when Hero.NormalAbilities[2] != null:
                UnlearnNormal(Hero, 2, Hero.NormalAbilities[2]);
                break;
            case "R" when Hero.NormalAbilities[3] != null:
                UnlearnNormal(Hero, 3, Hero.NormalAbilities[3]);
                break;
            case "X" when Hero.Special != null:
                UnlearnSpecial(Hero, Hero.Special);
                break;
            case "T" when Hero.Ultimate != null:
                UnlearnUltimate(Hero, Hero.Ultimate);
                break;
            case "Z":
                GetTriggerPlayer().DisplayTextTo("You can't unlearn your innate skill, Tony.");
                break;
        }
    }

    private static void UnlearnNormal(Hero InHero, int InSlot, NormalAbility InAbility)
    {
        InHero.Owner.Lumber += NormalAbility.LumberCost - 1;
        InHero.Unit.RemoveAbility(InAbility.AbilityIds[InSlot]);
        if (InHero.NormalAbilities[InSlot].OrbType != OrbType.None)
        {
            InHero.RemoveOrb(InHero.NormalAbilities[InSlot].OrbType);
        }

        if (InAbility.AbilityIds[InSlot] == ABILITY_ANCA_CLEAVING_ATTACK_Q
            || InAbility.AbilityIds[InSlot] == ABILITY_A04H_CLEAVING_ATTACK_W
            || InAbility.AbilityIds[InSlot] == ABILITY_A04I_CLEAVING_ATTACK_E
            || InAbility.AbilityIds[InSlot] == ABILITY_A04J_CLEAVING_ATTACK_R)
        {
            InHero.CleaveMultiTargets = 0;
            InHero.AttackMultiTargets = InHero.ItemMods.CleaveCount;
        }

        if (InAbility.AbilityIds[InSlot] == ABILITY_AOCR_CRITICAL_STRIKE_Q
            || InAbility.AbilityIds[InSlot] == ABILITY_A01D_CRITICAL_STRIKE_W
            || InAbility.AbilityIds[InSlot] == ABILITY_A01E_CRITICAL_STRIKE_E
            || InAbility.AbilityIds[InSlot] == ABILITY_A01F_CRITICAL_STRIKE_R)
        {
            InHero.CritChanceCrit = 0.0f;
            InHero.AttackCritChance = InHero.CritChanceBrawler;
        }

        if (InAbility.AbilityIds[InSlot] == ABILITY_ANDB_DRUNKEN_BRAWLER_Q
            || InAbility.AbilityIds[InSlot] == ABILITY_A04Q_DRUNKEN_BRAWLER_W
            || InAbility.AbilityIds[InSlot] == ABILITY_A04R_DRUNKEN_BRAWLER_E
            || InAbility.AbilityIds[InSlot] == ABILITY_A04S_DRUNKEN_BRAWLER_R)
        {
            InHero.CritChanceBrawler = 0.0f;
            InHero.EvasionBrawler = 0.0f;
            InHero.AttackCritChance = InHero.CritChanceCrit;
            InHero.AttackEvasionChance = InHero.EvasionEvade + InHero.ItemMods.EvasionChance;
        }

        if (InAbility.AbilityIds[InSlot] == ABILITY_AEEV_EVASION_Q
            || InAbility.AbilityIds[InSlot] == ABILITY_A03E_EVASION_W
            || InAbility.AbilityIds[InSlot] == ABILITY_A03F_EVASION_E
            || InAbility.AbilityIds[InSlot] == ABILITY_A03G_EVASION_R)
        {
            InHero.EvasionEvade = 0.0f;
            InHero.AttackEvasionChance = InHero.EvasionBrawler + InHero.ItemMods.EvasionChance;
        }
        InHero.NormalAbilities[InSlot] = null;
        UnlearnMessage(InHero, InAbility.Name);

    }

    private static void UnlearnSpecial(Hero InHero, SpecialAbility InAbility)
    {
        InHero.Owner.Lumber += SpecialAbility.LumberCost - 1;
        InHero.Unit.RemoveAbility(InAbility.AbilityId);
        InHero.Special = null;
        UnlearnMessage(InHero, InAbility.Name);
    }

    private static void UnlearnUltimate(Hero InHero, UltimateAbility InAbility)
    {
        if (InAbility.Name == "Mass Teleport")
        {
            InHero.Unit.RemoveAbility(FourCC("AHm2"));
            InHero.Unit.RemoveAbility(FourCC("AHm3"));
        }

        InHero.Owner.Lumber += UltimateAbility.LumberCost - 1;
        InHero.Unit.RemoveAbility(InAbility.AbilityId);
        InHero.Ultimate = null;
        UnlearnMessage(InHero, InAbility.Name);
    }

    private static void UnlearnMessage(Hero InHero, string InName)
    {
        string Msg = $"{GetPlayerName(InHero.Owner)} unlearned |cffff8000{InName}|r";
        if (GetLocalPlayer() == InHero.Owner || IsPlayerAlly(GetLocalPlayer(), InHero.Owner))
        {
            GetLocalPlayer().DisplayTextTo(Msg);
            StartSound(bj_questHintSound);
        }
    }

    public static void Initialize()
    {
        trigger Unlearn = trigger.Create();
        for (int i = 0; i <= 11; i++)
        {
            player CurrentPlayer = player.Create(i);
            if (CurrentPlayer.Controller == mapcontrol.User)
            {
                Unlearn.RegisterPlayerChatEvent(CurrentPlayer, "-unlearn ", false);
                Unlearn.RegisterPlayerChatEvent(CurrentPlayer, "-u ", false);
            }
        }

        Unlearn.AddAction(OnChatCommand);
        PlayerUnitEvents.Register(UnitTypeEvent.PicksUpItem, OnItemPickup);
        PlayerUnitEvents.Register(HeroTypeEvent.Levels, OnLevelUp);
    }

    private static void OnLevelUp()
    {
        if (GetTriggerUnit().Level < 10)
        {
            GetTriggerPlayer().Lumber += 2;
        }

        Hero LevelingHero = Get(GetTriggerPlayer());
        if (LevelingHero != null)
        {
            SpecialAbility SpecialAbil = Get(GetTriggerPlayer()).Special;
            if (SpecialAbil != null)
            {
                GetTriggerUnit().SetAbilityLevel(SpecialAbil.AbilityId, GetLevelingUnit().Level / 2);
            }
        }

        HeroData.HeroToBaseAbility.TryGetValue(GetTriggerUnit().UnitType, out int Value);
        SetUnitAbilityLevelSwapped(Value, GetTriggerUnit(), GetLevelingUnit().Level / 2);
        if (HeroData.HeroToBaseAbility[GetTriggerUnit().UnitType] == FourCC("A0LD"))
        {
            GetTriggerUnit().SetAbilityLevel(FourCC("A0KA"), GetLevelingUnit().Level / 2);
        }
    }
}

public class UltimateAbility : ILearnedAbility
{
    public const int LumberCost = 8;
    public string Name { get; set; }
    public int ItemId { get; set; }
    public int MaxLevel { get; set; }
    public string ExtendedTooltip { get; set; }
    public OrbType OrbType { get; set; }

    public UltimateAbility(string name, int itemId, int abilityId)
    {
        Name = name;
        ItemId = itemId;
        AbilityId = abilityId;
        MaxLevel = 3;
        HeroData.UltimateByItemId.Add(itemId, this);
    }

    public int AbilityId { get; private set; }

    public static UltimateAbility GetByItemId(int ItemId)
    {
        if (HeroData.UltimateByItemId.TryGetValue(ItemId, out UltimateAbility Ability))
        {
            return Ability;
        }

        return null;
    }

    public bool AddToHero(Hero InHero)
    {
        if (InHero.Ultimate != null && InHero.Ultimate != this)
        {
            InHero.Owner.DisplayTextTo("You already have an ultimate ability. Lumber returned.");
            InHero.Owner.Lumber += LumberCost;
            return false;
        }

        if (InHero.Ultimate == null)
        {
            InHero.Ultimate = this;
            InHero.SlotLevels[4] = Math.Max(1, InHero.SlotLevels[4]);
            InHero.Unit.AddAbility(AbilityId);
            InHero.Unit.SetAbilityLevel(AbilityId, InHero.SlotLevels[4]);
            LearnMessage(InHero);
            return true;
        }

        // Leveling existing ultimate
        if (Name == "Mass Teleport")
            return HandleMassTeleportUpgrade(InHero);

        if (InHero.SlotLevels[4] >= MaxLevel)
        {
            InHero.Owner.DisplayTextTo("Ultimate already at max level. Lumber returned.");
            InHero.Owner.Lumber += LumberCost;
            return false;
        }

        InHero.SlotLevels[4]++;
        InHero.Unit.SetAbilityLevel(AbilityId, InHero.SlotLevels[4]);
        LearnMessage(InHero);
        return true;
    }

    public void UpdateTooltip(string Tooltip)
    {
        // Set Ultimate tooltip
    }

    private bool HandleMassTeleportUpgrade(Hero InHero)
    {
        int Level = InHero.SlotLevels[4];
        switch (Level)
        {
            case 1:
                InHero.Unit.RemoveAbility(FourCC("AHmt"));
                InHero.Unit.AddAbility(FourCC("AHm2"));
                InHero.SlotLevels[4] = 2;
                InHero.Unit.SetAbilityLevel(FourCC("AHm2"), 2);
                break;
            case 2:
                InHero.Unit.RemoveAbility(FourCC("AHm2"));
                InHero.Unit.AddAbility(FourCC("AHm3"));
                InHero.SlotLevels[4] = 3;
                InHero.Unit.SetAbilityLevel(FourCC("AHm3"), 3);
                break;
            default:
                InHero.Owner.DisplayTextTo("Mass Teleport already at max level.");
                InHero.Owner.Lumber += LumberCost;
                return false;
        }

        LearnMessage(InHero);
        return true;
    }

    public void LearnMessage(Hero InHero, int InSlot = 0)
    {
        string Msg = $"{InHero.Owner.Name} learned |cffff8000{Name}|r [|cffffcc00Level {InHero.SlotLevels[4]}|r]";
        if (GetLocalPlayer() == InHero.Owner || IsPlayerAlly(GetLocalPlayer(), InHero.Owner))
        {
            GetLocalPlayer().DisplayTextTo(Msg);
            StartSound(bj_questHintSound);
        }
    }
}

public class SpecialAbility : ILearnedAbility
{
    public const int LumberCost = 5;
    public string Name { get; set; }
    public int ItemId { get; set; }
    public int MaxLevel { get; set; }
    public string ExtendedTooltip { get; set; }
    public OrbType OrbType { get; set; }

    public SpecialAbility(string name, int itemId, int abilityId)
    {
        Name = name;
        ItemId = itemId;
        AbilityId = abilityId;
        MaxLevel = 1;
        HeroData.SpecialByItemId.Add(itemId, this);
    }

    public int AbilityId { get; private set; }

    public static SpecialAbility GetByItemId(int itemId)
    {
        if (HeroData.SpecialByItemId.TryGetValue(itemId, out SpecialAbility Ability))
        {
            return Ability;
        }

        return null;
    }

    public bool AddToHero(Hero InHero)
    {
        if (InHero.Special != null)
        {
            InHero.Owner.DisplayTextTo("You already have a special ability. Lumber returned.");
            InHero.Owner.Lumber += LumberCost;
            return false;
        }

        InHero.Special = this;
        InHero.Unit.AddAbility(AbilityId);
        InHero.Unit.SetAbilityLevel(AbilityId, InHero.Unit.Level / 2);
        LearnMessage(InHero);

        return true;
    }

    public void LearnMessage(Hero InHero, int InSlot = 0)
    {
        string Msg = $"{InHero.Owner.Name} learned |cffff8000{Name}|r";
        if (GetLocalPlayer() == InHero.Owner || IsPlayerAlly(GetLocalPlayer(), InHero.Owner))
        {
            GetLocalPlayer().DisplayTextTo(Msg);
            StartSound(bj_questHintSound);
        }
    }

    public void UpdateTooltip(string Tooltip)
    {
        // Update special tooltip
    }
}

public class NormalAbility : ILearnedAbility
{
    public const int LumberCost = 2;
    public int LearnedId { get; set; }
    public string Name { get; set; }
    public int ItemId { get; set; }
    public int MaxLevel { get; set; }
    public string ExtendedTooltip { get; set; }
    public OrbType OrbType { get; set; }

    public NormalAbility(string InName, int InItemId, int Q, int W, int E, int R, OrbType InOrbType = OrbType.None)
    {
        Name = InName;
        ItemId = InItemId;
        MaxLevel = 10;

        AbilityIds[(int)AbilitySlot.Q] = Q;
        AbilityIds[(int)AbilitySlot.W] = W;
        AbilityIds[(int)AbilitySlot.E] = E;
        AbilityIds[(int)AbilitySlot.R] = R;

        OrbType = InOrbType;

        HeroData.NormalByItemId.Add(InItemId, this);
    }

    public int[] AbilityIds { get; } = new int[4]; // Q, W, E, R

    public static NormalAbility GetByItemId(int itemId)
    {
        if (HeroData.NormalByItemId.TryGetValue(itemId, out NormalAbility Ability))
        {
            return Ability;
        }

        return null;
    }

    public bool AddToHero(Hero InHero)
    {
        // Find existing slot or empty one
        int TargetSlot = -1;
        for (int i = 0; i <= 3; i++)
        {
            if (InHero.NormalAbilities[i] == this)
            {
                if (!CanLevelUp(InHero, i))
                {
                    return RefundLumber(InHero, LumberCost);
                }

                LevelUpExisting(InHero, i);
                return true;
            }

            if (InHero.NormalAbilities[i] == null && TargetSlot == -1)
            {
                TargetSlot = i;
            }
        }

        if (TargetSlot != -1)
        {
            LearnedId = AbilityIds[TargetSlot];
            InHero.NormalAbilities[TargetSlot] = this;
            InHero.SlotLevels[TargetSlot] = Math.Max(1, InHero.SlotLevels[TargetSlot]);
            InHero.Unit.AddAbility(AbilityIds[TargetSlot]);
            InHero.Unit.SetAbilityLevel(AbilityIds[TargetSlot], InHero.SlotLevels[TargetSlot]);
            LearnMessage(InHero, TargetSlot);
            if (OrbType != OrbType.None)
            {
                InHero.AddOrb(OrbType, InHero.SlotLevels[TargetSlot]);
            }

            if (AbilityIds[TargetSlot] == ABILITY_ANCA_CLEAVING_ATTACK_Q
                || AbilityIds[TargetSlot] == ABILITY_A04H_CLEAVING_ATTACK_W
                || AbilityIds[TargetSlot] == ABILITY_A04I_CLEAVING_ATTACK_E
                || AbilityIds[TargetSlot] == ABILITY_A04J_CLEAVING_ATTACK_R)
            {
                InHero.AttackMultiTargets = InHero.ItemMods.CleaveCount + Math.Max(2, InHero.SlotLevels[TargetSlot] / 2);
            }

            if (AbilityIds[TargetSlot] == ABILITY_AOCR_CRITICAL_STRIKE_Q
                || AbilityIds[TargetSlot] == ABILITY_A01D_CRITICAL_STRIKE_W
                || AbilityIds[TargetSlot] == ABILITY_A01E_CRITICAL_STRIKE_E
                || AbilityIds[TargetSlot] == ABILITY_A01F_CRITICAL_STRIKE_R)
            {
                InHero.CritChanceCrit = 0.05f * InHero.SlotLevels[TargetSlot];
                InHero.AttackCritChance = InHero.CritChanceCrit + InHero.CritChanceBrawler;
            }

            if (AbilityIds[TargetSlot] == ABILITY_ANDB_DRUNKEN_BRAWLER_Q
                || AbilityIds[TargetSlot] == ABILITY_A04Q_DRUNKEN_BRAWLER_W
                || AbilityIds[TargetSlot] == ABILITY_A04R_DRUNKEN_BRAWLER_E
                || AbilityIds[TargetSlot] == ABILITY_A04S_DRUNKEN_BRAWLER_R)
            {
                InHero.CritChanceBrawler = 0.05f + Math.Max(0.01f, InHero.SlotLevels[TargetSlot] / 2.0f);
                InHero.AttackCritChance = InHero.CritChanceCrit + InHero.CritChanceBrawler;
                InHero.EvasionBrawler = 0.1f;
                InHero.AttackEvasionChance = InHero.EvasionEvade + InHero.EvasionBrawler + InHero.ItemMods.EvasionChance;
            }

            if (AbilityIds[TargetSlot] == ABILITY_AEEV_EVASION_Q
                || AbilityIds[TargetSlot] == ABILITY_A03E_EVASION_W
                || AbilityIds[TargetSlot] == ABILITY_A03F_EVASION_E
                || AbilityIds[TargetSlot] == ABILITY_A03G_EVASION_R)
            {
                InHero.EvasionEvade = 0.09f + (0.01f * InHero.SlotLevels[TargetSlot]);
                InHero.AttackEvasionChance = InHero.EvasionEvade + InHero.EvasionBrawler + InHero.ItemMods.EvasionChance;
            }
            return true;
        }

        InHero.Owner.DisplayTextTo("Maximum normal abilities reached. Lumber returned.");
        InHero.Owner.Lumber += LumberCost;
        return false;
    }

    public void UpdateTooltip(string Tooltip)
    {
        // Update normal tooltip
    }

    private bool CanLevelUp(Hero InHero, int InSlot)
    {
        int CurrentLevel = InHero.Unit.GetAbilityLevel(AbilityIds[InSlot]);
        return CurrentLevel < MaxLevel && CurrentLevel < HeroData.AbilityMax[InHero.Unit.HeroLevel];
    }

    private void LevelUpExisting(Hero InHero, int InSlot)
    {
        InHero.SlotLevels[InSlot]++;
        InHero.Unit.SetAbilityLevel(AbilityIds[InSlot], InHero.SlotLevels[InSlot]);
        LearnMessage(InHero, InSlot);
        if (OrbType != OrbType.None)
        {
            if (InHero.OrbLookup.TryGetValue(OrbType, out IOrbEffect Value))
            {
                Value.Level = InHero.SlotLevels[InSlot];
            }
        }

        if (AbilityIds[InSlot] == ABILITY_ANCA_CLEAVING_ATTACK_Q
            || AbilityIds[InSlot] == ABILITY_A04H_CLEAVING_ATTACK_W
            || AbilityIds[InSlot] == ABILITY_A04I_CLEAVING_ATTACK_E
            || AbilityIds[InSlot] == ABILITY_A04J_CLEAVING_ATTACK_R)
        {
            InHero.AttackMultiTargets = InHero.ItemMods.CleaveCount + Math.Max(2, InHero.SlotLevels[InSlot] / 2);
        }

        if (AbilityIds[InSlot] == ABILITY_AOCR_CRITICAL_STRIKE_Q
            || AbilityIds[InSlot] == ABILITY_A01D_CRITICAL_STRIKE_W
            || AbilityIds[InSlot] == ABILITY_A01E_CRITICAL_STRIKE_E
            || AbilityIds[InSlot] == ABILITY_A01F_CRITICAL_STRIKE_R)
        {
            InHero.CritChanceCrit = 0.05f * InHero.SlotLevels[InSlot];
            InHero.AttackCritChance = InHero.CritChanceCrit + InHero.CritChanceBrawler;
        }

        if (AbilityIds[InSlot] == ABILITY_ANDB_DRUNKEN_BRAWLER_Q
            || AbilityIds[InSlot] == ABILITY_A04Q_DRUNKEN_BRAWLER_W
            || AbilityIds[InSlot] == ABILITY_A04R_DRUNKEN_BRAWLER_E
            || AbilityIds[InSlot] == ABILITY_A04S_DRUNKEN_BRAWLER_R)
        {
            InHero.CritChanceBrawler = 0.05f + Math.Max(0.01f, InHero.SlotLevels[InSlot] / 2.0f);
            InHero.AttackCritChance = InHero.CritChanceCrit + InHero.CritChanceBrawler;
            InHero.EvasionBrawler = 0.1f;
            InHero.AttackEvasionChance = InHero.EvasionEvade + InHero.EvasionBrawler + InHero.ItemMods.EvasionChance;
        }

        if (AbilityIds[InSlot] == ABILITY_AEEV_EVASION_Q
            || AbilityIds[InSlot] == ABILITY_A03E_EVASION_W
            || AbilityIds[InSlot] == ABILITY_A03F_EVASION_E
            || AbilityIds[InSlot] == ABILITY_A03G_EVASION_R)
        {
            InHero.EvasionEvade = 0.09f + (0.01f * InHero.SlotLevels[InSlot]);
            InHero.AttackEvasionChance = InHero.EvasionEvade + InHero.EvasionBrawler + InHero.ItemMods.EvasionChance;
        }
    }

    public void LearnMessage(Hero InHero, int InSlot)
    {
        string Msg = $"{InHero.Owner.Name} learned |cffff8000{Name}|r [|cffffcc00Level {InHero.SlotLevels[InSlot]}|r]";
        if (GetLocalPlayer() == InHero.Owner || GetLocalPlayer().IsAlly(InHero.Owner))
        {
            GetLocalPlayer().DisplayTextTo(Msg);
            StartSound(bj_questHintSound);
        }
    }

    private bool RefundLumber(Hero InHero, int InLumber)
    {
        if (GetLocalPlayer() == InHero.Owner)
        {
            GetLocalPlayer().DisplayTextTo("Cannot level this ability further. Lumber returned.");
            StartSound(bj_questHintSound);
        }

        InHero.Owner.Lumber += InLumber;
        return false;
    }
}

public interface ILearnedAbility
{
    public string Name { get; protected set; }
    public int ItemId { get; protected set; }
    public int MaxLevel { get; set; }
    public string ExtendedTooltip { get; set; }
    public OrbType OrbType { get; set; }

    public bool AddToHero(Hero InHero);
    public void LearnMessage(Hero InHero, int InSlot = 0);
    public void UpdateTooltip(string Tooltip);
}

internal enum AbilitySlot
{
    Q = 0,
    W = 1,
    E = 2,
    R = 3
}
