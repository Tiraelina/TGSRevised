using WCSharp.Api;
using WCSharp.Shared;
using WCSharp.Shared.Extensions;
using static Constants;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class DiffusionFlare : AbilityMissile
{
    private float Mana;

    public DiffusionFlare(unit caster, float targetX, float targetY, int abilityId) : base(caster, targetX, targetY, abilityId)
    {
        EffectString = @"Abilities\Spells\Undead\Darksummoning\DarkSummonMissile.mdl";
        Range = 800.0f;
        DummyAbilityId = ABILITY_A07K_STORM_BOLT_DUMMY;
        EffectScale = 2.0f;
        Interval = 1.0f;
        Damage = 100.0f + 40.0f * AbilityLevel;
        CollisionRadius = 100.0f;
        Speed = 600.0f;
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
            Mana = unit.Mana;
            if (Mana > 0.0f && !unit.IsUnitType(unittype.Summoned))
            {
                if (Mana > Damage)
                {
                    unit.Damage(Caster, Damage, attacktype.Magic);
                }
                else
                {
                    unit.Damage(Caster, Mana, attacktype.Magic);
                }

                unit.Mana -= Damage * 0.5f;
            }
            else if (unit.IsUnitType(unittype.Summoned))
            {
                unit.Mana -= Damage * 0.5f;
                unit.Damage(Caster, Damage, attacktype.Magic);
            }
        }
    }
}
