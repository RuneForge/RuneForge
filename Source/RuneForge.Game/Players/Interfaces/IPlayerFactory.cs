using RuneForge.Data.Players;

namespace RuneForge.Game.Players.Interfaces
{
    public interface IPlayerFactory
    {
        public Player CreateFromPrototype(PlayerPrototype playerPrototype);

        public Player CreateFromDto(PlayerDto playerDto);
    }
}
