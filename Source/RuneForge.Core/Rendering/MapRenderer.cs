using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Rendering.Interfaces;
using RuneForge.Core.TextureAtlases;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Maps;

namespace RuneForge.Core.Rendering
{
    public class MapRenderer : IRenderer
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly ISpriteBatchProvider m_spriteBatchProvider;
        private readonly Lazy<ContentManager> m_contentManagerProvider;
        private readonly Camera2D m_camera;
        private readonly Camera2DParameters m_cameraParameters;
        private TextureAtlas m_textureAtlas;
        private bool m_visible;
        private int m_drawOrder;

        public MapRenderer(
            IGameSessionContext gameSessionContext,
            ISpriteBatchProvider spriteBatchProvider,
            Lazy<ContentManager> contentManagerProvider,
            Camera2D camera,
            Camera2DParameters cameraParameters
            )
        {
            m_gameSessionContext = gameSessionContext;
            m_spriteBatchProvider = spriteBatchProvider;
            m_contentManagerProvider = contentManagerProvider;
            m_camera = camera;
            m_cameraParameters = cameraParameters;
            m_textureAtlas = null;
            m_visible = true;
            m_drawOrder = 0;
        }

        public bool Visible
        {
            get => m_visible;
            set
            {
                if (m_visible != value)
                {
                    m_visible = value;
                    OnVisibleChanged(EventArgs.Empty);
                }
            }
        }
        public int DrawOrder
        {
            get => m_drawOrder;
            set
            {
                if (m_drawOrder != value)
                {
                    m_drawOrder = value;
                    OnDrawOrderChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;

        public void Draw(GameTime gameTime)
        {
            Rectangle viewportBounds = m_cameraParameters.Viewport.Bounds;
            (int minVisibleX, int minVisibleY) = m_camera.TranslateScreenToWorld(new Vector2(viewportBounds.Left, viewportBounds.Top)).ToPoint();
            (int maxVisibleX, int maxVisibleY) = m_camera.TranslateScreenToWorld(new Vector2(viewportBounds.Right, viewportBounds.Bottom)).ToPoint();

            int minVisibleCellX = minVisibleX / Map.CellWidth;
            int minVisibleCellY = minVisibleY / Map.CellHeight;
            int maxVisibleCellX = maxVisibleX / Map.CellWidth;
            int maxVisibleCellY = maxVisibleY / Map.CellHeight;

            Map map = m_gameSessionContext.Map;
            MapTileset tileset = map.Tileset;
            SpriteBatch spriteBatch = m_spriteBatchProvider.WorldSpriteBatch;
            for (int y = Math.Max(minVisibleCellY, 0); y <= Math.Min(maxVisibleCellY, map.Height - 1); y++)
            {
                for (int x = Math.Max(minVisibleCellX, 0); x <= Math.Min(maxVisibleCellX, map.Width - 1); x++)
                {
                    MapLandscapeCell landscapeCell = map.GetLandscapeCell(x, y);
                    if (landscapeCell.Tier != MapLandscapeCellTier.None)
                    {
                        if (tileset.TryGetLandscapeCellPrototype(landscapeCell.Tier, landscapeCell.Type, out MapTilesetLandscapeCellPrototype landscapeCellPrototype))
                        {
                            TextureRegion2D textureRegion = m_textureAtlas.TextureRegions[landscapeCellPrototype.TextureRegionName];
                            spriteBatch.Draw(textureRegion, new Rectangle(x * Map.CellWidth, y * Map.CellHeight, Map.CellWidth, Map.CellHeight), Color.White);
                        }
                    }
                    MapDecorationCell decorationCell = map.GetDecorationCell(x, y);
                    if (decorationCell.Tier != MapDecorationCellTier.None)
                    {
                        if (tileset.TryGetDecorationCellPrototype(decorationCell.Tier, decorationCell.Type, out MapTilesetDecorationCellPrototype decorationCellPrototype))
                        {
                            TextureRegion2D textureRegion = m_textureAtlas.TextureRegions[decorationCellPrototype.TextureRegionName];
                            spriteBatch.Draw(textureRegion, new Rectangle(x * Map.CellWidth, y * Map.CellHeight, Map.CellWidth, Map.CellHeight), Color.White);
                        }
                    }
                }
            }
        }

        public void LoadContent()
        {
            Map map = m_gameSessionContext.Map;
            ContentManager contentManager = m_contentManagerProvider.Value;
            m_textureAtlas = contentManager.Load<TextureAtlas>(map.Tileset.TextureAtlasName);
        }

        protected virtual void OnVisibleChanged(EventArgs e)
        {
            VisibleChanged?.Invoke(this, e);
        }
        protected virtual void OnDrawOrderChanged(EventArgs e)
        {
            DrawOrderChanged?.Invoke(this, e);
        }
    }
}
