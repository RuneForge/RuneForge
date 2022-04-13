using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RuneForge.Game.Maps
{
    public class MapTileset
    {
        public string TextureAtlasName { get; }

        public ReadOnlyDictionary<(MapLandscapeCellTier, MapLandscapeCellTypes), MapTilesetLandscapeCellPrototype> LandscapeCellPrototypes { get; }
        public ReadOnlyDictionary<(MapDecorationTier, MapDecorationTypes), MapTilesetDecorationPrototype> DecorationPrototypes { get; }

        public MapTileset(
            string textureAtlasName,
            IDictionary<(MapLandscapeCellTier, MapLandscapeCellTypes), MapTilesetLandscapeCellPrototype> landscapeCellPrototypes,
            IDictionary<(MapDecorationTier, MapDecorationTypes), MapTilesetDecorationPrototype> decorationPrototypes
            )
        {
            TextureAtlasName = textureAtlasName;
            LandscapeCellPrototypes = new ReadOnlyDictionary<(MapLandscapeCellTier, MapLandscapeCellTypes), MapTilesetLandscapeCellPrototype>(landscapeCellPrototypes);
            DecorationPrototypes = new ReadOnlyDictionary<(MapDecorationTier, MapDecorationTypes), MapTilesetDecorationPrototype>(decorationPrototypes);
        }

        public MapTilesetLandscapeCellPrototype GetLandscapeCellPrototype(MapLandscapeCellTier tier, MapLandscapeCellTypes type)
        {
            return LandscapeCellPrototypes[(tier, type)];
        }
        public MapTilesetDecorationPrototype GetDecorationPrototype(MapDecorationTier tier, MapDecorationTypes type)
        {
            return DecorationPrototypes[(tier, type)];
        }

        public bool TryGetLandscapeCellPrototype(MapLandscapeCellTier tier, MapLandscapeCellTypes type, out MapTilesetLandscapeCellPrototype cellPrototype)
        {
            return LandscapeCellPrototypes.TryGetValue((tier, type), out cellPrototype);
        }
        public bool TryGetDecorationPrototype(MapDecorationTier tier, MapDecorationTypes type, out MapTilesetDecorationPrototype decorationPrototype)
        {
            return DecorationPrototypes.TryGetValue((tier, type), out decorationPrototype);
        }
    }
}
