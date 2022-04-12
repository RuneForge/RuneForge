namespace RuneForge.Game.Maps
{
    public readonly struct MapCell
    {
        public MapCellTier Tier { get; }
        public MapCellTypes Type { get; }

        public MapCell(MapCellTier tier, MapCellTypes type)
        {
            Tier = tier;
            Type = type;
        }
    }
}
