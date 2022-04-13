namespace RuneForge.Game.Maps.Interfaces
{
    public interface IMapDecorationCellTypeResolver
    {
        public MapDecorationCellTypes ResolveDecorationCellType(int x, int y, Map map);

        public bool TryResolveDecorationCellType(int x, int y, Map map, out MapDecorationCellTypes type);
    }
}
