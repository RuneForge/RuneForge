using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Entities.Components;
using RuneForge.Game.Extensions;

namespace RuneForge.Game.Entities.ComponentReaders
{
    public class LocationComponentPrototypeReader : ComponentPrototypeReader<LocationComponentPrototype>
    {
        public override LocationComponentPrototype ReadTypedComponentPrototype(ContentReader contentReader)
        {
            int? xCells = contentReader.ReadNullable(contentReader => contentReader.ReadInt32());
            int? yCells = contentReader.ReadNullable(contentReader => contentReader.ReadInt32());
            int? widthCells = contentReader.ReadNullable(contentReader => contentReader.ReadInt32());
            int? heightCells = contentReader.ReadNullable(contentReader => contentReader.ReadInt32());
            return new LocationComponentPrototype(xCells, yCells, widthCells, heightCells);
        }
    }
}
