using System.Collections.Generic;
using System.Linq;

using RuneForge.Game.Components.Implementations;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Units;

namespace RuneForge.Game.Components.Factories
{
    public class ProductionFacilityComponentFactory : ComponentFactory<ProductionFacilityComponent, ProductionFacilityComponentPrototype>
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly Dictionary<string, UnitPrototype> m_unitPrototypeCache;

        public ProductionFacilityComponentFactory(IGameSessionContext gameSessionContext)
        {
            m_gameSessionContext = gameSessionContext;
            m_unitPrototypeCache = new Dictionary<string, UnitPrototype>();
        }

        public override ProductionFacilityComponent CreateComponentFromPrototype(ProductionFacilityComponentPrototype componentPrototype, ProductionFacilityComponentPrototype componentPrototypeOverride)
        {
            List<UnitPrototype> unitsProduced = new List<UnitPrototype>(componentPrototype.UnitCodesProduced.Count);
            foreach (string unitCode in componentPrototype.UnitCodesProduced)
            {
                if (!m_unitPrototypeCache.TryGetValue(unitCode, out UnitPrototype unitPrototype))
                {
                    unitPrototype = m_gameSessionContext.Map.UnitPrototypes.Where(unitPrototype => unitPrototype.Code == unitCode).First();
                    m_unitPrototypeCache.Add(unitCode, unitPrototype);
                }
                unitsProduced.Add(unitPrototype);
            }
            return new ProductionFacilityComponent(unitsProduced);
        }
    }
}
