using Microsoft.Extensions.DependencyInjection;

using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMapServices(this IServiceCollection services)
        {
            services.AddScoped<IMapProvider, MapProvider>();
            services.AddScoped<IMapCellTypeResolver, MapCellTypeResolver>();
            services.AddScoped<IMapDecorationTypeResolver, MapDecorationTypeResolver>();

            return services;
        }
    }
}
