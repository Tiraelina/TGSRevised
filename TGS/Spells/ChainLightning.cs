using WCSharp.Api;
using WCSharp.Lightnings;
using WCSharp.Missiles;
using WCSharp.Shared.Extensions;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class ChainLightning : BasicMissile
{
    private int AbilityId;
    private int AbilityLevel;
    private int BounceCount;
    private int BOUNCES = 2;
    private int BOUNCES_PER_LVL = 1;
    private float COLLISION_SIZE = 32.0f;
    private float DAMAGE = 45.0f;
    private float DAMAGE_PER_LVL = 45.0f;
    private float DamageAmount;

    private timer BounceTimer;
    private bool bFirstHit = true;
    private group Excluded;
    private float RANGE = 470.0f;
    private float RANGE_PER_LVL = 30.0f;
    private float REDUCTION_PER_BOUNCE = 0.06f;
    private float REDUCTION_PER_BOUNCE_LEVEL = 0.01f;
    private group Targets;
    private unit LastTarget;

    public ChainLightning(unit caster, unit target, int abilityId) : base(caster, target)
    {
        AbilityId = abilityId;
        AbilityLevel = Caster.GetAbilityLevel(AbilityId);
        Speed = 1500.0f;
        EffectString = @"";
        CasterLaunchZ = 50.0f;
        DamageAmount = Damage(AbilityLevel);
        Excluded = group.Create();
        Excluded.Add(Target);
        BounceTimer = timer.Create();
    }

    public override void OnImpact()
    {
        if (bFirstHit)
        {
            Target.Damage(Caster, DamageAmount, ATTACK_TYPE_NORMAL, DAMAGE_TYPE_LIGHTNING);

            Lightning PrimaryChainLightning = new("CLPB", Caster, Target)
            {
                Duration = 1.0f,
                FadeDuration = 0.5f,
                CasterHeightOffset = 50f,
                TargetHeightOffset = 50f,
            };
            LightningSystem.Add(PrimaryChainLightning);
            effect.Create(@"Abilities\Weapons\Bolt\BoltImpact.mdl", Target, "origin").Dispose();
            bFirstHit = false;
            BounceTimer.Start(0.25f, true, () =>
            {
                if (BounceCount < Bounces(AbilityLevel))
                {
                    BounceCount += 1;
                    Targets = group.Create();
                    GroupEnumUnitsInRange(Targets, Target.X, Target.Y, Range(AbilityLevel), Condition(null));
                    foreach (unit NearestUnit in Targets.ToList())
                    {
                        if (!IsUnitInGroup(NearestUnit, Excluded)
                            && NearestUnit.IsEnemyTo(Caster.Owner)
                            && SpellsCore.IsValidTarget(NearestUnit))
                        {
                            Active = true;
                            Excluded.Add(NearestUnit);
                            LastTarget = Target;
                            Target = NearestUnit;
                            Targets.Dispose();
                            Lightning SecondaryChainLightning = new("CLSB", LastTarget, Target)
                            {
                                Duration = 1.0f,
                                FadeDuration = 0.5f,
                                CasterHeightOffset = 50f,
                                TargetHeightOffset = 50f,
                            };
                            LightningSystem.Add(SecondaryChainLightning);
                            effect.Create(@"Abilities\Weapons\Bolt\BoltImpact.mdl", Target, "origin").Dispose();
                            DamageAmount = Damage(AbilityLevel) * (1.0f - Reduction(AbilityLevel));
                            Target.Damage(Caster, DamageAmount, ATTACK_TYPE_NORMAL, DAMAGE_TYPE_LIGHTNING);
                            return;
                        }
                    }
                }
                BounceTimer.Dispose();
                Excluded.Dispose();
                Dispose();
            });
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

    private float Reduction(int level)
    {
        return REDUCTION_PER_BOUNCE - (REDUCTION_PER_BOUNCE_LEVEL * level);
    }
}
