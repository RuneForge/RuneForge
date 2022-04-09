using Microsoft.Extensions.DependencyInjection;

using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMaps(this IServiceCollection services)
        {
            services.AddTransient<IMapCellTypeResolver, MapCellTypeResolver>();

            return services;
        }
    }
}
