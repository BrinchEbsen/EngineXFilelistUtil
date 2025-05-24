using FilelistUtilities.Common;
using FilelistUtilities.Filelist;

const string v4 = "D:\\SpyroAHT_Dumps\\test\\test_v4";
const string v4BinName = "FILELIST.BIN";
const GamePlatform v4Platform = GamePlatform.PlayStation2;

const string v5 = "D:\\SpyroAHT_Dumps\\test\\test_v5";
const string v5BinName = "FILELIST.BIN";
const GamePlatform v5Platform = GamePlatform.PlayStation2;

const string v6 = "D:\\SpyroAHT_Dumps\\test\\test_v6";
const string v6BinName = "FILELIST.BIN";
const GamePlatform v6Platform = GamePlatform.PlayStation2;

const string v7 = "D:\\SpyroAHT_Dumps\\test\\test_v7";
const string v7BinName = "Filelist.bin";
const GamePlatform v7Platform = GamePlatform.Xbox;

const string vGC = "D:\\SpyroAHT_Dumps\\test\\test_gc";
const string vGCBinName = "Filelist.bin";
const GamePlatform vGCPlatform = GamePlatform.GameCube;



string basePath = v7;
string binName = v7BinName;
GamePlatform platform = v7Platform;

string binPath = Path.Join(Path.Join(basePath, "binary"), binName);
string outPath = Path.Combine(basePath, "output");
string rebuildPath = Path.Combine(basePath, "rebuild");

TestFilelist(basePath, binPath, outPath, rebuildPath, platform);

double match = GetPercentFileMatch(binPath, Path.Join(rebuildPath, binName));
Console.WriteLine($"Rebuilt .bin file {match*100}% match");

static void TestFilelist(string basePath, string binPath, string outPath, string rebuildBath, GamePlatform platform)
{
    var filelist = Filelist.Read(binPath);

    string scr = "Filelist.scr";
    filelist.ExportDescriptor(basePath, scr);

    string scrPath = Path.Combine(basePath, scr);

    filelist.ExportFiles(outPath);

    var settings = filelist.GetDefaultSettings(platform, scrPath);

    Filelist.ExportFileList(outPath, rebuildBath, platform, settings);
}

static double GetPercentFileMatch(string file1, string file2)
{
    using var reader1 = new BinaryReader(File.OpenRead(file1));
    using var reader2 = new BinaryReader(File.OpenRead(file2));

    long bytesToCheck = Math.Min(reader1.BaseStream.Length, reader2.BaseStream.Length);
    long difference = Math.Abs(reader1.BaseStream.Length - reader2.BaseStream.Length);

    long matchingBytes = 0;

    for (int i = 0; i < bytesToCheck; i++)
    {
        byte b1 = reader1.ReadByte();
        byte b2 = reader2.ReadByte();

        if (b1 == b2) matchingBytes++;
    }

    return (double)matchingBytes / (double)(bytesToCheck+difference);
}