using WCSharp.Api;
using WCSharp.Missiles;
using WCSharp.Shared.Extensions;
using static TGS.Util;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class ChainDeathCoil : BasicMissile
{
    private int AbilityId;
    private int AbilityLevel;
    private int BounceCount;
    private int BOUNCES = 0;
    private int BOUNCES_PER_LVL = 1;
    private float COLLISION_SIZE = 32.0f;
    private float DAMAGE = 25.0f;
    private float DAMAGE_PER_LVL = 45.0f;
    private float DamageAmount;

    private bool bEnemy;
    private group Excluded;
    private float RANGE = 350.0f;
    private float RANGE_PER_LVL = 25.0f;
    private float REDUCTION_PER_BOUNCE = 0.15f;
    private group Targets;

    public ChainDeathCoil(unit caster, unit target, int abilityId) : base(caster, target)
    {
        AbilityId = abilityId;
        AbilityLevel = Caster.GetAbilityLevel(AbilityId);
        Speed = 1100.0f;
        EffectString = @"Abilities\Spells\Undead\DeathCoil\DeathCoilMissile.mdl";
        CasterLaunchZ = 50.0f;
        DamageAmount = Damage(AbilityLevel);
        bEnemy = target.IsEnemyTo(Caster.Owner);
        Excluded = group.Create();
        Excluded.Add(Target);
    }

    public override void OnImpact()
    {
        if (bEnemy)
        {
            MakeTag(DamageAmount, Target, TagType.Spell);
            Target.Damage(Caster, DamageAmount, ATTACK_TYPE_NORMAL, DAMAGE_TYPE_DEATH);
        }
        else
        {
            float OutHealing = DamageAmount / 2;
            MakeTag(OutHealing, Target, TagType.Heal);
            Target.Heal(OutHealing);
        }

        effect.Create(@"Abilities\Spells\Undead\DeathCoil\DeathCoilSpecialArt.mdl", Target, "origin").Dispose();
        if (BounceCount < Bounces(AbilityLevel))
        {
            BounceCount += 1;
            Targets = group.Create();
            GroupEnumUnitsInRange(Targets, Target.X, Target.Y, Range(AbilityLevel), Condition(null));
            foreach (unit NearestUnit in Targets.ToList())
            {
                if (!IsUnitInGroup(NearestUnit, Excluded)
                    && ((NearestUnit.IsEnemyTo(Caster.Owner) && bEnemy)
                        || (NearestUnit.IsAllyTo(Caster.Owner) && !bEnemy))
                    && SpellsCore.IsValidTarget(NearestUnit))
                {
                    Active = true;
                    Excluded.Add(NearestUnit);
                    Target = NearestUnit;
                    Targets.Dispose();
                    DamageAmount = Damage(AbilityLevel) * (1.0f - REDUCTION_PER_BOUNCE);
                    return;
                }
            }

            Excluded.Dispose();
            Dispose();
        }
        else
        {
            Excluded.Dispose();
            Dispose();
        }
    }

    private float Damage(int level)
    {
        return DAMAGE + (DAMAGE_PER_LVL * level);
    }

    private float Range(int level)
    {
        return RANGE + (RANGE_PER_LVL * level);
    }

    private int Bounces(int level)
    {
        return BOUNCES + (BOUNCES_PER_LVL * level);
    }
}
