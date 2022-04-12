using System.Collections.ObjectModel;

namespace RuneForge.Game.Buildings.Interfaces
{
    public interface IBuildingService
    {
        public Building GetBuilding(int buildingId);

        public ReadOnlyCollection<Building> GetBuildings();

        public void AddBuilding(Building building);

        public void RemoveBuilding(int buildingId);
    }
}
