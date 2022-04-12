using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RuneForge.Game.Maps
{
    public class MapTileset
    {
        public string TextureAtlasName { get; }

        public ReadOnlyDictionary<(MapCellTier, MapCellTypes), MapTilesetCellPrototype> CellPrototypes { get; }
        public ReadOnlyDictionary<(MapDecorationTier, MapDecorationTypes), MapTilesetDecorationPrototype> DecorationPrototypes { get; }

        public MapTileset(
            string textureAtlasName,
            IDictionary<(MapCellTier, MapCellTypes), MapTilesetCellPrototype> cellPrototypes,
            IDictionary<(MapDecorationTier, MapDecorationTypes), MapTilesetDecorationPrototype> decorationPrototypes
            )
        {
            TextureAtlasName = textureAtlasName;
            CellPrototypes = new ReadOnlyDictionary<(MapCellTier, MapCellTypes), MapTilesetCellPrototype>(cellPrototypes);
            DecorationPrototypes = new ReadOnlyDictionary<(MapDecorationTier, MapDecorationTypes), MapTilesetDecorationPrototype>(decorationPrototypes);
        }

        public MapTilesetCellPrototype GetCellPrototype(MapCellTier tier, MapCellTypes type)
        {
            return CellPrototypes[(tier, type)];
        }
        public MapTilesetDecorationPrototype GetDecorationPrototype(MapDecorationTier tier, MapDecorationTypes type)
        {
            return DecorationPrototypes[(tier, type)];
        }

        public bool TryGetCellPrototype(MapCellTier tier, MapCellTypes type, out MapTilesetCellPrototype cellPrototype)
        {
            return CellPrototypes.TryGetValue((tier, type), out cellPrototype);
        }
        public bool TryGetDecorationPrototype(MapDecorationTier tier, MapDecorationTypes type, out MapTilesetDecorationPrototype decorationPrototype)
        {
            return DecorationPrototypes.TryGetValue((tier, type), out decorationPrototype);
        }
    }
}
