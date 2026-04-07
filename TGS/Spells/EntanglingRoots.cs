using WCSharp.Api;
using WCSharp.Dummies;
using WCSharp.Shared;

namespace TGS.Spells;

public class EntanglingRoots : AbilityMissile
{
    public EntanglingRoots(unit caster, float targetX, float targetY, int abilityId) : base(caster, targetX, targetY, abilityId)
    {
        EffectString = @"Abilities\Spells\NightElf\EntanglingRoots\EntanglingRootsTarget.mdl";
        DummyAbilityId = Constants.ABILITY_A07Q_ENTANGLING_ROOTS_NEW_DUMMY;
        Range = 800.0f;
        EffectScale = 1.2f;
        Interval = 1.15f;
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
            unit Dummy = DummySystem.GetDummy(unit.X, unit.Y, Caster.Owner);
            Dummy.AddAbility(DummyAbilityId);
            Dummy.SetAbilityLevel(DummyAbilityId, Caster.GetAbilityLevel(AbilityId));
            Dummy.IssueOrder(Constants.ORDER_SOUL_BURN, unit);
            DummySystem.RecycleDummy(Dummy, 0.25f);
        }
    }
}
