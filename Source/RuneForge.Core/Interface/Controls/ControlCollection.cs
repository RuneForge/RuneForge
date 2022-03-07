using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace RuneForge.Core.Interface.Controls
{
    public class ControlCollection : ObservableCollection<Control>
    {
        private static readonly ReadOnlyCollection<Control> s_emptyCollection = new ReadOnlyCollection<Control>(Array.Empty<Control>());

        private bool m_cacheInvalidated;

        private ReadOnlyCollection<Control> m_controlsByDrawOrder;

        public event EventHandler<ControlCollectionChangedEventArgs> ControlsAdded;

        public event EventHandler<ControlCollectionChangedEventArgs> ControlsRemoved;

        public ControlCollection()
        {
            RebuildCache();
        }

        public ControlCollection(IEnumerable<Control> collection)
            : base(collection)
        {
            RebuildCache();
        }

        public ControlCollection(List<Control> list)
            : base(list)
        {
            RebuildCache();
        }

        public ReadOnlyCollection<Control> GetControlsByDrawOrder()
        {
            if (m_cacheInvalidated)
            {
                return RebuildCache();
            }
            return m_controlsByDrawOrder;
        }

        protected virtual ReadOnlyCollection<Control> RebuildCache()
        {
            m_cacheInvalidated = false;
            m_controlsByDrawOrder = new ReadOnlyCollection<Control>((from control in this
                                                                     where control.Visible
                                                                     orderby control.DrawOrder
                                                                     select control).ToList());
            return m_controlsByDrawOrder;
        }

        protected virtual void OnControlsAdded(ControlCollectionChangedEventArgs e)
        {
            ControlsAdded?.Invoke(this, e);
        }

        protected virtual void OnControlsRemoved(ControlCollectionChangedEventArgs e)
        {
            ControlsRemoved?.Invoke(this, e);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            InvalidateCache();
            base.OnCollectionChanged(e);
        }

        protected override void InsertItem(int index, Control control)
        {
            SubscribeToControlEvents(control);
            base.InsertItem(index, control);
            OnControlsAdded(CreateEventArgsForAddedItem(control));
        }

        protected override void RemoveItem(int index)
        {
            Control control = base[index];
            UnsubscribeFromControlEvents(control);
            base.RemoveItem(index);
            OnControlsRemoved(CreateEventArgsForRemovedItem(control));
        }

        protected override void SetItem(int index, Control control)
        {
            Control control2 = base[index];
            SubscribeToControlEvents(control);
            UnsubscribeFromControlEvents(control2);
            base.SetItem(index, control);
            OnControlsAdded(CreateEventArgsForAddedItem(control));
            OnControlsRemoved(CreateEventArgsForRemovedItem(control2));
        }

        protected override void ClearItems()
        {
            Control[] array = this.ToArray();
            Control[] array2 = array;
            foreach (Control control in array2)
            {
                UnsubscribeFromControlEvents(control);
            }
            base.ClearItems();
            OnControlsRemoved(new ControlCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, s_emptyCollection, array));
        }

        private void InvalidateCache()
        {
            m_cacheInvalidated = true;
        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            InvalidateCache();
        }

        private void OnDrawOrderChanged(object sender, EventArgs e)
        {
            InvalidateCache();
        }

        private void SubscribeToControlEvents(Control control)
        {
            control.VisibleChanged += OnVisibleChanged;
            control.DrawOrderChanged += OnDrawOrderChanged;
        }

        private void UnsubscribeFromControlEvents(Control control)
        {
            control.VisibleChanged -= OnVisibleChanged;
            control.DrawOrderChanged -= OnDrawOrderChanged;
        }

        private ControlCollectionChangedEventArgs CreateEventArgsForAddedItem(Control control)
        {
            return new ControlCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<Control>(1) { control }, s_emptyCollection);
        }

        private ControlCollectionChangedEventArgs CreateEventArgsForRemovedItem(Control control)
        {
            return new ControlCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, s_emptyCollection, new List<Control>(1) { control });
        }
    }
}
