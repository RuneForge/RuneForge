using Microsoft.Xna.Framework;

using RuneForge.Core.Interface.Controls;
using RuneForge.Game.Components.Entities;

namespace RuneForge.Core.Interface.Windows
{
    public partial class PlayerResourceStatisticsWindow
    {
        private const int c_windowWidth = 160;
        private const int c_totalFramesWidth = 14;

        private static readonly ResourceTypeInfo[] s_resourceTypes = new ResourceTypeInfo[]
        {
            new ResourceTypeInfo() { ResourceType = ResourceTypes.Gold, DisplayName = "Gold", IconTextureRegionName = "Gold" },
        };

        private PictureBox[] m_iconPictureBoxes;
        private Label[] m_labels;

        private void CreateLayout()
        {
            Width = c_windowWidth;
            Height = 128;

            Title = "Available Resources";
            CanBeMoved = false;
            CanBeClosed = false;

            m_iconPictureBoxes = new PictureBox[s_resourceTypes.Length];
            m_labels = new Label[s_resourceTypes.Length];

            int previousY = 0;
            for (int i = 0; i < s_resourceTypes.Length; i++)
            {
                m_iconPictureBoxes[i] = new PictureBox(null, ContentManager, GraphicsDevice, SpriteBatch)
                {
                    X = 0,
                    Y = previousY,
                    Width = 16,
                    Height = 16,

                    ImageAlignment = Alignment.Center ^ Alignment.Right,
                };
                Controls.Add(m_iconPictureBoxes[i]);

                m_labels[i] = new Label(null, ContentManager, GraphicsDevice, SpriteBatch)
                {
                    X = m_iconPictureBoxes[i].X + m_iconPictureBoxes[i].Width + 4,
                    Y = previousY,
                    Width = c_windowWidth - c_totalFramesWidth - (m_iconPictureBoxes[i].X + m_iconPictureBoxes[i].Width + 4),
                    Height = 16,

                    TextAlignment = Alignment.Center ^ Alignment.Right,
                    TextColor = Color.White,
                    ShadowColor = Color.Transparent,
                };
                Controls.Add(m_labels[i]);

                previousY += 16 + 4;
            }

            Height = (previousY - 4) + 37;
        }

        private class ResourceTypeInfo
        {
            public ResourceTypes ResourceType { get; set; }

            public string DisplayName { get; set; }

            public string IconTextureRegionName { get; set; }
        }
    }
}
