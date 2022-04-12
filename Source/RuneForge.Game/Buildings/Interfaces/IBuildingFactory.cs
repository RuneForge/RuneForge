using RuneForge.Data.Buildings;

namespace RuneForge.Game.Buildings.Interfaces
{
    public interface IBuildingFactory
    {
        public Building CreateFromDto(BuildingDto building);
    }
}
