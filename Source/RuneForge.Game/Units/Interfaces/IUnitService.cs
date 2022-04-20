using System.Collections.ObjectModel;

namespace RuneForge.Game.Units.Interfaces
{
    public interface IUnitService
    {
        public Unit GetUnit(int unitId);

        public ReadOnlyCollection<Unit> GetUnits();

        public void AddUnit(Unit unit);

        public void RemoveUnit(int unitId);

        public void RegisterUnitChanges(int unitId);

        public void CommitChanges();
    }
}
