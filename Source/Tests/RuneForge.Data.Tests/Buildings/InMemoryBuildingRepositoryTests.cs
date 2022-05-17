using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RuneForge.Data.Buildings;

namespace RuneForge.Data.Tests.Buildings
{
    [TestClass]
    public class InMemoryBuildingRepositoryTests
    {
        private const int c_invalidBuildingId = 0;
        private const int c_validBuildingId = 1;

        [TestMethod]
        public void AddBuilding_NullBuildingId_ArgumentExceptionThrown()
        {
            InMemoryBuildingRepository buildingRepository = new InMemoryBuildingRepository();

            void Action() => buildingRepository.AddBuilding(new BuildingDto() { Id = c_invalidBuildingId });

            Assert.ThrowsException<ArgumentException>(Action);
        }

        [TestMethod]
        public void AddBuilding_ExistingBuildingId_InvalidOperationExceptionThrown()
        {
            InMemoryBuildingRepository buildingRepository = new InMemoryBuildingRepository();

            buildingRepository.AddBuilding(new BuildingDto() { Id = c_validBuildingId });
            void Action() => buildingRepository.AddBuilding(new BuildingDto() { Id = c_validBuildingId });

            Assert.ThrowsException<InvalidOperationException>(Action);
        }

        [TestMethod]
        public void AddBuilding_NormalBuilding_BuildingAdded()
        {
            InMemoryBuildingRepository buildingRepository = new InMemoryBuildingRepository();

            BuildingDto building = new BuildingDto() { Id = c_validBuildingId };
            buildingRepository.AddBuilding(building);

            Assert.AreSame(building, buildingRepository.GetBuilding(building.Id));
        }

        [TestMethod]
        public void SaveBuilding_NonExistingBuildingId_KeyNotFoundExceptionThrown()
        {
            InMemoryBuildingRepository buildingRepository = new InMemoryBuildingRepository();

            void Action() => buildingRepository.SaveBuilding(new BuildingDto() { Id = c_validBuildingId });

            Assert.ThrowsException<KeyNotFoundException>(Action);
        }

        [TestMethod]
        public void SaveBuilding_SameBuilding_NoAction()
        {
            InMemoryBuildingRepository buildingRepository = new InMemoryBuildingRepository();

            BuildingDto building = new BuildingDto() { Id = c_validBuildingId };
            buildingRepository.AddBuilding(building);
            buildingRepository.SaveBuilding(building);

            Assert.AreSame(building, buildingRepository.GetBuilding(building.Id));
        }

        [TestMethod]
        public void SaveBuilding_BuildingWithEqualId_BuildingSaved()
        {
            InMemoryBuildingRepository buildingRepository = new InMemoryBuildingRepository();

            BuildingDto initialBuilding = new BuildingDto() { Id = c_validBuildingId };
            BuildingDto finalBuilding = new BuildingDto() { Id = c_validBuildingId };
            buildingRepository.AddBuilding(initialBuilding);
            buildingRepository.SaveBuilding(finalBuilding);

            Assert.AreSame(finalBuilding, buildingRepository.GetBuilding(c_validBuildingId));
        }

        [TestMethod]
        public void RemoveBuilding_NonExistingBuildingId_KeyNotFoundExceptionThrown()
        {
            InMemoryBuildingRepository buildingRepository = new InMemoryBuildingRepository();

            void Action() => buildingRepository.RemoveBuilding(c_validBuildingId);

            Assert.ThrowsException<KeyNotFoundException>(Action);
        }

        [TestMethod]
        public void RemoveBuilding_ExistingBuildingId_BuildingRemoved()
        {
            InMemoryBuildingRepository buildingRepository = new InMemoryBuildingRepository();

            BuildingDto building = new BuildingDto() { Id = c_validBuildingId };
            buildingRepository.AddBuilding(building);
            buildingRepository.RemoveBuilding(c_validBuildingId);

            Assert.IsFalse(buildingRepository.GetBuildings().Any(building => building.Id == c_validBuildingId));
        }
    }
}
