using System;
using System.IO;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Controls.ImageSources;
using RuneForge.Core.Interface.Interfaces;
using RuneForge.Core.TextureAtlases;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Players;

namespace RuneForge.Core.Interface.Windows
{
    public partial class PlayerResourceStatisticsWindow : Window
    {
        private static readonly string s_iconTextureAtlasAssetName = Path.Combine("TextureAtlases", "Interface", "Icons");

        private readonly ISpriteFontProvider m_spriteFontProvider;
        private TextureAtlas m_iconTextureAtlas;
        private int m_resourceValuesHash;

        public Player Player { get; set; }

        public PlayerResourceStatisticsWindow(ControlEventSource eventSource, ContentManager contentManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, ISpriteFontProvider spriteFontProvider)
            : base(eventSource, contentManager, graphicsDevice, spriteBatch)
        {
            m_spriteFontProvider = spriteFontProvider;
            m_iconTextureAtlas = null;
            m_resourceValuesHash = 0;
            CreateLayout();
        }

        public override void LoadContent()
        {
            m_iconTextureAtlas = ContentManager.Load<TextureAtlas>(s_iconTextureAtlasAssetName);
            for (int i = 0; i < s_resourceTypes.Length; i++)
            {
                ResourceTypeInfo resourceType = s_resourceTypes[i];
                m_iconPictureBoxes[i].ImageSource = new TextureRegion2DImageSource(m_iconTextureAtlas.TextureRegions[resourceType.IconTextureRegionName]);
                m_labels[i].SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            }
            SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
            base.LoadContent();
        }

        public void UpdateStatistics()
        {
            if (Player == null)
                throw new InvalidOperationException("Unable to update resource statistics because the player was not set.");

            ResourceContainerComponent resourceContainerComponent = Player.GetComponentOfType<ResourceContainerComponent>();
            int resourceValuesHash = HashCode.Combine(resourceContainerComponent.GoldAmount);
            if (resourceValuesHash == m_resourceValuesHash)
                return;

            m_resourceValuesHash = resourceValuesHash;
            for (int i = 0; i < s_resourceTypes.Length; i++)
            {
                ResourceTypeInfo resourceType = s_resourceTypes[i];
                decimal resourceAmount = resourceContainerComponent.GetResourceAmount(resourceType.ResourceType);
                m_labels[i].Text = $"{resourceType.DisplayName}: {resourceAmount}";
            }
        }
    }
}
