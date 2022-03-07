using Microsoft.Xna.Framework.Graphics;

namespace RuneForge.Core.Interface.Interfaces
{
    public interface ISpriteFontProvider
    {
        SpriteFont GetSpriteFont(string family, int size);

        SpriteFont GetSpriteFont(string family, int size, string style);

        SpriteFont GetSpriteFontByType(string type);
    }
}
