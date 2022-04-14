using RuneForge.Game.Players.Interfaces;

namespace RuneForge.Game.Players
{
    public class PlayerFactory : IPlayerFactory
    {
        public Player CreateFromPrototype(PlayerPrototype prototype)
        {
            return new Player(prototype.Id, prototype.Name, prototype.Color);
        }
    }
}
