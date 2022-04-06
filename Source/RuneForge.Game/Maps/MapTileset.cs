using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RuneForge.Game.Maps
{
    public class MapTileset
    {
        public string TextureAtlasName { get; }

        public ReadOnlyDictionary<(MapCellTier, MapCellType), MapTilesetCellPrototype> CellPrototypes { get; }

        public MapTileset(string textureAtlasName, IDictionary<(MapCellTier, MapCellType), MapTilesetCellPrototype> cellPrototypes)
        {
            TextureAtlasName = textureAtlasName;
            CellPrototypes = new ReadOnlyDictionary<(MapCellTier, MapCellType), MapTilesetCellPrototype>(cellPrototypes);
        }

        public MapTilesetCellPrototype GetCellPrototype(MapCellTier tier, MapCellType type)
        {
            return CellPrototypes[(tier, type)];
        }

        public bool TryGetCellPrototype(MapCellTier tier, MapCellType type, out MapTilesetCellPrototype cellPrototype)
        {
            return CellPrototypes.TryGetValue((tier, type), out cellPrototype);
        }
    }
}
