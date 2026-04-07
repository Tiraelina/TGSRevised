using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Events;
using static Constants;
using static Regions;
using static WCSharp.Api.Common;

namespace TGS;

public static class Pathing
{
    private static Dictionary<region, rect> HumAttackPath = new();
    private static Dictionary<region, rect> OrcAttackPath = new();

    public static void Init()
    {
        HumAttackPath.Add(HumCross1.Region, HumAttackTopWay1.Rect);
        HumAttackPath.Add(HumWorkShopTop.Region, HumAttackTopWay1.Rect);
        HumAttackPath.Add(HumArcaneTop.Region, HumAttackTopWay1.Rect);
        HumAttackPath.Add(HumGryphonAviaryTop.Region, HumAttackTopWay1.Rect);
        HumAttackPath.Add(HumBarrackTop.Region, HumAttackTopWay1.Rect);
        HumAttackPath.Add(HumAttackTopWay1.Region, HumAttackTopWay2.Rect);
        HumAttackPath.Add(HumAttackTopWay2.Region, HumAttackTopWay3.Rect);
        HumAttackPath.Add(HumAttackTopWay3.Region, HumAttackTopWay4.Rect);
        HumAttackPath.Add(HumAttackTopWay4.Region, OrcBarrackTop.Rect);

        HumAttackPath.Add(TopWay1.Region, HumAttackTopWay1.Rect);
        HumAttackPath.Add(TopWay2.Region, HumAttackTopWay2.Rect);
        HumAttackPath.Add(TopWay3.Region, HumAttackTopWay3.Rect);
        HumAttackPath.Add(TopWay4.Region, HumAttackTopWay3.Rect);
        HumAttackPath.Add(TopWay5.Region, HumAttackTopWay4.Rect);
        HumAttackPath.Add(TopWay6.Region, OrcBarrackTop.Rect);

        HumAttackPath.Add(HumCross2.Region, HumAttackMidWay1.Rect);
        HumAttackPath.Add(HumCross3.Region, HumAttackMidWay1.Rect);
        HumAttackPath.Add(HumWorkShopMid.Region, HumAttackMidWay1.Rect);
        HumAttackPath.Add(HumArcaneMid.Region, HumAttackMidWay1.Rect);
        HumAttackPath.Add(HumGryphonAviaryMid.Region, HumAttackMidWay1.Rect);
        HumAttackPath.Add(HumBarrackMid.Region, HumAttackMidWay1.Rect);
        HumAttackPath.Add(HumAttackMidWay1.Region, HumAttackMidWay2.Rect);
        HumAttackPath.Add(HumAttackMidWay2.Region, HumAttackMidWay3.Rect);
        HumAttackPath.Add(HumAttackMidWay3.Region, HumAttackMidWay4.Rect);
        HumAttackPath.Add(HumAttackMidWay4.Region, HumAttackMidWay5.Rect);
        HumAttackPath.Add(HumAttackMidWay5.Region, HumAttackMidWay6.Rect);
        HumAttackPath.Add(HumAttackMidWay6.Region, OrcBarrackMid.Rect);

        HumAttackPath.Add(MidWay1.Region, HumAttackMidWay1.Rect);
        HumAttackPath.Add(MidWay2.Region, HumAttackMidWay2.Rect);
        HumAttackPath.Add(MidWay3.Region, HumAttackMidWay4.Rect);
        HumAttackPath.Add(MidWay4.Region, HumAttackMidWay4.Rect);
        HumAttackPath.Add(MidWay5.Region, HumAttackMidWay6.Rect);
        HumAttackPath.Add(MidWay6.Region, OrcBarrackMid.Rect);

        HumAttackPath.Add(HumCross4.Region, HumAttackBotWay1.Rect);
        HumAttackPath.Add(HumWorkShopBot.Region, HumAttackBotWay1.Rect);
        HumAttackPath.Add(HumArcaneBot.Region, HumAttackBotWay1.Rect);
        HumAttackPath.Add(HumGryphonAviaryBot.Region, HumAttackBotWay1.Rect);
        HumAttackPath.Add(HumBarrackBot.Region, HumAttackBotWay1.Rect);
        HumAttackPath.Add(HumAttackBotWay1.Region, HumAttackBotWay2.Rect);
        HumAttackPath.Add(HumAttackBotWay2.Region, HumAttackBotWay3.Rect);
        HumAttackPath.Add(HumAttackBotWay3.Region, HumAttackBotWay4.Rect);
        HumAttackPath.Add(HumAttackBotWay4.Region, OrcBarrackBot.Rect);

        HumAttackPath.Add(BotWay1.Region, HumAttackBotWay1.Rect);
        HumAttackPath.Add(BotWay2.Region, HumAttackBotWay2.Rect);
        HumAttackPath.Add(BotWay3.Region, HumAttackBotWay3.Rect);
        HumAttackPath.Add(BotWay4.Region, HumAttackBotWay3.Rect);
        HumAttackPath.Add(BotWay5.Region, HumAttackBotWay4.Rect);
        HumAttackPath.Add(BotWay6.Region, OrcBarrackBot.Rect);

        HumAttackPath.Add(CenterAirLine1.Region, HumAttackMidWay2.Rect);
        HumAttackPath.Add(CenterAirLine2.Region, HumAttackMidWay4.Rect);
        HumAttackPath.Add(CenterAirLine3.Region, HumAttackMidWay4.Rect);
        HumAttackPath.Add(CenterAirLine4.Region, HumAttackMidWay6.Rect);

        HumAttackPath.Add(OrcBarrackTop.Region, OrcHall.Rect);
        HumAttackPath.Add(OrcBarrackMid.Region, OrcHall.Rect);
        HumAttackPath.Add(OrcBarrackBot.Region, OrcHall.Rect);


        OrcAttackPath.Add(OrcCross1.Region, OrcAttackTopWay1.Rect);
        OrcAttackPath.Add(OrcTaurenTop.Region, OrcAttackTopWay1.Rect);
        OrcAttackPath.Add(OrcSpiritTop.Region, OrcAttackTopWay1.Rect);
        OrcAttackPath.Add(OrcBeastryTop.Region, OrcAttackTopWay1.Rect);
        OrcAttackPath.Add(OrcBarrackTop.Region, OrcAttackTopWay1.Rect);
        OrcAttackPath.Add(OrcAttackTopWay1.Region, OrcAttackTopWay2.Rect);
        OrcAttackPath.Add(OrcAttackTopWay2.Region, OrcAttackTopWay3.Rect);
        OrcAttackPath.Add(OrcAttackTopWay3.Region, OrcAttackTopWay4.Rect);
        OrcAttackPath.Add(OrcAttackTopWay4.Region, HumBarrackTop.Rect);

        OrcAttackPath.Add(TopWay6.Region, OrcAttackTopWay1.Rect);
        OrcAttackPath.Add(TopWay5.Region, OrcAttackTopWay2.Rect);
        OrcAttackPath.Add(TopWay4.Region, OrcAttackTopWay3.Rect);
        OrcAttackPath.Add(TopWay3.Region, OrcAttackTopWay3.Rect);
        OrcAttackPath.Add(TopWay2.Region, OrcAttackTopWay4.Rect);
        OrcAttackPath.Add(TopWay1.Region, HumBarrackTop.Rect);

        OrcAttackPath.Add(OrcCross2.Region, OrcAttackMidWay1.Rect);
        OrcAttackPath.Add(OrcCross3.Region, OrcAttackMidWay1.Rect);
        OrcAttackPath.Add(OrcTaurenMid.Region, OrcAttackMidWay1.Rect);
        OrcAttackPath.Add(OrcSpiritMid.Region, OrcAttackMidWay1.Rect);
        OrcAttackPath.Add(OrcBeastryMid.Region, OrcAttackMidWay1.Rect);
        OrcAttackPath.Add(OrcBarrackMid.Region, OrcAttackMidWay1.Rect);
        OrcAttackPath.Add(OrcAttackMidWay1.Region, OrcAttackMidWay2.Rect);
        OrcAttackPath.Add(OrcAttackMidWay2.Region, OrcAttackMidWay3.Rect);
        OrcAttackPath.Add(OrcAttackMidWay3.Region, OrcAttackMidWay4.Rect);
        OrcAttackPath.Add(OrcAttackMidWay4.Region, OrcAttackMidWay5.Rect);
        OrcAttackPath.Add(OrcAttackMidWay5.Region, OrcAttackMidWay6.Rect);
        OrcAttackPath.Add(OrcAttackMidWay6.Region, HumBarrackMid.Rect);

        OrcAttackPath.Add(MidWay6.Region, OrcAttackMidWay1.Rect);
        OrcAttackPath.Add(MidWay5.Region, OrcAttackMidWay2.Rect);
        OrcAttackPath.Add(MidWay4.Region, OrcAttackMidWay4.Rect);
        OrcAttackPath.Add(MidWay3.Region, OrcAttackMidWay4.Rect);
        OrcAttackPath.Add(MidWay2.Region, OrcAttackMidWay6.Rect);
        OrcAttackPath.Add(MidWay1.Region, HumBarrackMid.Rect);

        OrcAttackPath.Add(OrcCross4.Region, OrcAttackBotWay1.Rect);
        OrcAttackPath.Add(OrcTaurenBot.Region, OrcAttackBotWay1.Rect);
        OrcAttackPath.Add(OrcSpiritBot.Region, OrcAttackBotWay1.Rect);
        OrcAttackPath.Add(OrcBeastryBot.Region, OrcAttackBotWay1.Rect);
        OrcAttackPath.Add(OrcBarrackBot.Region, OrcAttackBotWay1.Rect);
        OrcAttackPath.Add(OrcAttackBotWay1.Region, OrcAttackBotWay2.Rect);
        OrcAttackPath.Add(OrcAttackBotWay2.Region, OrcAttackBotWay3.Rect);
        OrcAttackPath.Add(OrcAttackBotWay3.Region, OrcAttackBotWay4.Rect);
        OrcAttackPath.Add(OrcAttackBotWay4.Region, HumBarrackBot.Rect);

        OrcAttackPath.Add(BotWay6.Region, OrcAttackBotWay1.Rect);
        OrcAttackPath.Add(BotWay5.Region, OrcAttackBotWay2.Rect);
        OrcAttackPath.Add(BotWay4.Region, OrcAttackBotWay3.Rect);
        OrcAttackPath.Add(BotWay3.Region, OrcAttackBotWay3.Rect);
        OrcAttackPath.Add(BotWay2.Region, OrcAttackBotWay4.Rect);
        OrcAttackPath.Add(BotWay1.Region, HumBarrackBot.Rect);

        OrcAttackPath.Add(CenterAirLine4.Region, OrcAttackMidWay2.Rect);
        OrcAttackPath.Add(CenterAirLine2.Region, OrcAttackMidWay4.Rect);
        OrcAttackPath.Add(CenterAirLine3.Region, OrcAttackMidWay4.Rect);
        OrcAttackPath.Add(CenterAirLine1.Region, HumAttackMidWay6.Rect);

        OrcAttackPath.Add(HumBarrackTop.Region, HumHall.Rect);
        OrcAttackPath.Add(HumBarrackMid.Region, HumHall.Rect);
        OrcAttackPath.Add(HumBarrackBot.Region, HumHall.Rect);

        var AttackRegions = new HashSet<region>();

        foreach (var Kvp in HumAttackPath)
        {
            AttackRegions.Add(Kvp.Key);
        }

        foreach (var Kvp in OrcAttackPath)
        {
            AttackRegions.Add(Kvp.Key);
        }

        foreach (region Target in AttackRegions)
        {
            PlayerUnitEvents.Register(RegionUnitTypeEvent.Enters, GetPath, Target);
        }
    }

    private static void GetPath()
    {
        if (GetTriggerUnit().UnitType != UNIT_UFBD_ARCHITECT
            && GetTriggerUnit().Owner.Controller == mapcontrol.Computer)
        {
            if (GetTriggerUnit().Owner.IsAlly(player.Create(5)))
            {
                HumAttackPath.TryGetValue(GetTriggeringRegion(), out rect Value);
                if (Value != null)
                {
                    GetTriggerUnit().IssueOrder(ORDER_ATTACK, Value.CenterX, Value.CenterY);
                }
            }
            else
            {
                OrcAttackPath.TryGetValue(GetTriggeringRegion(), out rect Value);
                if (Value != null)
                {
                    GetTriggerUnit().IssueOrder(ORDER_ATTACK, Value.CenterX, Value.CenterY);
                }
            }
        }
    }
}
