using System;

using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Rendering.Interfaces;

namespace RuneForge.Core.Rendering
{
    public class SpriteBatchProvider : ISpriteBatchProvider, IDisposable
    {
        private SpriteBatch m_worldSpriteBatch;
        private SpriteBatch m_onDisplayInterfaceSpriteBatch;
        private bool m_worldSpriteBatchInitialized;
        private bool m_onDisplayInterfaceSpriteBatchInitialized;
        private bool m_disposed;

        public SpriteBatchProvider()
        {
            m_worldSpriteBatch = null;
            m_onDisplayInterfaceSpriteBatch = null;
            m_worldSpriteBatchInitialized = false;
            m_onDisplayInterfaceSpriteBatchInitialized = false;
            m_disposed = false;
        }

        public SpriteBatch WorldSpriteBatch
        {
            get
            {
                if (m_worldSpriteBatchInitialized)
                    return m_worldSpriteBatch;
                else
                    throw new InvalidOperationException("Unable to use the world-rendering sprite batch before it is initialized.");
            }
            set
            {
                if (m_worldSpriteBatchInitialized)
                    throw new InvalidOperationException("Unable to change the world-rendering sprite batch  once it has been initialized.");
                else if (value == null)
                    throw new InvalidOperationException("Unable to initialize the world-rendering sprite batch with a 'null' value.");
                else
                {
                    m_worldSpriteBatch = value;
                    m_worldSpriteBatchInitialized = true;
                }
            }
        }

        public SpriteBatch OnDisplayInterfaceSpriteBatch
        {
            get
            {
                if (m_onDisplayInterfaceSpriteBatchInitialized)
                    return m_onDisplayInterfaceSpriteBatch;
                else
                    throw new InvalidOperationException("Unable to use the HUD-rendering sprite batch before it is initialized.");
            }
            set
            {
                if (m_onDisplayInterfaceSpriteBatchInitialized)
                    throw new InvalidOperationException("Unable to change the HUD-rendering sprite batch  once it has been initialized.");
                else if (value == null)
                    throw new InvalidOperationException("Unable to initialize the HUD-rendering sprite batch with a 'null' value.");
                else
                {
                    m_onDisplayInterfaceSpriteBatch = value;
                    m_onDisplayInterfaceSpriteBatchInitialized = true;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                m_disposed = true;
                if (disposing)
                {
                    WorldSpriteBatch?.Dispose();
                    OnDisplayInterfaceSpriteBatch?.Dispose();
                }
            }
        }
    }
}
