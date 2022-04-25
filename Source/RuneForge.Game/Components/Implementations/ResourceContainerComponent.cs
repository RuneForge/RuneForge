using System;

using RuneForge.Game.Components.Entities;

namespace RuneForge.Game.Components.Implementations
{
    public class ResourceContainerComponent : Component
    {
        public decimal GoldAmount { get; set; }

        public decimal GetResourceAmount(ResourceTypes resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypes.Gold:
                    return GoldAmount;
                default:
                    throw new InvalidOperationException("Unknown resource type.");
            }
        }

        public void AddResource(ResourceTypes resourceType, decimal amount)
        {
            ModifyResourceAmount(resourceType, +amount);
        }
        public void WithdrawResource(ResourceTypes resourceType, decimal amount)
        {
            ModifyResourceAmount(resourceType, -amount);
        }

        private void ModifyResourceAmount(ResourceTypes resourceType, decimal amount)
        {
            switch (resourceType)
            {
                case ResourceTypes.Gold:
                    GoldAmount += amount;
                    break;
                default:
                    throw new InvalidOperationException("Unknown resource type.");
            }
        }
    }
}
