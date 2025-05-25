using FilelistUtilities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilelistUtilities.Filelist
{
    /// <summary>
    /// Settings for packing a set of files into a filelist:
    /// <list type="bullet">
    /// 
    /// <item><b>Version</b>: Version number of filelist. Default is 7.</item>
    /// 
    /// <item><b>FileListName</b>: Name of the exported filelist (both index and archive files). Default is "Filelist".</item>
    /// 
    /// <item><b>FileSizeSplit</b>: The file size limit in bytes of the filelist before it's split into multiple archives.
    /// This will be ignored for filelists of versions below 5, since they don't support multiple files. Default is -1 (no limit).</item>
    /// 
    /// <item><b>RootName</b>: The root directory name of the virtual file system. Default is 'x'.</item>
    /// 
    /// <item><b>UseDescriptor</b>: Use a descriptor text file to choose which files are included in the exported archive. Default is false.</item>
    /// 
    /// <item><b>DescriptorPath</b>: The path to the descriptor file. Ignored if UseDescriptor is false. Default is null.</item>
    /// 
    /// </list>
    /// </summary>
    public record FileListExportSettings
    {
        /// <summary>
        /// Version number of filelist. Default is 7.
        /// </summary>
        public uint Version = 7;
        /// <summary>
        /// Name of the exported filelist (both index and archive files). Default is "Filelist".
        /// </summary>
        public string FileListName = "Filelist";
        /// <summary>
        /// The file size limit in bytes of the filelist before it's split into multiple archives.
        /// This will be ignored for filelists of versions below 5, since they don't support multiple files. Default is -1 (no limit).
        /// </summary>
        public long FileSizeSplit = -1;
        /// <summary>
        /// The root directory name of the virtual file system. Default is 'x'.
        /// </summary>
        public char RootName = 'x';
        /// <summary>
        /// Use a descriptor text file to choose which files are included in the exported archive. Default is false.
        /// </summary>
        public bool UseDescriptor = false;
        /// <summary>
        /// The path to the descriptor file. Ignored if UseDescriptor is false. Default is null.
        /// </summary>
        public string DescriptorPath = null;
    }

    /// <summary>
    /// The FileList is a virtual file system used in EngineX games.
    /// This class contains methods for parsing, storing and handling filelist files.
    /// </summary>
    public class Filelist
    {
        /// <summary>
        /// Some arbitrary minimum for the file size split, just so we don't end up writing a million filelists.
        /// </summary>
        public const uint FILE_SIZE_SPLIT_MINIMUM = 0x100_0000;

        /// <summary>
        /// The default filelist size splitting point for Xbox EngineX games.
        /// </summary>
        public const uint FILE_SIZE_SPLIT_XBOX_DEFAULT = 0x1000_0000;

        /// <summary>
        /// Filelist version numbers supported for reading and exporting.
        /// </summary>
        public static uint[] SupportedVersions => [4, 5, 6, 7];

        /// <summary>
        /// Whether the filelist has been parsed yet. Must be true before handling any data contained in this object.
        /// </summary>
        private bool _haveRead = false;

        /// <summary>
        /// The source .bin file for this filelist. Only kept track of if the filename was provided before parsing.
        /// </summary>
        private string _filelistBinPath = null;

        /// <summary>
        /// The filelist's version number.
        /// </summary>
        public uint Version { get; set; }
        /// <summary>
        /// Size of filelist in bytes.
        /// </summary>
        public uint FileSize { get; set; }
        /// <summary>
        /// Number of files in filelist.
        /// </summary>
        public int NumFiles { get; set; }
        /// <summary>
        /// Version 5+ - Type of archive: 0 means loose files, 1 means packed virtual filesystem.
        /// 0 is used by games like the PC release of Sphinx.
        /// </summary>
        public ushort BuildType { get; set; } = 1;
        /// <summary>
        /// Version 5+ - Number of filelist archives.
        /// </summary>
        public ushort NumFileLists { get; set; } = 0;
        /// <summary>
        /// Offset to file names list
        /// </summary>
        public EXRelPtr32 FileNameListOffset { get; set; }
        
        /// <summary>
        /// List of offsets to filenames.
        /// </summary>
        public EXRelPtr32[] FileNameList { get; set; }

        /// <summary>
        /// Array of file info elements.
        /// </summary>
        public FileInfoElement[] FileInfo { get; set; }

        /// <summary>
        /// The root name of the file system detected while parsing.
        /// Will be <see cref="char.MinValue"/> if nothing was detected.
        /// </summary>
        public char DetectedRootName { get; set; } = char.MinValue;

        /// <summary>
        /// The total amount of files in the archive(s) - counting duplicate versions from multiple file locations.
        /// </summary>
        public int TotalFileLocs
        {
            get
            {
                if (_haveRead) return 0;

                //Version 4 only has 1 location per file
                if (Version < 5) return FileInfo.Length;

                int count = 0;

                foreach (FileInfoElement fi in FileInfo)
                    count += (int)fi.NumFileLoc;

                return count;
            }
        }

        private Filelist() { }

        /// <summary>
        /// Read a filelist index file.
        /// </summary>
        /// <param name="filePath">Path to the index file.</param>
        /// <returns>The parsed filelist.</returns>
        public static Filelist Read(string filePath)
        {
            using var stream = File.OpenRead(filePath);

            var filelist = Read(stream);

            filelist._filelistBinPath = filePath;

            return filelist;
        }

        /// <summary>
        /// Read a filelist index file.
        /// </summary>
        /// <param name="stream">Stream of the index file.</param>
        /// <returns>The parsed filelist.</returns>
        /// <exception cref="Exception"></exception>
        public static Filelist Read(Stream stream) {
            if (!stream.CanRead)
                throw new Exception("Cannot read filelist without a readable stream.");

            using BinaryReader reader = new(stream, Encoding.UTF8, true);

            stream.Seek(0, SeekOrigin.Begin);
            uint endianTest = reader.ReadUInt16();

            //If the first few bytes contain data, we assume it's little endian
            bool bigEndian = endianTest == 0;

            stream.Seek(0, SeekOrigin.Begin);

            Filelist filelist = new();

            //HEADER

            filelist.Version = reader.ReadUInt32(bigEndian);
            ThrowIfUnsupported(filelist.Version);

            filelist.FileSize = reader.ReadUInt32(bigEndian);

            filelist.NumFiles = reader.ReadInt32(bigEndian);

            if (filelist.Version >= 5)
            {
                filelist.BuildType = reader.ReadUInt16(bigEndian);
                filelist.NumFileLists = reader.ReadUInt16(bigEndian);
            }

            filelist.FileNameListOffset = reader.ReadEXRelPtr32(bigEndian);
            filelist.FileNameListOffset.ThrowIfOutOfBounds(stream);

            //FILE NAMES

            long fileInfoStartAddress = stream.Position;
            stream.Seek(filelist.FileNameListOffset.AbsOffset, SeekOrigin.Begin);

            filelist.FileNameList = new EXRelPtr32[filelist.NumFiles];

            for (int i = 0; i < filelist.NumFiles; i++)
            {
                filelist.FileNameList[i] = reader.ReadEXRelPtr32(bigEndian);
                try
                {
                    filelist.FileNameList[i].ThrowIfOutOfBounds(stream);
                } catch (IOException ex)
                {
                    throw new Exception($"Error reading string offset {i}: {ex.Message}");
                }
            }

            //FILE INFO

            stream.Seek(fileInfoStartAddress, SeekOrigin.Begin);

            filelist.FileInfo = new FileInfoElement[filelist.NumFiles];

            for (int i = 0; i < filelist.NumFiles; i++)
            {
                var elem = new FileInfoElement();

                long addr = stream.Position; //save for later
                stream.Seek(filelist.FileNameList[i].AbsOffset, SeekOrigin.Begin);
                elem.Path = ReadPathString(reader, filelist.Version >= 7, i);
                stream.Seek(addr, SeekOrigin.Begin);

                //Set the detected root name
                if (elem.Path.Contains(':') && filelist.DetectedRootName == char.MinValue)
                {
                    string[] parts = elem.Path.Split(':');
                    if (parts[0].Length > 0)
                        filelist.DetectedRootName = parts[0][0]; //Grab first char
                }

                if (filelist.Version == 4)
                    elem.FileLoc = reader.ReadUInt32(bigEndian); 

                elem.Length = reader.ReadUInt32(bigEndian);
                elem.HashCode = reader.ReadUInt32(bigEndian);
                elem.Version = reader.ReadUInt32(bigEndian);
                elem.Flags = reader.ReadUInt32(bigEndian);

                if (filelist.Version >= 5)
                {
                    elem.NumFileLoc = reader.ReadUInt32(bigEndian);

                    elem.FileLocInfo = new FileLocInfo[elem.NumFileLoc];

                    for (int j = 0; j < elem.NumFileLoc; j++)
                    {
                        elem.FileLocInfo[j] = new FileLocInfo
                        {
                            FileLoc = reader.ReadUInt32(bigEndian),
                            FileListNum = reader.ReadUInt32(bigEndian)
                        };
                    }
                }

                filelist.FileInfo[i] = elem;
            }

            filelist._haveRead = true;

            return filelist;
        }

        /// <summary>
        /// Throw an <see cref="Exception"/> if the version number isn't supported.
        /// </summary>
        /// <param name="ver">Version number to check.</param>
        /// <exception cref="Exception">If unsupported.</exception>
        internal static void ThrowIfUnsupported(uint ver)
        {
            if (!SupportedVersions.Contains(ver))
            {
                string str = "Version " + ver + " is not among the supported versions [";

                bool first = true;
                foreach (var v in SupportedVersions)
                {
                    if (!first) str += ", ";

                    str += v.ToString();

                    first = false;
                }

                str += "]";

                throw new Exception(str);
            }
        }

        /// <summary>
        /// Read a filelist virtual path string.
        /// </summary>
        /// <param name="reader">Reader containing the stream to read from.</param>
        /// <param name="cyphered">Whether this string should be parsed as cyphered/encrypted.</param>
        /// <param name="fileIndex">Index of the file with this path.</param>
        /// <returns>String read from the stream.</returns>
        internal static string ReadPathString(BinaryReader reader, bool cyphered, int fileIndex = 0)
        {
            StringBuilder sb = new();

            for (int i = 0; true; i++)
            {
                int n = reader.ReadByte();

                if (cyphered)
                    n = n + 0x16 - fileIndex - i;

                byte c = (byte)n;
                if (c == 0) break;

                sb.Append((char)c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Write a filelist virtual path string.
        /// </summary>
        /// <param name="writer">Writer containing the stream to write to.</param>
        /// <param name="cyphered">Whether this string should be written as cyphered/encrypted.</param>
        /// <param name="str">String to write.</param>
        /// <param name="fileIndex">Index of the file with this path.</param>
        internal static void WritePathString(BinaryWriter writer, bool cyphered, string str, int fileIndex = 0)
        {
            //Loop through all chars and then one more
            for (int i = 0; i <= str.Length; i++)
            {
                byte c;

                //Check if we're writing the null terminator
                if (i == str.Length)
                    c = 0;
                //Else just write the char
                else
                    c = (byte)str[i];

                if (cyphered)
                    c = (byte)(c - 0x16 + fileIndex + i);

                writer.Write(c);
            }
        }

        /// <summary>
        /// Export files from the filelist to the output directory. The .bin index file path is optional if the file name was provided when parsing.
        /// </summary>
        /// <param name="outputDir">Directory to write the files to.</param>
        /// <param name="filelistBinPath">Path to .bin index file, if none was provided when parsing.</param>
        public void ExportFiles(string outputDir, string filelistBinPath = null)
        {
            if (!_haveRead)
                throw new IOException("Cannot output files without having parsed the filelist.");

            if (BuildType == 0)
                throw new IOException("Filelist is of build type 0 and thus does not have any packed files to extract.");

            if (string.IsNullOrEmpty(filelistBinPath))
            {
                if (string.IsNullOrEmpty(_filelistBinPath))
                    throw new IOException("No input .bin fine was provided.");

                filelistBinPath = _filelistBinPath;
            }

            if (!File.Exists(filelistBinPath))
                throw new FileNotFoundException($"Filelist descriptor {filelistBinPath} was not found.");

            if (!filelistBinPath.EndsWith(".bin", StringComparison.InvariantCultureIgnoreCase))
                throw new IOException($"File {filelistBinPath} is not a .bin filelist descriptor.");

            if (!Directory.Exists(outputDir))
                throw new IOException($"Directory {outputDir} was not found.");

            string filelistName = Path.GetFileName(filelistBinPath).Replace(".bin", "", StringComparison.InvariantCultureIgnoreCase);
            string filelistPath = Path.GetDirectoryName(filelistBinPath);

            var filelistReaders = new BinaryReader[NumFileLists + 1];

            try
            {
                //Setup filelist readers (note that NumFileLists is zero-based!)
                for (int i = 0; i <= NumFileLists; i++)
                {
                    //Get the path of the binary
                    string binEnding;
                    string binPath;

                    if (Version >= 5)
                    {
                        //For v5+, the archive's extension is its index, so .000, .001 etc.
                        binEnding = "." + i.ToString().PadLeft(3, '0');
                    } else
                    {
                        //For v4, the archive's extension is .dat.
                        //Replicate the casing of the .bin file's extension.
                        binEnding = filelistBinPath[^4..] == ".BIN" ? ".DAT" : ".dat";
                    }

                    binPath = Path.Join(filelistPath, filelistName + binEnding);

                    if (!File.Exists(binPath))
                        throw new IOException($"Archive {binPath} was not found.");

                    //Open a new stream for the binary
                    filelistReaders[i] = new BinaryReader(File.OpenRead(binPath));
                }

                //Write files
                foreach (var info in FileInfo)
                {
                    //Set up directories and file names/offsets
                    string path = info.Path;

                    uint filelistNum;
                    uint filelistOffset;

                    if (Version >= 5)
                    {
                        if (info.FileLocInfo.Length == 0)
                        {
                            Console.WriteLine($"Skipping file {path}, as it does not have a defined location in an archive.");
                            continue;
                        }

                        //We just pick the first one. All copies should be identical.
                        filelistNum = info.FileLocInfo[0].FileListNum;
                        filelistOffset = info.FileLocInfo[0].FileLoc;
                    } else
                    {
                        //In v4, the file can only be in one location and in the first archive (.dat).
                        filelistNum = 0;
                        filelistOffset = info.FileLoc;
                    }

                    if (filelistNum > filelistReaders.Length-1)
                    {
                        Console.WriteLine($"Skipping file {path}, as it targets filelist {filelistNum}, which doesn't exist.");
                        continue;
                    }

                    //Split out the starting 'x:' or 'd:'
                    string[] separated = path.Split(':');
                    if (separated.Length > 1)
                        path = separated[1];

                    string fullFilePath = Path.Join(outputDir, path);
                    string fullFileDir = Path.GetDirectoryName(fullFilePath);

                    //Make the directory if it doesn't exist already
                    Directory.CreateDirectory(fullFileDir);

                    //Get filelist stream
                    var reader = filelistReaders[filelistNum];

                    if (filelistOffset >= reader.BaseStream.Length)
                        throw new IOException($"Start of file {path} exceeds bounds of filelist.");

                    //Seek to start of file
                    reader.BaseStream.Seek(filelistOffset, SeekOrigin.Begin);

                    int len;

                    if (path.EndsWith(".edb"))
                    {
                        //.edb files only report their base size. The full length must be read manually from the header.

                        uint marker = reader.ReadUInt32();

                        bool bigEndian;
                        if (marker == 0x47454F4D) //GEOM
                            bigEndian = false;
                        else if (marker == 0x4D4F4547) //MOEG
                            bigEndian = true;
                        else
                            throw new IOException($"GeoFile {path} has an invalid marker value (0x{marker:X}).");

                        reader.BaseStream.Seek(filelistOffset + 0x14, SeekOrigin.Begin);
                        len = reader.ReadInt32(bigEndian);
                    } else
                    {
                        len = (int)info.Length;
                    }

                    if (filelistOffset + len > reader.BaseStream.Length)
                        throw new IOException($"Reading file {path} from the filelist would exceed bounds of the file stream.");

                    reader.BaseStream.Seek(filelistOffset, SeekOrigin.Begin);

                    Console.WriteLine($"Writing file {path}...");

                    //Set up writing stream and copy the data
                    using BinaryWriter writer = new(File.Open(fullFilePath, FileMode.Create));
                    CopyStream(reader.BaseStream, writer.BaseStream, len);
                }
            } catch (Exception)
            {
                throw;
            } finally
            {
                //Dispose everything if things go haywire
                foreach (var reader in filelistReaders)
                    reader?.Dispose();
            }
        }

        /// <summary>
        /// Repackage the files in the input path to a new filelist in the output path for the given platform and with the default settings.
        /// </summary>
        /// <param name="inputPath">Folder containing the files to repackage.</param>
        /// <param name="outputPath">Folder where the new filelist will be written.</param>
        /// <param name="platform">The platform for which the filelist will be written.</param>
        public static void ExportFileList(string inputPath, string outputPath, GamePlatform platform)
            => ExportFileList(inputPath, outputPath, platform, new FileListExportSettings());

        /// <summary>
        /// Repackage the files in the input path to a new filelist in the output path for the given platform and using the given settings.
        /// </summary>
        /// <param name="inputPath">Folder containing the files to repackage.</param>
        /// <param name="outputPath">Folder where the new filelist will be written.</param>
        /// <param name="platform">The platform for which the filelist will be written.</param>
        /// <param name="settings">Settings for the filelist export.</param>
        public static void ExportFileList(string inputPath, string outputPath, GamePlatform platform, FileListExportSettings settings)
        {
            //Guards

            if (!Directory.Exists(inputPath))
                throw new DirectoryNotFoundException($"Directory {inputPath} not found.");

            if (!Directory.Exists(outputPath))
                throw new DirectoryNotFoundException($"Directory {outputPath} not found.");

            if (string.IsNullOrEmpty(settings.FileListName) || settings.FileListName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException($"{settings.FileListName} is not a valid name for the filelist.");

            ThrowIfUnsupported(settings.Version);

            if (settings.FileSizeSplit is >= 0 and < FILE_SIZE_SPLIT_MINIMUM)
                throw new ArgumentException($"File size split limit is below the minimum amount ({FILE_SIZE_SPLIT_MINIMUM}).");

            if (Path.GetInvalidFileNameChars().Contains(settings.RootName))
                throw new ArgumentException($"Root directory name {settings.RootName} is invalid.");



            //Get input files
            string[] inputFiles;
            if (settings.UseDescriptor == true)
            {
                if (!File.Exists(settings.DescriptorPath))
                    throw new FileNotFoundException($"Descriptor file not found.");

                //Read the provided descriptor file
                string descr = File.ReadAllText(settings.DescriptorPath);

                var descrLines = ParseDescriptorPathValues(descr, settings.RootName);
                inputFiles = ParseFullDescriptorPaths(descrLines, inputPath);
            } else
            {
                //Without a descriptor we just grab every file in there
                inputFiles = Directory.GetFiles(inputPath, "*", SearchOption.AllDirectories);
            }

            //GameCube is in big endian
            bool bigEndian = platform == GamePlatform.GameCube;

            //The PS2's disc format doesn't support lowercase chars, so just to be safe:
            bool makeUpperCase = platform == GamePlatform.PlayStation2;

            //The file name of the .bin file
            string filelistBinName;

            if (makeUpperCase)
                filelistBinName = settings.FileListName.ToUpper() + ".BIN";
            else
                filelistBinName = settings.FileListName + ".bin";

            //Open writer for .bin file
            using var binWriter = new BinaryWriter(File.Open(Path.Join(outputPath, filelistBinName), FileMode.Create));

            //Keep track of the current archive and writers for each (for v4 filelists there'll always only be 1)
            int currentArchive = 0;
            List<BinaryWriter> archiveWriters = new(1);

            //Start writing stuff

            try
            {
                //Create the first filelist writer (or the only one in the case of v4)
                string firstArchiveName;

                if (makeUpperCase)
                    firstArchiveName = settings.FileListName.ToUpper() + (settings.Version < 5 ? ".DAT" : ".000");
                else
                    firstArchiveName = settings.FileListName + (settings.Version < 5 ? ".dat" : ".000");

                archiveWriters.Add(new BinaryWriter(File.Open(Path.Join(outputPath, firstArchiveName), FileMode.Create)));

                //Write header
                binWriter.Write(settings.Version, bigEndian);
                binWriter.BaseStream.Seek(8, SeekOrigin.Current); //Filesize and NumFiles (calculated later)
                if (settings.Version > 4)
                {
                    binWriter.Write((short)1, bigEndian); //BuildType always 1 for now
                    binWriter.BaseStream.Seek(2, SeekOrigin.Current); //NumFileLists (calculated later)
                }
                binWriter.Seek(4, SeekOrigin.Current); //FileNameListOffset (calculated later)

                //Make new list of file info to write to the .bin later
                List<FileInfoElement> fileInfoList = new(inputFiles.Length);

                //Write files to the archives and build up FileInfo list
                for (int i = 0; i < inputFiles.Length; i++)
                {
                    //Absolute path of the input file
                    string fileAbs = inputFiles[i];
                    //Input file path relative to folder containing the files
                    string file = Path.GetRelativePath(inputPath, fileAbs);

                    //File name as it'll actually appear in the file system
                    string virtualFileName = Path.Join(settings.RootName + ":", file);

                    //Replace this system's directory delimeter with EngineX's "\"
                    virtualFileName = virtualFileName.Replace(Path.DirectorySeparatorChar, '\\');
                    
                    using var fileReader = new BinaryReader(File.OpenRead(fileAbs));

                    //Not sure how this could ever happen but oh well
                    if (fileReader.BaseStream.Length > int.MaxValue)
                        throw new IOException($"File {file} is {fileReader.BaseStream.Length} bytes long, which exceeds the 32 bit filesize limit.");

                    //If the file length exceeds the archive split size, something's terribly wrong
                    //and we could enter an infinite loop of creating new archive files
                    if ((settings.FileSizeSplit > 0) && (fileReader.BaseStream.Length > settings.FileSizeSplit))
                        throw new IOException($"File {file} is {fileReader.BaseStream.Length} bytes long, " +
                                              $"which is larger than the max size of the filelist archive before it splits.");

                    uint fileSize = (uint)fileReader.BaseStream.Length;

                    var archiveWriter = archiveWriters[currentArchive];

                    //If we've exceeded the size limit before splitting, we ditch the current archive and start writing a new one.
                    if (settings.Version > 4)
                    {
                        //Check if limit exceeded
                        bool exceededSplit;
                        if (settings.FileSizeSplit >= 0)
                            exceededSplit = archiveWriter.BaseStream.Position + fileSize > settings.FileSizeSplit;
                        else
                            exceededSplit = false;

                        //If exceeded, open up a new archive for writing
                        if (exceededSplit)
                        {
                            currentArchive++;

                            string archiveName;
                            if (makeUpperCase)
                                archiveName = settings.FileListName.ToUpper();
                            else
                                archiveName = settings.FileListName;

                            //.000, .001, .002 etc...
                            archiveName += "." + currentArchive.ToString().PadLeft(3, '0');

                            archiveWriters.Add(new BinaryWriter(File.Open(Path.Join(outputPath, archiveName), FileMode.Create)));
                            
                            //Reassign the current archive writer
                            archiveWriter = archiveWriters[currentArchive];
                        }
                    }

                    //Get the position of the file we're about to write
                    uint pos = (uint)archiveWriter.BaseStream.Position;

                    Console.WriteLine($"Writing to filelist: {virtualFileName}...");

                    //Write the file to the archive
                    CopyStream(fileReader.BaseStream, archiveWriter.BaseStream, (int)fileSize);
                    //Ensure aligned
                    EnsureStreamAlignment(archiveWriter.BaseStream);

                    //Now we obtain file info for this file.

                    FileInfoElement info;
                    bool initialise; //Whether this is the first time we've encountered this file

                    //Check to see if this file already has a FileInfo entry
                    var existingInfo = fileInfoList.Find((f) => f.Path == virtualFileName);

                    if (existingInfo == null)
                    {
                        info = new FileInfoElement();
                        initialise = true;
                    } else
                    {
                        info = existingInfo;
                        initialise = false;
                    }

                    //If this is the first time we're writing this file, populate it with info
                    if (initialise)
                    {
                        info.Path = virtualFileName;

                        if (settings.Version < 5)
                            info.FileLoc = pos;

                        //Assign values differently depending on the file type:

                        if (file.EndsWith(".edb")) // GeoFile
                        {
                            fileReader.BaseStream.Seek(0, SeekOrigin.Begin);

                            uint marker = fileReader.ReadUInt32(bigEndian);
                            //              G  E  O  M
                            if (marker != 0x47_45_4F_4D)
                                throw new IOException($"GeoFile {file} does not have a valid marker (0x{marker:X}).");

                            uint hashcode = fileReader.ReadUInt32(bigEndian);

                            // I would do this extra check, but some files don't have valid hashcodes to begin with for some reason.
                            //if ((hashcode & 0xFF_00_00_00) != 0x01_00_00_00)
                            //    throw new IOException($"GeoFile {file} does not have a valid file hashcode ({hashcode:X}).");

                            uint edbVersion = fileReader.ReadUInt32(bigEndian);
                            uint edbFlags = fileReader.ReadUInt32(bigEndian);

                            //GeoFiles only report their "base" filesize in the filelist.
                            fileReader.BaseStream.Seek(0x18, SeekOrigin.Begin);
                            uint edbBaseSize = fileReader.ReadUInt32(bigEndian);

                            info.Length = edbBaseSize;
                            info.HashCode = hashcode;
                            info.Version = edbVersion;
                            info.Flags = edbFlags;
                        }
                        else if (file.EndsWith(".sfx")) //SFX file
                        {
                            //Header information in SFX files is always little endian!

                            fileReader.BaseStream.Seek(0, SeekOrigin.Begin);

                            uint marker = fileReader.ReadUInt32(false);
                            //              X  S  U  M  (MUSX but little endian)
                            if (marker != 0x58_53_55_4D)
                                throw new IOException($"SFX file {file} does not have a valid marker (0x{marker:X}).");

                            //The SFX file only stores the bottom bytes of its hashcode.
                            uint hashcode = fileReader.ReadUInt32(false);
                            hashcode = 0x21_00_00_00 | (hashcode & 0xFF_FF_FF);

                            info.Length = fileSize;
                            info.HashCode = hashcode;

                            //Pre-7 versions don't store the SFX version for some reason
                            if (settings.Version >= 7)
                            {
                                uint sfxVersion = fileReader.ReadUInt32(false);
                                info.Version = sfxVersion;
                            }
                            else
                            {
                                info.Version = 0;
                            }

                            //Flags are always zero.
                            info.Flags = 0;
                        }
                        else //All other files
                        {
                            info.Length = fileSize;

                            //Seems like the hashcode is always just 0x81xxxxxx and then the file's index (+1 if it's a v6 filelist).
                            if (settings.Version == 6)
                                info.HashCode = (uint)(0x81_00_00_00 | (i + 1));
                            else
                                info.HashCode = (uint)(0x81_00_00_00 | i);

                            //Version and flags always zero.
                            info.Version = 0;
                            info.Flags = 0;
                        }
                    }

                    //Add the location to this file (on top of existing locations if they exist)
                    if (settings.Version > 4)
                    {
                        info.NumFileLoc++;

                        var list = info.FileLocInfo.ToList();
                        list.Add(new FileLocInfo
                        {
                            FileLoc = pos,
                            FileListNum = (uint)currentArchive
                        });
                        info.FileLocInfo = list.ToArray();
                    }

                    if (initialise)
                        fileInfoList.Add(info);
                }



                //All the file info has been obtained, write it into the FileInfo array
                foreach (var file in fileInfoList)
                {
                    if (settings.Version < 5)
                        binWriter.Write(file.FileLoc, bigEndian);

                    binWriter.Write(file.Length, bigEndian);
                    binWriter.Write(file.HashCode, bigEndian);
                    binWriter.Write(file.Version, bigEndian);
                    binWriter.Write(file.Flags, bigEndian);

                    if (settings.Version >= 5)
                    {
                        binWriter.Write(file.NumFileLoc, bigEndian);
                        foreach (var loc in file.FileLocInfo)
                        {
                            binWriter.Write(loc.FileLoc, bigEndian);
                            binWriter.Write(loc.FileListNum, bigEndian);
                        }
                    }
                }


                
                Console.WriteLine("Writing file names...");

                //Save start of filename offset table for later reference
                uint fileNameTableAddress = (uint)binWriter.BaseStream.Position;
                //Leave enough space for the relative offsets
                binWriter.BaseStream.Seek(fileInfoList.Count * 4, SeekOrigin.Current);

                //Position of the current path string being written
                uint currStringPosition = (uint)binWriter.BaseStream.Position;

                //Write file names
                for (int i = 0; i < fileInfoList.Count; i++)
                {
                    var info = fileInfoList[i];

                    WritePathString(binWriter, settings.Version >= 7, info.Path, i);

                    //Save current writing position
                    long nextPos = binWriter.BaseStream.Position;

                    //Write relative offset
                    binWriter.BaseStream.Seek(fileNameTableAddress + (i*4), SeekOrigin.Begin);
                    binWriter.Write(currStringPosition - (uint)binWriter.BaseStream.Position, bigEndian);

                    //Get back to where we write the strings
                    binWriter.BaseStream.Seek(nextPos, SeekOrigin.Begin);

                    //Advance current position to start of next string
                    currStringPosition = (uint)binWriter.BaseStream.Position;
                }

                //Make .bin file align to 32-byte boundary.
                EnsureStreamAlignment(binWriter.BaseStream, 0x20);



                //Go back and write some stuff we've now figured out

                binWriter.BaseStream.Seek(4, SeekOrigin.Begin);
                binWriter.Write((uint)binWriter.BaseStream.Length, bigEndian); //File length
                binWriter.Write((uint)fileInfoList.Count, bigEndian); //Number of files

                if (settings.Version > 4)
                {
                    binWriter.BaseStream.Seek(0xE, SeekOrigin.Begin);
                    binWriter.Write((ushort)currentArchive, bigEndian);
                } else
                {
                    binWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
                }

                //Relative offset to string table
                binWriter.Write(fileNameTableAddress - (uint)binWriter.BaseStream.Position, bigEndian);
            } catch (Exception)
            {
                throw;
            } finally
            {
                //These writers were not created with a "using" statement, so we must ensure they're disposed
                foreach (var writer in archiveWriters)
                    writer?.Dispose();
            }

            Console.WriteLine("Done.");
        }

        /// <summary>
        /// Get an appropriate <see cref="FileListExportSettings"/> object for this filelist.
        /// If the filelist has not been parsed yet, just returns a default <see cref="FileListExportSettings"/> object.
        /// </summary>
        /// <param name="platform">If non-null, adjusts the settings for the given platform.</param>
        /// <param name="descriptorPath">If non-null, enables descriptor output with the given path.</param>
        /// <returns>The <see cref="FileListExportSettings"/> object.</returns>
        public FileListExportSettings GetDefaultSettings(GamePlatform? platform = null, string descriptorPath = null)
        {
            if (!_haveRead) return new FileListExportSettings();

            var settings = new FileListExportSettings()
            {
                Version = this.Version,
            };

            //Xbox usually has split archives.
            if (platform == GamePlatform.Xbox)
            {
                settings.FileSizeSplit = FILE_SIZE_SPLIT_XBOX_DEFAULT;
            }

            if (this.DetectedRootName != char.MinValue)
            {
                settings.RootName = this.DetectedRootName;
            }

            if (descriptorPath != null)
            {
                settings.UseDescriptor = true;
                settings.DescriptorPath = descriptorPath;
            }

            return settings;
        }

        /// <summary>
        /// Export a descriptor text file for this filelist, listing all the files inside.
        /// </summary>
        /// <param name="outputPath">Path to write the descriptor file to.</param>
        /// <param name="fileName">Name of the descriptor file. If no extension is specified, ".scr" is used as the default.</param>
        public void ExportDescriptor(string outputPath, string fileName)
        {
            if (!_haveRead)
                throw new IOException("Cannot output descriptor without having parsed the filelist.");

            if (!Directory.Exists(outputPath))
                throw new DirectoryNotFoundException($"Directory {outputPath} not found.");

            if (string.IsNullOrEmpty(fileName) || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException($"{fileName} is not a valid file name.");

            //Append .scr extension if no extension is provided.
            if (!fileName.Contains('.'))
                fileName += ".scr";

            using var writer = new StreamWriter(File.Open(Path.Join(outputPath, fileName), FileMode.Create));

            writer.WriteLine("[FileInfomation]");
            writer.WriteLine();
            writer.WriteLine("[FileList]");
            writer.WriteLine();

            //Create full (actual) list of file paths - including duplicates
            List<DescriptorFileInfo> allPaths = new(TotalFileLocs);

            foreach (var file in FileInfo)
            {
                if (Version < 5)
                {
                    allPaths.Add(new DescriptorFileInfo
                    {
                        Path = file.Path,
                        FileLoc = file.FileLoc,
                        FileListNum = 0
                    });
                } else
                {
                    foreach (var loc in file.FileLocInfo)
                    {
                        allPaths.Add(new DescriptorFileInfo
                        {
                            Path = file.Path,
                            FileLoc = loc.FileLoc,
                            FileListNum = loc.FileListNum
                        });
                    }
                }
            }

            //Sort the list from first to last in terms of file location
            allPaths.Sort((path1, path2) =>
            {
                //Prioritize sorting by filelist index
                if (path1.FileListNum != path2.FileListNum)
                    return path1.FileListNum.CompareTo(path2.FileListNum);
                else
                    return path1.FileLoc.CompareTo(path2.FileLoc);
            });

            //Write the paths
            foreach (var file in allPaths)
                writer.WriteLine(file.Path);
        }

        /// <summary>
        /// Extract the lines from the descriptor text file that relate to the included files in the filelist,
        /// turned into relative paths (without the root).
        /// </summary>
        /// <param name="descr">Descriptor text file in string form.</param>
        /// <param name="rootName">Name of the root of the virtual file system.</param>
        /// <returns>Relative paths/globbing patterns for the included files.</returns>
        public static string[] ParseDescriptorPathValues(string descr, char rootName = 'x')
        {
            using var reader = new StringReader(descr);

            string pathStart = $"{rootName}:\\";

            List<string> paths = [];

            string line;
            bool inFileSection = false;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();

                if (line.Contains("[FileList]"))
                {
                    inFileSection = true;
                    continue;
                }

                if (inFileSection)
                {
                    if (line.StartsWith('['))
                    {
                        inFileSection = false;
                        continue;
                    }

                    if (line.Length == 0)
                        continue;

                    line = line
                        .Replace(pathStart, "")
                        .Replace(Environment.NewLine, "");

                    paths.Add(line);
                }
            }

            return paths.ToArray();
        }

        /// <summary>
        /// Turn a list of relative paths and globbing patterns from a descriptor file into a list of full file paths to include in the filelist.
        /// </summary>
        /// <param name="descr">Relative paths/globbing patterns for the included files.</param>
        /// <param name="baseDir">The base directory where the files can be found.</param>
        /// <returns>A list of full file paths to be included in the filelist.</returns>
        public static string[] ParseFullDescriptorPaths(string[] descr, string baseDir)
        {
            if (!Directory.Exists(baseDir))
                throw new DirectoryNotFoundException($"Directory {baseDir} not found.");

            List<string> paths = [];

            foreach (string p in descr)
            {
                //Handle globbing if the path has it
                if (p.Contains('*'))
                {
                    var files = Directory.GetFiles(baseDir, p, SearchOption.AllDirectories);

                    foreach(var f in files)
                    {
                        paths.Add(f);
                    }
                } else
                {
                    string abs = Path.Join(baseDir, p);

                    if (File.Exists(abs))
                        paths.Add(abs);
                    else
                        Console.WriteLine($"File {abs} from filelist descriptor does not exist!");
                }
            }

            return paths.ToArray();
        }

        /// <summary>
        /// Spread out a file info list such that elements with multiple locations instead have multiple entries with 1 location each.
        /// Any elements without any locations will be excluded.
        /// </summary>
        /// <param name="elems"></param>
        /// <returns></returns>
        public static FileInfoElement[] SpreadFileInfo(FileInfoElement[] elems)
        {
            List<FileInfoElement> output = [];

            foreach(var el in elems)
            {
                if (el.FileLocInfo.Length == 0) continue;

                for (int i = 0; i < el.FileLocInfo.Length; i++)
                {
                    output.Add(new FileInfoElement(el)
                    {
                        NumFileLoc = 1,
                        FileLocInfo = [new FileLocInfo
                        {
                            FileListNum = el.FileLocInfo[i].FileListNum,
                            FileLoc = el.FileLocInfo[i].FileLoc
                        }]
                    });
                }
            }

            output.Sort((e1, e2) =>
            {
                if (e1.FileLocInfo[0].FileListNum != e2.FileLocInfo[0].FileListNum)
                    return e1.FileLocInfo[0].FileListNum.CompareTo(e2.FileLocInfo[0].FileListNum);

                return e1.FileLocInfo[0].FileLoc.CompareTo(e2.FileLocInfo[0].FileLoc);
            });

            return output.ToArray();
        }

        public string CreateXUtilTextOutput()
        {
            if (!_haveRead) return "Error: Filelist not parsed.";

            FileInfoElement[] elems = SpreadFileInfo(FileInfo);

            var sb = new StringBuilder();

            for (int i = 0; i < elems.Length; i++)
            {
                FileInfoElement el = elems[i];

                uint filelistNum;
                uint filelistLoc;

                if (Version < 5)
                {
                    filelistNum = 0;
                    filelistLoc = el.FileLoc;
                } else
                {
                    filelistNum = el.FileLocInfo[0].FileListNum;
                    filelistLoc = el.FileLocInfo[0].FileLoc;
                }

                string line = el.Path.PadRight(72, ' ') + " : Len ";
                line += el.Length.ToString().PadLeft(10, ' ') + " : Ver ";
                line += el.Version.ToString().PadLeft(4, ' ') + " : Hash 0x";
                line += el.HashCode.ToString("X").PadLeft(8, '0') + " : Ts 0x";
                line += el.Flags.ToString("X").PadLeft(8, '0') + " :  Loc ";
                line += filelistLoc.ToString("X").PadLeft(11, ' ');
                if (Version >= 5)
                    line += ":" + filelistNum.ToString().PadLeft(3, '0');

                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        //https://stackoverflow.com/questions/13021866/any-way-to-use-stream-copyto-to-copy-only-certain-number-of-bytes
        private static void CopyStream(Stream input, Stream output, int bytes)
        {
            byte[] buffer = new byte[0x8000];
            int read;
            while (bytes > 0 &&
                   (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }

        private static void EnsureStreamAlignment(Stream input, long alignment = 0x800)
        {
            long leftover = input.Position % alignment;

            if (leftover > 0)
            {
                input.Seek(alignment - leftover - 1, SeekOrigin.Current);
                input.WriteByte(0); //Write a byte to make the alignment stick.
            }
        }
    }
}
