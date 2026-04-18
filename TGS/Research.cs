using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Events;
using static TGS.Globals;
using static Constants;
using static TGS.Util;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public static class Research
{
    public static readonly Dictionary<int, ResearchTech> Entries = new();
    public static int HumResearchMax { get; set; } = 86;
    public static int HumResearchCurrent { get; set; } = 0;
    public static int HumResearch { get; set; } = 0;
    public static int OrcResearchMax { get; set; } = 185;
    public static int OrcResearchCurrent { get; set; } = 100;
    public static int OrcResearch { get; set; } = 0;

    public static void Init()
    {
        trigger ResearchTick = trigger.Create();
        TriggerRegisterTimerEvent(ResearchTick, 5.0f, true);
        TriggerAddAction(ResearchTick, ResearchTickAction);

        PlayerUnitEvents.Register(UnitTypeEvent.Dies, ResearchKillGain);

        // === Alliance Upgrades ===
        new ResearchTech(2, UPGRADE_RHME_MITHRIL_FORGED_SWORDS_III, 1, "Iron Forged Swords");
        new ResearchTech(11, UPGRADE_RHME_MITHRIL_FORGED_SWORDS_III, 2, "Steel Forged");
        new ResearchTech(20, UPGRADE_RHME_MITHRIL_FORGED_SWORDS_III, 3, "Mithril Forged Swords");
        new ResearchTech(30, UPGRADE_RHME_MITHRIL_FORGED_SWORDS_III, 4, "Mithril Forged Swords");
        new ResearchTech(32, UPGRADE_RHME_MITHRIL_FORGED_SWORDS_III, 5, "Mithril Forged Swords");
        new ResearchTech(34, UPGRADE_RHME_MITHRIL_FORGED_SWORDS_III, 6, "Mithril Forged Swords");

        new ResearchTech(3, UPGRADE_RHAR_IRON_PLATING, 1, "Iron Plating");
        new ResearchTech(12, UPGRADE_RHAR_IRON_PLATING, 2, "Steel Plating");
        new ResearchTech(21, UPGRADE_RHAR_IRON_PLATING, 3, "Mithril Plating");

        new ResearchTech(4, UPGRADE_RHRA_IMBUED_GUNPOWDER_III, 1, "Black Gunpowder");
        new ResearchTech(13, UPGRADE_RHRA_IMBUED_GUNPOWDER_III, 2, "Refined Gunpowder");
        new ResearchTech(22, UPGRADE_RHRA_IMBUED_GUNPOWDER_III, 3, "Imbued Gunpowder");
        new ResearchTech(31, UPGRADE_RHRA_IMBUED_GUNPOWDER_III, 4, "Imbued Gunpowder I");
        new ResearchTech(33, UPGRADE_RHRA_IMBUED_GUNPOWDER_III, 5, "Imbued Gunpowder II");
        new ResearchTech(35, UPGRADE_RHRA_IMBUED_GUNPOWDER_III, 6, "Imbued Gunpowder III");

        new ResearchTech(5, UPGRADE_RHLA_STUDDED_LEATHER_ARMOR, 1, "Studded Leather Armour");
        new ResearchTech(14, UPGRADE_RHLA_STUDDED_LEATHER_ARMOR, 2, "Reinforced Leather Armour");
        new ResearchTech(23, UPGRADE_RHLA_STUDDED_LEATHER_ARMOR, 3, "Dragonhide Armour");

        new ResearchTech(1, UPGRADE_RHDE_DEFEND, 1, "Defend");
        new ResearchTech(7, UPGRADE_RHRI_LONG_RIFLES, 1, "Long Rifles");
        new ResearchTech(25, UPGRADE_RHAN_ANIMAL_WAR_TRAINING, 1, "Animal War Training");
        new ResearchTech(10, UPGRADE_RHHB_STORM_HAMMERS, 1, "Storm Hammers");
        new ResearchTech(16, UPGRADE_RHCD_CLOUD, 1, "Cloud");
        new ResearchTech(27, UPGRADE_RHRT_BARRAGE, 1, "Barrage");
        new ResearchTech(28, UPGRADE_RHGB_FLYING_MACHINE_BOMBS, 1, "Flying Machine Bombs");
        new ResearchTech(26, UPGRADE_RHFS_FRAGMENTATION_SHARDS, 1, "Fragmentation Shards");
        new ResearchTech(29, UPGRADE_RHFC_FLAK_CANNONS, 1, "Flak Cannons");

        new ResearchTech(8, UPGRADE_RHPT_PRIEST_MASTER_TRAINING, 1, "Priest Adept Training");
        new ResearchTech(17, UPGRADE_RHPT_PRIEST_MASTER_TRAINING, 2, "Priest Master Training");
        new ResearchTech(9, UPGRADE_RHST_SORCERESS_MASTER_TRAINING, 1, "Sorceress Adept Training");
        new ResearchTech(19, UPGRADE_RHST_SORCERESS_MASTER_TRAINING, 2, "Sorceress Master Training");
        new ResearchTech(18, UPGRADE_RHSS_CONTROL_MAGIC, 1, "Control Magic");

        new ResearchTech(6, UPGRADE_RHAC_IMPROVED_MASONRY, 1, "Improved Masonry");
        new ResearchTech(15, UPGRADE_RHAC_IMPROVED_MASONRY, 2, "Advanced Masonry");
        new ResearchTech(24, UPGRADE_RHAC_IMPROVED_MASONRY, 3, "Imbued Masonry");

        new ResearchTech(36, UPGRADE_R002_DEPRESSION_AURA, 1, "Depression Aura");
        for (int i = 37; i <= 87; i++)
        {
            new ResearchTech(i, UPGRADE_R001_DEMOLISH, Math.Min(i - 36, 50), "Demolish");
        }

        // === Horde Upgrades ===
        new ResearchTech(103, UPGRADE_ROME_ARCANITE_MELEE_WEAPONS_III, 1, "Steel Melee Weapons");
        new ResearchTech(111, UPGRADE_ROME_ARCANITE_MELEE_WEAPONS_III, 2, "Thorium Melee Weapons");
        new ResearchTech(120, UPGRADE_ROME_ARCANITE_MELEE_WEAPONS_III, 3, "Arcanite Melee Weapons");
        new ResearchTech(129, UPGRADE_ROME_ARCANITE_MELEE_WEAPONS_III, 4, "Arcanite Melee Weapons");
        new ResearchTech(131, UPGRADE_ROME_ARCANITE_MELEE_WEAPONS_III, 5, "Arcanite Melee Weapons");
        new ResearchTech(133, UPGRADE_ROME_ARCANITE_MELEE_WEAPONS_III, 6, "Arcanite Melee Weapons");

        new ResearchTech(105, UPGRADE_RORA_ARCANITE_RANGED_WEAPONS_III, 1, "Steel Ranged Weapons");
        new ResearchTech(113, UPGRADE_RORA_ARCANITE_RANGED_WEAPONS_III, 2, "Thorium Ranged Weapons");
        new ResearchTech(122, UPGRADE_RORA_ARCANITE_RANGED_WEAPONS_III, 3, "Arcanite Ranged Weapons");
        new ResearchTech(130, UPGRADE_RORA_ARCANITE_RANGED_WEAPONS_III, 4, "Arcanite Ranged Weapons");
        new ResearchTech(132, UPGRADE_RORA_ARCANITE_RANGED_WEAPONS_III, 5, "Arcanite Ranged Weapons");
        new ResearchTech(134, UPGRADE_RORA_ARCANITE_RANGED_WEAPONS_III, 6, "Arcanite Ranged Weapons");

        new ResearchTech(104, UPGRADE_ROAR_STEEL_ARMOR, 1, "Steel Armour");
        new ResearchTech(112, UPGRADE_ROAR_STEEL_ARMOR, 2, "Thorium Armour");
        new ResearchTech(121, UPGRADE_ROAR_STEEL_ARMOR, 3, "Arcanite Armour");

        new ResearchTech(102, UPGRADE_ROBS_BRUTE_STRENGTH, 1, "Berserker Strength");
        new ResearchTech(126, UPGRADE_ROBK_BERSERKER_UPGRADE, 1, "Berserker Upgrade");
        new ResearchTech(101, UPGRADE_ROTR_TROLL_REGENERATION, 1, "Troll Regeneration");
        new ResearchTech(127, UPGRADE_ROBF_BURNING_OIL, 1, "Burning Oil");
        new ResearchTech(114, UPGRADE_ROVS_ENVENOMED_SPEARS, 1, "Envenomed Spears");
        new ResearchTech(107, UPGRADE_ROEN_ENSNARE, 1, "Ensnare");
        new ResearchTech(116, UPGRADE_ROWS_PULVERIZE, 1, "Pulverize");
        new ResearchTech(128, UPGRADE_ROLF_LIQUID_FIRE, 1, "Liquid Fire");
        new ResearchTech(125, UPGRADE_RWDM_WAR_DRUMS_DAMAGE_INCREASE, 1, "War Drums Damage Increase");

        new ResearchTech(108, UPGRADE_ROST_SHAMAN_ADEPT_TRAINING, 1, "Shaman Adept Training");
        new ResearchTech(117, UPGRADE_ROST_SHAMAN_ADEPT_TRAINING, 2, "Shaman Master Training");
        new ResearchTech(109, UPGRADE_ROWD_WITCH_DOCTOR_ADEPT_TRAINING, 1, "Witch Doctor Adept Training");
        new ResearchTech(118, UPGRADE_ROWD_WITCH_DOCTOR_ADEPT_TRAINING, 2, "Witch Doctor Master Training");
        new ResearchTech(110, UPGRADE_ROWT_SPIRIT_WALKER_ADEPT_TRAINING, 1, "Spirit Walker Adept Training");
        new ResearchTech(119, UPGRADE_ROWT_SPIRIT_WALKER_ADEPT_TRAINING, 2, "Spirit Walker Master Training");

        new ResearchTech(106, UPGRADE_ROSP_SPIKED_BARRICADES, 1, "Spiked Barricades");
        new ResearchTech(115, UPGRADE_ROSP_SPIKED_BARRICADES, 2, "Improved Spiked Barricades");
        new ResearchTech(124, UPGRADE_ROSP_SPIKED_BARRICADES, 3, "Advanced Spiked Barricades");
        new ResearchTech(123, UPGRADE_RORB_REINFORCED_DEFENSES, 1, "Reinforced Defenses");

        new ResearchTech(135, UPGRADE_R002_DEPRESSION_AURA, 1, "Depression Aura");
        for (int i = 136; i <= 186; i++)
        {
            new ResearchTech(i, UPGRADE_R001_DEMOLISH, Math.Min(i - 135, 50), "Demolish");
        }
    }

    private static void ResearchTickAction()
    {
        if ((HumResearch += 5) >= 180 && HumResearchCurrent <= HumResearchMax)
        {
            ResearchTech.Upgrade(HumResearchCurrent += 1, ArmyForce.Alliance);
            HumResearch -= 180;
        }

        if ((OrcResearch += 5) >= 180 && OrcResearchCurrent <= OrcResearchMax)
        {
            ResearchTech.Upgrade(OrcResearchCurrent += 1, ArmyForce.Horde);
            OrcResearch -= 180;
        }

        string HumCurrentString;
        string HumNextString;
        string OrcCurrentString;
        string OrcNextString;
        ResearchTech HumCurrentTech = ResearchTech.LookUpBySlot(HumResearchCurrent);
        if (HumCurrentTech == null)
        {
            HumCurrentString = $"Current Tech: |cffff8000-|r [|cffffcc00Level -|r]|r|n";
        }
        else
        {
            HumCurrentString = $"Current Tech: |cffff8000{HumCurrentTech.UpgradeName}|r [|cffffcc00Level {HumCurrentTech.UpgradeLevel}|r]|r|n";
        }

        ResearchTech HumNextTech = ResearchTech.LookUpBySlot(HumResearchCurrent + 1);
        if (HumNextTech == null)
        {
            HumNextString = $"Next Tech: |cffff8000Max|r [|cffffcc00Level Max|r]";
        }
        else
        {
            HumNextString = $"Next Tech: |cffff8000{HumNextTech.UpgradeName}|r [|cffffcc00Level{HumNextTech.UpgradeLevel}|r]";
        }

        ResearchTech OrcCurrentTech = ResearchTech.LookUpBySlot(OrcResearchCurrent);
        if (OrcCurrentTech == null)
        {
            OrcCurrentString = $"Current Tech: |cffff8000-|r [|cffffcc00Level -|r]|r|n";
        }
        else
        {
            OrcCurrentString = $"Current Tech: |cffff8000{OrcCurrentTech.UpgradeName}|r [|cffffcc00Level {OrcCurrentTech.UpgradeLevel}|r]|r|n";
        }

        ResearchTech OrcNextTech = ResearchTech.LookUpBySlot(OrcResearchCurrent + 1);
        if (OrcNextTech == null)
        {
            OrcNextString = $"Next Tech: |cffff8000Max|r [|cffffcc00Level Max|r]";
        }
        else
        {
            OrcNextString = $"Next Tech: |cffff8000{OrcNextTech.UpgradeName}|r [|cffffcc00Level {OrcNextTech.UpgradeLevel}|r]";
        }

        BlzSetAbilityExtendedTooltip(ABILITY_A085_ALLIANCE_TECH, $"Current Research: |cffff8000{HumResearch}/180|r|n{HumCurrentString}{HumNextString}", 0);
        BlzSetAbilityExtendedTooltip(ABILITY_A086_HORDE_TECH, $"Current Research: |cffff8000{OrcResearch}/180|r|n{OrcCurrentString}{OrcNextString}", 0);
    }

    private static void ResearchKillGain()
    {
        // Research
        if (GetDyingUnit().Owner == player.Create(PLAYER_NEUTRAL_AGGRESSIVE))
        {
            if (Human.Contains(GetKillingUnit().Owner))
            {
                HumResearch += Math.Max(1, GetDyingUnit().Level * 2);
            }

            if (Orc.Contains(GetKillingUnit().Owner))
            {
                OrcResearch += Math.Max(1, GetDyingUnit().Level * 2);
            }
        }
    }
}

public class ResearchTech
{
    public ResearchTech(int slot, int upgradeId, int level, string name)
    {
        UpgradeSlot = slot;
        UpgradeId = upgradeId;
        UpgradeLevel = level;
        UpgradeName = name;
        Research.Entries.Add(slot, this);
    }

    public int UpgradeSlot { get; private set; }
    public int UpgradeId { get; private set; }
    public int UpgradeLevel { get; private set; }
    public string UpgradeName { get; private set; }

    public static ResearchTech LookUpBySlot(int slot)
    {
        if (Research.Entries.TryGetValue(slot, out ResearchTech Tech))
        {
            return Tech;
        }

        return null;
    }

    public void ResearchUpgrade(ArmyForce Force)
    {
        string PlayerName;
        if (Force == ArmyForce.Alliance)
        {
            PlayerName = Player(5).Name;
        }
        else
        {
            PlayerName = Player(11).Name;
        }
        string Message = $"{PlayerName} researched |cffff8000{UpgradeName}|r [|cffffcc00Level {UpgradeLevel}|r]";

        switch (Force)
        {
            case ArmyForce.Alliance:
            {
                QuestMessageBJ(GetPlayersAllies(Player(5)), bj_QUESTMESSAGE_UPDATED, Message);

                for (int i = 0; i <= 4; i++)
                {
                    player.Create(i).SetTechResearched(UpgradeId, IMinBJ(3, UpgradeLevel));
                }

                player.Create(5).SetTechResearched(UpgradeId, UpgradeLevel);
                player.Create(12).SetTechResearched(UpgradeId, UpgradeLevel);
                player.Create(13).SetTechResearched(UpgradeId, UpgradeLevel);
                player.Create(14).SetTechResearched(UpgradeId, UpgradeLevel);
                break;
            }
            case ArmyForce.Horde:
            {
                QuestMessageBJ(GetPlayersAllies(Player(11)), bj_QUESTMESSAGE_UPDATED, Message);

                for (int i = 6; i <= 10; i++)
                {
                    player.Create(i).SetTechResearched(UpgradeId, IMinBJ(3, UpgradeLevel));
                }

                player.Create(11).SetTechResearched(UpgradeId, UpgradeLevel);
                player.Create(15).SetTechResearched(UpgradeId, UpgradeLevel);
                player.Create(16).SetTechResearched(UpgradeId, UpgradeLevel);
                player.Create(17).SetTechResearched(UpgradeId, UpgradeLevel);
                break;
            }
        }
    }

    public static void Upgrade(int UpgradeID, ArmyForce Force)
    {
        ResearchTech ResearchTech = LookUpBySlot(UpgradeID);
        if (ResearchTech != null)
        {
            ResearchTech.ResearchUpgrade(Force);
        }
        else
        {
            QuestMessageBJ(GetPlayersAll(), bj_QUESTMESSAGE_HINT, $"{Force.ToString()} {UpgradeID} failed");
        }
    }
}
