using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Controllers;
using RuneForge.Core.Controllers.Interfaces;
using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Controls.ImageSources;
using RuneForge.Core.Interface.Interfaces;
using RuneForge.Core.TextureAtlases;
using RuneForge.Game.Buildings;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Units;

namespace RuneForge.Core.Interface.Windows
{
    public partial class EntityDetailsWindow : Window
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IEntitySelectionContext m_entitySelectionContext;
        private readonly ISpriteFontProvider m_spriteFontProvider;
        private readonly List<Control> m_requiredControls;
        private readonly List<Control> m_unitSpecificControls;
        private readonly List<Control> m_buildingSpecificControls;
        private readonly StringBuilder m_stringBuilder;
        private Entity m_entity;
        private int m_previousStatusDetailsHash;

        public EntityDetailsWindow(
            ControlEventSource eventSource,
            ContentManager contentManager,
            GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch,
            IGameSessionContext gameSessionContext,
            IEntitySelectionContext entitySelectionContext,
            ISpriteFontProvider spriteFontProvider
            )
            : base(eventSource, contentManager, graphicsDevice, spriteBatch, DefaultEnabledValue, DefaultVisibleValue, DefaultDrawOrder)
        {
            m_gameSessionContext = gameSessionContext;
            m_entitySelectionContext = entitySelectionContext;
            m_spriteFontProvider = spriteFontProvider;
            m_requiredControls = new List<Control>();
            m_unitSpecificControls = new List<Control>();
            m_buildingSpecificControls = new List<Control>();
            m_stringBuilder = new StringBuilder();
            m_entity = null;
            m_previousStatusDetailsHash = 0;
            CreateLayout();
        }

        public Entity Entity
        {
            get => m_entity;
            set
            {
                if (m_entity != value)
                {
                    m_entity = value;
                    RefreshLayoutForEntity();
                }
            }
        }

        public event EventHandler<InstantOrderScheduledEventArgs> InstantOrderScheduled;
        public event EventHandler<TargetBasedOrderScheduledEventArgs> TargetBasedOrderScheduled;
        public event EventHandler<EventArgs> OrderQueueClearingScheduled;

        public override void LoadContent()
        {
            SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);

            foreach (Control control in m_requiredControls.Concat(m_unitSpecificControls).Concat(m_buildingSpecificControls))
            {
                control.LoadContent();
                switch (control)
                {
                    case Label label:
                        label.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
                        break;
                    case Button button:
                        button.SpriteFont = m_spriteFontProvider.GetSpriteFont("Kingthings-Petrock", 12);
                        break;
                }
            }

            base.LoadContent();
        }

        public void UpdateEntityDetails()
        {
            if (Entity == null)
                return;

            if ((Entity.TryGetComponentOfType(out HealthComponent healthComponent) && healthComponent.Health == 0)
                || (Entity.TryGetComponentOfType(out DurabilityComponent durabilityComponent) && durabilityComponent.Durability == 0))
            {
                m_entitySelectionContext.Entity = null;
                return;
            }

            int currentStatusDetailsHash = CalculateStatusDetailsHash();
            if (m_previousStatusDetailsHash != currentStatusDetailsHash)
            {
                FillStatusDetails();
                m_previousStatusDetailsHash = currentStatusDetailsHash;
            }
        }

        protected virtual void OnInstantOrderScheduled(InstantOrderScheduledEventArgs e)
        {
            InstantOrderScheduled?.Invoke(this, e);
        }
        protected virtual void OnTargetBasedOrderScheduled(TargetBasedOrderScheduledEventArgs e)
        {
            TargetBasedOrderScheduled?.Invoke(this, e);
        }
        protected virtual void OnOrderQueueClearingScheduled(EventArgs e)
        {
            OrderQueueClearingScheduled?.Invoke(this, e);
        }

        private int CalculateStatusDetailsHash()
        {
            if (m_entity == null)
                return 0;

            int hash = 0;
            if (m_entity.TryGetComponentOfType(out HealthComponent healthComponent))
                hash = HashCode.Combine(hash, healthComponent.Health, healthComponent.MaxHealth);
            if (m_entity.TryGetComponentOfType(out DurabilityComponent durabilityComponent))
                hash = HashCode.Combine(hash, durabilityComponent.Durability, durabilityComponent.MaxDurability);

            if (m_entity.TryGetComponentOfType(out MovementComponent movementComponent))
                hash = HashCode.Combine(hash, movementComponent.MovementType, movementComponent.MovementSpeed);

            if (m_entity.TryGetComponentOfType(out MeleeCombatComponent meleeCombatComponent))
                hash = HashCode.Combine(hash, meleeCombatComponent.AttackPower, meleeCombatComponent.CycleTime);

            if (m_entity.TryGetComponentOfType(out ResourceContainerComponent resourceContainerComponent))
                hash = HashCode.Combine(hash, resourceContainerComponent.GoldAmount);

            return hash;
        }
        private void FillStatusDetails()
        {
            m_stringBuilder.Clear();
            if (m_entity.TryGetComponentOfType(out HealthComponent healthComponent))
            {
                m_stringBuilder.AppendFormat("Health: {0} / {1}", healthComponent.Health, healthComponent.MaxHealth);
                m_stringBuilder.AppendLine();
            }
            if (m_entity.TryGetComponentOfType(out DurabilityComponent durabilityComponent))
            {
                m_stringBuilder.AppendFormat("Durability: {0} / {1}", durabilityComponent.Durability, durabilityComponent.MaxDurability);
                m_stringBuilder.AppendLine();
            }

            if (m_entity.TryGetComponentOfType(out MovementComponent movementComponent))
            {
                m_stringBuilder.AppendFormat("Movement Speed: {0:0.##}", movementComponent.MovementSpeed);
                m_stringBuilder.AppendLine();
            }

            if (m_entity.TryGetComponentOfType(out MeleeCombatComponent meleeCombatComponent))
            {
                m_stringBuilder.AppendFormat("Attack Power: {0} ({1:0.##} sec)", meleeCombatComponent.AttackPower, meleeCombatComponent.CycleTime.TotalSeconds);
                m_stringBuilder.AppendLine();
            }

            if (m_entity.TryGetComponentOfType(out ResourceContainerComponent resourceContainerComponent))
            {
                m_stringBuilder.AppendFormat("Gold Amount: {0}", resourceContainerComponent.GoldAmount);
                m_stringBuilder.AppendLine();
            }

            m_stringBuilder.Remove(m_stringBuilder.Length - Environment.NewLine.Length, Environment.NewLine.Length);
            m_entityStatusTextLabel.Text = m_stringBuilder.ToString();
            RefreshLayoutForEntity();
        }

        private void RefreshLayoutForEntity()
        {
            // Add required controls.
            Controls.Clear();
            foreach (Control control in m_requiredControls)
                Controls.Add(control);

            if (m_entity == null)
                return;

            // Set icon if possible.
            if (m_entity.TryGetComponentOfType(out TextureAtlasComponent textureAtlasComponent))
            {
                TextureAtlas textureAtlas = ContentManager.Load<TextureAtlas>(textureAtlasComponent.TextureAtlasAssetName);
                TextureRegion2D textureRegion = textureAtlas.TextureRegions["Icon"];
                m_entityIconPictureBox.ImageSource = new TextureRegion2DImageSource(textureRegion);
            }
            else
                m_entityIconPictureBox.ImageSource = new EmptyImageSource();

            // Fill out the required fields.
            switch (m_entity)
            {
                case Unit unit:
                    m_entityNameLabel.Text = unit.Name;
                    m_entityOwnerNameLabel.Text = unit.Owner.Name;
                    break;
                case Building building:
                    m_entityNameLabel.Text = building.Name;
                    m_entityOwnerNameLabel.Text = building.Owner.Name;
                    break;
            }

            // Figure out available actions.
            List<Control> additionalControls = new List<Control>();
            switch (m_entity)
            {
                case Unit unit:
                    additionalControls = m_unitSpecificControls;
                    break;
                case Building building:
                    additionalControls = m_buildingSpecificControls;
                    break;
            }

            foreach (Control control in additionalControls)
                control.Enabled = false;

            switch (m_entity)
            {
                case Unit unit:
                    if (unit.Owner.Id != m_gameSessionContext.Map.HumanPlayerId)
                        break;
                    m_unitMoveButton.Enabled = unit.HasComponentOfType<MovementComponent>();
                    m_unitAttackButton.Enabled = unit.HasComponentOfType<MeleeCombatComponent>();
                    m_unitGatherResourcesButton.Enabled = unit.HasComponentOfType<ResourceContainerComponent>() && unit.HasComponentOfType<UnitShelterOccupantComponent>();
                    m_unitClearOrderQueueButton.Enabled = unit.HasComponentOfType<OrderQueueComponent>();
                    break;
                case Building building:
                    if (building.Owner.Id != m_gameSessionContext.Map.HumanPlayerId)
                        break;
                    m_buildingClearOrderQueueButton.Enabled = building.HasComponentOfType<OrderQueueComponent>();
                    break;
            }

            m_entityStatusTextLabel.ResizeToFitText(false, true);
            Control previousControl = m_entityStatusTextLabel;
            foreach (Control control in additionalControls.Where(control => control.Enabled))
            {
                control.Y = previousControl.Y + previousControl.Height + 4;
                previousControl = control;
                Controls.Add(control);
            }
            Height = previousControl.Y + previousControl.Height + 37;
        }
    }
}
