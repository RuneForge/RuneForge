namespace RuneForge.Game.GameSessions
{
    public class GameSessionParameters
    {
        public GameSessionType Type { get; set; }

        public string MapAssetName { get; set; }

        public SerializableGameSessionContext GameSessionContext { get; set; }

        public bool StartPaused { get; set; }
    }
}
