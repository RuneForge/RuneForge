using System.IO;

namespace RuneForge.Content.Pipeline.Game.Extensions
{
    public delegate void ValueTypeWriterMethod<T>(BinaryWriter binaryWriter, T value)
        where T : struct;
}
