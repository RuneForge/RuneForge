using System.IO;

namespace RuneForge.Game.Extensions
{
    public delegate T ValueTypeReaderMethod<T>(BinaryReader binaryReader)
        where T : struct;
}
