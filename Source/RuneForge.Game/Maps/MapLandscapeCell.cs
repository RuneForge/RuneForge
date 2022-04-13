namespace RuneForge.Game.Maps
{
    public readonly struct MapLandscapeCell
    {
        public MapLandscapeCellTier Tier { get; }
        public MapLandscapeCellTypes Type { get; }

        public MapLandscapeCell(MapLandscapeCellTier tier, MapLandscapeCellTypes type)
        {
            Tier = tier;
            Type = type;
        }
    }
}
