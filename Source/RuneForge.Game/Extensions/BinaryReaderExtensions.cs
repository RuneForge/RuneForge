using System;
using System.IO;

namespace RuneForge.Game.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static Guid ReadGuid(this BinaryReader binaryReader)
        {
            return new Guid(binaryReader.ReadBytes(16));
        }
    }
}
