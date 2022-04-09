using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Rendering.Interfaces;
using RuneForge.Core.TextureAtlases;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Core.Rendering
{
    public class MapRenderer
    {
        private readonly IMapProvider m_mapProvider;
        private readonly ISpriteBatchProvider m_spriteBatchProvider;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Camera2D m_camera;
        private readonly Camera2DParameters m_cameraParameters;
        private TextureAtlas m_textureAtlas;

        public MapRenderer(
            IMapProvider mapProvider,
            ISpriteBatchProvider spriteBatchProvider,
            Lazy<ContentManager> contentManagerProvider,
            Camera2D camera,
            Camera2DParameters cameraParameters
            )
        {
            m_mapProvider = mapProvider;
            m_spriteBatchProvider = spriteBatchProvider;
            m_contentManagerProvider = contentManagerProvider;
            m_camera = camera;
            m_cameraParameters = cameraParameters;
            m_textureAtlas = null;
        }

        public void Draw()
        {
            Rectangle viewportBounds = m_cameraParameters.Viewport.Bounds;
            (int minVisibleX, int minVisibleY) = m_camera.TranslateScreenToWorld(new Vector2(viewportBounds.Left, viewportBounds.Top)).ToPoint();
            (int maxVisibleX, int maxVisibleY) = m_camera.TranslateScreenToWorld(new Vector2(viewportBounds.Right, viewportBounds.Bottom)).ToPoint();

            int minVisibleCellX = minVisibleX / Map.CellWidth;
            int minVisibleCellY = minVisibleY / Map.CellHeight;
            int maxVisibleCellX = maxVisibleX / Map.CellWidth;
            int maxVisibleCellY = maxVisibleY / Map.CellHeight;

            Map map = m_mapProvider.Map;
            MapTileset tileset = map.Tileset;
            SpriteBatch spriteBatch = m_spriteBatchProvider.WorldSpriteBatch;
            for (int y = Math.Max(minVisibleCellY, 0); y <= Math.Min(maxVisibleCellY, map.Height - 1); y++)
            {
                for (int x = Math.Max(minVisibleCellX, 0); x <= Math.Min(maxVisibleCellX, map.Width - 1); x++)
                {
                    MapCell cell = map.GetCell(x, y);
                    if (cell.Tier != MapCellTier.None)
                    {
                        if (tileset.TryGetCellPrototype(cell.Tier, cell.Type, out MapTilesetCellPrototype cellPrototype))
                        {
                            TextureRegion2D textureRegion = m_textureAtlas.TextureRegions[cellPrototype.TextureRegionName];
                            spriteBatch.Draw(textureRegion, new Rectangle(x * Map.CellWidth, y * Map.CellHeight, Map.CellWidth, Map.CellHeight), Color.White);
                        }
                    }
                }
            }
        }

        public void LoadContent()
        {
            Map map = m_mapProvider.Map;
            ContentManager contentManager = m_contentManagerProvider.Value;
            m_textureAtlas = contentManager.Load<TextureAtlas>(map.Tileset.TextureAtlasName);
        }
    }
}
