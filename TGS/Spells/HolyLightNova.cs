using WCSharp.Api;
using WCSharp.Shared.Extensions;
using static TGS.TextTags;
using static TGS.Util;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class HolyLightNova
{
    private int AbilityId;
    private unit Caster;
    private float Damage;
    private int Level;
    private unit Target;
    private group Targets;

    public HolyLightNova(unit InCaster, unit InTarget, int InAbilityId)
    {
        Caster = InCaster;
        Target = InTarget;
        AbilityId = InAbilityId;
        Level = Caster.GetAbilityLevel(AbilityId);
        Damage = 25.0f + (37.5f * Level);
        Targets = group.Create();
    }

    public void Cast()
    {
        GroupEnumUnitsInRange(Targets, Target.X, Target.Y, 250.0f + (10.0f * Level), Condition(null));
        foreach (unit NearestUnit in Targets.ToList())
        {
            if (TGSSpells.IsValidTarget(NearestUnit))
            {
                if (NearestUnit.IsEnemyTo(Caster.Owner))
                {
                    effect.Create(@"Abilities\Spells\Human\HolyBolt\HolyBoltSpecialArt.mdl", NearestUnit, "overhead").Dispose();
                    UnitDamageTarget(Caster, NearestUnit, Damage, true, false, ATTACK_TYPE_MAGIC, DAMAGE_TYPE_UNKNOWN, WEAPON_TYPE_WHOKNOWS);
                }
                else if (NearestUnit.IsAllyTo(Caster.Owner))
                {
                    effect.Create(@"Abilities\Spells\Human\HolyBolt\HolyBoltSpecialArt.mdl", NearestUnit, "overhead").Dispose();
                    float OutHealing = Damage / 2.0f;
                    MakeTag(OutHealing, Target, TagType.Heal);
                    NearestUnit.Heal(OutHealing);
                }
            }
        }

        Targets.Dispose();
    }
}
