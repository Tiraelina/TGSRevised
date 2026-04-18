using WCSharp.Api;
using WCSharp.Dummies;
using WCSharp.Shared.Extensions;
using static Constants;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class Disarm
{
    private int AbilityId;
    private unit Caster;
    private int Level;
    private unit Target;
    private group Targets;

    public Disarm(unit InCaster, unit InTarget, int InAbilityId)
    {
        Caster = InCaster;
        Target = InTarget;
        AbilityId = InAbilityId;
        Level = Caster.GetAbilityLevel(AbilityId);
        Targets = group.Create();
    }

    public void Cast()
    {
        unit Dummy = DummySystem.GetDummy(Target.X, Target.Y, Caster.Owner);
        GroupEnumUnitsInRange(Targets, Target.X, Target.Y, 200.0f + (0.0f * Level), Condition(null));
        foreach (unit NearestUnit in Targets.ToList())
        {
            if (NearestUnit.IsEnemyTo(Caster.Owner)
                && TGSSpells.IsValidTarget(NearestUnit))
            {
                Dummy.AddAbility(ABILITY_ACRS_CURSE);
                Dummy.SetAbilityLevel(ABILITY_ACRS_CURSE, Level);
                Dummy.IssueOrder(ORDER_CURSE, NearestUnit);
            }
        }

        Targets.Dispose();
        DummySystem.RecycleDummy(Dummy, 1.0f);
    }
}
