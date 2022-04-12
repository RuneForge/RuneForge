namespace RuneForge.Game.Maps
{
    public class MapTilesetCellPrototype
    {
        public MapCellMovementFlags MovementFlags { get; }
        public MapCellBuildingFlags BuildingFlags { get; }

        public string TextureRegionName { get; }

        public MapTilesetCellPrototype(MapCellMovementFlags movementFlags, MapCellBuildingFlags buildingFlags, string textureRegionName)
        {
            MovementFlags = movementFlags;
            BuildingFlags = buildingFlags;
            TextureRegionName = textureRegionName;
        }
    }
}
