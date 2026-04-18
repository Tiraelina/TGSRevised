using WCSharp.Api;
using WCSharp.Missiles;
using static TGS.Globals;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS.Spells;

public class BlessedHammer : OrbitalMissile
{
    private int AbilityId;
    private int AbilityLevel;
    private float Damage;
    public bool Expired = false;
    private float Lifespan;
    private float LifespanCurrent;

    public BlessedHammer(unit caster, unit target, int abilityId) : base(caster, target)
    {
        AbilityId = abilityId;
        AbilityLevel = Caster.GetAbilityLevel(AbilityId);
        EffectString = @"Abilities\Spells\Human\StormBolt\StormBoltMissile.mdl";
        Range = 150.0f;
        EffectScale = 2.0f;
        Interval = 0.1f;
        LifespanCurrent = 0.0f;
        Lifespan = 10.0f;
        Damage = 15.0f + (5.0f * AbilityLevel);
        CollisionRadius = 100.0f;
        OrbitalPeriod = 1.5f;
        CasterLaunchZ = 50.0f;
        PlaySoundOnUnitBJ(HeroMountainKingYesAttack1, 100, GetSpellAbilityUnit());
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
        LifespanCurrent += 0.1f;
        TargetsHit.Clear();
        if (LifespanCurrent >= Lifespan)
        {
            Expired = true;
            Active = false;
        }
    }

    public override void OnCollision(unit unit)
    {
        if (TGSSpells.IsValidTarget(unit))
        {
            UnitDamageTarget(Caster, unit, Damage, true, false, ATTACK_TYPE_MAGIC, DAMAGE_TYPE_UNKNOWN, WEAPON_TYPE_WHOKNOWS);
        }
    }
}
