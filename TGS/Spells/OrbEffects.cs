using System;
using WCSharp.Api;
using WCSharp.Api.Enums;
using WCSharp.Buffs;
using static TGS.Util;
using static Constants;
using static WCSharp.Api.Common;

namespace TGS.Spells;

public class Feedback : IOrbEffect
{
    private float ManaBurnedBase = 2.0f;
    private float ManaBurnedInc = 8.0f;
    private float SummonBonusInc = 40.0f;
    public unit Owner { get; set; }
    public int Level { get; set; }
    public effect WeaponEffect { get; set; }

    public void Aquire(unit InOwner, int InLevel)
    {
        Owner = InOwner;
        Level = InLevel;
        effect.Create(@"Abilities\Spells\Items\AIlb\AIlbTarget.mdl", Owner, "weapon");
    }

    public void Remove()
    {
        WeaponEffect.Dispose();
    }

    public void OnHit(unit Source, unit Target, ref float Damage)
    {
        float TargetMana = Target.Mana;
        if (TargetMana <= 0)
        {
            return;
        }
        float ManaBurned = ManaBurnedBase + ManaBurnedInc * Level;
        float BurnAmount = Math.Min(TargetMana, ManaBurned);
        Target.Mana -= BurnAmount;
        Damage += BurnAmount * 0.5f;

        if (Target.UnitClassification == UnitClassifications.Summoned)
        {
            Damage += SummonBonusInc * Level;
        }

        MakeTag(ManaBurnedInc, Target, TagType.Spell);
        AddSpecialEffectTarget(@"Abilities\Spells\Human\Feedback\ArcaneTowerAttack.mdl", Target, "origin").Dispose();
    }

    public string GetName()
    {
        return $"Feedback Level {Level}";
    }
}

public class BlackArrow : IOrbEffect
{
    private float DamageBonusInc = 7.5f;
    private float ManaCost = 8.0f;
    public unit Owner { get; set; }
    public int Level { get; set; }
    public effect WeaponEffect { get; set; }

    public void Aquire(unit InOwner, int InLevel)
    {
        Owner = InOwner;
        Level = InLevel;
        WeaponEffect = effect.Create(@"Abilities\Spells\Items\OrbDarkness\OrbDarkness.mdl", Owner, "weapon");
    }

    public void Remove()
    {
        WeaponEffect.Dispose();
    }

    public void OnHit(unit Source, unit Target, ref float Damage)
    {
        var ASS = new BlackArrowBuff(Source, Target, Level);
        BuffSystem.Add(ASS, StackBehaviour.StackPlayer);
        if (Source.Mana >= ManaCost)
        {
            Source.Mana -= ManaCost;
            Damage += DamageBonusInc * Level;
            AddSpecialEffectTarget(@"Abilities\Spells\Other\BlackArrow\BlackArrowMissile.mdl", Target, "origin").Dispose();
        }
    }

    public string GetName()
    {
        return $"Black Arrow Level {Level}";
    }
}

public class BlackArrowBuff : PassiveBuff
{
    private float DurationBase = 1.5f;
    private float DurationInc = 0.5f;
    private float LifespanBase = 60.0f;
    private float LifespanInc = 20.0f;
    private int Level;
    public BlackArrowBuff(unit caster, unit target, int InLevel) : base(caster, target)
    {
        Level = InLevel;
        Duration = DurationBase + DurationInc * Level;
    }

    public override void OnDeath(bool bKillingBlow)
    {
        int BonerMan = UNIT_NDR1_LESSER_DARK_MINION_L1;
        //int Amount = 1;
        switch (Level)
        {
            case 1:
                BonerMan = UNIT_NDR1_LESSER_DARK_MINION_L1;
                break;
            case 2:
                BonerMan = UNIT_NDR2_DARK_MINION_L2;
                break;
            case 3:
                BonerMan = UNIT_NDR3_GREATER_DARK_MINION_L3;
                break;
            case 4:
                BonerMan = UNIT_N013_GREATER_DARK_MINION_L4;
                break;
            case 5:
                BonerMan = UNIT_N014_GREATER_DARK_MINION_L5;
                break;
            case 6:
                BonerMan = UNIT_N015_GREATER_DARK_MINION_L6;
                break;
            case 7:
                BonerMan = UNIT_N016_GREATER_DARK_MINION_L7;
                break;
            case 8:
                BonerMan = UNIT_N017_GREATER_DARK_MINION_L8;
                break;
            case 9:
                BonerMan = UNIT_N018_GREATER_DARK_MINION_L9;
                break;
            case 10:
                BonerMan = UNIT_N012_GREATER_DARK_MINION_L10;
                break;
        }
        unit Boner = unit.Create(Caster.Owner, BonerMan, Target.X, Target.Y);
        Boner.ApplyTimedLife(FourCC("BTLF"), LifespanBase + LifespanInc * Level);
        Summons.ScaleSummon(Caster, Boner);

    }
}

public class Pillage : IOrbEffect
{
    private float Step = 1.0f;
    private float CostRatio = 0.05f;
    public unit Owner { get; set; }
    public int Level { get; set; }
    public effect WeaponEffect { get; set; }

    public void Aquire(unit InOwner, int InLevel)
    {
        Owner = InOwner;
        Level = InLevel;
    }

    public void Remove()
    {
    }

    public void OnHit(unit Source, unit Target, ref float Damage)
    {
        // Not concerned about exact accuracy of Step. Would probably need to track per target damage otherwise.
        float GoldValue = Target.GoldCost * (CostRatio * Level);
        if (GoldValue > 0.0f)
        {
            float LifeDelta = Math.Min(((Damage - Target.MaxLife) / Target.MaxLife) + 1.0f, 1.0f);
            float GoldGain = GoldValue * LifeDelta;
            float GoldTaken = Math.Max(GoldGain, Step);
            if (GoldTaken > 0.0f)
            {
                Source.Owner.Gold += (int)GoldTaken;
                MakeTag(GoldTaken, Source, TagType.Gold);
                AddSpecialEffectTarget(@"Abilities\Spells\Items\ResourceItems\ResourceEffectTarget.mdl", Target, "origin").Dispose();
            }
        }
    }

    public string GetName()
    {
        return $"Pillage Level {Level}";
    }
}

public class Cleave : IOrbEffect
{
    private float ManaBurnedBase = 2.0f;
    private float ManaBurnedInc = 8.0f;
    private float SummonBonusInc = 40.0f;
    public unit Owner { get; set; }
    public int Level { get; set; }
    public effect WeaponEffect { get; set; }

    public void Aquire(unit InOwner, int InLevel)
    {
        Owner = InOwner;
        Level = InLevel;
    }

    public void Remove()
    {
        WeaponEffect.Dispose();
    }

    public void OnHit(unit Source, unit Target, ref float Damage)
    {
    }

    public string GetName()
    {
        return $"Cleave Level {Level}";
    }
}

public interface IOrbEffect
{
    public unit Owner { get; set; }
    public int Level { get; set; }
    public effect WeaponEffect { get; set; }

    public void Aquire(unit InOwner, int InLevel);
    public void Remove();
    public void OnHit(unit Source, unit Target, ref float Damage);

    public string GetName();
}
