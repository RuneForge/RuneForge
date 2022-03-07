using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

using RuneForge.Core.Input.Components;
using RuneForge.Core.Input.EventProviders;
using RuneForge.Core.Input.EventProviders.Configuration;
using RuneForge.Core.Input.EventProviders.Interfaces;

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
    }
}
