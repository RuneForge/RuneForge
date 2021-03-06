using System;
using System.Collections.ObjectModel;

using RuneForge.Data.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components;
using RuneForge.Game.Components.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;

namespace RuneForge.Game.Buildings
{
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IServiceProvider m_serviceProvider;
        private readonly IPlayerService m_playerService;
        private int m_nextBuildingId;

        public BuildingFactory(IServiceProvider serviceProvider, IPlayerService playerService)
        {
            m_serviceProvider = serviceProvider;
            m_playerService = playerService;
            m_nextBuildingId = 1;
        }

        public Building CreateFromDto(BuildingDto building)
        {
            m_nextBuildingId = Math.Max(m_nextBuildingId, building.Id + 1);
            Player owner = m_playerService.GetPlayer(building.OwnerId);
            return new Building(building.Id, building.Name, owner, Array.Empty<IComponent>());
        }

        public Building CreateFromInstancePrototype(BuildingInstancePrototype instancePrototype)
        {
            int id = m_nextBuildingId++;
            Player owner = m_playerService.GetPlayer(instancePrototype.OwnerId);
            BuildingPrototype buildingPrototype = instancePrototype.EntityPrototype;
            Collection<IComponent> components = ComponentFactoryHelpers.CreateComponentCollection(m_serviceProvider, buildingPrototype.ComponentPrototypes, instancePrototype.ComponentPrototypeOverrides);
            return new Building(id, buildingPrototype.Name, owner, components);
        }
    }
}
