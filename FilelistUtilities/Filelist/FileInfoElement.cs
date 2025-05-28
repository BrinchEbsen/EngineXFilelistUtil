namespace FilelistUtilities.Filelist
{
    internal record DescriptorFileInfo
    {
        public string Path { get; set; }
        public uint FileLoc { get; set; }
        public uint FileListNum { get; set; }
    }

    /// <summary>
    /// Version 5+ only - Information about a file's location.
    /// </summary>
    public record FileLocInfo
    {
        /// <summary>
        /// File offset into the binary file.
        /// </summary>
        public uint FileLoc { get; set; }
        /// <summary>
        /// The index of the target filelist.
        /// The index will be the extension of the binary file, so .000, .001, .002 etc...
        /// </summary>
        public uint FileListNum { get; set; }

        public FileLocInfo() { }

        public FileLocInfo(FileLocInfo o)
        {
            this.FileLoc = o.FileLoc;
            this.FileListNum = o.FileListNum;
        }

        public override string ToString()
        {
            return FileLoc.ToString("X") + ":" + FileListNum.ToString().PadLeft(3, '0');
        }
    }

    /// <summary>
    /// Information about a file.
    /// </summary>
    public record FileInfoElement
    {
        /// <summary>
        /// File path within the filelist.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Version 4 only - File offset into the .dat binary file.
        /// </summary>
        public uint FileLoc { get; set; }
        /// <summary>
        /// Length of file in bytes.
        /// If an .edb file, will only be the "base" length when only the first section is loaded.
        /// Read the full length of the file from offset 0x14 into the .edb file header.
        /// </summary>
        public uint Length { get; set; }
        /// <summary>
        /// The file's hashcode.
        /// </summary>
        public uint HashCode { get; set; }
        /// <summary>
        /// The file's version (only non-zero if the file format has a version number).
        /// </summary>
        public uint Version { get; set; }
        /// <summary>
        /// Flags (only non-zero if the file format has any flags).
        /// </summary>
        public uint Flags { get; set; }
        /// <summary>
        /// Version 5+ only - Number of file offset info elements in <see cref="FileLocInfo"/>.
        /// </summary>
        public uint NumFileLoc { get; set; }
        /// <summary>
        /// Version 5+ only - File offset info.
        /// A file can be located in multiple places for disk seeking reasons.
        /// </summary>
        public FileLocInfo[] FileLocInfo { get; set; } = [];

        public FileInfoElement() { }

        public FileInfoElement(FileInfoElement o)
        {
            this.Path = o.Path;
            this.FileLoc = o.FileLoc;
            this.Length = o.Length;
            this.HashCode = o.HashCode;
            this.Version = o.Version;
            this.Flags = o.Flags;
            this.NumFileLoc = o.NumFileLoc;
            
            this.FileLocInfo = new FileLocInfo[o.FileLocInfo.Length];
            for(int i = 0; i < o.FileLocInfo.Length; i++)
                this.FileLocInfo[i] = new FileLocInfo(o.FileLocInfo[i]);
        }
    }
}
