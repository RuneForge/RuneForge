using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.DependencyInjection;

namespace RuneForge.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRuneForgeGame(this IServiceCollection services)
        {
            services.AddMonoGame((serviceProvider) =>
            {
                GameWindow gameWindow = serviceProvider.GetRequiredService<GameWindow>();
                IEnumerable<IGameComponent> gameComponents = serviceProvider.GetRequiredService<IEnumerable<IGameComponent>>();
                return new RuneForgeGame(serviceProvider, gameWindow, gameComponents);
            });

            services.AddSingleton(serviceProvider => (RuneForgeGame)serviceProvider.GetRequiredService<Game>());
            services.AddSingleton(serviceProvider =>
            {
                return new Lazy<RuneForgeGame>(() =>
                {
                    return (RuneForgeGame)serviceProvider.GetRequiredService<Game>();
                });
            });

            return services;
        }
    }
}
