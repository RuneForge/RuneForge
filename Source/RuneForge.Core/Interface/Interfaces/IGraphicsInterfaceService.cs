using System.Collections.ObjectModel;

using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Interface.Controls;

namespace RuneForge.Core.Interface.Interfaces
{
    public interface IGraphicsInterfaceService
    {
        Viewport Viewport { get; set; }

        ReadOnlyCollection<Control> GetControls();

        ReadOnlyCollection<Control> GetControlsByDrawOrder();

        void RegisterControl(Control control);

        void UnregisterControl(Control control);
    }
}
