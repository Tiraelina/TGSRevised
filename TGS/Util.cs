using System;
using TGS.Creeps;
using WCSharp.Api;
using WCSharp.Api.Enums;
using static WCSharp.Api.Common;

namespace TGS;

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
            if (Army.FactoryLookup[GetTriggerUnit()] != null)
            {
                TriggerPlayer.DisplayTextTo($"|cffffcc00Factory State:|r {Army.FactoryLookup[GetTriggerUnit()].State.ToString()}");
                foreach (FactorySpawn Spawn in Army.FactoryLookup[GetTriggerUnit()].SpawnedUnits)
                {
                    Console.WriteLine($"I spawn {Spawn.Count} {Id2String(Spawn.UnitId)}.");
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

    public enum TagType
    {
        Normal,
        Crit,
        Miss,
        Evade,
        Spell,
        SpellCrit,
        Heal,
        Cleave,
        CleaveCrit
    }

    public static void MakeTag(float EventDamage, unit DamageTarget, TagType Type)
    {
        texttag DamageTag = texttag.Create();
        string DamageText;
        DamageTag.SetPermanent(false);
        DamageTag.SetLifespan(0.75f);
        DamageTag.SetFadepoint(0.55f);
        DamageTag.SetVisibility(false);
        var LocalPlayer = GetLocalPlayer();
        if (DamageTarget.IsVisibleTo(LocalPlayer))
        {
            DamageTag.SetVisibility(true);
        }

        switch (Type)
        {
            case TagType.Normal:
                DamageText = $"{EventDamage:F}";
                DamageTag.SetColor(255, 255, 255);
                DamageTag.SetVelocity(0.0f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Crit:
                DamageText = $"{EventDamage:F}!";
                DamageTag.SetColor(255, 0, 0);
                DamageTag.SetVelocity(0.04f, 0.06f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Miss:
                DamageText = $"Miss";
                DamageTag.SetColor(255, 147, 0);
                DamageTag.SetVelocity(-0.04f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Evade:
                DamageText = $"Evade";
                DamageTag.SetColor(255, 255, 0);
                DamageTag.SetVelocity(-0.04f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Spell:
                DamageText = $"{EventDamage:F}";
                DamageTag.SetColor(0, 150, 255);
                DamageTag.SetVelocity(0.0f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.SpellCrit:
                DamageText = $"{EventDamage:F}!";
                DamageTag.SetColor(255, 150, 0);
                DamageTag.SetVelocity(0.04f, 0.06f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Heal:
                DamageText = $"{EventDamage:F}";
                DamageTag.SetColor(0, 255, 0);
                DamageTag.SetVelocity(0.0f, -0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Cleave:
                DamageText = $"{EventDamage:F}";
                DamageTag.SetColor(128, 128, 128);
                DamageTag.SetVelocity(0.0f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.CleaveCrit:
                DamageText = $"{EventDamage:F}!";
                DamageTag.SetColor(128, 0, 0);
                DamageTag.SetVelocity(0.04f, 0.06f);
                DamageTag.SetText(DamageText, 0.012f);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type, null);
        }
        DamageTag.SetPosition(DamageTarget.X-(DamageText.Length * 10), DamageTarget.Y, DamageTarget.Z+DamageTarget.FlyHeight);

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

    public static void NegateBounty(unit InUnit)
    {
        InUnit.GoldBountyAwardedBase = 0;
        InUnit.GoldBountyAwardedNumberOfDice = 0;
        InUnit.GoldBountyAwardedSidesPerDie = 0;
        InUnit.LumberBountyAwardedBase = 0;
        InUnit.LumberBountyAwardedNumberOfDice = 0;
        InUnit.LumberBountyAwardedSidesPerDie = 0;
    }

    public static string Id2String(int id)
    {
        char[] FourCC = new char[4];
        FourCC[0] = (char)((id >> 24) & 0xFF);
        FourCC[1] = (char)((id >> 16) & 0xFF);
        FourCC[2] = (char)((id >> 8) & 0xFF);
        FourCC[3] = (char)(id & 0xFF);
        return new string(FourCC).TrimEnd('\0');
    }

    public static void SetShopState(unit Shop, bool Enabled)
    {
        Shop.NeutralBuildingShowsMinimapIcon = Enabled;
        Shop.DisableAbility(FourCC("Aneu"), !Enabled, !Enabled);
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

    public struct PlayerColor
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

    public static bool IsHumUnit(unit KillingUnit)
    {
        return IsPlayerAlly(GetOwningPlayer(KillingUnit), Player(5));
    }

    public static bool IsTopCreepGroup(unit TargetUnit)
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

    public static bool IsTopCreep(unit TargetUnit)
    {
        return IsTopCreepGroup(TargetUnit);
    }

    public static bool IsMidCreepGroup(unit TargetUnit)
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

    public static bool IsMidCreep(unit TargetUnit)
    {
        return IsMidCreepGroup(TargetUnit);
    }

    public static bool IsOrcSpiritwalker()
    {
        return GetFilterUnit().UnitType == FourCC("ospw")
               && GetFilterUnit().Owner == player.Create(11)
               && GetUnitState(GetFilterUnit(), UNIT_STATE_MANA) >= 150;
    }

    public static bool IsHumSorc()
    {
        return GetFilterUnit().UnitType == FourCC("hsor")
               && GetFilterUnit().Owner == player.Create(5)
               && GetFilterUnit().Mana >= 110;
    }

    public static bool IsHumSpellbreaker()
    {
        return GetFilterUnit().UnitType == FourCC("hspt")
               && GetFilterUnit().Owner == player.Create(5);
    }

    public static void EnumRadius(group InGroup, float X, float Y, float Radius, Func<bool> func)
    {
        boolexpr b = Filter(func);
        GroupEnumUnitsInRange(InGroup, X, Y, Radius, b);
        b.Dispose();
    }
}
