namespace RuneForge.Game.Maps
{
    public class MapTilesetLandscapeCellPrototype
    {
        public MapLandscapeCellMovementFlags MovementFlags { get; }
        public MapLandscapeCellBuildingFlags BuildingFlags { get; }

        public string TextureRegionName { get; }

        public MapTilesetLandscapeCellPrototype(MapLandscapeCellMovementFlags movementFlags, MapLandscapeCellBuildingFlags buildingFlags, string textureRegionName)
        {
            MovementFlags = movementFlags;
            BuildingFlags = buildingFlags;
            TextureRegionName = textureRegionName;
        }
    }
}
