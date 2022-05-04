using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using RuneForge.Content.Pipeline.Game.Components;
using RuneForge.Content.Pipeline.Game.Components.Extensions;
using RuneForge.Content.Pipeline.Game.Extensions;

namespace RuneForge.Content.Pipeline.Game.Units
{
    [ContentTypeWriter]
    public class UnitPrototypeWriter : ContentTypeWriter<UnitPrototype>
    {
        private const string c_runtimeReaderTypeName = "RuneForge.Game.Units.UnitPrototypeReader, RuneForge.Game";

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return c_runtimeReaderTypeName;
        }

        protected override void Write(ContentWriter writer, UnitPrototype unitPrototype)
        {
            writer.Write(unitPrototype.Name);
            writer.Write(unitPrototype.Code);

            List<ComponentPrototype> componentPrototypes = unitPrototype.ComponentPrototypes;
            if (componentPrototypes == null)
                throw new InvalidOperationException("The unit prototype should have at least an empty list of component prototypes.");
            else
            {
                writer.Write(componentPrototypes.Count);
                foreach (ComponentPrototype componentPrototype in componentPrototypes)
                    writer.Write(componentPrototype);
            }
        }
    }
}
