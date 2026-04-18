using System;
using WCSharp.Api;
using WCSharp.Events;
using WCSharp.Shared;
using WCSharp.Shared.Extensions;
using WCSharp.Sync;
using static TGS.Util;
using static WCSharp.Api.Common;
using static WCSharp.Api.Blizzard;

namespace TGS
{
    public static class Program
    {
        public static bool Debug { get; private set; } = false;
        public static int InitCounter { get; private set; } = 0;

        public static void Main()
        {
            // Delay a little since some stuff can break otherwise
            timer MainTimer = CreateTimer();
            TimerStart(MainTimer, 0.01f, false, () =>
            {
                DestroyTimer(MainTimer);
                Start();
            });
        }

        private static void Start()
        {
            try
            {
#if DEBUG
				// This part of the code will only run if the map is compiled in Debug mode
				Debug = true;
				Console.WriteLine("This map is in debug mode. The map may not function as expected.");
				// By calling these methods, whenever these systems call external code (i.e. your code),
				// they will wrap the call in a try-catch and output any errors to the chat for easier debugging
				PeriodicEvents.EnableDebug();
				PlayerUnitEvents.EnableDebug();
				SyncSystem.EnableDebug();
				Delay.EnableDebug();
				OnClickXY();
				// timer MainTimer = CreateTimer();
				// TimerStart(MainTimer, 2.0f, false, () =>
				// {
				// 	DebugRestock();
				// 	DestroyTimer(MainTimer);
				// });
				timer MainTimer = CreateTimer();
				TimerStart(MainTimer, 3.0f, false, () =>
				{
					DestroyTimer(MainTimer);
					InitCount();
				});
				PreloadGenClear();
				PreloadGenStart();
				trigger PreloadSave = trigger.Create();
				PreloadSave.RegisterPlayerChatEvent(player.Create(0), "-save", false);
				PreloadSave.AddAction(() =>
				{
					foreach(string BufferLine in Globals.OutputBuffer)
					{
						Preload(BufferLine);
					}
					PreloadGenEnd("TGS/Cat.txt");
				});
#endif
				FogMaskEnableOn();
                FogEnableOn();

                group HumUnits = group.Create();
                HumUnits.EnumUnitsOfPlayer(player.Create(5));
                foreach (unit FoundUnit in HumUnits.ToList())
                {
                    FoundUnit.Mana = FoundUnit.MaxMana;
                }

                group OrcUnits = group.Create();
                OrcUnits.EnumUnitsOfPlayer(player.Create(11));
                foreach (unit FoundUnit in OrcUnits.ToList())
                {
                    FoundUnit.Mana = FoundUnit.MaxMana;
                }

                // GetPlayersAll() isn't picking up players >11
                player.Create(12).Color = playercolor.Orange;
                player.Create(12).Name = "Alliance";
                player.Create(12).SetState(playerstate.GivesBounty, 1);
                player.Create(13).Color = playercolor.Orange;
                player.Create(13).Name = "Alliance";
                player.Create(13).SetState(playerstate.GivesBounty, 1);
                player.Create(14).Color = playercolor.Orange;
                player.Create(14).Name = "Alliance";
                player.Create(14).SetState(playerstate.GivesBounty, 1);

                player.Create(15).Color = playercolor.Brown;
                player.Create(15).Name = "Horde";
                player.Create(15).SetState(playerstate.GivesBounty, 1);
                player.Create(16).Color = playercolor.Brown;
                player.Create(16).Name = "Horde";
                player.Create(16).SetState(playerstate.GivesBounty, 1);
                player.Create(17).Color = playercolor.Brown;
                player.Create(17).Name = "Horde";
                player.Create(17).SetState(playerstate.GivesBounty, 1);

                EnableCreepSleepBJ(false);
                Globals.Init();
                InitCounter += 1;
                Pathing.Init();
                InitCounter += 1;
                Army.Init();
                InitCounter += 1;
                Research.Init();
                InitCounter += 1;
                Creeps.CreepsCore.Init();
                InitCounter += 1;
                HeroSelection.Init();
                InitCounter += 1;
                Drops.Init();
                InitCounter += 1;
                Leaderboard.Init();
                InitCounter += 1;
                HeroData.Init();
                InitCounter += 1;
                Items.Init();
                InitCounter += 1;
                SummonLimiter.Init();
                InitCounter += 1;
                Summons.Init();
                InitCounter += 1;
                SpellsCore.Init();
                InitCounter += 1;
            }
            catch (Exception ex)
            {
	            GetLocalPlayer().DisplayTextTo(ex.Message);
            }
        }

#if DEBUG
        private static void InitCount()
        {
	        Console.WriteLine($"Init count {InitCounter}/13 succeeded.");
        }
#endif
    }
}
