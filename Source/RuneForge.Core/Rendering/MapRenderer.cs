using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.TextureAtlases;
using RuneForge.Game.Maps;

namespace RuneForge.Core.Rendering
{
    public class MapRenderer
    {
        private readonly Map m_map;
        private readonly Camera2D m_camera;
        private readonly SpriteBatch m_spriteBatch;
        private readonly ContentManager m_contentManager;
        private readonly Viewport m_viewport;
        private TextureAtlas m_textureAtlas;

        public MapRenderer(Map map, Camera2D camera, SpriteBatch spriteBatch, ContentManager contentManager, Viewport viewport)
        {
            m_map = map;
            m_camera = camera;
            m_spriteBatch = spriteBatch;
            m_contentManager = contentManager;
            m_viewport = viewport;
            m_textureAtlas = null;
        }

        public void Draw()
        {
            (int minVisibleX, int minVisibleY) = m_camera.TranslateScreenToWorld(new Vector2(m_viewport.Bounds.Left, m_viewport.Bounds.Top)).ToPoint();
            (int maxVisibleX, int maxVisibleY) = m_camera.TranslateScreenToWorld(new Vector2(m_viewport.Bounds.Right, m_viewport.Bounds.Bottom)).ToPoint();

            int minVisibleCellX = minVisibleX / Map.CellWidth;
            int minVisibleCellY = minVisibleY / Map.CellHeight;
            int maxVisibleCellX = maxVisibleX / Map.CellWidth;
            int maxVisibleCellY = maxVisibleY / Map.CellHeight;

            MapTileset tileset = m_map.Tileset;
            for (int y = Math.Max(minVisibleCellY, 0); y <= Math.Min(maxVisibleCellY, m_map.Height - 1); y++)
            {
                for (int x = Math.Max(minVisibleCellX, 0); x <= Math.Min(maxVisibleCellX, m_map.Width - 1); x++)
                {
                    MapCell cell = m_map.GetCell(x, y);
                    if (cell.Tier != MapCellTier.None)
                    {
                        if (tileset.TryGetCellPrototype(cell.Tier, cell.Type, out MapTilesetCellPrototype cellPrototype))
                        {
                            TextureRegion2D textureRegion = m_textureAtlas.TextureRegions[cellPrototype.TextureRegionName];
                            m_spriteBatch.Draw(textureRegion, new Rectangle(x * Map.CellWidth, y * Map.CellHeight, Map.CellWidth, Map.CellHeight), Color.White);
                        }
                    }
                }
            }
        }

        public void LoadContent()
        {
            m_textureAtlas = m_contentManager.Load<TextureAtlas>(m_map.Tileset.TextureAtlasName);
        }
    }
}
