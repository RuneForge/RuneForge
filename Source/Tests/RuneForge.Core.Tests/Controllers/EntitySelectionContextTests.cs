using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RuneForge.Core.Controllers;
using RuneForge.Game.Entities;

namespace RuneForge.Core.Tests.Controllers
{
    [TestClass]
    public class EntitySelectionContextTests
    {
        [TestMethod]
        public void Entity_NullReplacedWithObject_EntitySelectedInvoked()
        {
            bool entitySelectedInvoked = false;
            void SubscribeAction(EntitySelectionContext context) => context.EntitySelected += (sender, e) => entitySelectedInvoked = true;
            void AssertAction(EntitySelectionContext context) => Assert.AreEqual(true, entitySelectedInvoked);
            TestEntitySelectionContextEntitySetter(null, new TestEntity(), SubscribeAction, AssertAction);
        }

        [TestMethod]
        public void Entity_NullReplacedWithObject_EntitySelectionDroppedNotInvoked()
        {
            bool entitySelectionDroppedInvoked = false;
            void SubscribeAction(EntitySelectionContext context) => context.EntitySelectionDropped += (sender, e) => entitySelectionDroppedInvoked = true;
            void AssertAction(EntitySelectionContext context) => Assert.AreEqual(false, entitySelectionDroppedInvoked);
            TestEntitySelectionContextEntitySetter(null, new TestEntity(), SubscribeAction, AssertAction);
        }

        [TestMethod]
        public void Entity_ObjectReplacedWithObject_EntitySelectedInvoked()
        {
            bool entitySelectedInvoked = false;
            void SubscribeAction(EntitySelectionContext context) => context.EntitySelected += (sender, e) => entitySelectedInvoked = true;
            void AssertAction(EntitySelectionContext context) => Assert.AreEqual(true, entitySelectedInvoked);
            TestEntitySelectionContextEntitySetter(new TestEntity(), new TestEntity(), SubscribeAction, AssertAction);
        }

        [TestMethod]
        public void Entity_ObjectReplacedWithObject_EntitySelectionDroppedInvoked()
        {
            bool entitySelectionDroppedInvoked = false;
            void SubscribeAction(EntitySelectionContext context) => context.EntitySelectionDropped += (sender, e) => entitySelectionDroppedInvoked = true;
            void AssertAction(EntitySelectionContext context) => Assert.AreEqual(true, entitySelectionDroppedInvoked);
            TestEntitySelectionContextEntitySetter(new TestEntity(), new TestEntity(), SubscribeAction, AssertAction);
        }

        [TestMethod]
        public void Entity_ObjectReplacedWithNull_EntitySelectedNotInvoked()
        {
            bool entitySelectedInvoked = false;
            void SubscribeAction(EntitySelectionContext context) => context.EntitySelected += (sender, e) => entitySelectedInvoked = true;
            void AssertAction(EntitySelectionContext context) => Assert.AreEqual(false, entitySelectedInvoked);
            TestEntitySelectionContextEntitySetter(new TestEntity(), null, SubscribeAction, AssertAction);
        }

        [TestMethod]
        public void Entity_ObjectReplacedWithNull_EntitySelectionDroppedInvoked()
        {
            bool entitySelectionDroppedInvoked = false;
            void SubscribeAction(EntitySelectionContext context) => context.EntitySelectionDropped += (sender, e) => entitySelectionDroppedInvoked = true;
            void AssertAction(EntitySelectionContext context) => Assert.AreEqual(true, entitySelectionDroppedInvoked);
            TestEntitySelectionContextEntitySetter(new TestEntity(), null, SubscribeAction, AssertAction);
        }

        [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "The Entity property is not set in the initializer to follow the Arrange-Act-Assert pattern.")]
        private static void TestEntitySelectionContextEntitySetter(Entity initialEntity, Entity finalEntity, Action<EntitySelectionContext> subscribeAction, Action<EntitySelectionContext> assertAction)
        {
            EntitySelectionContext entitySelectionContext = new EntitySelectionContext();

            entitySelectionContext.Entity = initialEntity;
            subscribeAction(entitySelectionContext);
            entitySelectionContext.Entity = finalEntity;
            assertAction(entitySelectionContext);
        }

        private class TestEntity : Entity
        {
        }
    }
}
