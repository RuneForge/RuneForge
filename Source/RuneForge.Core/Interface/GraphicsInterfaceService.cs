using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Input;
using RuneForge.Core.Input.EventProviders.Interfaces;
using RuneForge.Core.Interface.Controls;
using RuneForge.Core.Interface.Controls.Helpers;
using RuneForge.Core.Interface.Interfaces;

namespace RuneForge.Core.Interface
{
    public class GraphicsInterfaceService : IGraphicsInterfaceService, IDisposable
    {
        private delegate EventHandler<TEventArgs> ControlEventMethodSelector<TEventArgs>(Control control, ControlEventSource eventSource);

        private readonly IKeyboardEventProvider m_keyboardEventProvider;

        private readonly IMouseEventProvider m_mouseEventProvider;

        private readonly ControlCollection m_controls;

        private readonly HashSet<Control> m_uniqueControls;

        private readonly List<Control> m_controlsToAdd;

        private readonly List<Control> m_controlsToRemove;

        private ReadOnlyCollection<Control> m_controlsCache;

        private bool m_processingInputEvents;

        private bool m_cacheInvalidated;

        private bool m_disposed;

        public Viewport Viewport { get; set; }

        public GraphicsInterfaceService(IKeyboardEventProvider keyboardEventProvider, IMouseEventProvider mouseEventProvider)
        {
            m_keyboardEventProvider = keyboardEventProvider;
            m_mouseEventProvider = mouseEventProvider;
            m_controls = new ControlCollection();
            m_controls.ControlsAdded += delegate (object sender, ControlCollectionChangedEventArgs e)
            {
                InvalidateCache();
                foreach (Control addedControl in e.AddedControls)
                {
                    m_uniqueControls.Add(addedControl);
                }
            };
            m_controls.ControlsRemoved += delegate (object sender, ControlCollectionChangedEventArgs e)
            {
                InvalidateCache();
                foreach (Control removedControl in e.RemovedControls)
                {
                    m_uniqueControls.Remove(removedControl);
                }
            };
            m_uniqueControls = new HashSet<Control>();
            m_controlsToAdd = new List<Control>();
            m_controlsToRemove = new List<Control>();
            SubscribeToKeyboardEventProvider();
            SubscribeToMouseEventProvider();
        }

        public ReadOnlyCollection<Control> GetControls()
        {
            return (!m_cacheInvalidated) ? m_controlsCache : RebuildCache();
        }

        public ReadOnlyCollection<Control> GetControlsByDrawOrder()
        {
            return m_controls.GetControlsByDrawOrder();
        }

        public void RegisterControl(Control control)
        {
            if (!m_uniqueControls.Contains(control))
            {
                if (!m_processingInputEvents)
                {
                    m_controls.Add(control);
                }
                else
                {
                    m_controlsToAdd.Add(control);
                }
            }
        }

        public void UnregisterControl(Control control)
        {
            if (!m_processingInputEvents)
            {
                m_controls.Remove(control);
            }
            else
            {
                m_controlsToRemove.Add(control);
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void OnKeyDown(object sender, KeyboardEventArgs e)
        {
            InvokeControlEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleKeyDown, sender, e);
        }

        protected virtual void OnKeyPressed(object sender, KeyboardEventArgs e)
        {
            InvokeControlEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleKeyPressed, sender, e);
        }

        protected virtual void OnKeyReleased(object sender, KeyboardEventArgs e)
        {
            InvokeControlEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleKeyReleased, sender, e);
        }

        protected virtual void OnTextTyped(object sender, KeyboardEventArgs e)
        {
            InvokeControlEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleTextTyped, sender, e);
        }

        protected virtual void OnMouseMoved(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseMoved, sender, e);
        }

        protected virtual void OnMouseButtonDown(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseButtonDown, sender, e);
        }

        protected virtual void OnMouseButtonPressed(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseButtonPressed, sender, e);
        }

        protected virtual void OnMouseButtonReleased(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseButtonReleased, sender, e);
        }

        protected virtual void OnMouseButtonClicked(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseButtonClicked, sender, e);
        }

        protected virtual void OnMouseButtonDoubleClicked(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseButtonDoubleClicked, sender, e);
        }

        protected virtual void OnMouseScrollWheelMoved(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseScrollWheelMoved, sender, e);
        }

        protected virtual void OnMouseHorizontalScrollWheelMoved(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseHorizontalScrollWheelMoved, sender, e);
        }

        protected virtual void OnMouseDragStarted(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseDragStarted, sender, e);
        }

        protected virtual void OnMouseDragFinished(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseDragFinished, sender, e);
        }

        protected virtual void OnMouseDragMoved(object sender, MouseEventArgs e)
        {
            InvokeControlMouseEventMethod((Control control, ControlEventSource eventSource) => eventSource.HandleMouseDragMoved, sender, e);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                m_disposed = true;
                if (disposing)
                {
                    UnsubscribeFromKeyboardEventProvider();
                    UnsubscribeFromMouseEventProvider();
                }
            }
        }

        private void InvalidateCache()
        {
            m_cacheInvalidated = true;
        }

        private ReadOnlyCollection<Control> RebuildCache()
        {
            m_cacheInvalidated = false;
            m_controlsCache = new ReadOnlyCollection<Control>(m_controls);
            return m_controlsCache;
        }

        private void SubscribeToKeyboardEventProvider()
        {
            m_keyboardEventProvider.KeyDown += OnKeyDown;
            m_keyboardEventProvider.KeyPressed += OnKeyPressed;
            m_keyboardEventProvider.KeyReleased += OnKeyReleased;
            m_keyboardEventProvider.TextTyped += OnTextTyped;
        }

        private void SubscribeToMouseEventProvider()
        {
            m_mouseEventProvider.MouseMoved += OnMouseMoved;
            m_mouseEventProvider.MouseButtonDown += OnMouseButtonDown;
            m_mouseEventProvider.MouseButtonPressed += OnMouseButtonPressed;
            m_mouseEventProvider.MouseButtonReleased += OnMouseButtonReleased;
            m_mouseEventProvider.MouseButtonClicked += OnMouseButtonClicked;
            m_mouseEventProvider.MouseButtonDoubleClicked += OnMouseButtonDoubleClicked;
            m_mouseEventProvider.MouseScrollWheelMoved += OnMouseScrollWheelMoved;
            m_mouseEventProvider.MouseHorizontalScrollWheelMoved += OnMouseHorizontalScrollWheelMoved;
            m_mouseEventProvider.MouseDragStarted += OnMouseDragStarted;
            m_mouseEventProvider.MouseDragFinished += OnMouseDragFinished;
            m_mouseEventProvider.MouseDragMoved += OnMouseDragMoved;
        }

        private void UnsubscribeFromKeyboardEventProvider()
        {
            m_keyboardEventProvider.KeyDown -= OnKeyDown;
            m_keyboardEventProvider.KeyPressed -= OnKeyPressed;
            m_keyboardEventProvider.KeyReleased -= OnKeyReleased;
            m_keyboardEventProvider.TextTyped -= OnTextTyped;
        }

        private void UnsubscribeFromMouseEventProvider()
        {
            m_mouseEventProvider.MouseMoved -= OnMouseMoved;
            m_mouseEventProvider.MouseButtonDown -= OnMouseButtonDown;
            m_mouseEventProvider.MouseButtonPressed -= OnMouseButtonPressed;
            m_mouseEventProvider.MouseButtonReleased -= OnMouseButtonReleased;
            m_mouseEventProvider.MouseButtonClicked -= OnMouseButtonClicked;
            m_mouseEventProvider.MouseButtonDoubleClicked -= OnMouseButtonDoubleClicked;
            m_mouseEventProvider.MouseScrollWheelMoved -= OnMouseScrollWheelMoved;
            m_mouseEventProvider.MouseHorizontalScrollWheelMoved -= OnMouseHorizontalScrollWheelMoved;
            m_mouseEventProvider.MouseDragStarted -= OnMouseDragStarted;
            m_mouseEventProvider.MouseDragFinished -= OnMouseDragFinished;
            m_mouseEventProvider.MouseDragMoved -= OnMouseDragMoved;
        }

        private void InvokeControlEventMethod<TEventArgs>(ControlEventMethodSelector<TEventArgs> methodSelector, object sender, TEventArgs e)
        {
            m_processingInputEvents = true;
            foreach (Control control in m_controls)
            {
                if (control.EventSource != null)
                {
                    EventHandler<TEventArgs> eventHandler = methodSelector(control, control.EventSource);
                    eventHandler(sender, e);
                }
            }
            m_processingInputEvents = false;
            foreach (Control item in m_controlsToAdd)
            {
                m_controls.Add(item);
            }
            m_controlsToAdd.Clear();
            foreach (Control item2 in m_controlsToRemove)
            {
                m_controls.Remove(item2);
            }
            m_controlsToRemove.Clear();
        }

        private void InvokeControlMouseEventMethod(ControlEventMethodSelector<MouseEventArgs> methodSelector, object sender, MouseEventArgs e)
        {
            Viewport viewport = e.Viewport;
            Viewport currentViewport = Viewport;
            InvokeControlEventMethod(MethodSelector, sender, e);
            e.SetViewport(in viewport);
            EventHandler<MouseEventArgs> MethodSelector(Control control, ControlEventSource eventSource)
            {
                Viewport viewport2 = GraphicsControlGeometryHelpers.CreateChildViewport(in currentViewport, control.Bounds);
                e.SetViewport(in viewport2);
                return methodSelector(control, eventSource);
            }
        }
    }
}
