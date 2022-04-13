namespace RuneForge.Game.Maps
{
    public readonly struct MapDecorationCell
    {
        public MapDecorationCellTier Tier { get; }
        public MapDecorationCellTypes Type { get; }

        public MapDecorationCell(MapDecorationCellTier tier, MapDecorationCellTypes type)
        {
            Tier = tier;
            Type = type;
        }
    }
}
