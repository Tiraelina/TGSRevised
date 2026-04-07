using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Events;
using static TGS.Util;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public enum FactoryState
{
    Dead,
    Pending,
    UnderConstruction,
    Alive,
}

public class Factory
{
    public int FactoryId;
    public FactoryState State;
    public location Location;
    public player Owner;
    private List<int> SpawnedCreeps;
    public List<FactorySpawn> SpawnedUnits;
    public unit Unit;
    public string UnitName;

    public Factory(player InOwner, unit inUnit)
    {
        Owner = InOwner;
        Unit = inUnit;
        UnitName = Unit.Name;
        FactoryId = Unit.UnitType;
        State = FactoryState.Alive;
        SpawnedUnits = new List<FactorySpawn>();
        SpawnedCreeps = new List<int>();
        Location = GetUnitLoc(Unit);
        PlayerUnitEvents.Register(UnitEvent.Dies, Died, Unit);
    }

    public void SetNewFactory(unit NewFactory)
    {
        PlayerUnitEvents.Register(UnitEvent.Dies, Died, NewFactory);
        Army.FactoryLookup.Add(NewFactory, this);
        State = FactoryState.UnderConstruction;
        Unit = NewFactory;
    }

    public void AddSpawn(FactorySpawn InSpawn)
    {
        SpawnedUnits.Add(InSpawn);
    }

    public void AddCreep(int InCreepId)
    {
        SpawnedCreeps.Add(InCreepId);
    }

    public void Spawn()
    {
        foreach (FactorySpawn SpawnedUnit in SpawnedUnits)
        {
            int SpawnTotal;
            if (SpawnedUnit.bInitialGroup)
            {
                SpawnTotal = SpawnedUnit.Count + Army.TechGroupOne;
            }
            else
            {
                SpawnTotal = SpawnedUnit.Count + Army.TechGroupTwo;
            }

            if (Army.OpposingFactory[this].State == FactoryState.Alive)
            {
                CreateNUnitsAtLoc(SpawnTotal, SpawnedUnit.UnitId, Owner, Location, bj_UNIT_FACING);
            }
            else
            {
                CreateNUnitsAtLoc(SpawnTotal, SpawnedUnit.SuperUnitId, Owner, Location, bj_UNIT_FACING);
            }
        }

        foreach (int CreepId in SpawnedCreeps)
        {
            unit Creep = unit.Create(Owner, CreepId, Location.X, Location.Y);
            NegateBounty(Creep);
        }

        SpawnedCreeps.Clear();
    }

    private void Died()
    {
        PlayerUnitEvents.Unregister(UnitEvent.Dies, Died, Unit);
        Army.FactoryLookup.Remove(Unit);
        State = FactoryState.Dead;
        Unit = null;
    }
}

public class FactorySpawn
{
    public bool bInitialGroup;

    public FactorySpawn(int InUnitId, int InSuperUnitId, bool inbInitialGroup, int InCount)
    {
        UnitId = InUnitId;
        SuperUnitId = InSuperUnitId;
        Count = InCount;
        bInitialGroup = inbInitialGroup;
    }

    public int UnitId { get; set; }
    public int SuperUnitId { get; set; }
    public int Count { get; set; }
}
