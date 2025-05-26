using System.CommandLine;
using FilelistUtilities.Common;
using FilelistUtilities.Filelist;

internal class Program
{
    private static readonly Dictionary<string, GamePlatform> PlatformArgs = new()
    {
        {"xb", GamePlatform.Xbox},
        {"xbox", GamePlatform.Xbox},
        {"ps2", GamePlatform.PlayStation2},
        {"gc", GamePlatform.GameCube},
        {"gamecube", GamePlatform.GameCube}
    };

    private static int Main(string[] args)
    {
        var rootCmd = new RootCommand(
            "Utility for extracting and creating EngineX filelists (version 4, 5, 6 and 7).");

        var extractCmd = MakeExtractCmd();
        var createCmd = MakeCreateCmd();

        rootCmd.AddCommand(extractCmd);
        rootCmd.AddCommand(createCmd);

        return rootCmd.Invoke(args);
    }

    private static Command MakeExtractCmd()
    {
        var cmd = new Command("extract", "Extract the contents of a Filelist.");

        var binfileArg = new Argument<FileInfo?>(
            name: "BIN_FILE",
            description: ".bin filelist index file.",
            parse: result =>
            {
                string? filePath = result.Tokens.Single().Value;
                if (!File.Exists(filePath))
                {
                    result.ErrorMessage = ".bin file not found.";
                    return null;
                }
                else
                {
                    return new FileInfo(filePath);
                }
            });

        var outdirArg = new Argument<DirectoryInfo?>(
            name: "OUTPUT",
            description: "Output directory for extracted files.",
            isDefault: true,
            parse: result =>
            {
                if (result.Tokens.Count == 0)
                    return new DirectoryInfo("./");

                string? path = result.Tokens.Single().Value;

                if (!Directory.Exists(path))
                {
                    result.ErrorMessage = "Output folder not found.";
                    return null;
                }

                return new DirectoryInfo(path);
            });

        var scrOption = new Option<FileInfo?>(
            name: "--create-scr",
            description: "Create an .scr descriptor file listing the contents of the source filelist.",
            parseArgument: result =>
            {
                if (result.Tokens.Count == 0)
                {
                    result.ErrorMessage = "No .scr file path provided";
                    return null;
                }

                string? path = result.Tokens.Single().Value;
                string? dir = Path.GetDirectoryName(path);

                if (!Directory.Exists(dir))
                {
                    result.ErrorMessage = "Base directory for .scr file not found.";
                    return null;
                }

                //Check if no filename was specified
                string? name = Path.GetFileName(path);
                if (string.IsNullOrEmpty(name))
                {
                    result.ErrorMessage = "No name for .scr file specified in output path.";
                    return null;
                }

                return new FileInfo(path);
            });
        scrOption.AddAlias("-s");
        scrOption.ArgumentHelpName = "SCR_FILE";

        cmd.AddArgument(binfileArg);
        cmd.AddArgument(outdirArg);
        cmd.AddOption(scrOption);

        cmd.SetHandler(ExtractFilelist!, binfileArg, outdirArg, scrOption);

        return cmd;
    }

    private static Command MakeCreateCmd()
    {
        var cmd = new Command("create", "Repackage a list of files into a new Filelist.");

        var inputFolderArg = new Argument<DirectoryInfo?>(
            name: "INPUT_FOLDER",
            description: "Folder containing the files to include in the filelist.",
            parse: result =>
            {
                string? path = result.Tokens.Single().Value;

                if (!Directory.Exists(path))
                {
                    result.ErrorMessage = "Input folder not found.";
                    return null;
                }

                return new DirectoryInfo(path);
            });

        var outputFileArg = new Argument<FileInfo?>(
            name: "OUTPUT_FILE",
            description: "Destination for the created filelist (without filename extension.)",
            isDefault: true,
            parse: result =>
            {
                if (result.Tokens.Count == 0)
                    return new FileInfo("Filelist");

                string? path = result.Tokens.Single().Value;
                string? dir = Path.GetDirectoryName(path);

                if (!Directory.Exists(dir))
                {
                    result.ErrorMessage = "Base directory for filelist not found.";
                    return null;
                }

                //Check if no filename was specified
                string? name = Path.GetFileName(path);
                if (string.IsNullOrEmpty(name))
                {
                    result.ErrorMessage = "No name for filelist specified in output path.";
                    return null;
                }

                //Check if it has an extension
                if (name.Contains('.'))
                {
                    result.ErrorMessage = "The given name of the filelist should not have a file extension.";
                    return null;
                }

                return new FileInfo(path);
            });

        var rootNameOption = new Option<string>(
            name: "--drive-letter",
            description: "Letter to represent the root of the virtual file system.",
            getDefaultValue: () => "x");
        rootNameOption.AddAlias("-l");
        rootNameOption.ArgumentHelpName = "DRIVE_LETTER";

        var versionOption = new Option<uint>(
            name: "--version",
            description: "Version of the outputted filelist.",
            getDefaultValue: () => 7)
            .FromAmong(
                Filelist.SupportedVersions.Select((ver) => ver.ToString()).ToArray()
            );
        versionOption.AddAlias("-v");
        versionOption.ArgumentHelpName = "VERSION";

        var platformOption = new Option<string>(
            name: "--platform",
            description: "Platform of the outputted filelist.")
            .FromAmong(
                PlatformArgs.Keys.ToArray()
            );
        platformOption.IsRequired = true;
        platformOption.AddAlias("-p");
        platformOption.ArgumentHelpName = "PLATFORM";

        var splitSizeOption = new Option<long?>(
            name: "--split-size",
            description: "Maximum size (in bytes) for each archive before being split into another archive. " +
                $"Default is -1 (no limit), except for Xbox where it's {Filelist.FILE_SIZE_SPLIT_XBOX_DEFAULT} bytes.");
        splitSizeOption.AddAlias("-z");
        splitSizeOption.ArgumentHelpName = "SPLIT_SIZE";

        var scrOption = new Option<FileInfo?>(
            name: "--scr-file",
            description: ".scr descriptor file to read options from.",
            parseArgument: result =>
            {
                if (result.Tokens.Count == 0)
                {
                    result.ErrorMessage = "No .scr file path provided";
                    return null;
                }
                string? filePath = result.Tokens.Single().Value;
                if (!File.Exists(filePath))
                {
                    result.ErrorMessage = ".scr file not found.";
                    return null;
                }
                else
                {
                    return new FileInfo(filePath);
                }
            });
        scrOption.AddAlias("-s");
        scrOption.ArgumentHelpName = "SCR_FILE";

        cmd.AddArgument(inputFolderArg);
        cmd.AddArgument(outputFileArg);
        cmd.AddOption(rootNameOption);
        cmd.AddOption(versionOption);
        cmd.AddOption(platformOption);
        cmd.AddOption(splitSizeOption);
        cmd.AddOption(scrOption);

        cmd.SetHandler(CreateFilelist!,
            inputFolderArg, outputFileArg, rootNameOption, versionOption, platformOption, splitSizeOption, scrOption);

        return cmd;
    }

    private static void CreateFilelist(
        DirectoryInfo inputDir, FileInfo outputFile, string rootName, uint version,
        string platformStr, long? splitSize, FileInfo? scrFile)
    {
        try
        {
            if (!PlatformArgs.TryGetValue(platformStr, out GamePlatform platform))
            {
                PrintError($"Platform arg {platformStr} does not correspond to a supported platform argument.");
                return;
            }

            var settings = new FileListExportSettings()
            {
                Version = version,
                FileListName = outputFile.Name,
            };

            if (splitSize.HasValue)
            {
                settings.FileSizeSplit = splitSize.Value;
            }
            else if (platform == GamePlatform.Xbox)
            {
                settings.FileSizeSplit = Filelist.FILE_SIZE_SPLIT_XBOX_DEFAULT;
            }

            if (rootName.Length == 0)
            {
                PrintWarning("Given drive letter was empty, using default.");
            }
            else
            {
                settings.RootName = rootName[0];

                if (rootName.Length > 1)
                    PrintWarning($"Given drive letter is more than one letter, using first character '{settings.RootName}'.");
            }

            if (scrFile != null)
            {
                settings.UseDescriptor = true;
                settings.DescriptorPath = scrFile.FullName;
            }

            Filelist.ExportFileList(inputDir.FullName, outputFile.DirectoryName, platform, settings);
        } catch (Exception ex)
        {
            PrintError($"Error creating filelist: {ex.Message}");
        }
    }

    private static void ExtractFilelist(FileInfo binfile, DirectoryInfo outdir, FileInfo? scrFile)
    {
        try
        {
            var filelist = Filelist.Read(binfile.FullName);

            if (scrFile != null)
                filelist.ExportDescriptor(scrFile.DirectoryName, scrFile.Name);

            filelist.ExportFiles(outdir.FullName);
        } catch (Exception e)
        {
            PrintError($"Error extracting files: {e.Message}");
        }
    }

    private static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void PrintWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}