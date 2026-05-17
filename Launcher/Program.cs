using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CSharpLua;
using CSharpLua.CoreSystem;
using Microsoft.CodeAnalysis;
using War3Net.Build;
using War3Net.Build.Extensions;
using War3Net.IO.Mpq;
using WCSharp.ConstantGenerator;

namespace Launcher
{
    internal static class Program
    {
        // Input
        private const string SOURCE_CODE_PROJECT_FOLDER_PATH = @"..\..\..\..\..\TGS";
        private const string BASE_MAP_PATH = @"..\..\..\..\..\TGSMap.w3x";
        private const string BUILD_VERSION_FILE = @"..\..\..\..\..\buildnumber.txt";

        // Output
        private const string OUTPUT_FOLDER_PATH = @"..\..\..\..\..\artifacts";
        private const string OUTPUT_SCRIPT_NAME = @"war3map.lua";
        private const string OUTPUT_TEST_MAP_NAME = $"target.w3x";
        private const string OUTPUT_MAP_FILE_NAME = $"TGSRevised";
        private const string OUTPUT_MAP_FULL_NAME = $"TGS II Revised";
        private const int MAP_NAME_WTS_INDEX = 1;

        // Warcraft III
        private const string GRAPHICS_API = "Direct3D11";
        private const bool PAUSE_GAME_ON_LOSE_FOCUS = false;
#if DEBUG
		private const bool DEBUG = true;
#else
        private const bool DEBUG = false;
#endif

        private static void Main()
        {
            Console.WriteLine("The following actions are available:");
            Console.WriteLine("1. Generate constants");
            Console.WriteLine("2. Compile map");
            Console.WriteLine("3. Compile and run map");
            MakeDecision();
        }

        private static void MakeDecision()
        {
            Console.Write("Please type the number of your desired action: ");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    ConstantGenerator.Run(BASE_MAP_PATH, SOURCE_CODE_PROJECT_FOLDER_PATH, new ConstantGeneratorOptions
                    {
                        IncludeCode = true
                    });
                    break;
                case ConsoleKey.D2:
                    Build(false, true);
                    break;
                case ConsoleKey.D3:
                    Build(true);
                    break;
                default:
                    Console.WriteLine($"{Environment.NewLine}Invalid input. Please choose again.");
                    MakeDecision();
                    break;
            }
        }

        public static void Build(bool launch, bool bExport = false, bool bIncrement = false)
        {
            if (bIncrement)
            {
                IncrementBuild();
            }
            // Ensure these folders exist
            Directory.CreateDirectory(OUTPUT_FOLDER_PATH);

            // Load existing map data
            var map = Map.Open(BASE_MAP_PATH);
            var builder = new MapBuilder(map);
            builder.AddFiles(BASE_MAP_PATH, "*", SearchOption.AllDirectories);
            // WTS seems stable enough. TODO: Check the actual built map.
            if (builder.Map.TriggerStrings != null)
            {
                var MapVersionName = builder.Map.TriggerStrings.Strings[MAP_NAME_WTS_INDEX];
                MapVersionName.Value = $"{OUTPUT_MAP_FULL_NAME} {CurrentBuild}";
                builder.Map.TriggerStrings.Strings[MAP_NAME_WTS_INDEX] = MapVersionName;
                Console.WriteLine($"\n{builder.Map.TriggerStrings?.Strings[MAP_NAME_WTS_INDEX].Key} {builder.Map.TriggerStrings?.Strings[MAP_NAME_WTS_INDEX].Value} {builder.Map.TriggerStrings?.Strings[MAP_NAME_WTS_INDEX].Comment}");
            }

            // Set debug options if necessary, configure compiler
            var csc = DEBUG ? "-debug -define:DEBUG" : null;
            var csproj = Directory.EnumerateFiles(SOURCE_CODE_PROJECT_FOLDER_PATH, "*.csproj", SearchOption.TopDirectoryOnly).Single();
            var compiler = new Compiler(csproj, OUTPUT_FOLDER_PATH, string.Empty, null, "War3Api.*;WCSharp.*", "", csc, false, null, string.Empty)
            {
                IsExportMetadata = true,
                IsModule = false,
                IsInlineSimpleProperty = false,
                IsPreventDebugObject = true,
                IsCommentsDisabled = !DEBUG,
            };

            // Collect required paths and compile
            var coreSystemFiles = CoreSystemProvider.GetCoreSystemFiles(Wc3Api.WCSharp);
            var blizzardJ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Warcraft III/JassHelper/Blizzard.j");
            var commonJ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Warcraft III/JassHelper/common.j");
            var compileResult = map.CompileScript(compiler, coreSystemFiles, blizzardJ, commonJ);

            // If compilation failed, output an error
            if (!compileResult.Success)
            {
                throw new Exception(compileResult.Diagnostics.First(x => x.Severity == DiagnosticSeverity.Error).GetMessage());
            }

            // Update war3map.lua so you can inspect the generated Lua code easily
            File.WriteAllText(Path.Combine(OUTPUT_FOLDER_PATH, OUTPUT_SCRIPT_NAME), map.Script);

            // Build w3x file
            var archiveCreateOptions = new MpqArchiveCreateOptions
            {
                ListFileCreateMode = MpqFileCreateMode.Overwrite,
                AttributesCreateMode = MpqFileCreateMode.Prune,
                BlockSize = 3,
            };

            string finalMapPath = Path.Combine(OUTPUT_FOLDER_PATH, OUTPUT_TEST_MAP_NAME);
            builder.Build(finalMapPath, archiveCreateOptions);

            if (bExport)
            {
                string MapsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $@"Warcraft III\Maps\{OUTPUT_MAP_FILE_NAME}");

                Directory.CreateDirectory(MapsFolder);

                string MapName = $"{OUTPUT_MAP_FILE_NAME}{CurrentBuild}.w3x";
                string MapPath = Path.Combine(MapsFolder, MapName);

                try
                {
                    File.Copy(finalMapPath, MapName, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to copy map: {ex.Message}");
                }
            }

            // Launch if that option was selected
            if (launch)
            {
                var wc3exe = ConfigurationManager.AppSettings["wc3exe"];
                if (File.Exists(wc3exe))
                {
                    var commandLineArgs = new StringBuilder();
                    var isReforged = Version.Parse(FileVersionInfo.GetVersionInfo(wc3exe).FileVersion) >= new Version(1, 32);
                    if (isReforged)
                    {
                        commandLineArgs.Append(" -launch");
                    }
                    else if (GRAPHICS_API != null)
                    {
                        commandLineArgs.Append($" -graphicsapi {GRAPHICS_API}");
                    }

                    if (!PAUSE_GAME_ON_LOSE_FOCUS)
                    {
                        commandLineArgs.Append(" -nowfpause");
                    }

                    var absoluteMapPath = new FileInfo(finalMapPath).FullName;
                    commandLineArgs.Append($" -loadfile \"{absoluteMapPath}\"");

                    Process.Start(wc3exe, commandLineArgs.ToString());
                }
                else
                {
                    throw new Exception("Please set wc3exe in Launcher/app.config to the path of your Warcraft III executable.");
                }
            }
        }
        
        public static int CurrentBuild
        {
            get
            {
                try
                {
                    if (File.Exists(BUILD_VERSION_FILE))
                    {
                        string text = File.ReadAllText(BUILD_VERSION_FILE).Trim();
                        return int.TryParse(text, out int n) ? n : 0;
                    }
                }
                catch { }
                return 0;
            }
        }
    
        public static int IncrementBuild()
        {
            int newBuild = CurrentBuild + 1;

            try
            {
                File.WriteAllText(BUILD_VERSION_FILE, newBuild.ToString());
                Console.WriteLine($"\nBuild number incremented to: {newBuild}");
                return newBuild;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFailed to increment build number: {ex.Message}");
                return CurrentBuild;
            }
        }
    }
}
