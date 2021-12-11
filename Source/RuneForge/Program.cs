using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.DependencyInjection;

namespace RuneForge
{
    internal static class Program
    {
        [STAThread]
        private static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).ConfigureServices(ConfigureServices).Build();
            using Game game = host.Services.GetRequiredService<Game>();

            await host.StartAsync();
            game.Run();

            await host.StopAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args);
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddMonoGame((serviceProvider, gameWindow) => new RuneForgeGame(serviceProvider, gameWindow));
        }
    }
}
