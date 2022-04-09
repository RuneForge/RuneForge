namespace RuneForge.Game.Maps.Interfaces
{
    public interface IMapCellTypeResolver
    {
        public MapCellType ResolveMapCellType(int x, int y, Map map);

        public bool TryResolveMapCellType(int x, int y, Map map, out MapCellType type);
    }
}
