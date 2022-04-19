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

        public static T? ReadNullable<T>(this BinaryReader binaryReader, ValueTypeReaderMethod<T> structContentsReaderAction)
            where T : struct
        {
            bool hasValue = binaryReader.ReadBoolean();
            if (!hasValue)
                return new T?();
            else
                return new T?(structContentsReaderAction(binaryReader));
        }
    }
}
