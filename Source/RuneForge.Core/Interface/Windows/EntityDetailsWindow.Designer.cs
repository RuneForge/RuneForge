using System;

using Microsoft.Xna.Framework;

using RuneForge.Core.Controllers;
using RuneForge.Core.Interface.Controls;
using RuneForge.Game.Orders.Implementations;

namespace RuneForge.Core.Interface.Windows
{
    partial class EntityDetailsWindow
    {
        private const int c_windowWidth = 256;
        private const int c_totalFramesWidth = 14;

        private PictureBox m_entityIconPictureBox;
        private Label m_entityNameLabel;
        private Label m_entityOwnerNameLabel;

        private Label m_entityStatusTextLabel;

        private Button m_unitMoveButton;
        private Button m_unitAttackButton;
        private Button m_unitGatherResourcesButton;
        private Button m_unitRepairButton;
        private Button m_unitBuildBarracksButton;
        private Button m_unitBuildFarmButton;
        private Button m_unitClearOrderQueueButton;

        private Button m_buildingTrainPeasantButton;
        private Button m_buildingTrainFootmanButton;
        private Button m_buildingClearOrderQueueButton;

        private void CreateLayout()
        {
            Width = c_windowWidth;
            Height = 512;

            Title = "Entity Details";
            CanBeMoved = false;
            CanBeClosed = false;

            m_entityIconPictureBox = new PictureBox(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = 0,
                Width = 64,
                Height = 64,

                ImageAlignment = Alignment.TopLeft,
            };
            m_requiredControls.Add(m_entityIconPictureBox);

            m_entityNameLabel = new Label(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = m_entityIconPictureBox.X + m_entityIconPictureBox.Width + 4,
                Y = 0,
                Width = c_windowWidth - c_totalFramesWidth - (m_entityIconPictureBox.X + m_entityIconPictureBox.Width) - 4,
                Height = 16,

                TextAlignment = Alignment.TopLeft,
                TextColor = Color.White,
                ShadowColor = Color.Transparent,
            };
            m_requiredControls.Add(m_entityNameLabel);

            m_entityOwnerNameLabel = new Label(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = m_entityIconPictureBox.X + m_entityIconPictureBox.Width + 4,
                Y = m_entityNameLabel.Y + m_entityNameLabel.Height,
                Width = c_windowWidth - c_totalFramesWidth - (m_entityIconPictureBox.X + m_entityIconPictureBox.Width) - 4,
                Height = 16,

                TextAlignment = Alignment.TopLeft,
                TextColor = Color.White,
                ShadowColor = Color.Transparent,
            };
            m_requiredControls.Add(m_entityOwnerNameLabel);

            m_entityStatusTextLabel = new Label(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = (m_entityIconPictureBox.X + m_entityIconPictureBox.Height) + 4,
                Width = c_windowWidth - c_totalFramesWidth,
                Height = 96,

                TextAlignment = Alignment.TopLeft,
                TextColor = Color.White,
                ShadowColor = Color.Transparent,
            };
            m_requiredControls.Add(m_entityStatusTextLabel);

            // Unit-specific buttons
            m_unitMoveButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_entityStatusTextLabel.Y + m_entityStatusTextLabel.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Move",
            };
            m_unitMoveButton.Clicked += (sender, e) => OnTargetBasedOrderScheduled(new TargetBasedOrderScheduledEventArgs(typeof(MoveOrder)));
            m_unitSpecificControls.Add(m_unitMoveButton);

            m_unitAttackButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_unitMoveButton.Y + m_unitMoveButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Attack",
            };
            m_unitAttackButton.Clicked += (sender, e) => OnTargetBasedOrderScheduled(new TargetBasedOrderScheduledEventArgs(typeof(AttackOrder)));
            m_unitSpecificControls.Add(m_unitAttackButton);

            m_unitGatherResourcesButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_unitAttackButton.Y + m_unitAttackButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Gather Resources",
            };
            m_unitGatherResourcesButton.Clicked += (sender, e) => OnTargetBasedOrderScheduled(new TargetBasedOrderScheduledEventArgs(typeof(GatherResourcesOrder)));
            m_unitSpecificControls.Add(m_unitGatherResourcesButton);

            m_unitRepairButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_unitGatherResourcesButton.Y + m_unitGatherResourcesButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Repair",
            };
            m_unitSpecificControls.Add(m_unitRepairButton);

            m_unitBuildBarracksButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_unitRepairButton.Y + m_unitRepairButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Build Barracks",
            };
            m_unitSpecificControls.Add(m_unitBuildBarracksButton);

            m_unitBuildFarmButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_unitBuildBarracksButton.Y + m_unitBuildBarracksButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Build Farm",
            };
            m_unitSpecificControls.Add(m_unitBuildFarmButton);

            m_unitClearOrderQueueButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_unitBuildFarmButton.Y + m_unitBuildFarmButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Clear Order Queue",
            };
            m_unitClearOrderQueueButton.Clicked += (sender, e) => OnOrderQueueClearingScheduled(EventArgs.Empty);
            m_unitSpecificControls.Add(m_unitClearOrderQueueButton);

            // Building-specific buttons
            m_buildingTrainPeasantButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_entityStatusTextLabel.Y + m_entityStatusTextLabel.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Train Peasant",
            };
            m_buildingSpecificControls.Add(m_buildingTrainPeasantButton);

            m_buildingTrainFootmanButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_buildingTrainPeasantButton.Y + m_buildingTrainPeasantButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Train Footman",
            };
            m_buildingSpecificControls.Add(m_buildingTrainFootmanButton);

            m_buildingClearOrderQueueButton = new Button(null, ContentManager, GraphicsDevice, SpriteBatch)
            {
                X = 0,
                Y = m_buildingTrainFootmanButton.Y + m_buildingTrainFootmanButton.Height + 4,
                Width = c_windowWidth - 7 * 2,
                Height = 32,

                Text = "Clear Order Queue",
            };
            m_buildingClearOrderQueueButton.Clicked += (sender, e) => OnOrderQueueClearingScheduled(EventArgs.Empty);
            m_buildingSpecificControls.Add(m_buildingClearOrderQueueButton);
        }
    }
}
