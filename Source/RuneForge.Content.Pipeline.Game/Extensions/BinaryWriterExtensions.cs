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

        public static void Write<T>(this BinaryWriter binaryWriter, T? nullableValue, ValueTypeWriterMethod<T> structContentsWriterAction)
            where T : struct
        {
            binaryWriter.Write(nullableValue.HasValue);
            if (nullableValue.HasValue)
                structContentsWriterAction(binaryWriter, nullableValue.Value);
        }
    }
}
