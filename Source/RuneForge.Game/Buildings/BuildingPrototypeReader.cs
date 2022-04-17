﻿using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;

using RuneForge.Game.Entities;
using RuneForge.Game.Entities.Extensions;

namespace RuneForge.Game.Buildings
{
    public class BuildingPrototypeReader : ContentTypeReader<BuildingPrototype>
    {
        protected override BuildingPrototype Read(ContentReader reader, BuildingPrototype existingInstance)
        {
            string name = reader.ReadString();

            int componentPrototypesCount = reader.ReadInt32();
            List<ComponentPrototype> componentPrototypes = new List<ComponentPrototype>();
            for (int i = 0; i < componentPrototypesCount; i++)
            {
                ComponentPrototype componentPrototype = reader.ReadComponentPrototype();
                componentPrototypes.Add(componentPrototype);
            }

            return new BuildingPrototype(name, componentPrototypes);
        }
    }
}
