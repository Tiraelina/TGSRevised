using WCSharp.Api;
using WCSharp.Dummies;
using WCSharp.Shared;
using WCSharp.Shared.Extensions;

namespace TGS.Spells;

public class HurlBoulder : AbilityMissile
{
    public HurlBoulder(unit caster, float targetX, float targetY, int abilityId) : base(caster, targetX, targetY, abilityId)
    {
        EffectString = @"Abilities\Weapons\RockBoltMissile\RockBoltMissile.mdl";
        Range = 800.0f;
        DummyAbilityId = Constants.ABILITY_A07L_HURL_BOULDER_DUMMY;
        CollisionRadius = 75.0f;
        EffectScale = 2.0f;
        Interval = 1.0f;
        Damage = 600.0f + 25.0f * AbilityLevel;
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
        if (unit.IsEnemyTo(Caster.Owner) && TGSSpells.IsValidTarget(unit))
        {
            Active = false;
            Expired = true;
            unit Dummy = DummySystem.GetDummy(unit.X, unit.Y, Caster.Owner);
            Dummy.AddAbility(DummyAbilityId);
            Dummy.SetAbilityLevel(DummyAbilityId, Caster.GetAbilityLevel(AbilityId));
            Dummy.IssueOrder(Constants.ORDER_THUNDERBOLT, unit);
            unit.Damage(Caster, Damage, attacktype.Magic);
            DummySystem.RecycleDummy(Dummy, 0.25f);
            Dispose();
        }
    }
}
