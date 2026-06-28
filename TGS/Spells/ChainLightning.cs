using WCSharp.Api;
using WCSharp.Lightnings;
using WCSharp.Missiles;
using WCSharp.Shared.Extensions;
using static WCSharp.Api.Common;

namespace TGS.Spells
{
    public class ChainLightning : BasicMissile
    {
        private int AbilityId { get; }
        private int AbilityLevel { get; }
        private int BounceCount { get; set; }
        private int Bounces { get; } = 2;
        private int BouncesPerLvl { get; } = 1;
        private float Damage { get; } = 45.0f;
        private float DamagePerLvl { get; } = 45.0f;
        private float DamageAmount { get; set; }

        private timer BounceTimer { get; }
        private bool BFirstHit { get; set; } = true;
        private group Excluded { get; }
        private float Range { get; } = 470.0f;
        private float RangePerLvl { get; } = 30.0f;
        private float ReductionPerBounce { get; } = 0.06f;
        private float ReductionPerBounceLevel { get; } = 0.01f;
        private group Targets { get; set; }
        private unit LastTarget { get; set; }
        private float ReductionFactor { get; set; }
        private int BounceMax { get; set; }
        private float BounceRange { get; set; }

        public ChainLightning(unit caster, unit target, int abilityId) : base(caster, target)
        {
            AbilityId = abilityId;
            AbilityLevel = Caster.GetAbilityLevel(AbilityId);
            Speed = 1500.0f;
            EffectString = @"";
            CasterLaunchZ = 50.0f;
            DamageAmount = Damage + (DamagePerLvl * AbilityLevel);
            BounceRange = Range + (RangePerLvl * AbilityLevel);
            BounceMax = Bounces + (BouncesPerLvl * AbilityLevel);
            ReductionFactor = ReductionPerBounce - (ReductionPerBounceLevel * AbilityLevel);
            Excluded = group.Create();
            Excluded.Add(Target);
            BounceTimer = timer.Create();
        }

        public override void OnImpact()
        {
            if (BFirstHit)
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
                BFirstHit = false;
                BounceTimer.Start(0.25f, true, () =>
                {
                    if (BounceCount < BounceMax)
                    {
                        BounceCount += 1;
                        Targets = group.Create();
                        GroupEnumUnitsInRange(Targets, Target.X, Target.Y, BounceRange, Condition(Filter));
                        foreach (unit NearestUnit in Targets.ToList())
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
                            DamageAmount *= (1.0f - ReductionFactor);
                            Target.Damage(Caster, DamageAmount, ATTACK_TYPE_NORMAL, DAMAGE_TYPE_LIGHTNING);
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
                   && GetFilterUnit().IsEnemyTo(Caster.Owner)
                   && GetFilterUnit().IsValidTarget();
        }
    }
}
