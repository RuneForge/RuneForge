using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RuneForge.Data.Units;

namespace RuneForge.Data.Tests.Units
{
    [TestClass]
    public class InMemoryUnitRepositoryTests
    {
        private const int c_invalidUnitId = 0;
        private const int c_validUnitId = 1;

        [TestMethod]
        public void AddUnit_NullUnitId_ArgumentExceptionThrown()
        {
            InMemoryUnitRepository unitRepository = new InMemoryUnitRepository();

            void Action() => unitRepository.AddUnit(new UnitDto() { Id = c_invalidUnitId });

            Assert.ThrowsException<ArgumentException>(Action);
        }

        [TestMethod]
        public void AddUnit_ExistingUnitId_InvalidOperationExceptionThrown()
        {
            InMemoryUnitRepository unitRepository = new InMemoryUnitRepository();

            unitRepository.AddUnit(new UnitDto() { Id = c_validUnitId });
            void Action() => unitRepository.AddUnit(new UnitDto() { Id = c_validUnitId });

            Assert.ThrowsException<InvalidOperationException>(Action);
        }

        [TestMethod]
        public void AddUnit_NormalUnit_UnitAdded()
        {
            InMemoryUnitRepository unitRepository = new InMemoryUnitRepository();

            UnitDto unit = new UnitDto() { Id = c_validUnitId };
            unitRepository.AddUnit(unit);

            Assert.AreSame(unit, unitRepository.GetUnit(unit.Id));
        }

        [TestMethod]
        public void SaveUnit_NonExistingUnitId_KeyNotFoundExceptionThrown()
        {
            InMemoryUnitRepository unitRepository = new InMemoryUnitRepository();

            void Action() => unitRepository.SaveUnit(new UnitDto() { Id = c_validUnitId });

            Assert.ThrowsException<KeyNotFoundException>(Action);
        }

        [TestMethod]
        public void SaveUnit_SameUnit_NoAction()
        {
            InMemoryUnitRepository unitRepository = new InMemoryUnitRepository();

            UnitDto unit = new UnitDto() { Id = c_validUnitId };
            unitRepository.AddUnit(unit);
            unitRepository.SaveUnit(unit);

            Assert.AreSame(unit, unitRepository.GetUnit(unit.Id));
        }

        [TestMethod]
        public void SaveUnit_UnitWithEqualId_UnitSaved()
        {
            InMemoryUnitRepository unitRepository = new InMemoryUnitRepository();

            UnitDto initialUnit = new UnitDto() { Id = c_validUnitId };
            UnitDto finalUnit = new UnitDto() { Id = c_validUnitId };
            unitRepository.AddUnit(initialUnit);
            unitRepository.SaveUnit(finalUnit);

            Assert.AreSame(finalUnit, unitRepository.GetUnit(c_validUnitId));
        }

        [TestMethod]
        public void RemoveUnit_NonExistingUnitId_KeyNotFoundExceptionThrown()
        {
            InMemoryUnitRepository unitRepository = new InMemoryUnitRepository();

            void Action() => unitRepository.RemoveUnit(c_validUnitId);

            Assert.ThrowsException<KeyNotFoundException>(Action);
        }

        [TestMethod]
        public void RemoveUnit_ExistingUnitId_UnitRemoved()
        {
            InMemoryUnitRepository unitRepository = new InMemoryUnitRepository();

            UnitDto unit = new UnitDto() { Id = c_validUnitId };
            unitRepository.AddUnit(unit);
            unitRepository.RemoveUnit(c_validUnitId);

            Assert.IsFalse(unitRepository.GetUnits().Any(unit => unit.Id == c_validUnitId));
        }
    }
}
