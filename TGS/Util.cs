using System;
using TGS.Creeps;
using WCSharp.Api;
using WCSharp.Api.Enums;
using static Constants;
using static WCSharp.Api.Common;

namespace TGS
{
    public static class Util
    {
        public static unit GetUnitAt(float X, float Y, float Tolerance = 64.0f)
        {
            rect Loc = rect.Create(X - Tolerance / 2, Y - Tolerance / 2, X + Tolerance / 2, Y + Tolerance / 2);
            group UnitGroup = group.Create();
            UnitGroup.EnumUnitsInRect(Loc);

            unit FoundUnit = UnitGroup.First;

            if (FoundUnit == null)
            {
                Console.WriteLine($"|cff000000ERROR:|r No unit found at |cffffff00X:|r {X:F1}, |cffffff00Y:|r {Y:F1}");
            }

            UnitGroup.Dispose();
            Loc.Dispose();
            return FoundUnit;
        }

#if DEBUG
    private static readonly trigger OnClickXYTrigger = trigger.Create();

    public static void OnClickXY()
    {
        TriggerRegisterPlayerUnitEvent(OnClickXYTrigger, player.Create(0), EVENT_PLAYER_UNIT_SELECTED, null);

        TriggerAddAction(OnClickXYTrigger, () =>
        {
            unit ClickedUnit = GetTriggerUnit();
            player TriggerPlayer = GetTriggerPlayer();

            float X = GetUnitX(ClickedUnit);
            float Y = GetUnitY(ClickedUnit);

            TriggerPlayer.DisplayTextTo($"|cffffcc00Clicked:|r {ClickedUnit.Name} at |cffffff00X:|r {X:F1}, |cffffff00Y:|r {Y:F1}");
            Globals.OutputBuffer.Add($" GetUnitAt({X:F1}f, {Y:F1}f) {ClickedUnit.Name}");
            if (Army.FactoryLookup[GetTriggerUnit()] != null)
            {
                TriggerPlayer.DisplayTextTo($"|cffffcc00Factory State:|r {Army.FactoryLookup[GetTriggerUnit()].State.ToString()}");
                foreach (FactorySpawn Spawn in Army.FactoryLookup[GetTriggerUnit()].SpawnedUnits)
                {
                    Console.WriteLine($"I spawn {Spawn.Count} {Spawn.UnitId.Id2String()}.");
                }
            }
        });
    }
#endif

        public enum ArmyForce
        {
            Alliance,
            Horde
        }

        public static float GetArmorSpellReduction(DefenseType InDefenseType)
        {
            switch (InDefenseType)
            {
                case DefenseType.Light:
                    return 0.5f;
                case DefenseType.Medium:
                    return 0.75f;
                case DefenseType.Large:
                    return 1.0f;
                case DefenseType.Fort:
                    return 1.0f;
                case DefenseType.Normal:
                    return 1.0f;
                case DefenseType.Hero:
                    return 0.5f;
                case DefenseType.Divine:
                    return 0.25f;
                case DefenseType.None:
                    return 0.5f;
            }

            return 1.0f;
        }

        private static string GetDamageTypeName(int DamageType)
        {
            return DamageType switch
            {
                0 => "UNKNOWN",
                4 => "NORMAL",
                5 => "ENHANCED",
                8 => "FIRE",
                9 => "COLD",
                10 => "LIGHTNING",
                11 => "POISON",
                12 => "DISEASE",
                13 => "DIVINE",
                14 => "MAGIC",
                15 => "SONIC",
                16 => "ACID",
                17 => "FORCE",
                18 => "DEATH",
                19 => "MIND",
                20 => "PLANT",
                21 => "DEFENSIVE",
                22 => "DEMOLITION",
                23 => "SLOW_POISON",
                24 => "SPIRIT_LINK",
                25 => "SHADOW_STRIKE",
                26 => "UNIVERSAL",
                _ => "CUSTOM/UNKNOWN(" + DamageType + ")"
            };
        }

        public static void NegateBounty(this unit InUnit)
        {
            InUnit.GoldBountyAwardedBase = 0;
            InUnit.GoldBountyAwardedNumberOfDice = 0;
            InUnit.GoldBountyAwardedSidesPerDie = 0;
            InUnit.LumberBountyAwardedBase = 0;
            InUnit.LumberBountyAwardedNumberOfDice = 0;
            InUnit.LumberBountyAwardedSidesPerDie = 0;
        }

        public static string Id2String(this int InFourCC)
        {
            char C1 = (char)((InFourCC >> 24) & 0xFF);
            char C2 = (char)((InFourCC >> 16) & 0xFF);
            char C3 = (char)((InFourCC >> 8) & 0xFF);
            char C4 = (char)(InFourCC & 0xFF);
            string FourCC = $"{C1}{C2}{C3}{C4}";
        
            return FourCC.ToUpper();
        }

        public static void SetShopState(this unit Shop, bool Enabled)
        {
            Shop.NeutralBuildingShowsMinimapIcon = Enabled;
            Shop.DisableAbility(ABILITY_ANEU_SELECT_HERO, !Enabled, !Enabled);
            if (Enabled)
            {
                if (Globals.ShopEffects.TryGetValue(Shop, out effect Fire))
                {
                    Fire.Scale = 0.01f;
                }
            }
            else
            {
                effect Fire = effect.Create(@"Environment\LargeBuildingFire\LargeBuildingFire1.mdl", Shop.X, Shop.Y);
                Globals.ShopEffects.Add(Shop, Fire);
            }
        }

        public class PlayerColor
        {
            public float R { get; init; }
            public float G { get; init; }
            public float B { get; init; }

            public PlayerColor(float r, float g, float b)
            {
                R = r;
                G = g;
                B = b;
            }
        }

        public static bool IsHumUnit(this unit KillingUnit)
        {
            return IsPlayerAlly(GetOwningPlayer(KillingUnit), Player(5));
        }

        public static bool IsTopCreepGroup(this unit TargetUnit)
        {
            Camp Camp = CreepCamp.GetCampForUnit(TargetUnit).Camp;
            switch (Camp)
            {
                case Camp.FrenchmansEast:
                case Camp.FrenchmansWest:
                case Camp.OcculordWest:
                case Camp.OcculordEast:
                case Camp.HeroTop:
                case Camp.GolemWest:
                case Camp.GolemEast:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsTopCreep(this unit TargetUnit)
        {
            return TargetUnit.IsTopCreepGroup();
        }

        public static bool IsMidCreepGroup(this unit TargetUnit)
        {
            Camp Camp = CreepCamp.GetCampForUnit(TargetUnit).Camp;
            switch (Camp)
            {
                case Camp.SiegeWest:
                case Camp.SiegeEast:
                case Camp.GateNorth:
                case Camp.GateSouth:
                case Camp.GoblinEast:
                case Camp.GoblinWest:
                case Camp.Urtle:
                case Camp.Crabnar:
                case Camp.PigeonWest:
                case Camp.PigeonEast:
                case Camp.HeroMid:
                case Camp.FurbolgFarSouth:
                case Camp.MoonkinEast:
                case Camp.MoonkinWest:
                case Camp.GnollsSoutheast:
                case Camp.GnollsSouthwest:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsMidCreep(this unit TargetUnit)
        {
            return TargetUnit.IsMidCreepGroup();
        }

        public static bool IsOrcSpiritwalker()
        {
            return GetFilterUnit().UnitType == UNIT_OSPW_SPIRIT_WALKER
                   && GetFilterUnit().Owner == player.Create(11)
                   && GetUnitState(GetFilterUnit(), UNIT_STATE_MANA) >= 150;
        }

        public static bool IsHumSorc()
        {
            return GetFilterUnit().UnitType == UNIT_HSOR_SORCERESS
                   && GetFilterUnit().Owner == player.Create(5)
                   && GetFilterUnit().Mana >= 110;
        }

        public static bool IsHumSpellbreaker()
        {
            return GetFilterUnit().UnitType == UNIT_HSPT_SUMMONER_UPDATED_FROM_SPELLBREAKER_6_14_2025
                   && GetFilterUnit().Owner == player.Create(5);
        }

        public static void EnumRadius(group InGroup, float X, float Y, float Radius, Func<bool> func)
        {
            boolexpr b = Filter(func);
            GroupEnumUnitsInRange(InGroup, X, Y, Radius, b);
            b.Dispose();
        }
    }
}
