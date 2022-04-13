namespace RuneForge.Game.Maps.Interfaces
{
    public interface IMapLandscapeCellTypeResolver
    {
        public MapLandscapeCellTypes ResolveLandscapeCellType(int x, int y, Map map);

        public bool TryResolveLandscapeCellType(int x, int y, Map map, out MapLandscapeCellTypes type);
    }
}
