using WCSharp.Api;
using WCSharp.Dummies;
using WCSharp.Shared;
using WCSharp.Shared.Extensions;
using static Constants;

namespace TGS.Spells;

public class StormBolt : AbilityMissile
{
    public StormBolt(unit caster, float targetX, float targetY, int abilityId) : base(caster, targetX, targetY, abilityId)
    {
        EffectString = @"Abilities\Spells\Human\StormBolt\StormBoltMissile.mdl";
        Range = 800.0f;
        DummyAbilityId = ABILITY_A07K_STORM_BOLT_DUMMY;
        EffectScale = 1.2f;
        Interval = 1.0f;
        Damage = 100.0f + 35.0f * AbilityLevel;
        float X = caster.X;
        float Y = caster.Y;
        float DX = targetX - X;
        float DY = targetY - Y;
        float Distance = FastUtil.DistanceBetweenPoints(X, Y, DX, DY);
        float Scale = Distance / Range;
        float FinalX = X + DX * Scale;
        float FinalY = Y + DY * Scale;
        TargetX = FinalX;
        TargetY = FinalY;
    }

    public override void OnImpact()
    {
        if (!Expired)
        {
            Active = true;
        }
    }

    public override void OnPeriodic()
    {
        Expired = true;
        Active = false;
    }

    public override void OnCollision(unit unit)
    {
        if (unit.IsEnemyTo(Caster.Owner) && SpellsCore.IsValidTarget(unit))
        {
            Active = false;
            Expired = true;
            unit Dummy = DummySystem.GetDummy(unit.X, unit.Y, Caster.Owner);
            Dummy.AddAbility(DummyAbilityId);
            Dummy.SetAbilityLevel(DummyAbilityId, Caster.GetAbilityLevel(AbilityId));
            Dummy.IssueOrder(ORDER_THUNDERBOLT, unit);
            unit.Damage(Caster, Damage, attacktype.Magic);
            DummySystem.RecycleDummy(Dummy, 0.25f);
            Dispose();
        }
    }
}
