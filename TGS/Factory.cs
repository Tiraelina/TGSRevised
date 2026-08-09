using System;
using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Events;
using static TGS.Util;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS
{
    public enum FactoryState
    {
        Dead,
        Pending,
        UnderConstruction,
        Alive,
    }

    public class Factory
    {
        public int FactoryId { get; }
        public FactoryState State { get; set; }
        public location FactoryLocation { get; }
        public player Owner { get; }
        private List<int> SpawnedCreeps { get; }
        public List<FactorySpawn> SpawnedUnits { get; }
        public unit Unit { get; set; }
        public string UnitName { get; }
        public timer PendingTimeout { get; set; }
        public timer ConstructionTimeout { get; set; }

        public Factory(player InOwner, unit inUnit)
        {
            Owner = InOwner;
            Unit = inUnit;
            UnitName = Unit.Name;
            FactoryId = Unit.UnitType;
            State = FactoryState.Alive;
            SpawnedUnits = new List<FactorySpawn>();
            SpawnedCreeps = new List<int>();
            FactoryLocation = GetUnitLoc(Unit);
            PlayerUnitEvents.Register(UnitEvent.Dies, Died, Unit);
        }

        public void SetNewFactory(unit NewFactory)
        {
            PendingTimeout.Pause();
            PendingTimeout.Dispose();
            State = FactoryState.UnderConstruction;
            PlayerUnitEvents.Register(UnitEvent.Dies, Died, NewFactory);
            Army.FactoryLookup.Add(NewFactory, this);
            Unit = NewFactory;
            ConstructionTimeout = timer.Create();
            ConstructionTimeout.Start(75.0f, false, ConstructionFailed);
#if DEBUG
            Console.WriteLine($"{Unit.Name} {Unit.UnitType.Id2String()} new under construction");
#endif
        }

        public void SetPendingConstruction(force InForce, unit InBuyingUnit, item InSoldItem)
        {
            State = FactoryState.Pending;
            QuestMessageBJ(InForce, bj_QUESTMESSAGE_HINT,
                $"{InBuyingUnit.Owner.Name} bought |cffff8000{InSoldItem.Name}|cffffffff to rebuild |cffff8000{UnitName}");
            PingMinimapLocForForce(InForce, FactoryLocation, 5.0f);
            PendingTimeout = timer.Create();
            PendingTimeout.Start(20.0f, false, ConstructionFailed);
        }

        private void ConstructionFailed()
        {
            State = FactoryState.Dead;
        }

        public void SetAlive()
        {
            ConstructionTimeout.Pause();
            ConstructionTimeout.Dispose();
            State = FactoryState.Alive;
#if DEBUG
            Console.WriteLine($"{Unit.Name} {Unit.UnitType.Id2String()} finished construction");
#endif
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
                    CreateNUnitsAtLoc(SpawnTotal, SpawnedUnit.UnitId, Owner, FactoryLocation, bj_UNIT_FACING);
                }
                else
                {
                    CreateNUnitsAtLoc(SpawnTotal, SpawnedUnit.SuperUnitId, Owner, FactoryLocation, bj_UNIT_FACING);
                }
            }

            foreach (int CreepId in SpawnedCreeps)
            {
                unit Creep = unit.Create(Owner, CreepId, FactoryLocation.X, FactoryLocation.Y);
                Creep.NegateBounty();
            }

            SpawnedCreeps.Clear();
        }

        private void Died()
        {
            if (State == FactoryState.UnderConstruction)
            {
                ConstructionTimeout.Pause();
                ConstructionTimeout.Dispose();
            }
            State = FactoryState.Dead;
            PlayerUnitEvents.Unregister(UnitEvent.Dies, Died, Unit);
            Army.FactoryLookup.Remove(Unit);
#if DEBUG
            Console.WriteLine($"{Unit.Name} {Unit.UnitType.Id2String()} died");
#endif
            Unit = null;
        }
    }

    public class FactorySpawn
    {
        public bool bInitialGroup { get; }

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
}
