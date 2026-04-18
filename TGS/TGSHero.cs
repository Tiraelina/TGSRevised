using System;
using System.Collections.Generic;
using TGS.Spells;
using WCSharp.Api;
using WCSharp.Events;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public class TGSHero
{
    public TGSHero(unit unit, player owner)
    {
        Unit = unit;
        Owner = owner;
        TGSAbilities.ByUnit[unit] = this;
        TGSAbilities.ByPlayer[owner] = this;
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

    private static TGSHero Get(unit InUnit)
    {
        return TGSAbilities.ByUnit[InUnit];
    }

    private static TGSHero Get(player InPlayer)
    {
        return TGSAbilities.ByPlayer[InPlayer];
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
                    foreach (OrbType Orb in itemData.OrbEffects)
                    {
                        ItemMods.OrbEffects.Add(Orb);
                        AddOrb(Orb, 0);
                        Console.WriteLine(Orb.ToString());
                    }
                }
            }
        }
        UpdateTooltips();
    }

    public void UpdateTooltips()
    {
        if (GetLocalPlayer() == Owner)
        {
            foreach (NormalAbility Ability in NormalAbilities)
            {
                if (Ability != null)
                {
                    Ability.UpdateTooltip(Ability, Unit);
                }
            }
            Special.UpdateTooltip(Special, Unit);
            Ultimate.UpdateTooltip(Ultimate, Unit);
        }
    }

    public void UpdateTooltip(NormalAbility Ability)
    {
        if (GetLocalPlayer() == Owner)
        {
            Ability.UpdateTooltip(Ability, Unit);
        }
    }

    public void UpdateTooltip(SpecialAbility Ability)
    {
        if (GetLocalPlayer() == Owner)
        {
            Special.UpdateTooltip(Special, Unit);
        }
    }

    public void UpdateTooltip(UltimateAbility Ability)
    {
        if (GetLocalPlayer() == Owner)
        {
            Ultimate.UpdateTooltip(Ultimate, Unit);
        }
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
        foreach (var Orb in ItemMods.OrbEffects)
        {
            RemoveOrb(Orb);
        }
        ItemMods.OrbEffects.Clear();
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

        TGSHero TGSHero = Get(GetTriggerUnit());
        if (TGSHero == null)
        {
            return;
        }

        int ItemId = GetManipulatedItem().TypeId;
#if DEBUG
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, $"Picked up item ID: {ItemId} (hex {(uint)ItemId:X8}) | Registered normals: {NormalAbility.GetByItemId(ItemId).MaxLevel}");
#endif

        if (NormalAbility.GetByItemId(ItemId) is { } Normal)
        {
            Normal.AddToHero(TGSHero);
        }
        else if (SpecialAbility.GetByItemId(ItemId) is { } Special)
        {
            Special.AddToHero(TGSHero);
        }
        else if (UltimateAbility.GetByItemId(ItemId) is { } Ultimate)
        {
            Ultimate.AddToHero(TGSHero);
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
            case OrbType.BlackArrow:
            {
                BlackArrow Orb = new();
                Orb.Aquire(Unit, InOrbLevel);
                Orbs.Add(Orb);
                OrbLookup.Add(InOrbType, Orb);
                break;
            }
            case OrbType.Ooze:
            {
                Ooze Orb = new();
                Orb.Aquire(Unit, InOrbLevel);
                Orbs.Add(Orb);
                OrbLookup.Add(InOrbType, Orb);
                break;
            }
            // case OrbType.Corruption:
            // {
            //     Corruption Orb = new();
            //     Orb.Aquire(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Fire:
            // {
            //     Fire Orb = new();
            //     Orb.Aquire(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Slow:
            // {
            //     Slow Orb = new();
            //     Orb.Aquire(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Poison:
            // {
            //     Poison Orb = new();
            //     Orb.Aquire(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            // case OrbType.Incinerate:
            // {
            //     Incinerate Orb = new();
            //     Orb.Aquire(Unit, InOrbLevel);
            //     Orbs.Add(Orb);
            //     OrbLookup.Add(InOrbType, Orb);
            //     break;
            // }
            case OrbType.Pillage:
            {
                Pillage Orb = new();
                Orb.Aquire(Unit, InOrbLevel);
                Orbs.Add(Orb);
                OrbLookup.Add(InOrbType, Orb);
                break;
            }
            // case OrbType.Purge:
            // {
            //     Purge Orb = new();
            //     Orb.Aquire(Unit, InOrbLevel);
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
        TGSHero TGSHero = Get(GetTriggerPlayer());
        if (TGSHero == null)
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

        switch (Letter)
        {
            case "Q" when TGSHero.NormalAbilities[0] != null:
                UnlearnNormal(TGSHero, 0, TGSHero.NormalAbilities[0]);
                break;
            case "W" when TGSHero.NormalAbilities[1] != null:
                UnlearnNormal(TGSHero, 1, TGSHero.NormalAbilities[1]);
                break;
            case "E" when TGSHero.NormalAbilities[2] != null:
                UnlearnNormal(TGSHero, 2, TGSHero.NormalAbilities[2]);
                break;
            case "R" when TGSHero.NormalAbilities[3] != null:
                UnlearnNormal(TGSHero, 3, TGSHero.NormalAbilities[3]);
                break;
            case "X" when TGSHero.Special != null:
                UnlearnSpecial(TGSHero, TGSHero.Special);
                break;
            case "T" when TGSHero.Ultimate != null:
                UnlearnUltimate(TGSHero, TGSHero.Ultimate);
                break;
            case "Z":
                GetTriggerPlayer().DisplayTextTo("You can't unlearn your innate skill, Tony.");
                break;
        }
    }

    private static void UnlearnNormal(TGSHero InTGSHero, int InSlot, NormalAbility InAbility)
    {
        InTGSHero.Owner.Lumber += NormalAbility.LumberCost - 1;
        InTGSHero.Unit.RemoveAbility(InAbility.AbilityIds[InSlot]);
        if (InTGSHero.NormalAbilities[InSlot].OrbType != OrbType.None)
        {
            InTGSHero.RemoveOrb(InTGSHero.NormalAbilities[InSlot].OrbType);
        }

        if (InAbility.AbilityIds[InSlot] == Constants.ABILITY_A03E_CLEAVING_ATTACK_Q
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A03F_CLEAVING_ATTACK_W
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A03G_CLEAVING_ATTACK_E
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A05L_CLEAVING_ATTACK_R)
        {
            InTGSHero.CleaveMultiTargets = 0;
            InTGSHero.AttackMultiTargets = InTGSHero.ItemMods.CleaveCount;
        }

        if (InAbility.AbilityIds[InSlot] == Constants.ABILITY_A001_CRITICAL_STRIKE_Q
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A0J7_CRITICAL_STRIKE_W
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A0OC_CRITICAL_STRIKE_E
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A0OD_CRITICAL_STRIKE_R)
        {
            InTGSHero.CritChanceCrit = 0.0f;
            InTGSHero.AttackCritChance = InTGSHero.CritChanceBrawler;
        }

        if (InAbility.AbilityIds[InSlot] == Constants.ABILITY_A04H_DRUNKEN_BRAWLER_Q
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A04I_DRUNKEN_BRAWLER_W
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A04J_DRUNKEN_BRAWLER_E
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A0CG_DRUNKEN_BRAWLER_R)
        {
            InTGSHero.CritChanceBrawler = 0.0f;
            InTGSHero.EvasionBrawler = 0.0f;
            InTGSHero.AttackCritChance = InTGSHero.CritChanceCrit;
            InTGSHero.AttackEvasionChance = InTGSHero.EvasionEvade + InTGSHero.ItemMods.EvasionChance;
        }

        if (InAbility.AbilityIds[InSlot] == Constants.ABILITY_A01D_EVASION_Q
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A01E_EVASION_W
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A01F_EVASION_E
            || InAbility.AbilityIds[InSlot] == Constants.ABILITY_A0OE_EVASION_R)
        {
            InTGSHero.EvasionEvade = 0.0f;
            InTGSHero.AttackEvasionChance = InTGSHero.EvasionBrawler + InTGSHero.ItemMods.EvasionChance;
        }
        InTGSHero.NormalAbilities[InSlot] = null;
        UnlearnMessage(InTGSHero, InAbility.Name);

    }

    private static void UnlearnSpecial(TGSHero InTGSHero, SpecialAbility InAbility)
    {
        InTGSHero.Owner.Lumber += SpecialAbility.LumberCost - 1;
        InTGSHero.Unit.RemoveAbility(InAbility.AbilityId);
        InTGSHero.Special = null;
        UnlearnMessage(InTGSHero, InAbility.Name);
    }

    private static void UnlearnUltimate(TGSHero InTGSHero, UltimateAbility InAbility)
    {
        if (InAbility.Name == "Mass Teleport")
        {
            InTGSHero.Unit.RemoveAbility(FourCC("AHm2"));
            InTGSHero.Unit.RemoveAbility(FourCC("AHm3"));
        }

        InTGSHero.Owner.Lumber += UltimateAbility.LumberCost - 1;
        InTGSHero.Unit.RemoveAbility(InAbility.AbilityId);
        InTGSHero.Ultimate = null;
        UnlearnMessage(InTGSHero, InAbility.Name);
    }

    private static void UnlearnMessage(TGSHero InTGSHero, string InName)
    {
        string Msg = $"{GetPlayerName(InTGSHero.Owner)} unlearned |cffff8000{InName}|r";
        if (GetLocalPlayer() == InTGSHero.Owner || IsPlayerAlly(GetLocalPlayer(), InTGSHero.Owner))
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

        TGSHero LevelingTGSHero = Get(GetTriggerPlayer());
        if (LevelingTGSHero != null)
        {
            SpecialAbility SpecialAbil = Get(GetTriggerPlayer()).Special;
            if (SpecialAbil != null)
            {
                GetTriggerUnit().SetAbilityLevel(SpecialAbil.AbilityId, GetLevelingUnit().Level / 2);
            }
        }

        TGSAbilities.HeroToBaseAbility.TryGetValue(GetTriggerUnit().UnitType, out int Value);
        SetUnitAbilityLevelSwapped(Value, GetTriggerUnit(), GetLevelingUnit().Level / 2);
        if (TGSAbilities.HeroToBaseAbility[GetTriggerUnit().UnitType] == FourCC("A0LD"))
        {
            GetTriggerUnit().SetAbilityLevel(FourCC("A0KA"), GetLevelingUnit().Level / 2);
        }
    }
}