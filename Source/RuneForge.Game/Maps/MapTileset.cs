using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RuneForge.Game.Maps
{
    public class MapTileset
    {
        public string TextureAtlasName { get; }

        public ReadOnlyDictionary<(MapLandscapeCellTier, MapLandscapeCellTypes), MapTilesetLandscapeCellPrototype> LandscapeCellPrototypes { get; }
        public ReadOnlyDictionary<(MapDecorationCellTier, MapDecorationCellTypes), MapTilesetDecorationCellPrototype> DecorationCellPrototypes { get; }

        public MapTileset(
            string textureAtlasName,
            IDictionary<(MapLandscapeCellTier, MapLandscapeCellTypes), MapTilesetLandscapeCellPrototype> landscapeCellPrototypes,
            IDictionary<(MapDecorationCellTier, MapDecorationCellTypes), MapTilesetDecorationCellPrototype> decorationCellPrototypes
            )
        {
            TextureAtlasName = textureAtlasName;
            LandscapeCellPrototypes = new ReadOnlyDictionary<(MapLandscapeCellTier, MapLandscapeCellTypes), MapTilesetLandscapeCellPrototype>(landscapeCellPrototypes);
            DecorationCellPrototypes = new ReadOnlyDictionary<(MapDecorationCellTier, MapDecorationCellTypes), MapTilesetDecorationCellPrototype>(decorationCellPrototypes);
        }

        public MapTilesetLandscapeCellPrototype GetLandscapeCellPrototype(MapLandscapeCellTier tier, MapLandscapeCellTypes type)
        {
            return LandscapeCellPrototypes[(tier, type)];
        }
        public MapTilesetDecorationCellPrototype GetDecorationCellPrototype(MapDecorationCellTier tier, MapDecorationCellTypes type)
        {
            return DecorationCellPrototypes[(tier, type)];
        }

        public bool TryGetLandscapeCellPrototype(MapLandscapeCellTier tier, MapLandscapeCellTypes type, out MapTilesetLandscapeCellPrototype cellPrototype)
        {
            return LandscapeCellPrototypes.TryGetValue((tier, type), out cellPrototype);
        }
        public bool TryGetDecorationCellPrototype(MapDecorationCellTier tier, MapDecorationCellTypes type, out MapTilesetDecorationCellPrototype decorationPrototype)
        {
            return DecorationCellPrototypes.TryGetValue((tier, type), out decorationPrototype);
        }
    }
}
