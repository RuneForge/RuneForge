using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

using RuneForge.Core.GameStates;
using RuneForge.Core.GameStates.Components;
using RuneForge.Core.GameStates.Implementations;
using RuneForge.Core.GameStates.Interfaces;
using RuneForge.Core.Input.Components;
using RuneForge.Core.Input.EventProviders;
using RuneForge.Core.Input.EventProviders.Configuration;
using RuneForge.Core.Input.EventProviders.Interfaces;
using RuneForge.Core.Interface;
using RuneForge.Core.Interface.Components;
using RuneForge.Core.Interface.Interfaces;

namespace RuneForge.Core.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInputServices(this IServiceCollection services)
        {
            services.Configure(delegate (KeyboardEventProviderConfiguration configuration)
            {
                configuration.InitialEventDelay = KeyboardEventProviderConfiguration.DefaultInitialEventDelay;
                configuration.RepeatedEventDelay = KeyboardEventProviderConfiguration.DefaultRepeatedEventDelay;
            });
            services.Configure(delegate (MouseEventProviderConfiguration configuration)
            {
                configuration.DoubleClickTimeSpan = MouseEventProviderConfiguration.DefaultDoubleClickTimeSpan;
                configuration.DragDistanceThreshold = MouseEventProviderConfiguration.DefaultDragDistanceThreshold;
            });
            services.AddSingleton<IKeyboardEventProvider, KeyboardEventProvider>();
            services.AddSingleton<IMouseEventProvider, MouseEventProvider>();
            services.AddSingleton<IGameComponent, InputComponent>();
            return services;
        }

        public static IServiceCollection AddGameStateManagementServices(this IServiceCollection services)
        {
            services.AddSingleton<IGameStateService, GameStateService>();
            services.AddSingleton<IGameComponent, GameStateComponent>();
            services.AddScoped<MainMenuGameState>();
            services.AddScoped<GameplayGameState>();
            return services;
        }

        public static IServiceCollection AddGraphicsInterfaceServices(this IServiceCollection services)
        {
            services.AddSingleton<ISpriteFontProvider, SpriteFontProvider>();
            services.AddSingleton<IGraphicsInterfaceService, GraphicsInterfaceService>();
            services.AddSingleton<IGameComponent, GraphicsInterfaceComponent>();
            return services;
        }
    }
}
