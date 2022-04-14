using Microsoft.Xna.Framework.Content;

namespace RuneForge.Game.Units
{
    public class UnitPrototypeReader : ContentTypeReader<UnitPrototype>
    {
        protected override UnitPrototype Read(ContentReader reader, UnitPrototype existingInstance)
        {
            string name = reader.ReadString();

            return new UnitPrototype(name);
        }
    }
}
