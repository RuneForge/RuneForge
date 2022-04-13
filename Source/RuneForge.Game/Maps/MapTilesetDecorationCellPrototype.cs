namespace RuneForge.Game.Maps
{
    public class MapTilesetDecorationCellPrototype
    {
        public MapDecorationCellMovementFlags MovementFlags { get; }
        public MapDecorationCellBuildingFlags BuildingFlags { get; }

        public string TextureRegionName { get; }

        public MapTilesetDecorationCellPrototype(MapDecorationCellMovementFlags movementFlags, MapDecorationCellBuildingFlags buildingFlags, string textureRegionName)
        {
            MovementFlags = movementFlags;
            BuildingFlags = buildingFlags;
            TextureRegionName = textureRegionName;
        }
    }
}
