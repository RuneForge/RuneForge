using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Entities.Components;

namespace RuneForge.Game.Entities.ComponentReaders
{
    public class LocationComponentPrototypeReader : ComponentPrototypeReader<LocationComponentPrototype>
    {
        public override LocationComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            int xCells = contentReader.ReadInt32();
            int yCells = contentReader.ReadInt32();
            int widthCells = contentReader.ReadInt32();
            int heightCells = contentReader.ReadInt32();
            return new LocationComponentPrototype(xCells, yCells, widthCells, heightCells);
        }
    }
}
