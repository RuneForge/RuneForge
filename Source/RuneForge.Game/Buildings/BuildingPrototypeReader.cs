using Microsoft.Xna.Framework.Content;

namespace RuneForge.Game.Buildings
{
    public class BuildingPrototypeReader : ContentTypeReader<BuildingPrototype>
    {
        protected override BuildingPrototype Read(ContentReader reader, BuildingPrototype existingInstance)
        {
            string name = reader.ReadString();

            return new BuildingPrototype(name);
        }
    }
}
