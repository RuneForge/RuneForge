using System;

using Microsoft.Xna.Framework;

namespace RuneForge.Core.GameStates
{
    public abstract class GameState : IUpdateable, IDrawable, IDisposable
    {
        private bool m_disposed;

        public bool Enabled { get; } = true;
        public int UpdateOrder { get; } = 0;

        public bool Visible { get; } = true;
        public int DrawOrder { get; } = 0;

        protected GameState()
        {
            m_disposed = false;
        }

        public event EventHandler<EventArgs> EnabledChanged
        {
            add => throw new NotSupportedException();
            remove => throw new NotSupportedException();
        }
        public event EventHandler<EventArgs> UpdateOrderChanged
        {
            add => throw new NotSupportedException();
            remove => throw new NotSupportedException();
        }

        public event EventHandler<EventArgs> VisibleChanged
        {
            add => throw new NotSupportedException();
            remove => throw new NotSupportedException();
        }
        public event EventHandler<EventArgs> DrawOrderChanged
        {
            add => throw new NotSupportedException();
            remove => throw new NotSupportedException();
        }

        public event EventHandler<EventArgs> Disposed;

        public virtual void Run() { }
        public virtual void Stop() { }

        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }

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
                OnDisposed(EventArgs.Empty);
            }
        }

        protected virtual void OnDisposed(EventArgs e)
        {
            Disposed?.Invoke(this, e);
        }
    }
}
