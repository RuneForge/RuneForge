using RuneForge.Data.Units;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Units
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IPlayerService m_playerService;

        public UnitFactory(IPlayerService playerService)
        {
            m_playerService = playerService;
        }

        public Unit CreateFromDto(UnitDto unit)
        {
            Player owner = m_playerService.GetPlayer(unit.OwnerId);
            return new Unit(unit.Id, unit.Name, owner);
        }
    }
}
