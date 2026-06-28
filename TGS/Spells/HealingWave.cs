using WCSharp.Api;
using WCSharp.Lightnings;
using WCSharp.Missiles;
using WCSharp.Shared.Extensions;
using static TGS.TextTags;
using static TGS.Util;
using static WCSharp.Api.Common;

namespace TGS.Spells
{
    public class HealingWave : BasicMissile
    {
        private int AbilityId { get; }
        private int AbilityLevel { get; }
        private int BounceCount { get; set; }
        private int Bounces { get; } = 3;
        private int BouncesPerLvl { get; } = 1;
        private float HealingBase { get; } = 50.0f;
        private float HealingPerLvl { get; } = 50.0f;
        private float HealingAmount { get; set; }
        private float Range { get; }
        private float BounceMax { get; }
        private float ReductionFactor { get; }

        private timer BounceTimer { get; }
        private bool bFirstHit { get; set; } = true;
        private group Excluded { get; }
        private float RangeBase { get; } = 470.0f;
        private float RangePerLvl { get; } = 30.0f;
        private float ReductionPerBounce { get; } = 0.26f;
        private float ReductionPerBounceLevel { get; } = 0.01f;
        private group Targets { get; set; }
        private unit LastTarget { get; set; }

        public HealingWave(unit caster, unit target, int abilityId) : base(caster, target)
        {
            AbilityId = abilityId;
            AbilityLevel = Caster.GetAbilityLevel(AbilityId);
            Speed = 1500.0f;
            EffectString = @"";
            CasterLaunchZ = 50.0f;
            HealingAmount = HealingBase + (HealingPerLvl * AbilityLevel);
            Range = RangeBase + (RangePerLvl * AbilityLevel);
            BounceMax = Bounces + (BouncesPerLvl * AbilityLevel);
            ReductionFactor = ReductionPerBounce - (ReductionPerBounceLevel * AbilityLevel);
            Excluded = group.Create();
            Excluded.Add(Target);
            BounceTimer = timer.Create();
        }

        public override void OnImpact()
        {
            if (bFirstHit)
            {
                MakeTag(HealingAmount, Target, TextTags.TagType.Heal);
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
                    if (BounceCount < BounceMax)
                    {
                        BounceCount += 1;
                        Targets = group.Create();
                        GroupEnumUnitsInRange(Targets, Target.X, Target.Y, Range, Condition(Filter));
                        foreach (unit NearestUnit in Targets.ToList())
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
                            HealingAmount = HealingAmount * (1.0f - ReductionFactor);
                            MakeTag(HealingAmount, Target, TextTags.TagType.Heal);
                            Target.Heal(HealingAmount);
                            return;
                        }
                    }
                    BounceTimer.Dispose();
                    Excluded.Dispose();
                    Dispose();
                });
            }
        }

        private bool Filter()
        {
            return !IsUnitInGroup(GetFilterUnit(), Excluded)
                   && GetFilterUnit().IsAllyTo(Caster.Owner)
                   && GetFilterUnit().IsValidTarget();
        }
    }
}
