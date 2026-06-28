using WCSharp.Api;
using WCSharp.Missiles;
using WCSharp.Shared.Extensions;
using static TGS.TextTags;
using static TGS.Util;
using static WCSharp.Api.Common;

namespace TGS.Spells
{
    public class ChainDeathCoil : BasicMissile
    {
        private int AbilityId { get; }
        private int AbilityLevel { get; }
        private int BounceCount { get; set; }
        private int BounceMax { get; }
        private int BouncesPerLvl { get; } = 1;
        private float Damage { get; } = 25.0f;
        private float DamagePerLvl { get; } = 45.0f;
        private float DamageAmount { get; set; }

        private bool bEnemy { get; }
        private group Excluded { get; }
        private float Range { get; } = 350.0f;
        private float RangePerLvl { get; } = 25.0f;
        private float ReductionPerBounce { get; } = 0.15f;
        private float BounceRange { get; }
        private group Targets { get; set; }

        public ChainDeathCoil(unit caster, unit target, int abilityId) : base(caster, target)
        {
            AbilityId = abilityId;
            AbilityLevel = Caster.GetAbilityLevel(AbilityId);
            Speed = 1100.0f;
            EffectString = @"Abilities\Spells\Undead\DeathCoil\DeathCoilMissile.mdl";
            CasterLaunchZ = 50.0f;
            DamageAmount = Damage + (DamagePerLvl * AbilityLevel);
            BounceRange = Range + (RangePerLvl * AbilityLevel);
            BounceMax = BouncesPerLvl * AbilityLevel;
            bEnemy = Target.IsEnemyTo(Caster.Owner);
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
            if (BounceCount < BounceMax)
            {
                BounceCount += 1;
                Targets = group.Create();
                GroupEnumUnitsInRange(Targets, Target.X, Target.Y, BounceRange, Condition(Filter));
                foreach (unit NearestUnit in Targets.ToList())
                {
                    Active = true;
                    Excluded.Add(NearestUnit);
                    Target = NearestUnit;
                    Targets.Dispose();
                    DamageAmount *= 1.0f - ReductionPerBounce;
                    return;
                }
            }

            Excluded.Dispose();
            Dispose();
        }

        private bool Filter()
        {
            return !IsUnitInGroup(GetFilterUnit(), Excluded)
                   && ((GetFilterUnit().IsEnemyTo(Caster.Owner) && bEnemy)
                       || (GetFilterUnit().IsAllyTo(Caster.Owner) && !bEnemy))
                   && GetFilterUnit().IsValidTarget();
        }
    }
}
