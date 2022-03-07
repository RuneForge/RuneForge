using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Interfaces;

namespace RuneForge.Core.Interface
{
    public class SpriteFontProvider : ISpriteFontProvider
    {
        private static readonly Dictionary<string, string> s_spriteFontNamesByTypes = new Dictionary<string, string>();

        private readonly Dictionary<string, SpriteFont> m_spriteFontsByParameters;

        private readonly Dictionary<string, SpriteFont> m_spriteFontsByTypes;

        private readonly Lazy<ContentManager> m_lazyContentManager;

        public SpriteFontProvider(Lazy<ContentManager> lazyContentManager)
        {
            m_spriteFontsByParameters = new Dictionary<string, SpriteFont>();
            m_spriteFontsByTypes = new Dictionary<string, SpriteFont>();
            m_lazyContentManager = lazyContentManager;
        }

        public SpriteFont GetSpriteFont(string family, int size)
        {
            return GetSpriteFont(family, size, string.Empty);
        }

        public SpriteFont GetSpriteFont(string family, int size, string style)
        {
            string spriteFontAssetName = GetSpriteFontAssetName(family, size, style);
            if (m_spriteFontsByParameters.TryGetValue(spriteFontAssetName, out SpriteFont value))
            {
                return value;
            }
            if (TryLoadSpriteFont(spriteFontAssetName, out value))
            {
                m_spriteFontsByParameters[spriteFontAssetName] = value;
                return value;
            }
            throw new InvalidOperationException("Unable to load a sprite font by parameters.");
        }

        public SpriteFont GetSpriteFontByType(string type)
        {
            if (m_spriteFontsByTypes.TryGetValue(type, out SpriteFont value))
            {
                return value;
            }
            if (s_spriteFontNamesByTypes.TryGetValue(type, out string value2))
            {
                if (TryLoadSpriteFont(value2, out value))
                {
                    m_spriteFontsByTypes[type] = value;
                    return value;
                }
                throw new InvalidOperationException("Unable to load a sprite font by parameters.");
            }
            throw new InvalidOperationException("Unable to load a sprite font by type as such type is not registered.");
        }

        private string GetSpriteFontAssetName(string family, int size, string style)
        {
            string text = $"{family}-{size}";
            if (!string.IsNullOrEmpty(style))
            {
                text = text + "-" + style;
            }
            return text;
        }

        private bool TryLoadSpriteFont(string assetName, out SpriteFont spriteFont)
        {
            try
            {
                ContentManager value = m_lazyContentManager.Value;
                spriteFont = value.Load<SpriteFont>(Path.Combine("Fonts", assetName));
                return true;
            }
            catch (ContentLoadException)
            {
                spriteFont = null;
                return false;
            }
        }
    }
}
