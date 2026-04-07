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
        CircleToHero.Add(GetUnitAt(-8640.0f, 6528.0f), GetUnitAt(-8641.0f, 6682.5f));
        CircleToHero.Add(GetUnitAt(-8512.0f, 6528.0f), GetUnitAt(-8485.5f, 6673.8f));
        CircleToHero.Add(GetUnitAt(-8384.0f, 6464.0f), GetUnitAt(-8313.2f, 6585.2f));
        CircleToHero.Add(GetUnitAt(-8256.0f, 6336.0f), GetUnitAt(-8150.0f, 6436.5f));
        CircleToHero.Add(GetUnitAt(-8192.0f, 6208.0f), GetUnitAt(-8051.5f, 6212.8f));
        CircleToHero.Add(GetUnitAt(-8192.0f, 6016.0f), GetUnitAt(-8049.8f, 6014.8f));
        CircleToHero.Add(GetUnitAt(-8256.0f, 5888.0f), GetUnitAt(-8144.0f, 5808.0f));
        CircleToHero.Add(GetUnitAt(-8384.0f, 5824.0f), GetUnitAt(-8304.0f, 5680.0f));
        CircleToHero.Add(GetUnitAt(-8512.0f, 5760.0f), GetUnitAt(-8474.8f, 5554.2f));
        CircleToHero.Add(GetUnitAt(-8640.0f, 5760.0f), GetUnitAt(-8640.5f, 5587.0f));
        CircleToHero.Add(GetUnitAt(-8768.0f, 5760.0f), GetUnitAt(-8784.0f, 5584.0f));
        CircleToHero.Add(GetUnitAt(-8896.0f, 5824.0f), GetUnitAt(-9002.5f, 5691.5f));
        CircleToHero.Add(GetUnitAt(-9024.0f, 5888.0f), GetUnitAt(-9148.0f, 5794.8f));
        CircleToHero.Add(GetUnitAt(-9088.0f, 6016.0f), GetUnitAt(-9256.2f, 5997.5f));
        CircleToHero.Add(GetUnitAt(-9088.0f, 6208.0f), GetUnitAt(-9224.5f, 6209.0f));
        CircleToHero.Add(GetUnitAt(-9024.0f, 6336.0f), GetUnitAt(-9136.8f, 6405.5f));
        CircleToHero.Add(GetUnitAt(-8896.0f, 6464.0f), GetUnitAt(-8991.5f, 6564.2f));
        CircleToHero.Add(GetUnitAt(-8768.0f, 6528.0f), GetUnitAt(-8807.5f, 6671.0f));

        //ORC — circle → hero
        CircleToHero.Add(GetUnitAt(8128.0f, 6528.0f), GetUnitAt(8126.5f, 6691.8f));
        CircleToHero.Add(GetUnitAt(8256.0f, 6528.0f), GetUnitAt(8301.5f, 6666.8f));
        CircleToHero.Add(GetUnitAt(8384.0f, 6464.0f), GetUnitAt(8454.5f, 6586.8f));
        CircleToHero.Add(GetUnitAt(8512.0f, 6400.0f), GetUnitAt(8620.5f, 6505.0f));
        CircleToHero.Add(GetUnitAt(8576.0f, 6272.0f), GetUnitAt(8736.0f, 6319.2f));
        CircleToHero.Add(GetUnitAt(8576.0f, 6144.0f), GetUnitAt(8721.5f, 6149.2f));
        CircleToHero.Add(GetUnitAt(8576.0f, 6016.0f), GetUnitAt(8727.5f, 5985.5f));
        CircleToHero.Add(GetUnitAt(8512.0f, 5888.0f), GetUnitAt(8656.0f, 5808.0f));
        CircleToHero.Add(GetUnitAt(8384.0f, 5824.0f), GetUnitAt(8468.0f, 5670.5f));
        CircleToHero.Add(GetUnitAt(8256.0f, 5760.0f), GetUnitAt(8309.5f, 5586.5f));
        CircleToHero.Add(GetUnitAt(8128.0f, 5760.0f), GetUnitAt(8132.5f, 5581.8f));
        CircleToHero.Add(GetUnitAt(8000.0f, 5760.0f), GetUnitAt(7977.8f, 5615.8f));
        CircleToHero.Add(GetUnitAt(7872.0f, 5824.0f), GetUnitAt(7805.5f, 5696.0f));
        CircleToHero.Add(GetUnitAt(7744.0f, 5888.0f), GetUnitAt(7626.0f, 5828.5f));
        CircleToHero.Add(GetUnitAt(7680.0f, 6016.0f), GetUnitAt(7549.2f, 5980.8f));
        CircleToHero.Add(GetUnitAt(7680.0f, 6144.0f), GetUnitAt(7535.8f, 6147.2f));
        CircleToHero.Add(GetUnitAt(7680.0f, 6272.0f), GetUnitAt(7528.5f, 6285.0f));
        CircleToHero.Add(GetUnitAt(7744.0f, 6400.0f), GetUnitAt(7620.8f, 6483.5f));
        CircleToHero.Add(GetUnitAt(7872.0f, 6464.0f), GetUnitAt(7810.5f, 6600.0f));
        CircleToHero.Add(GetUnitAt(8000.0f, 6528.0f), GetUnitAt(7988.0f, 6680.2f));
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
