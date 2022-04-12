namespace RuneForge.Game.Maps
{
    public struct MapDecoration
    {
        public MapDecorationTier Tier { get; }
        public MapDecorationTypes Type { get; }

        public MapDecoration(MapDecorationTier tier, MapDecorationTypes type)
        {
            Tier = tier;
            Type = type;
        }
    }
}
