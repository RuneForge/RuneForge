using System;
using System.IO;

namespace RuneForge.Content.Pipeline.Game.Extensions
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter binaryWriter, Guid value)
        {
            binaryWriter.Write(value.ToByteArray());
        }
    }
}
