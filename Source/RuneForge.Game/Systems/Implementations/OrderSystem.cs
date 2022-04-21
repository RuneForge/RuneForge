using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Game.Buildings;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Components.Implementations;
using RuneForge.Game.Entities;
using RuneForge.Game.GameSessions.Interfaces;
using RuneForge.Game.Orders;
using RuneForge.Game.Units;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Game.Systems.Implementations
{
    public class OrderSystem : System
    {
        private readonly IGameSessionContext m_gameSessionContext;
        private readonly IUnitService m_unitService;
        private readonly IBuildingService m_buildingService;

        public OrderSystem(IGameSessionContext gameSessionContext, IUnitService unitService, IBuildingService buildingService)
        {
            m_gameSessionContext = gameSessionContext;
            m_unitService = unitService;
            m_buildingService = buildingService;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Entity entity in m_gameSessionContext.Units.Concat<Entity>(m_gameSessionContext.Buildings))
            {
                if (!entity.TryGetComponentOfType(out OrderQueueComponent orderQueueComponent))
                    continue;

                bool changesIntroduced = false;
                if ((orderQueueComponent.CurrentOrder == null
                    || orderQueueComponent.CurrentOrder.State == OrderState.Completed
                    || orderQueueComponent.CurrentOrder.State == OrderState.Cancelled)
                    && orderQueueComponent.PendingOrders.Count > 0)
                {
                    changesIntroduced = true;
                    orderQueueComponent.ExecuteNextOrder();
                }

                if (orderQueueComponent.CurrentOrder != null)
                {
                    Order currentOrder = orderQueueComponent.CurrentOrder;
                    if (currentOrder.State == OrderState.Scheduled)
                    {
                        currentOrder.Execute();
                        changesIntroduced = true;
                    }
                    if (currentOrder.State == OrderState.InProgress)
                    {
                        currentOrder.Update(gameTime, out bool stateChanged);
                        changesIntroduced |= stateChanged;
                    }
                }

                if (changesIntroduced)
                {
                    switch (entity)
                    {
                        case Unit unit:
                            m_unitService.RegisterUnitChanges(unit.Id);
                            break;
                        case Building building:
                            m_buildingService.RegisterBuildingChanges(building.Id);
                            break;
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
