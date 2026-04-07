using WCSharp.Api;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class Berserk
{
    private timer BerserkAnimTick;
    private unit Caster;
    private int DamageBonusAbilityId = Constants.ABILITY_ALT4_ITEM_DAMAGE_BONUS_40;
    private float Duration = 20.0f;
    private int GreenBlue = 255;

    public Berserk(unit InCaster)
    {
        Caster = InCaster;
        BerserkAnimTick = timer.Create();
    }

    private void Expire()
    {
        Duration -= 0.025f;
        if (Duration <= 0.0f)
        {
            Caster.SetVertexColor(255, 255, 255);
            Caster.RemoveAbility(DamageBonusAbilityId);
        }
        else
        {
            if (GreenBlue > 55)
            {
                GreenBlue -= 2;
                Caster.SetVertexColor(255, GreenBlue, GreenBlue);
            }
        }
    }

    public void Cast()
    {
        Caster.AddAbility(DamageBonusAbilityId);
        TimerStart(BerserkAnimTick, 0.025f, true, Expire);
    }
}
