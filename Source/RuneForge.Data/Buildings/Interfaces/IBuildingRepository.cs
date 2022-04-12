using System.Collections.ObjectModel;

namespace RuneForge.Data.Buildings.Interfaces
{
    public interface IBuildingRepository
    {
        public BuildingDto GetBuilding(int buildingId);

        public ReadOnlyCollection<BuildingDto> GetBuildings();

        public void AddBuilding(BuildingDto building);

        public void SaveBuilding(BuildingDto building);

        public void RemoveBuilding(int buildingId);
    }
}
