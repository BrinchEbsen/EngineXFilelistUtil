using System;
using System.IO;

namespace FilelistUtilities.Common
{
    public record EXRelPtr32
    {
        public int Address;
        public int Offset;
        public int AbsOffset => Offset == 0 ? 0 : Address + Offset;
        public override string ToString() => Offset == 0 ? "Null" : $"RelPtr=>{AbsOffset}";

        /// <exception cref="IOException">If offset is out of stream bounds.</exception>
        public void ThrowIfOutOfBounds(Stream stream)
        {
            if (AbsOffset < 0 || AbsOffset > stream.Length)
                throw new IOException($"Relative pointer offset {AbsOffset} is outside the file bounds.");
        }
    }

    public record EXRelPtr16
    {
        public int Address;
        public short Offset;
        public int AbsOffset => Offset == 0 ? 0 : Address + Offset;
        public override string ToString() => Offset == 0 ? "Null" : $"RelPtr=>{AbsOffset}";

        public void ThrowIfOutOfBounds(Stream stream)
        {
            if (AbsOffset < 0 || AbsOffset > stream.Length)
                throw new IOException($"Relative pointer offset {AbsOffset} is outside the file bounds.");
        }
    }
}
