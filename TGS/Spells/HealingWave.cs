using WCSharp.Api;
using WCSharp.Lightnings;
using WCSharp.Missiles;
using WCSharp.Shared.Extensions;
using static TGS.TextTags;
using static TGS.Util;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class HealingWave : BasicMissile
{
    private int AbilityId;
    private int AbilityLevel;
    private int BounceCount;
    private int BOUNCES = 3;
    private int BOUNCES_PER_LVL = 1;
    private float COLLISION_SIZE = 32.0f;
    private float HEALING = 50.0f;
    private float HEALING_PER_LVL = 50.0f;
    private float HealingAmount;

    private timer BounceTimer;
    private bool bFirstHit = true;
    private group Excluded;
    private float RANGE = 470.0f;
    private float RANGE_PER_LVL = 30.0f;
    private float REDUCTION_PER_BOUNCE = 0.26f;
    private float REDUCTION_PER_BOUNCE_LEVEL = 0.01f;
    private group Targets;
    private unit LastTarget;

    public HealingWave(unit caster, unit target, int abilityId) : base(caster, target)
    {
        AbilityId = abilityId;
        AbilityLevel = Caster.GetAbilityLevel(AbilityId);
        Speed = 1500.0f;
        EffectString = @"";
        CasterLaunchZ = 50.0f;
        HealingAmount = Healing(AbilityLevel);
        Excluded = group.Create();
        Excluded.Add(Target);
        BounceTimer = timer.Create();
    }

    public override void OnImpact()
    {
        if (bFirstHit)
        {
            MakeTag(HealingAmount, Target, TagType.Heal);
            Target.Heal(HealingAmount);

            Lightning PrimaryHealingWave = new("HWPB", Caster, Target)
            {
                Duration = 1.0f,
                FadeDuration = 0.5f,
                CasterHeightOffset = 50f,
                TargetHeightOffset = 50f,
            };
            LightningSystem.Add(PrimaryHealingWave);
            effect.Create(@"Abilities\Spells\Orc\HealingWave\HealingWaveTarget.mdl", Target, "origin").Dispose();
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
                            && NearestUnit.IsAllyTo(Caster.Owner)
                            && TGSSpells.IsValidTarget(NearestUnit))
                        {
                            Active = true;
                            Excluded.Add(NearestUnit);
                            LastTarget = Target;
                            Target = NearestUnit;
                            Targets.Dispose();
                            Lightning SecondaryHealingWave = new("HWSB", LastTarget, Target)
                            {
                                Duration = 1.0f,
                                FadeDuration = 0.5f,
                                CasterHeightOffset = 50f,
                                TargetHeightOffset = 50f,
                            };
                            LightningSystem.Add(SecondaryHealingWave);
                            effect.Create(@"Abilities\Spells\Orc\HealingWave\HealingWaveTarget.mdl", Target, "origin").Dispose();
                            HealingAmount = Healing(AbilityLevel) * (1.0f - Reduction(AbilityLevel));
                            MakeTag(HealingAmount, Target, TagType.Heal);
                            Target.Heal(HealingAmount);
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

    private float Healing(int level)
    {
        return HEALING + (HEALING_PER_LVL * level);
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
