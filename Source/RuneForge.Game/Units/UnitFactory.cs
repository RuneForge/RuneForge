using RuneForge.Data.Units;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Units
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IPlayerService m_playerService;
        private int m_nextUnitId;

        public UnitFactory(IPlayerService playerService)
        {
            m_playerService = playerService;
            m_nextUnitId = 1;
        }

        public Unit CreateFromDto(UnitDto unit)
        {
            Player owner = m_playerService.GetPlayer(unit.OwnerId);
            return new Unit(unit.Id, unit.Name, owner);
        }

        public Unit CreateFromInstancePrototype(UnitInstancePrototype instancePrototype)
        {
            int id = m_nextUnitId++;
            Player owner = m_playerService.GetPlayer(instancePrototype.OwnerId);
            return new Unit(id, instancePrototype.EntityPrototype.Name, owner);
        }
    }
}
