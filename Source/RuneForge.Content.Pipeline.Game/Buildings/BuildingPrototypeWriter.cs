using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components;
using RuneForge.Content.Pipeline.Game.Components.Extensions;
using RuneForge.Content.Pipeline.Game.Extensions;

namespace RuneForge.Content.Pipeline.Game.Buildings
{
    [ContentTypeWriter]
    public class BuildingPrototypeWriter : ContentTypeWriter<BuildingPrototype>
    {
        private const string c_runtimeReaderTypeName = "RuneForge.Game.Buildings.BuildingPrototypeReader, RuneForge.Game";

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return c_runtimeReaderTypeName;
        }

        protected override void Write(ContentWriter writer, BuildingPrototype buildingPrototype)
        {
            writer.Write(buildingPrototype.Name);
            writer.Write(buildingPrototype.Code);

            List<ComponentPrototype> componentPrototypes = buildingPrototype.ComponentPrototypes;
            if (componentPrototypes == null)
                throw new InvalidOperationException("The building prototype should have at least an empty list of component prototypes.");
            else
            {
                writer.Write(componentPrototypes.Count);
                foreach (ComponentPrototype componentPrototype in componentPrototypes)
                    writer.Write(componentPrototype);
            }
        }
    }
}
