namespace RuneForge.Game.Maps
{
    public class MapTilesetDecorationCellPrototype
    {
        public MapDecorationCellMovementFlags MovementFlags { get; }
        public MapDecorationCellBuildingFlags BuildingFlags { get; }

        public MapDecorationPrototype EntityPrototype { get; }

        public string TextureRegionName { get; }

        public MapTilesetDecorationCellPrototype(MapDecorationCellMovementFlags movementFlags, MapDecorationCellBuildingFlags buildingFlags, MapDecorationPrototype entityPrototype, string textureRegionName)
        {
            MovementFlags = movementFlags;
            BuildingFlags = buildingFlags;
            EntityPrototype = entityPrototype;
            TextureRegionName = textureRegionName;
        }
    }
}
