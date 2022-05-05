using System;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework;

using RuneForge.Data.Players;
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

        public Player CreateFromDto(PlayerDto playerDto)
        {
            PlayerColor playerColor = new PlayerColor(
                new Color(playerDto.Color.MainColorPacked),
                new Color(playerDto.Color.EntityColorShadeAPacked),
                new Color(playerDto.Color.EntityColorShadeBPacked),
                new Color(playerDto.Color.EntityColorShadeCPacked),
                new Color(playerDto.Color.EntityColorShadeDPacked)
                );
            return new Player(playerDto.Id, playerDto.Name, playerColor, Array.Empty<IComponent>());
        }
    }
}
