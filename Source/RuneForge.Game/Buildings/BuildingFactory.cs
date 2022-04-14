using RuneForge.Data.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;

namespace RuneForge.Game.Buildings
{
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IPlayerService m_playerService;
        private int m_nextUnitId;

        public BuildingFactory(IPlayerService playerService)
        {
            m_playerService = playerService;
            m_nextUnitId = 1;
        }

        public Building CreateFromDto(BuildingDto building)
        {
            Player owner = m_playerService.GetPlayer(building.OwnerId);
            return new Building(building.Id, building.Name, owner);
        }

        public Building CreateFromInstancePrototype(BuildingInstancePrototype instancePrototype)
        {
            int id = m_nextUnitId++;
            Player owner = m_playerService.GetPlayer(instancePrototype.OwnerId);
            return new Building(id, instancePrototype.EntityPrototype.Name, owner);
        }
    }
}
