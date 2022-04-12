namespace RuneForge.Game.Maps.Interfaces
{
    public interface IMapDecorationTypeResolver
    {
        public MapDecorationTypes ResolveMapDecorationType(int x, int y, Map map);

        public bool TryResolveMapDecorationType(int x, int y, Map map, out MapDecorationTypes type);
    }
}
