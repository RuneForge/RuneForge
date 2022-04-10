namespace RuneForge.Game.Maps.Interfaces
{
    public interface IMapDecorationTypeResolver
    {
        public MapDecorationType ResolveMapDecorationType(int x, int y, Map map);

        public bool TryResolveMapDecorationType(int x, int y, Map map, out MapDecorationType type);
    }
}
