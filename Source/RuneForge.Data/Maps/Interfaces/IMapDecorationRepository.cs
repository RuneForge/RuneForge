using System.Collections.ObjectModel;

namespace RuneForge.Data.Maps.Interfaces
{
    public interface IMapDecorationRepository
    {
        public MapDecorationDto GetMapDecoration(int mapDecorationId);

        public ReadOnlyCollection<MapDecorationDto> GetMapDecorations();

        public void AddMapDecoration(MapDecorationDto mapDecoration);

        public void SaveMapDecoration(MapDecorationDto mapDecoration);

        public void RemoveMapDecoration(int mapDecorationId);
    }
}
