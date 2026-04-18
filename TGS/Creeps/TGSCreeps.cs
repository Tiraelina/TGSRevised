using System;
using System.Collections.Generic;
using System.Linq;
using WCSharp.Api;
using WCSharp.Shared.Data;
using static Constants;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS.Creeps;

public static class TGSCreeps
{
    private static int CreepUpgradeTick = 100;
#if DEBUG
    public static float CreepRespawnTime3m = 5.0f;
    public static float CreepRespawnTime4m = 5.0f;
    public static float CreepRespawnTime5m = 5.0f;
#else
    public static float CreepRespawnTime3m = 180.0f;
    public static float CreepRespawnTime4m = 240.0f;
    public static float CreepRespawnTime5m = 300.0f;
#endif
    private static int CreepUpgradeLevel;
    private static int CreepUpgradeLevelMax = 100;
    public static List<CreepCamp> CreepCamps = new();
    public static Dictionary<unit, CreepCamp> UnitToCamp = new();

    public static void Init()
    {
        CampsTop.Init();
        CampsMid.Init();
        CampsBot.Init();

#if DEBUG
        Console.WriteLine("Camps initialized.");
#endif
        trigger CreepDeaths = trigger.Create();
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(PLAYER_NEUTRAL_AGGRESSIVE), playerunitevent.Death);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(0), playerunitevent.ChangeOwner);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(1), playerunitevent.ChangeOwner);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(2), playerunitevent.ChangeOwner);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(3), playerunitevent.ChangeOwner);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(4), playerunitevent.ChangeOwner);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(6), playerunitevent.ChangeOwner);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(7), playerunitevent.ChangeOwner);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(8), playerunitevent.ChangeOwner);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(9), playerunitevent.ChangeOwner);
        TriggerRegisterPlayerUnitEventSimple(CreepDeaths, player.Create(10), playerunitevent.ChangeOwner);
        CreepDeaths.AddAction(() => { CreepCamp.GetCampForUnit(GetTriggerUnit()).UnitDied(GetTriggerUnit(), GetKillingUnit()); });
    }

    public static void UpgradeCreeps()
    {
        CreepUpgradeLevel = Math.Min(CreepUpgradeLevel + CreepUpgradeTick, CreepUpgradeLevelMax);
        player.Create(PLAYER_NEUTRAL_AGGRESSIVE).SetTechResearched(UPGRADE_R000_CREEP_UPGRADE, CreepUpgradeLevel);
        player.Create(5).SetTechResearched(UPGRADE_R000_CREEP_UPGRADE, CreepUpgradeLevel);
        player.Create(11).SetTechResearched(UPGRADE_R000_CREEP_UPGRADE, CreepUpgradeLevel);
        player.Create(12).SetTechResearched(UPGRADE_R000_CREEP_UPGRADE, CreepUpgradeLevel);
        player.Create(13).SetTechResearched(UPGRADE_R000_CREEP_UPGRADE, CreepUpgradeLevel);
        player.Create(14).SetTechResearched(UPGRADE_R000_CREEP_UPGRADE, CreepUpgradeLevel);
        player.Create(15).SetTechResearched(UPGRADE_R000_CREEP_UPGRADE, CreepUpgradeLevel);
        player.Create(16).SetTechResearched(UPGRADE_R000_CREEP_UPGRADE, CreepUpgradeLevel);
        player.Create(17).SetTechResearched(UPGRADE_R000_CREEP_UPGRADE, CreepUpgradeLevel);
    }
}

public enum Camp
{
    FrenchmansWest = 0,
    RuinsWest = 1,
    FrenchmansEast = 2,
    RuinsEast = 3,
    GateNorth = 4,
    GoblinEast = 5,
    GoblinWest = 6,
    GateSouth = 7,
    SiegeWest = 8,
    GolemWest = 9,
    TortalWest = 10,
    SiegeEast = 11,
    GolemEast = 12,
    TortalEast = 13,
    Urtle = 14,
    Crabnar = 15,
    PigeonWest = 16,
    PigeonEast = 17,
    OcculordWest = 18,
    OcculordEast = 19,
    IcerogWest = 20,
    IcerogEast = 21,
    KoboldWest = 22,
    KoboldEast = 23,
    NightlordWest = 24,
    NightlordEast = 25,
    GnollsSouthwest = 26,
    GnollsSoutheast = 27,
    DragonspawnWest = 28,
    DragonspawnEast = 29,
    FurbolgFarSouth = 30,
    HeroTop = 31,
    HeroMid = 32,
    HeroBot = 33,
    MoonkinEast = 34,
    MoonkinWest = 35,
}

public struct CampUnits
{
    public int CampUnit { get; set; }
    public DropID LootID { get; set; }

    public CampUnits(int unitID, DropID lootID = DropID.Nothing)
    {
        CampUnit = unitID;
        LootID = lootID;
    }
}

public struct CampGroup
{
    public List<CampUnits> CampUnitIDs { get; set; } = new();

    public CampGroup()
    {
    }
}

public class CreepCamp
{
    public CreepCamp(Camp camp, float respawnTime, Action<CreepCamp> onCleared = null, Action<CreepCamp> onRespawn = null)
    {
        Camp = camp;
        CampLevel = 0;
        MaxCampLevel = 0;
        Cleared = 0;
        RespawnTime = respawnTime;
        RespawnTimer = timer.Create();
        UnitGroup = group.Create();
        Units = new List<CampGroup>();
        Location = new List<Point>();
        Rotation = new List<float>();
        OnCleared = onCleared;
        OnRespawn = onRespawn;
        Units.Add(new CampGroup());
        TGSCreeps.CreepCamps.Add(this);
    }

    public Camp Camp { get; init; }
    public int CampLevel { get; set; }
    public int MaxCampLevel { get; set; }
    public int Cleared { get; set; }
    public float RespawnTime { get; init; }
    public timer RespawnTimer { get; set; }
    public group UnitGroup { get; set; }
    public List<CampGroup> Units { get; set; }
    public List<Point> Location { get; set; }
    public List<float> Rotation { get; set; }
    public Action<CreepCamp> OnCleared { get; set; }
    public Action<CreepCamp> OnRespawn { get; set; }

    public void UnitDied(unit killedUnit, unit killingUnit)
    {
#if DEBUG
        Console.WriteLine(killedUnit.Name + " in " + Camp.ToString());
#endif
        Army.AddCreepToArmy(killedUnit, killingUnit);
        UnitGroup.Remove(killedUnit);
        TGSCreeps.UnitToCamp.Remove(killedUnit);
        Drops.DropItem(killedUnit);
        if (UnitGroup.Count == 0)
        {
#if DEBUG
            Console.WriteLine(Camp.ToString() + " was cleared, starting " + RespawnTime + " timer.");
#endif
            Cleared += 1;
            if (Cleared == 2)
            {
                Cleared = 0;
                CampLevel = Math.Min(CampLevel + 1, MaxCampLevel);
            }

            RespawnTimer.Start(RespawnTime, false, RespawnCamp);
            TGSCreeps.UpgradeCreeps();
            OnCleared?.Invoke(this);
        }
    }

    public void AddUnitSet(CampGroup unitSet)
    {
        MaxCampLevel += 1;
        Units.Add(unitSet);
    }

    public void AddUnit(unit inUnit, DropID lootID = DropID.Nothing)
    {
        UnitGroup.Add(inUnit);
        Location.Add(new Point(inUnit.X, inUnit.Y));
        Rotation.Add(inUnit.Facing);
        inUnit.UserData = (int)lootID;
        TGSCreeps.UnitToCamp[inUnit] = this;
        Units.First().CampUnitIDs.Add(new CampUnits(inUnit.UnitType, lootID));
    }

    private void RespawnCamp()
    {
#if DEBUG
            Console.WriteLine(Camp.ToString() + " was respawned." );
#endif
        for (int i = 0; i < Units[CampLevel].CampUnitIDs.Count; i++)
        {
            unit SpawnedUnit = unit.Create(player.Create(PLAYER_NEUTRAL_AGGRESSIVE), Units[CampLevel].CampUnitIDs[i].CampUnit, Location[i].X, Location[i].Y, Rotation[i]);
            SpawnedUnit.UserData = (int)Units[CampLevel].CampUnitIDs[i].LootID;
            TGSCreeps.UnitToCamp.Add(SpawnedUnit, this);
            UnitGroup.Add(SpawnedUnit);
        }

        OnRespawn?.Invoke(this);
    }

    public static CreepCamp GetCampForUnit(unit killedUnit)
    {
        return TGSCreeps.UnitToCamp[killedUnit];
    }
}
