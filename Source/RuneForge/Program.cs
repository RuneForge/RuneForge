using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RuneForge.Configuration;
using RuneForge.Core.DependencyInjection;
using RuneForge.DependencyInjection;
using RuneForge.Game.DependencyInjection;

using XnaGame = Microsoft.Xna.Framework.Game;

namespace RuneForge
{
    internal static class Program
    {
        [STAThread]
        private static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).ConfigureServices(ConfigureServices).Build();
            using XnaGame game = host.Services.GetRequiredService<XnaGame>();

            await host.StartAsync();

            try
            {
                game.Run();
            }
            catch (Exception e)
            {
                ILogger<XnaGame> gameLogger = host.Services.GetRequiredService<ILogger<XnaGame>>();
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
            services.Configure<GraphicsConfiguration>(context.Configuration.GetSection(nameof(GraphicsConfiguration)));

            services.AddRuneForgeGame();

            services.AddInputServices();
            services.AddGameStateManagementServices();
            services.AddGraphicsInterfaceServices();
            services.AddRenderingServices();
            services.AddControllers();

            services.AddGameSessionContext();
            services.AddMapServices();
        }
    }
}
