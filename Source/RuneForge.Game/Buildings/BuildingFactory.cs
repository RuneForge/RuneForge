using RuneForge.Data.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;

namespace RuneForge.Game.Buildings
{
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IPlayerService m_playerService;

        public BuildingFactory(IPlayerService playerService)
        {
            m_playerService = playerService;
        }

        public Building CreateFromDto(BuildingDto building)
        {
            Player owner = m_playerService.GetPlayer(building.OwnerId);
            return new Building(building.Id, building.Name, owner);
        }
    }
}
