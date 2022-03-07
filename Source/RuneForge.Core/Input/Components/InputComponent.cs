using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using RuneForge.Core.Input.EventProviders.Interfaces;

namespace RuneForge.Core.Input.Components
{
    public class InputComponent : GameComponent
    {
        private readonly IUpdateable[] m_eventProviders;

        public InputComponent(IEnumerable<IKeyboardEventProvider> keyboardEventProviders, IEnumerable<IMouseEventProvider> mouseEventProviders)
        {
            m_eventProviders = (from eventProvider in keyboardEventProviders.Cast<IUpdateable>().Concat(mouseEventProviders.Cast<IUpdateable>())
                                orderby eventProvider.UpdateOrder
                                select eventProvider).ToArray();
        }

        public override void Update(GameTime gameTime)
        {
            IUpdateable[] eventProviders = m_eventProviders;
            foreach (IUpdateable val in eventProviders)
            {
                val.Update(gameTime);
            }
            base.Update(gameTime);
        }
    }
}
