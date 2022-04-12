namespace RuneForge.Game.Maps
{
    public class MapTilesetDecorationPrototype
    {
        public MapDecorationMovementFlags MovementFlags { get; }
        public MapDecorationBuildingFlags BuildingFlags { get; }

        public string TextureRegionName { get; }

        public MapTilesetDecorationPrototype(MapDecorationMovementFlags movementFlags, MapDecorationBuildingFlags buildingFlags, string textureRegionName)
        {
            MovementFlags = movementFlags;
            BuildingFlags = buildingFlags;
            TextureRegionName = textureRegionName;
        }
    }
}
