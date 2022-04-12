using RuneForge.Data.Units;

namespace RuneForge.Game.Units.Interfaces
{
    public interface IUnitFactory
    {
        public Unit CreateFromDto(UnitDto unit);
    }
}
