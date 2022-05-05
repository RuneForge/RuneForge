using System.Collections.Generic;
using System.Linq;

using RuneForge.Data.Components;
using RuneForge.Game.Components.Attributes;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Units;

namespace RuneForge.Game.Components.Factories
{
    [ComponentDto(typeof(ProductionFacilityComponentDto))]
    public class ProductionFacilityComponentFactory : ComponentFactory<ProductionFacilityComponent, ProductionFacilityComponentPrototype, ProductionFacilityComponentDto>
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

        public override ProductionFacilityComponent CreateComponentFromDto(ProductionFacilityComponentDto componentDto)
        {
            List<UnitPrototype> unitsProduced = new List<UnitPrototype>(componentDto.UnitCodesProduced.Count);
            foreach (string unitCode in componentDto.UnitCodesProduced)
            {
                if (!m_unitPrototypeCache.TryGetValue(unitCode, out UnitPrototype unitPrototype))
                {
                    unitPrototype = m_gameSessionContext.Map.UnitPrototypes.Where(unitPrototype => unitPrototype.Code == unitCode).First();
                    m_unitPrototypeCache.Add(unitCode, unitPrototype);
                }
                unitsProduced.Add(unitPrototype);
            }

            UnitPrototype unitCurrentlyProduced = null;
            if (componentDto.UnitCodeCurrentlyProduced != null)
                unitCurrentlyProduced = unitsProduced.Where(unitPrototype => unitPrototype.Code == componentDto.UnitCodeCurrentlyProduced).Single();

            return new ProductionFacilityComponent(unitsProduced)
            {
                UnitCurrentlyProduced = unitCurrentlyProduced,
                TimeElapsed = componentDto.TimeElapsed,
                ProductionFinished = componentDto.ProductionFinished,
            };
        }
    }
}
