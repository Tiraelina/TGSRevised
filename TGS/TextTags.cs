using System;
using WCSharp.Api;
using static TGS.Globals;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public static class TextTags
{
    public enum TagType
    {
        Normal,
        Crit,
        Miss,
        Evade,
        Spell,
        SpellCrit,
        Heal,
        Cleave,
        CleaveCrit,
        Gold,
        Research
    }

    public static void MakeTag(float EventDamage, unit DamageTarget, TagType Type)
    {
        texttag DamageTag = texttag.Create();
        string DamageText;
        DamageTag.SetPermanent(false);
        DamageTag.SetLifespan(0.75f);
        DamageTag.SetFadepoint(0.55f);
        DamageTag.SetVisibility(false);
        var LocalPlayer = GetLocalPlayer();
        if (DamageTarget.IsVisibleTo(LocalPlayer))
        {
            DamageTag.SetVisibility(true);
        }

        switch (Type)
        {
            case TagType.Normal:
                DamageText = $"{EventDamage:F}";
                DamageTag.SetColor(255, 255, 255);
                DamageTag.SetVelocity(0.0f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Crit:
                DamageText = $"{EventDamage:F}!";
                DamageTag.SetColor(255, 0, 0);
                DamageTag.SetVelocity(0.04f, 0.06f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Miss:
                DamageText = $"Miss";
                DamageTag.SetColor(255, 147, 0);
                DamageTag.SetVelocity(-0.04f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Evade:
                DamageText = $"Evade";
                DamageTag.SetColor(255, 255, 0);
                DamageTag.SetVelocity(-0.04f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Spell:
                DamageText = $"{EventDamage:F}";
                DamageTag.SetColor(0, 150, 255);
                DamageTag.SetVelocity(0.0f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.SpellCrit:
                DamageText = $"{EventDamage:F}!";
                DamageTag.SetColor(255, 150, 0);
                DamageTag.SetVelocity(0.04f, 0.06f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Heal:
                DamageText = $"{EventDamage:F}";
                DamageTag.SetColor(0, 255, 0);
                DamageTag.SetVelocity(0.0f, -0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.Cleave:
                DamageText = $"{EventDamage:F}";
                DamageTag.SetColor(128, 128, 128);
                DamageTag.SetVelocity(0.0f, 0.04f);
                DamageTag.SetText(DamageText, 0.024f);
                break;
            case TagType.CleaveCrit:
                DamageText = $"{EventDamage:F}!";
                DamageTag.SetColor(128, 0, 0);
                DamageTag.SetVelocity(0.04f, 0.06f);
                DamageTag.SetText(DamageText, 0.012f);
                break;
            case TagType.Gold:
                DamageText = $"{EventDamage:F}!";
                DamageTag.SetColor(209, 177, 22);
                DamageTag.SetVelocity(-0.04f, -0.06f);
                DamageTag.SetText(DamageText, 0.012f);
                break;
            case TagType.Research:
                DamageText = $"+{EventDamage:F} R";
                DamageTag.SetColor(104, 150, 255);
                DamageTag.SetVelocity(0.0f, 0.08f);
                DamageTag.SetText(DamageText, 0.028f);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type, null);
        }
        DamageTag.SetPosition(DamageTarget.X-(DamageText.Length * 10), DamageTarget.Y, 15.0f);
    }
}