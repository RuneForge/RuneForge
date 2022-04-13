namespace RuneForge.Game.Maps
{
    public readonly struct MapDecorationCell
    {
        public MapDecorationCellTier Tier { get; }
        public MapDecorationCellTypes Type { get; }

        public MapDecoration Decoration { get; }

        public MapDecorationCell(MapDecorationCellTier tier, MapDecorationCellTypes type)
            : this(tier, type, null)
        {
        }
        public MapDecorationCell(MapDecorationCellTier tier, MapDecorationCellTypes type, MapDecoration decoration)
        {
            Tier = tier;
            Type = type;
            Decoration = decoration;
        }
    }
}
