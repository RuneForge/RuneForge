using System.IO;

namespace RuneForge.Core.Helpers.Interfaces
{
    public interface IGameStateSerializer
    {
        public string GetSaveFileName();

        public void SerializeGameState(Stream stream);
    }
}
