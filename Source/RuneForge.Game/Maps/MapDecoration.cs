namespace RuneForge.Game.Maps
{
    public struct MapDecoration
    {
        public MapDecorationTier Tier { get; }
        public MapDecorationType Type { get; }

        public MapDecoration(MapDecorationTier tier, MapDecorationType type)
        {
            Tier = tier;
            Type = type;
        }
    }
}
