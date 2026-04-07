using WCSharp.Api;
using WCSharp.Dummies;
using static WCSharp.Api.Blizzard;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class WaveSpell
{
    private int AbilityId;
    private unit Caster;
    private timer CastTimer;
    private unit Dummy;
    private int MaxWaves;
    private int OrderId;
    private location Target;
    private int Waves;

    public WaveSpell(unit InCaster, int InMaxWaves, int InAbilityId, int InOrderId, location InTarget)
    {
        Caster = InCaster;
        MaxWaves = InMaxWaves;
        AbilityId = InAbilityId;
        OrderId = InOrderId;
        Target = InTarget;
        Waves = 1;
        CastTimer = timer.Create();
    }

    public void Cast()
    {
        Dummy = DummySystem.GetDummy(Target.X, Target.Y, Caster.Owner);
        CastWave(Dummy.X, Dummy.Y);
        CastTimer.Start(1.0f, true, WaveSpellFinish);
    }

    private void CastWave(float X, float Y)
    {
        Dummy.AddAbility(AbilityId);
        Dummy.SetAbilityLevel(AbilityId, Caster.GetAbilityLevel(AbilityId));
        Dummy.IssueOrder(OrderId, X, Y);
    }

    private void WaveSpellFinish()
    {
        location RandomPoint = PolarProjectionBJ(GetUnitLoc(Dummy), GetRandomReal(50.00f, 150.00f), GetRandomDirectionDeg());
        SetUnitPositionLoc(Dummy, RandomPoint);
        CastWave(RandomPoint.X, RandomPoint.Y);
        RandomPoint.Dispose();
        if ((Waves += 1) >= MaxWaves)
        {
            DummySystem.RecycleDummy(Dummy, 0.25f);
            Target.Dispose();
            CastTimer.Dispose();
        }
    }
}
