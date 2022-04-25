using System;
using System.Collections.ObjectModel;

using RuneForge.Game.Components;
using RuneForge.Game.Components.Interfaces;
using RuneForge.Game.Players.Interfaces;

namespace RuneForge.Game.Players
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IServiceProvider m_serviceProvider;

        public PlayerFactory(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
        }

        public Player CreateFromPrototype(PlayerPrototype prototype)
        {
            Collection<IComponent> components = ComponentFactoryHelpers.CreateComponentCollection(m_serviceProvider, prototype.ComponentPrototypes);
            return new Player(prototype.Id, prototype.Name, prototype.Color, components);
        }
    }
}
