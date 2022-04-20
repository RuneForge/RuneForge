﻿using System;
using System.Collections.ObjectModel;

using RuneForge.Data.Units;
using RuneForge.Game.Components;
using RuneForge.Game.Components.Interfaces;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Units
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IServiceProvider m_serviceProvider;
        private readonly IPlayerService m_playerService;
        private int m_nextUnitId;

        public UnitFactory(IServiceProvider serviceProvider, IPlayerService playerService)
        {
            m_serviceProvider = serviceProvider;
            m_playerService = playerService;
            m_nextUnitId = 1;
        }

#warning Add support for restoring the component collection from a DTO object.
        public Unit CreateFromDto(UnitDto unit)
        {
            Player owner = m_playerService.GetPlayer(unit.OwnerId);
            return new Unit(unit.Id, unit.Name, owner, Array.Empty<IComponent>());
        }

        public Unit CreateFromInstancePrototype(UnitInstancePrototype instancePrototype)
        {
            int id = m_nextUnitId++;
            Player owner = m_playerService.GetPlayer(instancePrototype.OwnerId);
            UnitPrototype unitPrototype = instancePrototype.EntityPrototype;
            Collection<IComponent> components = ComponentFactoryHelpers.CreateComponentCollection(m_serviceProvider, unitPrototype.ComponentPrototypes, instancePrototype.ComponentPrototypeOverrides);
            return new Unit(id, unitPrototype.Name, owner, components);
        }
    }
}
