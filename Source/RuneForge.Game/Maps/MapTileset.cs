using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RuneForge.Game.Maps
{
    public class MapTileset
    {
        public string TextureAtlasName { get; }

        public ReadOnlyDictionary<(MapCellTier, MapCellType), MapTilesetCellPrototype> CellPrototypes { get; }
        public ReadOnlyDictionary<(MapDecorationTier, MapDecorationType), MapTilesetDecorationPrototype> DecorationPrototypes { get; }

        public MapTileset(
            string textureAtlasName,
            IDictionary<(MapCellTier, MapCellType), MapTilesetCellPrototype> cellPrototypes,
            IDictionary<(MapDecorationTier, MapDecorationType), MapTilesetDecorationPrototype> decorationPrototypes
            )
        {
            TextureAtlasName = textureAtlasName;
            CellPrototypes = new ReadOnlyDictionary<(MapCellTier, MapCellType), MapTilesetCellPrototype>(cellPrototypes);
            DecorationPrototypes = new ReadOnlyDictionary<(MapDecorationTier, MapDecorationType), MapTilesetDecorationPrototype>(decorationPrototypes);
        }

        public MapTilesetCellPrototype GetCellPrototype(MapCellTier tier, MapCellType type)
        {
            return CellPrototypes[(tier, type)];
        }
        public MapTilesetDecorationPrototype GetDecorationPrototype(MapDecorationTier tier, MapDecorationType type)
        {
            return DecorationPrototypes[(tier, type)];
        }

        public bool TryGetCellPrototype(MapCellTier tier, MapCellType type, out MapTilesetCellPrototype cellPrototype)
        {
            return CellPrototypes.TryGetValue((tier, type), out cellPrototype);
        }
        public bool TryGetDecorationPrototype(MapDecorationTier tier, MapDecorationType type, out MapTilesetDecorationPrototype decorationPrototype)
        {
            return DecorationPrototypes.TryGetValue((tier, type), out decorationPrototype);
        }
    }
}
