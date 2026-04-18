using WCSharp.Api;
using static Constants;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class PocketFactory : AbilityMissile
{
    public PocketFactory(unit caster, float targetX, float targetY, int abilityId) : base(caster, targetX, targetY, abilityId)
    {
        EffectString = @"Units\Creeps\HeroTinkerFactory\HeroTinkerFactoryMissle.mdl";
        IsArcing = true;
        Arc = 0.350f;
        CasterLaunchZ = 50.0f;
        TargetX = targetX;
        TargetY = targetY;
        EffectScale = 1.0f;
        AbilityLevel = Caster.GetAbilityLevel(AbilityId);
        Speed = 1000.0f;
        CasterX = Caster.X;
        CasterY = Caster.Y;
    }

    public override void OnImpact()
    {
        TGSSpells.PocketFactoryLookup.TryGetValue(AbilityLevel, out int UnitId);
        if (UnitId != 0)
        {
            new PocketFactoryUnit(Caster, AbilityLevel, TargetX, TargetY).Cast();
        }
    }
}

public class PocketFactoryUnit
{
    private int AbilityLevel;
    private unit Caster;
    private unit Factory;
    private trigger FactoryDeath;
    private timer FactoryInterval;
    private timer FactoryLifespan;
    private int GoblinId;
    private float TargetX;
    private float TargetY;

    public PocketFactoryUnit(unit caster, int abilityLevel, float targetX, float targetY)
    {
        Caster = caster;
        AbilityLevel = abilityLevel;
        TargetX = targetX;
        TargetY = targetY;
        FactoryInterval = timer.Create();
        FactoryLifespan = timer.Create();
    }

    public void Cast()
    {
        TGSSpells.PocketFactoryLookup.TryGetValue(AbilityLevel, out int UnitId);
        if (UnitId != 0)
        {
            Factory = unit.Create(Caster.Owner, UnitId, TargetX, TargetY);
            Factory.SetAnimation("birth");
            Summons.ScaleSummon(Caster, Factory);
            FactoryLifespan.Start(40.0f + (AbilityLevel * 2.2f), false, KillFactory);
            Blizzard.SetUnitRallyUnit(Factory, Factory);
            FactoryDeath = trigger.Create();
            FactoryDeath.RegisterUnitEvent(Factory, EVENT_UNIT_DEATH);
            FactoryDeath.AddAction(Cleanup);
            TGSSpells.ClockwerkGoblinLookup.TryGetValue(AbilityLevel, out int GoblinUnitId);
            if (GoblinUnitId != 0)
            {
                GoblinId = GoblinUnitId;
                FactoryInterval.Start(5.0f - (AbilityLevel * 0.275f), true, SpawnGoblin);
            }
        }
    }

    private void SpawnGoblin()
    {
        unit Goblin = unit.Create(Caster.Owner, GoblinId, Factory.X, Factory.Y);
        Summons.ScaleSummon(Caster, Goblin);
        Goblin.ApplyTimedLife(FourCC("BNcg"), 12.0f + (AbilityLevel * 1.1f));
        if (Factory.RallyUnit != null)
        {
            Goblin.IssueOrder(ORDER_ATTACK, Factory.RallyUnit.X, Factory.RallyUnit.Y);
        }
        else
        {
            Goblin.IssueOrder(ORDER_ATTACK, Factory.RallyPoint.X, Factory.RallyPoint.Y);
        }
    }

    private void KillFactory()
    {
        FactoryDeath.Dispose();
        Factory.Kill();
        Cleanup();
    }

    private void Cleanup()
    {
        FactoryLifespan.Dispose();
        FactoryInterval.Dispose();
        FactoryDeath.Dispose();
        Factory = null;
    }
}
