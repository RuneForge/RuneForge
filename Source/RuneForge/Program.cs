using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

            try
            {
                game.Run();
            }
            catch (Exception e)
            {
                ILogger<Game> gameLogger = host.Services.GetRequiredService<ILogger<Game>>();
                gameLogger.LogError(e, "Shutting down the application host because of an unhandled exception.");
                throw;
            }
            finally
            {
                await host.StopAsync();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args);
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddMonoGame((serviceProvider) =>
            {
                GameWindow gameWindow = serviceProvider.GetRequiredService<GameWindow>();
                IEnumerable<IGameComponent> gameComponents = serviceProvider.GetRequiredService<IEnumerable<IGameComponent>>();
                return new RuneForgeGame(serviceProvider, gameWindow, gameComponents);
            });
        }
    }
}
