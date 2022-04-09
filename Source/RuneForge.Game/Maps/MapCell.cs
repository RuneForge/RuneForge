namespace RuneForge.Game.Maps
{
    public readonly struct MapCell
    {
        public MapCellTier Tier { get; }
        public MapCellType Type { get; }

        public MapCell(MapCellTier tier, MapCellType type)
        {
            Tier = tier;
            Type = type;
        }
    }
}
