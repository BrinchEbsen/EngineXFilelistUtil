﻿using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;

namespace FilelistUtilities.Common
{
    public static class BinaryReaderExtensions
    {
        //Big-endian compatible methods

        public static ushort ReadUInt16(this BinaryReader reader, bool BigEndian)
        {
            if (!BigEndian)
            {
                return reader.ReadUInt16();
            }
            else
            {
                ushort x = reader.ReadUInt16();

                return (ushort)((ushort)((x & 0xff) << 8) | ((x >> 8) & 0xff));
            }
        }

        public static short ReadInt16(this BinaryReader reader, bool BigEndian)
        {
            if (!BigEndian)
            {
                return reader.ReadInt16();
            }
            else
            {
                return (short)ReadUInt16(reader, BigEndian);
            }
        }

        public static uint ReadUInt32(this BinaryReader reader, bool BigEndian)
        {
            if (!BigEndian)
            {
                return reader.ReadUInt32();
            }
            else
            {
                uint x = reader.ReadUInt32();

                return ((x & 0x000000ff) << 24) +
                       ((x & 0x0000ff00) << 8) +
                       ((x & 0x00ff0000) >> 8) +
                       ((x & 0xff000000) >> 24);
            }
        }

        public static int ReadInt32(this BinaryReader reader, bool BigEndian)
        {
            if (!BigEndian)
            {
                return reader.ReadInt32();
            }
            else
            {
                return (int)ReadUInt32(reader, BigEndian);
            }
        }

        public static float ReadSingle(this BinaryReader reader, bool BigEndian)
        {
            if (!BigEndian)
            {
                return reader.ReadSingle();
            }
            else
            {
                byte[] bytes = BitConverter.GetBytes(reader.ReadUInt32(true));
                return BitConverter.ToSingle(bytes, 0);
            }
        }
        public static EXRelPtr32 ReadEXRelPtr32(this BinaryReader reader, bool BigEndian)
        {
            long addr = reader.BaseStream.Position;

            return new EXRelPtr32
            {
                Address = (int)addr,
                Offset = reader.ReadInt32(BigEndian)
            };
        }

        public static EXRelPtr16 ReadEXRelPtr16(this BinaryReader reader, bool BigEndian)
        {
            long addr = reader.BaseStream.Position;

            return new EXRelPtr16
            {
                Address = (int)addr,
                Offset = reader.ReadInt16(BigEndian)
            };
        }

        //Reading special data types

        public static string ReadASCIIString(this BinaryReader reader)
        {
            return ReadASCIIString(reader, -1);
        }

        public static string ReadASCIIString(this BinaryReader reader, int length)
        {
            if (length == 0) return string.Empty;

            StringBuilder sb = new();

            int index = 0;
            for (char c = (char)reader.ReadByte(); true; c = (char)reader.ReadByte())
            {
                if (length >= 0)
                    if (index >= length) break;
                    else
                    if (c == 0) break;

                sb.Append(c);
                index++;
            }

            return sb.ToString();
        }
    }
}
