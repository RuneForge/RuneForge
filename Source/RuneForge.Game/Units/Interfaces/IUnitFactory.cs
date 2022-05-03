using RuneForge.Data.Units;
using RuneForge.Game.Players;

namespace RuneForge.Game.Units.Interfaces
{
    public interface IUnitFactory
    {
        public Unit CreateFromDto(UnitDto unit);

        public Unit CreateFromPrototype(UnitPrototype prototype, Player owner);

        public Unit CreateFromInstancePrototype(UnitInstancePrototype instancePrototype);
    }
}
