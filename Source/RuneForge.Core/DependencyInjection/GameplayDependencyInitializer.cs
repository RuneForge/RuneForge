using System;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Rendering;
using RuneForge.Core.Rendering.Interfaces;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Core.DependencyInjection
{
    public class GameplayDependencyInitializer
    {
        private readonly IMapProvider m_mapProvider;
        private readonly ISpriteBatchProvider m_spriteBatchProvider;
        private readonly IMapCellTypeResolver m_mapCellTypeResolver;
        private readonly IMapDecorationTypeResolver m_mapDecorationTypeResolver;
        private readonly MapRenderer m_mapRenderer;
        private readonly Camera2DParameters m_cameraParameters;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Lazy<GraphicsDevice> m_graphicsDeviceProvider;

        public GameplayDependencyInitializer(
            IMapProvider mapProvider,
            ISpriteBatchProvider spriteBatchProvider,
            IMapCellTypeResolver mapCellTypeResolver,
            IMapDecorationTypeResolver mapDecorationTypeResolver,
            MapRenderer mapRenderer,
            Camera2DParameters cameraParameters,
            Lazy<ContentManager> contentManagerProvider,
            Lazy<GraphicsDevice> graphicsDeviceProvider
            )
        {
            m_mapProvider = mapProvider;
            m_spriteBatchProvider = spriteBatchProvider;
            m_mapCellTypeResolver = mapCellTypeResolver;
            m_mapDecorationTypeResolver = mapDecorationTypeResolver;
            m_mapRenderer = mapRenderer;
            m_cameraParameters = cameraParameters;
            m_contentManagerProvider = contentManagerProvider;
            m_graphicsDeviceProvider = graphicsDeviceProvider;
        }

        public void InitializeDependencies(string mapAssetName)
        {
            ContentManager contentManager = m_contentManagerProvider.Value;
            GraphicsDevice graphicsDevice = m_graphicsDeviceProvider.Value;

            Map map = contentManager.Load<Map>(mapAssetName);
            map.ResolveMapCellTypes(m_mapCellTypeResolver);
            map.ResolveMapDecorationTypes(m_mapDecorationTypeResolver);
            m_mapProvider.Map = map;

            SpriteBatch worldSpriteBatch = new SpriteBatch(graphicsDevice);
            m_spriteBatchProvider.WorldSpriteBatch = worldSpriteBatch;

            m_cameraParameters.Viewport = graphicsDevice.Viewport;

            m_mapRenderer.LoadContent();
        }
    }
}
