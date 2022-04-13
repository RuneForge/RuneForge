using System;

using Microsoft.Extensions.DependencyInjection;

namespace RuneForge.Game.DependencyInjection
{
    internal static class ServiceProviderExtensions
    {
        public static Lazy<TService> GetLazyInitializedService<TService>(this IServiceProvider serviceProvider)
        {
            return new Lazy<TService>(() => serviceProvider.GetRequiredService<TService>());
        }
    }
}
