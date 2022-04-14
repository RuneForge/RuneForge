namespace RuneForge.Game.Players.Interfaces
{
    public interface IPlayerFactory
    {
        public Player CreateFromPrototype(PlayerPrototype playerPrototype);
    }
}
