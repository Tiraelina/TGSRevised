using System.Collections.Generic;
using WCSharp.Api;
using WCSharp.Shared.Data;
using static TGS.Util;
using static Constants;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS;

public static class HeroSelection
{
    private static int HumWisps;
    private static int OrcWisps;
    private static Point HumSelectors;
    private static Point OrcSelectors;
    private static Dictionary<trigger, SelectedHero> TriggerToHero = new();
    private static Dictionary<unit, unit> CircleToHero = new();

    public static void Init()
    {
        HumSelectors = new Point(GetPlayerStartLocationX(player.Create(0)), GetPlayerStartLocationY(player.Create(0)));
        OrcSelectors = new Point(GetPlayerStartLocationX(player.Create(6)), GetPlayerStartLocationY(player.Create(6)));
        SpawnSelectors();

        // ALLIANCE — circle → hero
        CircleToHero.Add(GetUnitAt(-9920.0f, 12608.0f), GetUnitAt(-9902.0f, 12744.2f));
        CircleToHero.Add(GetUnitAt(-9728.0f, 12544.0f), GetUnitAt(-9651.5f, 12695.5f));
        CircleToHero.Add(GetUnitAt(-9536.0f, 12416.0f), GetUnitAt(-9430.0f, 12516.0f));
        CircleToHero.Add(GetUnitAt(-9408.0f, 12288.0f), GetUnitAt(-9285.5f, 12389.0f));
        CircleToHero.Add(GetUnitAt(-9280.0f, 12096.0f), GetUnitAt(-9143.8f, 12141.8f));
        CircleToHero.Add(GetUnitAt(-9216.0f, 11904.0f), GetUnitAt(-9070.5f, 11910.0f));
        CircleToHero.Add(GetUnitAt(-9216.0f, 11712.0f), GetUnitAt(-9083.5f, 11706.5f));
        CircleToHero.Add(GetUnitAt(-9280.0f, 11520.0f), GetUnitAt(-9128.5f, 11508.0f));
        CircleToHero.Add(GetUnitAt(-9344.0f, 11328.0f), GetUnitAt(-9251.0f, 11264.5f));
        CircleToHero.Add(GetUnitAt(-9472.0f, 11200.0f), GetUnitAt(-9370.8f, 11104.8f));
        CircleToHero.Add(GetUnitAt(-9664.0f, 11072.0f), GetUnitAt(-9551.5f, 10934.0f));
        CircleToHero.Add(GetUnitAt(-9856.0f, 11008.0f), GetUnitAt(-9825.5f, 10840.5f));
        CircleToHero.Add(GetUnitAt(-10048.0f, 11008.0f), GetUnitAt(-10037.5f, 10882.5f));
        CircleToHero.Add(GetUnitAt(-10240.0f, 11072.0f), GetUnitAt(-10298.5f, 10941.5f));
        CircleToHero.Add(GetUnitAt(-10432.0f, 11200.0f), GetUnitAt(-10535.0f, 11054.0f));
        CircleToHero.Add(GetUnitAt(-10560.0f, 11328.0f), GetUnitAt(-10680.0f, 11226.2f));
        CircleToHero.Add(GetUnitAt(-10624.0f, 11520.0f), GetUnitAt(-10771.2f, 11487.5f));
        CircleToHero.Add(GetUnitAt(-10688.0f, 11712.0f), GetUnitAt(-10837.2f, 11692.8f));
        CircleToHero.Add(GetUnitAt(-10624.0f, 12096.0f), GetUnitAt(-10766.8f, 12173.8f));
        CircleToHero.Add(GetUnitAt(-10560.0f, 12288.0f), GetUnitAt(-10659.2f, 12375.2f));
        CircleToHero.Add(GetUnitAt(-10304.0f, 12544.0f), GetUnitAt(-10385.5f, 12685.8f));
        CircleToHero.Add(GetUnitAt(-10112.0f, 12608.0f), GetUnitAt(-10138.5f, 12737.0f));
        CircleToHero.Add(GetUnitAt(-10432.0f, 12416.0f), GetUnitAt(-10513.5f, 12518.2f));
        CircleToHero.Add(GetUnitAt(-10688.0f, 11904.0f), GetUnitAt(-10832.2f, 11901.8f));

        //ORC — circle → hero
        CircleToHero.Add(GetUnitAt(10368.0f, 12096.0f), GetUnitAt(10390.0f, 12223.8f));
        CircleToHero.Add(GetUnitAt(10560.0f, 12096.0f), GetUnitAt(10557.5f, 12239.8f));
        CircleToHero.Add(GetUnitAt(10752.0f, 12096.0f), GetUnitAt(10792.8f, 12248.0f));
        CircleToHero.Add(GetUnitAt(10944.0f, 11968.0f), GetUnitAt(11084.5f, 12071.0f));
        CircleToHero.Add(GetUnitAt(11008.0f, 11776.0f), GetUnitAt(11156.0f, 11843.5f));
        CircleToHero.Add(GetUnitAt(11072.0f, 11584.0f), GetUnitAt(11257.5f, 11590.2f));
        CircleToHero.Add(GetUnitAt(11072.0f, 11392.0f), GetUnitAt(11250.5f, 11349.5f));
        CircleToHero.Add(GetUnitAt(11072.0f, 11136.0f), GetUnitAt(11245.2f, 11116.5f));
        CircleToHero.Add(GetUnitAt(11008.0f, 10944.0f), GetUnitAt(11128.8f, 10839.2f));
        CircleToHero.Add(GetUnitAt(10816.0f, 10816.0f), GetUnitAt(10928.8f, 10683.2f));
        CircleToHero.Add(GetUnitAt(10688.0f, 10688.0f), GetUnitAt(10786.8f, 10542.2f));
        CircleToHero.Add(GetUnitAt(10496.0f, 10560.0f), GetUnitAt(10523.0f, 10368.5f));
        CircleToHero.Add(GetUnitAt(10304.0f, 10560.0f), GetUnitAt(10319.2f, 10383.0f));
        CircleToHero.Add(GetUnitAt(10112.0f, 10560.0f), GetUnitAt(10129.5f, 10406.8f));
        CircleToHero.Add(GetUnitAt(9920.0f, 10688.0f), GetUnitAt(9819.0f, 10547.5f));
        CircleToHero.Add(GetUnitAt(9792.0f, 10816.0f), GetUnitAt(9668.5f, 10733.5f));
        CircleToHero.Add(GetUnitAt(9664.0f, 11008.0f), GetUnitAt(9523.8f, 10993.2f));
        CircleToHero.Add(GetUnitAt(9664.0f, 11200.0f), GetUnitAt(9505.8f, 11190.8f));
        CircleToHero.Add(GetUnitAt(9664.0f, 11392.0f), GetUnitAt(9505.2f, 11373.0f));
        CircleToHero.Add(GetUnitAt(9664.0f, 11584.0f), GetUnitAt(9505.2f, 11602.5f));
        CircleToHero.Add(GetUnitAt(9728.0f, 11776.0f), GetUnitAt(9607.2f, 11856.0f));
        CircleToHero.Add(GetUnitAt(9856.0f, 11904.0f), GetUnitAt(9756.5f, 12012.8f));
        CircleToHero.Add(GetUnitAt(9984.0f, 12032.0f), GetUnitAt(9881.5f, 12149.8f));
        CircleToHero.Add(GetUnitAt(10176.0f, 12096.0f), GetUnitAt(10155.5f, 12243.5f));
        InitHeroSelectionEvents();
    }

    public static void InitHeroSelectionEvents()
    {
        foreach (var Hvp in CircleToHero)
        {
            trigger HeroSelectionTrigger = trigger.Create();
            TriggerToHero.Add(HeroSelectionTrigger, new SelectedHero(Hvp.Value, Hvp.Key));
            TriggerRegisterUnitInRangeSimple(HeroSelectionTrigger, 75.0f, Hvp.Key);
            TriggerAddAction(HeroSelectionTrigger, () =>
            {
                if (!TriggerToHero[HeroSelectionTrigger].Selected)
                {
                    unit EnteringUnit = GetTriggerUnit();
                    player OwningPlayer = GetOwningPlayer(EnteringUnit);
                    HeroSelect(OwningPlayer);
                    RemoveUnit(EnteringUnit);
                    CircleToHero.Remove(GetTriggerUnit());
                }
            });
        }
    }

    public static void HeroSelect(player Player)
    {
        var InHero = TriggerToHero[GetTriggeringTrigger()];
        GetTriggeringTrigger().Dispose();
        InHero.Hero.SetVertexColor(100, 100, 100, 60);
        unit NewHero;
        if (Globals.Human.Contains(Player))
        {
            HumWisps -= 1;
            NewHero = unit.Create(Player, InHero.Hero.UnitType, Globals.HumFountain.X, Globals.HumFountain.Y);
            PanCameraToTimedForPlayer(Player, Globals.HumFountain.X, Globals.HumFountain.Y, 0.5f);
        }
        else
        {
            OrcWisps -= 1;
            NewHero = unit.Create(Player, InHero.Hero.UnitType, Globals.OrcFountain.X, Globals.OrcFountain.Y);
            PanCameraToTimedForPlayer(Player, Globals.OrcFountain.X, Globals.OrcFountain.Y, 0.5f);
        }

        Globals.Players[Player].SetHero(NewHero);
        if (HumWisps == 0 && OrcWisps == 0)
        {
            foreach (var Kvp in CircleToHero)
            {
                RemoveUnit(Kvp.Key);
                RemoveUnit(Kvp.Value);
                Kvp.Key.Dispose();
                Kvp.Value.Dispose();
            }

            CircleToHero.Clear();
        }
    }

    private static void SpawnSelectors()
    {
        for (int i = 0; i <= 4; i++)
        {
            player Player = player.Create(i);
            if (Player.SlotState == playerslotstate.Playing && Player.Controller == mapcontrol.User)
            {
                unit Wisp = unit.Create(Player, UNIT_E006_HERO_SELECTOR, HumSelectors.X, HumSelectors.Y);
                HumWisps += 1;
                SelectUnitAddForPlayer(Wisp, Player);
                PanCameraToForPlayer(Player, HumSelectors.X, HumSelectors.Y);
            }
        }

        for (int i = 6; i <= 10; i++)
        {
            player Player = player.Create(i);
            if (Player.SlotState == playerslotstate.Playing && Player.Controller == mapcontrol.User)
            {
                unit Wisp = unit.Create(Player, UNIT_E006_HERO_SELECTOR, OrcSelectors.X, OrcSelectors.Y);
                OrcWisps += 1;
                SelectUnitAddForPlayer(Wisp, Player);
                PanCameraToForPlayer(Player, OrcSelectors.X, OrcSelectors.Y);
            }
        }
    }
}

internal struct SelectedHero
{
    public unit Hero { get; init; }
    public unit Circle { get; init; }
    public bool Selected { get; init; } = false;

    public SelectedHero(unit hero, unit circle)
    {
        Hero = hero;
        Circle = circle;
    }
}
