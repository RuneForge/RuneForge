using System;
using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Game.Buildings;
using RuneForge.Game.Components.Entities;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.Players;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units;

namespace RuneForge.Game.Orders.Implementations
{
    public class ProduceUnitOrder : Order
    {
        private readonly IPlayerService m_playerService;

        public UnitPrototype UnitPrototype { get; }

        public ProduceUnitOrder(Entity entity, UnitPrototype unitPrototype, IPlayerService playerService)
            : this(entity, unitPrototype, OrderState.Scheduled, playerService)
        {
        }
        public ProduceUnitOrder(Entity entity, UnitPrototype unitPrototype, OrderState orderState, IPlayerService playerService)
            : base(entity)
        {
            m_playerService = playerService;
            State = orderState;
            UnitPrototype = unitPrototype;
        }

        public override void Update(GameTime gameTime, out bool stateChanged)
        {
            stateChanged = false;
            if (State == OrderState.InProgress)
            {
                ProductionFacilityComponent productionFacilityComponent = Entity.GetComponentOfType<ProductionFacilityComponent>();
                if (productionFacilityComponent.ProductionFinished)
                {
                    Complete();
                    productionFacilityComponent.UnitCurrentlyProduced = null;
                    productionFacilityComponent.TimeElapsed = TimeSpan.Zero;
                    productionFacilityComponent.ProductionFinished = false;
                    stateChanged = true;
                }
            }
            base.Update(gameTime, out bool stateChangedInternal);
            stateChanged |= stateChangedInternal;
        }

        public override void Execute()
        {
            if (State != OrderState.Scheduled)
                return;

            ProductionFacilityComponent productionFacilityComponent = Entity.GetComponentOfType<ProductionFacilityComponent>();
            if (productionFacilityComponent.UnitCurrentlyProduced == null && !productionFacilityComponent.ProductionFinished)
            {
                Player owner = ((Building)Entity).Owner;
                ResourceContainerComponent ownerResourceContainerComponent = owner.GetComponentOfType<ResourceContainerComponent>();
                ProductionCostComponentPrototype productionCostComponentPrototype = UnitPrototype.ComponentPrototypes
                    .OfType<ProductionCostComponentPrototype>()
                    .Single();

                if (ownerResourceContainerComponent.GetResourceAmount(ResourceTypes.Gold) >= productionCostComponentPrototype.GoldAmount)
                {
                    productionFacilityComponent.UnitCurrentlyProduced = UnitPrototype;
                    productionFacilityComponent.TimeElapsed = TimeSpan.Zero;
                    productionFacilityComponent.ProductionFinished = false;

                    ownerResourceContainerComponent.WithdrawResource(ResourceTypes.Gold, productionCostComponentPrototype.GoldAmount);
                    m_playerService.RegisterPlayerChanges(owner.Id);

                    base.Execute();
                }
                else
                    Cancel();
            }
        }

        public override void Complete()
        {
            if (State == OrderState.Completed || State == OrderState.Cancelled)
                return;

            base.Complete();
        }

        public override void Cancel()
        {
            if (State == OrderState.Completed || State == OrderState.Cancelled)
                return;

            if (State == OrderState.InProgress)
            {
                Player owner = ((Building)Entity).Owner;
                ProductionFacilityComponent productionFacilityComponent = Entity.GetComponentOfType<ProductionFacilityComponent>();

                productionFacilityComponent.UnitCurrentlyProduced = null;
                productionFacilityComponent.TimeElapsed = TimeSpan.Zero;
                productionFacilityComponent.ProductionFinished = false;

                ResourceContainerComponent ownerResourceContainerComponent = owner.GetComponentOfType<ResourceContainerComponent>();
                ProductionCostComponentPrototype productionCostComponentPrototype = UnitPrototype.ComponentPrototypes
                    .OfType<ProductionCostComponentPrototype>()
                    .Single();

                ownerResourceContainerComponent.AddResource(ResourceTypes.Gold, productionCostComponentPrototype.GoldAmount);
                m_playerService.RegisterPlayerChanges(owner.Id);
            }

            base.Cancel();
        }
    }
}
