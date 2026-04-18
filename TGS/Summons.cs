using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Events;
using static TGS.Util;
using static TGS.Globals;
using static Constants;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public static class Summons
{
    public static Dictionary<int, SummonScalar> UnitIdToSummon = new();

    public static void Init()
    {
        PlayerUnitEvents.Register(UnitTypeEvent.Summons, HeroSummons);
        PlayerUnitEvents.Register(UnitTypeEvent.Summons, SummonScaling);
        List<int> SummonIds = new List<int>();
        SummonIds.Add(UNIT_OSW1_SPIRIT_WOLF_L1);
        SummonIds.Add(UNIT_OSW2_DIRE_WOLF_L2);
        SummonIds.Add(UNIT_OSW3_SHADOW_WOLF_L3);
        SummonIds.Add(UNIT_O008_SHADOW_WOLF_L4);
        SummonIds.Add(UNIT_O009_SHADOW_WOLF_L5);
        SummonIds.Add(UNIT_O00A_SHADOW_WOLF_L6);
        SummonIds.Add(UNIT_O00B_SHADOW_WOLF_L7);
        SummonIds.Add(UNIT_O00C_SHADOW_WOLF_L8);
        SummonIds.Add(UNIT_O00D_SHADOW_WOLF_L9);
        SummonIds.Add(UNIT_O007_SHADOW_WOLF_L10);
        List<int> AbilityIds = new List<int>();
        AbilityIds.Add(ABILITY_AOSF_FERAL_SPIRIT_Q);
        AbilityIds.Add(ABILITY_A01M_FERAL_SPIRIT_W);
        AbilityIds.Add(ABILITY_A01N_FERAL_SPIRIT_E);
        AbilityIds.Add(ABILITY_A01O_FERAL_SPIRIT_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_HWAT_WALTER_LEMONTAL_L1);
        SummonIds.Add(UNIT_HWT2_WALTER_LEMONTAL_L2);
        SummonIds.Add(UNIT_HWT3_WALTER_LEMONTAL_L3);
        SummonIds.Add(UNIT_H007_WALTER_LEMONTAL_L4);
        SummonIds.Add(UNIT_H008_WALTER_LEMONTAL_L5);
        SummonIds.Add(UNIT_H009_WALTER_LEMONTAL_L6);
        SummonIds.Add(UNIT_H00A_WALTER_LEMONTAL_L7);
        SummonIds.Add(UNIT_H00B_WALTER_LEMONTAL_L8);
        SummonIds.Add(UNIT_H00C_WALTER_LEMONTAL_L9);
        SummonIds.Add(UNIT_H006_WALTER_LEMONTAL_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_AHWE_SUMMON_WALTER_LEMONTAL_Q);
        AbilityIds.Add(ABILITY_A00S_SUMMON_WALTER_LEMONTAL_W);
        AbilityIds.Add(ABILITY_A00T_SUMMON_WALTER_LEMONTAL_E);
        AbilityIds.Add(ABILITY_A00U_SUMMON_WALTER_LEMONTAL_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NGZ1_BEAR_L1);
        SummonIds.Add(UNIT_NGZ2_BEAR_L2);
        SummonIds.Add(UNIT_NGZ3_BEAR_L3);
        SummonIds.Add(UNIT_N00C_BEAR_L4);
        SummonIds.Add(UNIT_N00D_BEAR_L5);
        SummonIds.Add(UNIT_N00E_BEAR_L6);
        SummonIds.Add(UNIT_N00F_BEAR_L7);
        SummonIds.Add(UNIT_N00G_BEAR_L8);
        SummonIds.Add(UNIT_N00H_BEAR_L9);
        SummonIds.Add(UNIT_N00B_BEAR_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_ANSG_SUMMON_BEAR_Q);
        AbilityIds.Add(ABILITY_A05Q_SUMMON_BEAR_W);
        AbilityIds.Add(ABILITY_A05R_SUMMON_BEAR_E);
        AbilityIds.Add(ABILITY_A05S_SUMMON_BEAR_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NWE1_HAWK_L1);
        SummonIds.Add(UNIT_NWE2_HAWK_L2);
        SummonIds.Add(UNIT_NWE3_HAWK_L3);
        SummonIds.Add(UNIT_N00J_HAWK_L4);
        SummonIds.Add(UNIT_N00K_HAWK_L5);
        SummonIds.Add(UNIT_N00L_HAWK_L6);
        SummonIds.Add(UNIT_N00M_HAWK_L7);
        SummonIds.Add(UNIT_N00N_HAWK_L8);
        SummonIds.Add(UNIT_N00I_HAWK_L9);
        SummonIds.Add(UNIT_N00O_HAWK_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_ANSW_SUMMON_HAWK_Q);
        AbilityIds.Add(ABILITY_A05T_SUMMON_HAWK_W);
        AbilityIds.Add(ABILITY_A05U_SUMMON_HAWK_E);
        AbilityIds.Add(ABILITY_A05V_SUMMON_HAWK_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NQB1_QUILBEAST_L1);
        SummonIds.Add(UNIT_NQB2_QUILBEAST_L2);
        SummonIds.Add(UNIT_NQB3_QUILBEAST_L3);
        SummonIds.Add(UNIT_NQB4_QUILBEAST_L4);
        SummonIds.Add(UNIT_N00X_QUILBEAST_L5);
        SummonIds.Add(UNIT_N00Y_QUILBEAST_L6);
        SummonIds.Add(UNIT_N00Z_QUILBEAST_L7);
        SummonIds.Add(UNIT_N00W_QUILBEAST_L8);
        SummonIds.Add(UNIT_N011_QUILBEAST_L9);
        SummonIds.Add(UNIT_N010_QUILBEAST_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_ANSQ_SUMMON_QUILBEAST_Q);
        AbilityIds.Add(ABILITY_A05W_SUMMON_QUILBEAST_W);
        AbilityIds.Add(ABILITY_A05X_SUMMON_QUILBEAST_E);
        AbilityIds.Add(ABILITY_A05Y_SUMMON_QUILBEAST_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_EFON_TREANT_L1);
        SummonIds.Add(UNIT_E001_TREANT_L2);
        SummonIds.Add(UNIT_E002_TREANT_L3);
        SummonIds.Add(UNIT_E00A_TREANT_L4);
        SummonIds.Add(UNIT_E00B_TREANT_L5);
        SummonIds.Add(UNIT_E00D_TREANT_L6);
        SummonIds.Add(UNIT_E00C_TREANT_L7);
        SummonIds.Add(UNIT_E00E_TREANT_L8);
        SummonIds.Add(UNIT_E00F_TREANT_L9);
        SummonIds.Add(UNIT_E00G_TREANT_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_AEFN_FORCE_OF_NATURE_Q);
        AbilityIds.Add(ABILITY_A03N_FORCE_OF_NATURE_W);
        AbilityIds.Add(ABILITY_A03O_FORCE_OF_NATURE_E);
        AbilityIds.Add(ABILITY_A03P_FORCE_OF_NATURE_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_OSP1_SERPENT_WARD_L1);
        SummonIds.Add(UNIT_OSP2_SERPENT_WARD_L2);
        SummonIds.Add(UNIT_OSP3_SERPENT_WARD_L3);
        SummonIds.Add(UNIT_OSP4_SERPENT_WARD_L4);
        SummonIds.Add(UNIT_O00F_SERPENT_WARD_L5);
        SummonIds.Add(UNIT_O00G_SERPENT_WARD_L6);
        SummonIds.Add(UNIT_O00H_SERPENT_WARD_L7);
        SummonIds.Add(UNIT_O00I_SERPENT_WARD_L8);
        SummonIds.Add(UNIT_O00J_SERPENT_WARD_L9);
        SummonIds.Add(UNIT_O00E_SERPENT_WARD_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_AOSW_SERPENT_WARD_Q);
        AbilityIds.Add(ABILITY_A01Y_SERPENT_WARD_W);
        AbilityIds.Add(ABILITY_A01Z_SERPENT_WARD_E);
        AbilityIds.Add(ABILITY_A020_SERPENT_WARD_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.0f, 0.0f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_UCS1_CARRION_BEETLE_L1);
        SummonIds.Add(UNIT_UCS2_CARRION_BEETLE_L2);
        SummonIds.Add(UNIT_UCS3_CARRION_BEETLE_L3);
        SummonIds.Add(UNIT_U008_CARRION_BEETLE_L4);
        SummonIds.Add(UNIT_U009_CARRION_BEETLE_L5);
        SummonIds.Add(UNIT_U00A_CARRION_BEETLE_L6);
        SummonIds.Add(UNIT_U00B_CARRION_BEETLE_L7);
        SummonIds.Add(UNIT_U00C_CARRION_BEETLE_L8);
        SummonIds.Add(UNIT_U00D_CARRION_BEETLE_L9);
        SummonIds.Add(UNIT_U007_CARRION_BEETLE_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_AOSW_SERPENT_WARD_Q);
        AbilityIds.Add(ABILITY_A01Y_SERPENT_WARD_W);
        AbilityIds.Add(ABILITY_A01Z_SERPENT_WARD_E);
        AbilityIds.Add(ABILITY_A020_SERPENT_WARD_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NFAC_POCKET_FACTORY_L1);
        SummonIds.Add(UNIT_NFA1_POCKET_FACTORY_L2);
        SummonIds.Add(UNIT_NFA2_POCKET_FACTORY_L3);
        SummonIds.Add(UNIT_N01Z_POCKET_FACTORY_L4);
        SummonIds.Add(UNIT_N020_POCKET_FACTORY_L5);
        SummonIds.Add(UNIT_N021_POCKET_FACTORY_L6);
        SummonIds.Add(UNIT_N022_POCKET_FACTORY_L7);
        SummonIds.Add(UNIT_N023_POCKET_FACTORY_L8);
        SummonIds.Add(UNIT_N024_POCKET_FACTORY_L9);
        SummonIds.Add(UNIT_N01Y_ULTRA_POCKET_FACTORY_L10);
        SummonIds.Add(UNIT_NCGB_CLOCKWERK_GOBLIN_L1);
        SummonIds.Add(UNIT_NCG1_CLOCKWERK_GOBLIN_L2);
        SummonIds.Add(UNIT_NCG2_CLOCKWERK_GOBLIN_L3);
        SummonIds.Add(UNIT_N027_CLOCKWERK_GOBLIN_L4);
        SummonIds.Add(UNIT_N028_CLOCKWERK_GOBLIN_L5);
        SummonIds.Add(UNIT_N029_CLOCKWERK_GOBLIN_L6);
        SummonIds.Add(UNIT_N02A_CLOCKWERK_GOBLIN_L7);
        SummonIds.Add(UNIT_N02B_CLOCKWERK_GOBLIN_L8);
        SummonIds.Add(UNIT_N025_CLOCKWERK_GOBLIN_L9);
        SummonIds.Add(UNIT_N026_CLOCKWERK_GOBLIN_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0AO_POCKET_FACTORY_Q);
        AbilityIds.Add(ABILITY_A0FM_POCKET_FACTORY_W);
        AbilityIds.Add(ABILITY_A0FN_POCKET_FACTORY_E);
        AbilityIds.Add(ABILITY_A0FO_POCKET_FACTORY_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NOWL_OWL_SCOUT_L1);
        SummonIds.Add(UNIT_NOW2_OWL_SCOUT_L2);
        SummonIds.Add(UNIT_NOW3_OWL_SCOUT_L3);
        SummonIds.Add(UNIT_N00Q_OWL_SCOUT_L4);
        SummonIds.Add(UNIT_N00R_OWL_SCOUT_L5);
        SummonIds.Add(UNIT_N00S_OWL_SCOUT_L6);
        SummonIds.Add(UNIT_N00T_OWL_SCOUT_L7);
        SummonIds.Add(UNIT_N00U_OWL_SCOUT_L8);
        SummonIds.Add(UNIT_N00V_OWL_SCOUT_L9);
        SummonIds.Add(UNIT_N00P_OWL_SCOUT_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_AEST_SCOUT_Q);
        AbilityIds.Add(ABILITY_A03T_SCOUT_W);
        AbilityIds.Add(ABILITY_A03U_SCOUT_E);
        AbilityIds.Add(ABILITY_A03V_SCOUT_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NDR1_LESSER_DARK_MINION_L1);
        SummonIds.Add(UNIT_NDR2_DARK_MINION_L2);
        SummonIds.Add(UNIT_NDR3_GREATER_DARK_MINION_L3);
        SummonIds.Add(UNIT_N013_GREATER_DARK_MINION_L4);
        SummonIds.Add(UNIT_N014_GREATER_DARK_MINION_L5);
        SummonIds.Add(UNIT_N015_GREATER_DARK_MINION_L6);
        SummonIds.Add(UNIT_N016_GREATER_DARK_MINION_L7);
        SummonIds.Add(UNIT_N017_GREATER_DARK_MINION_L8);
        SummonIds.Add(UNIT_N018_GREATER_DARK_MINION_L9);
        SummonIds.Add(UNIT_N012_GREATER_DARK_MINION_L10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0NT_BOOK_OF_THE_DEAD_X);
        AbilityIds.Add(ABILITY_ANBA_BLACK_ARROW_Q);
        AbilityIds.Add(ABILITY_A04B_BLACK_ARROW_W);
        AbilityIds.Add(ABILITY_A04C_BLACK_ARROW_E);
        AbilityIds.Add(ABILITY_A04D_BLACK_ARROW_R);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_UBDR_SAPPHIRON_ACTIVE_SUMMON);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0M7_SUMMON_SAPPHIRON_X);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_N01J_SEA_LEMONTAL);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0M0_SUMMON_SEA_LEMONTAL_X);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NBA2_LESSER_DOOMGUARD_L1);
        SummonIds.Add(UNIT_N01G_DOOM_GUARD_L2);
        SummonIds.Add(UNIT_N01H_GREATER_DOOM_GUARD_L3);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_ANDO_DOOM_T);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NINF_INFERNAL_L1);
        SummonIds.Add(UNIT_N01A_INFERNAL_L2);
        SummonIds.Add(UNIT_N019_INFERNAL_L3);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_AUIN_INFERNO_T);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_U006_FELHUNTER_LV1);
        SummonIds.Add(UNIT_U00E_FELHUNTER_LV2);
        SummonIds.Add(UNIT_U00F_FELHUNTER_LV3);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0HG_SUMMON_FELHUNTER_T);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_N003_LESSER_INFERNAL);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A05A_LESSER_INFERNO_Z_GUL_DAN);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_O013_SENTRY_GUN);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0HU_SENTRY_GUN_Z_Z);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_HPHX_PHOENIX_L1);
        SummonIds.Add(UNIT_H00E_PHOENIX_L2);
        SummonIds.Add(UNIT_H00D_PHOENIX_L3);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_AHPX_PHOENIX_T);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_ESPV_AVATAR_OF_VENGEANCE_L1);
        SummonIds.Add(UNIT_E009_AVATAR_OF_VENGEANCE_L2);
        SummonIds.Add(UNIT_E008_AVATAR_OF_VENGEANCE_L3);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_AESV_VENGEANCE_T);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_EVEN_SPIRIT_OF_VENGEANCE_SUMMON);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_AVNG_SPIRIT_OF_VENGEANCE_LV1);
        AbilityIds.Add(ABILITY_A0KZ_SPIRIT_OF_VENGEANCE_LV2);
        AbilityIds.Add(ABILITY_A0KY_SPIRIT_OF_VENGEANCE_LV3);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, true);

        SummonIds.Clear();
        SummonIds.Add(UNIT_N02D_SKELETON_ARCHER_BOOK_OF_THE_DEAD);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0NT_BOOK_OF_THE_DEAD_X);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_N01Q_TREANT_OF_THE_DEAD);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0JO_TREANT_OF_THE_DEAD_Z);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NCFS_WALTERY_MINION_L1);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0L8_PARASITE_Z_VASHJ);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NBDO_DRAGONSPAWN_OVERSEER);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0DV_SUMMON_OVERSEER);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NSSP_SPITTING_SPIDER);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0CE_SUMMON_SPITTING_SPIDER_BUFFED_6_14_2025);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NMCF_MUR_GUL_SNAGGLETOOTH);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A097_STRENGTH_IN_NUMBERS_MUR_GUL);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_N01W_BIG_FRENCH_RAT);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0AM_RATATOUILLE_FRENCH_COOKBOOK);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_NHYH_HYDRA_HATCHLING_SUMMONABLE);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A093_SUMMON_HYDRA_HATCHLING_SUMMON_STAFF);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_N02H_SHADOW_OOZE_SHADOW_ORB);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0BS_ORB_OF_OOZE);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_N01P_GROUNDED_FROST_WYRM_GROUND_WYRM);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A09Y_SUMMON_FROST_WYRM_NERFED_6_14_2025);
        new SummonScalar(SummonIds, AbilityIds,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f, 0.1f, 0.02f,
            0.1f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_E003_OWL_COMPANION_N_N_LEVEL_1);
        SummonIds.Add(UNIT_E000_OWL_COMPANION_N_N_LEVEL_2);
        SummonIds.Add(UNIT_E007_OWL_COMPANION_N_N_LEVEL_3);
        SummonIds.Add(UNIT_E00H_OWL_COMPANION_N_N_LEVEL_4);
        SummonIds.Add(UNIT_E00I_OWL_COMPANION_N_N_LEVEL_5);
        SummonIds.Add(UNIT_E00J_OWL_COMPANION_N_N_LEVEL_6);
        SummonIds.Add(UNIT_E00K_OWL_COMPANION_N_N_LEVEL_7);
        SummonIds.Add(UNIT_E00L_OWL_COMPANION_N_N_LEVEL_8);
        SummonIds.Add(UNIT_E00M_OWL_COMPANION_N_N_LEVEL_9);
        SummonIds.Add(UNIT_E00N_OWL_COMPANION_N_N_LEVEL_10);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0EQ_OWL_COMPANION_Z_TYRANDE);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);

        SummonIds.Clear();
        SummonIds.Add(UNIT_N000_ZOMBIE_DARK_CONVERSION);
        AbilityIds.Clear();
        AbilityIds.Add(ABILITY_A0CY_DARK_CONVERSION_Z_TICHONDRIUS);
        new SummonScalar(SummonIds, AbilityIds,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f, 0.25f, 0.02f,
            0.25f, 0.02f);
    }

    private static void SummonScaling()
    {
        if (GetSummoningUnit().Owner != player.Create(PLAYER_NEUTRAL_AGGRESSIVE))
        {
            ScaleSummon(GetSummoningUnit(), GetSummonedUnit());
        }
    }

    private static void HeroSummons()
    {
        if (ConditionFilter.Contains(GetTriggerPlayer()))
        {
            unit SummonedUnit = GetSummonedUnit();
            // Goblin Landmines
            if (SummonedUnit.UnitType == UNIT_N005_GOBLIN_LAND_MINE_L1)
            {
                for (int i = 1; i <= GetSummoningUnit().Level / 2; i++)
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        SummonGoblinMine(SummonedUnit.UnitType);
                    }
                }

                return;
            }

            if (SummonedUnit.UnitType == UNIT_N01R_GOBLIN_LAND_MINE_L2)
            {
                for (int i = 1; i <= GetSummoningUnit().Level / 2; i++)
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        SummonGoblinMine(SummonedUnit.UnitType);
                    }
                }

                return;
            }

            // Mirror image bounty/transport because of blasting wangs
            if (UnitHasBuffBJ(SummonedUnit, FourCC("BOmi")))
            {
                SummonedUnit.AddType(UNIT_TYPE_SAPPER);
                NegateBounty(SummonedUnit);
                return;
            }

            // Raise Dead bounty
            if (UnitHasBuffBJ(SummonedUnit, FourCC("BUan")))
            {
                NegateBounty(SummonedUnit);
                return;
            }

            if (SummonedUnit.UnitType == UNIT_N02G_WEASEL_S_SLASH)
            {
                SummonedUnit.SetAbilityLevel(ABILITY_A0MJ_DAMAGE_AURA_WEASEL_S_SLASH, SummonedUnit.Level / 2);
            }
        }
    }

    private static void SummonGoblinMine(int UnitId)
    {
        location RandomPoint = PolarProjectionBJ(GetUnitLoc(GetSummonedUnit()), GetRandomReal(100.00f, 500.00f), GetRandomDirectionDeg());
        unit Mine = unit.Create(GetTriggerPlayer(), UnitId, RandomPoint.X, RandomPoint.Y, GetRandomDirectionDeg());
        Mine.ApplyTimedLife(FourCC("BTLF"), 240.0f);
        RandomPoint.Dispose();
    }

    public static void ScaleSummon(unit Summoner, unit Summon)
    {
        UnitIdToSummon.TryGetValue(Summon.UnitType, out SummonScalar SummonStats);
        if (SummonStats != null)
        {
#if DEBUG
            Console.WriteLine($"{Summon.Name} summoned and found.");
#endif
            float ItemBaseDamage = 0.0f;
            float ItemAttackSpeed = 0.0f;
            float ItemHealthRegen = 0.0f;
            float ItemManaRegen = 0.0f;
            HeroData.ByPlayer.TryGetValue(Summon.Owner, out Hero SummonerHero);
            if (SummonerHero != null)
            {
                ItemBaseDamage = SummonerHero.ItemMods.BaseDamage;
                ItemAttackSpeed = SummonerHero.ItemMods.AttackSpeed;
                ItemHealthRegen = SummonerHero.ItemMods.HealthRegen;
                ItemManaRegen = SummonerHero.ItemMods.ManaRegen;
                if (SummonStats.bUsePlayerAsSummoner)
                {
                    Summoner = SummonerHero.Unit;
                }
            }

            int Level = 1;
            foreach (int AbilityId in SummonStats.SourceAbilities)
            {
                Level = Summoner.GetAbilityLevel(AbilityId);
                if (Level > 0)
                {
                    break;
                }
            }

            // Primary attribute increases base damage. Add average of dice.
            float BaseDamage = (Summoner.AttackBaseDamage1 + ((Summoner.AttackDiceSides1 / 2.0f) * Summoner.AttackDiceNumber1)) + ItemBaseDamage;

            // Attributes do not affect these.
            float LifeRegen = Summoner.HitPointsRegeneration + (Summoner.Strength * 0.05f) + ItemHealthRegen;
            float ManaRegen = Summoner.ManaRegeneration + (Summoner.Intelligence * 0.1f) + ItemManaRegen;
            float AttackSpeedRaw = ((Summoner.Agility * 2.0f) + ItemAttackSpeed) * (SummonStats.AttackSpeedScalar + (SummonStats.AttackSpeedScalarPerLevel * Level));
            float AttackSpeed = 1.0f + (AttackSpeedRaw / 100.0f);
            float BaseSpeed1 = Summon.AttackCooldown1;
            float BaseSpeed2 = Summon.AttackCooldown2;

            Summon.MaxLife += (int)Math.Round(Summoner.MaxLife * (SummonStats.LifeScalar + (SummonStats.LifeScalarPerLevel * Level)));
            Summon.Life = Summon.MaxLife;
            Summon.HitPointsRegeneration += LifeRegen * (SummonStats.LifeRegenScalar + (SummonStats.LifeRegenScalarPerLevel * Level));
            Summon.MaxMana += (int)Math.Round(Summoner.MaxMana * (SummonStats.ManaScalar + (SummonStats.ManaScalarPerLevel * Level)));
            Summon.Mana = Summon.MaxMana;
            Summon.ManaRegeneration += ManaRegen * (SummonStats.ManaRegenScalar + (SummonStats.ManaRegenScalarPerLevel * Level));
            Summon.AttackBaseDamage1 += (int)Math.Round(BaseDamage * (SummonStats.DamageScalar + (SummonStats.DamageScalarPerLevel * Level)));
            Summon.AttackBaseDamage2 += (int)Math.Round(BaseDamage * (SummonStats.DamageScalar + (SummonStats.DamageScalarPerLevel * Level)));
            Summon.AttackCooldown1 = BaseSpeed1 / AttackSpeed;
            Summon.AttackCooldown2 = BaseSpeed2 / AttackSpeed;
            Summon.Armor += Summoner.Armor * (SummonStats.ArmourScalar + (SummonStats.ArmourScalarPerLevel * Level));
        }
    }
}

public class SummonScalar
{
    public float ArmourScalar;
    public float ArmourScalarPerLevel;
    public float AttackSpeedScalar;
    public float AttackSpeedScalarPerLevel;
    public bool bUsePlayerAsSummoner;
    public float DamageScalar;
    public float DamageScalarPerLevel;
    public float LifeRegenScalar;
    public float LifeRegenScalarPerLevel;
    public float LifeScalar;
    public float LifeScalarPerLevel;
    public float ManaRegenScalar;
    public float ManaRegenScalarPerLevel;
    public float ManaScalar;
    public float ManaScalarPerLevel;
    public List<int> SourceAbilities;

    public SummonScalar(List<int> unitIds, List<int> sourceAbilities,
        float lifeScalar, float lifeScalarPerLevel, float lifeRegenScalar, float lifeRegenScalarPerLevel,
        float manaScalar, float manaScalarPerLevel, float manaRegenScalar, float manaRegenScalarPerLevel,
        float damageScalar, float damageScalarPerLevel, float attackSpeedScalar, float attackSpeedScalarPerLevel,
        float armourScalar, float armourScalarPerLevel, bool bUsePlayerAsSummoner = false)
    {
        SourceAbilities = new List<int>();
        LifeScalar = lifeScalar;
        LifeScalarPerLevel = lifeScalarPerLevel;
        LifeRegenScalar = lifeRegenScalar;
        LifeRegenScalarPerLevel = lifeRegenScalarPerLevel;
        ManaScalar = manaScalar;
        ManaScalarPerLevel = manaScalarPerLevel;
        ManaRegenScalar = manaRegenScalar;
        ManaRegenScalarPerLevel = manaRegenScalarPerLevel;
        DamageScalar = damageScalar;
        DamageScalarPerLevel = damageScalarPerLevel;
        AttackSpeedScalar = attackSpeedScalar;
        AttackSpeedScalarPerLevel = attackSpeedScalarPerLevel;
        ArmourScalar = armourScalar;
        ArmourScalarPerLevel = armourScalarPerLevel;
        this.bUsePlayerAsSummoner = bUsePlayerAsSummoner;
        foreach (int unitId in unitIds)
        {
            Summons.UnitIdToSummon.Add(unitId, this);
        }
    }
}
