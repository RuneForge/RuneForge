using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Entities;
using RuneForge.Game.Entities.Extensions;

namespace RuneForge.Game.Units
{
    public class UnitPrototypeReader : ContentTypeReader<UnitPrototype>
    {
        protected override UnitPrototype Read(ContentReader reader, UnitPrototype existingInstance)
        {
            string name = reader.ReadString();

            int componentPrototypesCount = reader.ReadInt32();
            List<ComponentPrototype> componentPrototypes = new List<ComponentPrototype>();
            for (int i = 0; i < componentPrototypesCount; i++)
            {
                ComponentPrototype componentPrototype = reader.ReadComponentPrototype();
                componentPrototypes.Add(componentPrototype);
            }

            return new UnitPrototype(name, componentPrototypes);
        }
    }
}
