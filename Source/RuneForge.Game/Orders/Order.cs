using Microsoft.Xna.Framework;

using RuneForge.Game.Entities;

namespace RuneForge.Game.Orders
{
    public abstract class Order
    {
        public Entity Entity { get; }

        public OrderState State { get; protected set; }

        protected Order(Entity entity)
        {
            Entity = entity;
            State = OrderState.Scheduled;
        }

        public virtual void Execute()
        {
            State = OrderState.InProgress;
        }

        public virtual void Complete()
        {
            State = OrderState.Completed;
        }

        public virtual void Cancel()
        {
            State = OrderState.Cancelled;
        }

        public virtual void Update(GameTime gameTime, out bool stateChanged)
        {
            stateChanged = false;
        }
    }
}
