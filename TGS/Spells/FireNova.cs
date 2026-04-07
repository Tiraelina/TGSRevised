using WCSharp.Api;
using WCSharp.Dummies;
using WCSharp.Shared.Extensions;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class FireNova
{
    private int AbilityId;
    private unit Caster;
    private float Damage;
    private effect Explode;
    private int Level;
    private unit Target;
    private group Targets;

    public FireNova(unit InCaster, unit InTarget, int InAbilityId)
    {
        Caster = InCaster;
        Target = InTarget;
        AbilityId = InAbilityId;
        Level = Caster.GetAbilityLevel(AbilityId);
        Damage = 75.0f + (25.0f * Level);
        Targets = group.Create();
    }

    public void Cast()
    {
        Explode = effect.Create(@"Abilities\Spells\Other\Incinerate\FireLordDeathExplode.mdl", Target, "overhead");
        Explode.Scale = 1.4f;
        Explode.Dispose();
        unit Dummy = DummySystem.GetDummy(Target.X, Target.Y, Caster.Owner);
        GroupEnumUnitsInRange(Targets, Target.X, Target.Y, 200.0f + (0.0f * Level), Condition(null));
        foreach (unit NearestUnit in Targets.ToList())
        {
            if (NearestUnit.IsEnemyTo(Caster.Owner)
                && SpellsCore.IsValidTarget(NearestUnit))
            {
                Dummy.AddAbility(Constants.ABILITY_ANSO_SOUL_BURN_FIRE_NOVA_DOT);
                Dummy.SetAbilityLevel(Constants.ABILITY_ANSO_SOUL_BURN_FIRE_NOVA_DOT, Level);
                Dummy.IssueOrder(Constants.ORDER_SOUL_BURN, NearestUnit);
                UnitDamageTarget(Caster, NearestUnit, Damage, true, false, ATTACK_TYPE_MAGIC, DAMAGE_TYPE_UNKNOWN, WEAPON_TYPE_WHOKNOWS);
            }
        }

        Targets.Dispose();
        DummySystem.RecycleDummy(Dummy, 1.0f);
    }
}
