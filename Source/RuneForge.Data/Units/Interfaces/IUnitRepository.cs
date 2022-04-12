using System.Collections.ObjectModel;

namespace RuneForge.Data.Units.Interfaces
{
    public interface IUnitRepository
    {
        public UnitDto GetUnit(int unitId);

        public ReadOnlyCollection<UnitDto> GetUnits();

        public void AddUnit(UnitDto unit);

        public void SaveUnit(UnitDto unit);

        public void RemoveUnit(int unitId);
    }
}
