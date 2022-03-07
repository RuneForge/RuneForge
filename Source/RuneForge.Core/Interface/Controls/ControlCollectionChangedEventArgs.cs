using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RuneForge.Core.Interface.Controls
{
    public class ControlCollectionChangedEventArgs : EventArgs
    {
        public NotifyCollectionChangedAction Action { get; }

        public ReadOnlyCollection<Control> AddedControls { get; }

        public ReadOnlyCollection<Control> RemovedControls { get; }

        public ControlCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<Control> addedControls, IList<Control> removedControls)
        {
            Action = action;
            AddedControls = new ReadOnlyCollection<Control>(addedControls);
            RemovedControls = new ReadOnlyCollection<Control>(removedControls);
        }
    }
}
