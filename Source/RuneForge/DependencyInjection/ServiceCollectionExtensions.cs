﻿using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.DependencyInjection;

using RuneForge.Configuration;
using RuneForge.Core.GameStates.Interfaces;

using XnaGame = Microsoft.Xna.Framework.Game;

namespace RuneForge.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRuneForgeGame(this IServiceCollection services)
        {
            services.AddMonoGame((serviceProvider) =>
            {
                GameWindow gameWindow = serviceProvider.GetRequiredService<GameWindow>();
                IGameStateService gameStateService = serviceProvider.GetRequiredService<IGameStateService>();
                Lazy<GraphicsDeviceManager> graphicsDeviceManagerProvider = serviceProvider.GetRequiredService<Lazy<GraphicsDeviceManager>>();
                IOptions<GraphicsConfiguration> graphicsConfigurationOptions = serviceProvider.GetRequiredService<IOptions<GraphicsConfiguration>>();
                IEnumerable<IGameComponent> gameComponents = serviceProvider.GetRequiredService<IEnumerable<IGameComponent>>();
                return new RuneForgeGame(serviceProvider, gameWindow, gameStateService, graphicsDeviceManagerProvider, graphicsConfigurationOptions, gameComponents);
            });

            services.AddSingleton(serviceProvider => (RuneForgeGame)serviceProvider.GetRequiredService<XnaGame>());
            services.AddSingleton(serviceProvider =>
            {
                return new Lazy<RuneForgeGame>(() =>
                {
                    return (RuneForgeGame)serviceProvider.GetRequiredService<XnaGame>();
                });
            });

            return services;
        }
    }
}
