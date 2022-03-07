using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RuneForge.Core.Input;
using RuneForge.Core.Interface.Controls.Helpers;

namespace RuneForge.Core.Interface.Controls
{
    public abstract class Control : IDrawable, IDisposable
    {
        private delegate EventHandler<TEventArgs> ControlEventMethodSelector<TEventArgs>(Control control);

        private int m_x;

        private int m_y;

        private int m_width;

        private int m_height;

        private bool m_enabled;

        private bool m_visible;

        private int m_drawOrder;

        private bool m_disposed;

        public static bool DefaultEnabledValue { get; } = true;

        public static bool DefaultVisibleValue { get; } = true;

        public static int DefaultDrawOrder { get; } = 0;

        public ControlEventSource EventSource { get; }

        public ControlCollection Controls { get; }

        protected ControlCollection BuiltInControls { get; }

        public int X
        {
            get
            {
                return m_x;
            }
            set
            {
                if (m_x != value)
                {
                    m_x = value;
                    OnLocationChanged(EventArgs.Empty);
                }
            }
        }

        public int Y
        {
            get
            {
                return m_y;
            }
            set
            {
                if (m_y != value)
                {
                    m_y = value;
                    OnLocationChanged(EventArgs.Empty);
                }
            }
        }

        public int Width
        {
            get
            {
                return m_width;
            }
            set
            {
                if (m_width != value)
                {
                    m_width = value;
                    OnSizeChanged(EventArgs.Empty);
                }
            }
        }

        public int Height
        {
            get
            {
                return m_height;
            }
            set
            {
                if (m_height != value)
                {
                    m_height = value;
                    OnSizeChanged(EventArgs.Empty);
                }
            }
        }

        public Point Location
        {
            get
            {
                //IL_000c: Unknown result type (might be due to invalid IL or missing references)
                return new Point(m_x, m_y);
            }
            set
            {
                //IL_0002: Unknown result type (might be due to invalid IL or missing references)
                //IL_0007: Unknown result type (might be due to invalid IL or missing references)
                //IL_0012: Unknown result type (might be due to invalid IL or missing references)
                //IL_0013: Unknown result type (might be due to invalid IL or missing references)
                if (Location != value)
                {
                    Point val = value;
                    val.Deconstruct(out int x, out int y);
                    m_x = x;
                    m_y = y;
                    OnLocationChanged(EventArgs.Empty);
                }
            }
        }

        public Point Size
        {
            get
            {
                //IL_000c: Unknown result type (might be due to invalid IL or missing references)
                return new Point(m_width, m_height);
            }
            set
            {
                //IL_0002: Unknown result type (might be due to invalid IL or missing references)
                //IL_0007: Unknown result type (might be due to invalid IL or missing references)
                //IL_0012: Unknown result type (might be due to invalid IL or missing references)
                //IL_0013: Unknown result type (might be due to invalid IL or missing references)
                if (Size != value)
                {
                    Point val = value;
                    val.Deconstruct(out int width, out int height);
                    m_width = width;
                    m_height = height;
                    OnSizeChanged(EventArgs.Empty);
                }
            }
        }

        public Rectangle Bounds
        {
            get
            {
                //IL_0018: Unknown result type (might be due to invalid IL or missing references)
                return new Rectangle(m_x, m_y, m_width, m_height);
            }
            set
            {
                //IL_0002: Unknown result type (might be due to invalid IL or missing references)
                //IL_0009: Unknown result type (might be due to invalid IL or missing references)
                //IL_001a: Unknown result type (might be due to invalid IL or missing references)
                //IL_001f: Unknown result type (might be due to invalid IL or missing references)
                //IL_0048: Unknown result type (might be due to invalid IL or missing references)
                //IL_004f: Unknown result type (might be due to invalid IL or missing references)
                //IL_0062: Unknown result type (might be due to invalid IL or missing references)
                //IL_0067: Unknown result type (might be due to invalid IL or missing references)
                Point val;
                int num = default(int);
                int num2 = default(int);
                if (Location != value.Location)
                {
                    val = value.Location;
                    val.Deconstruct(out num, out num2);
                    m_x = num;
                    m_y = num2;
                    OnLocationChanged(EventArgs.Empty);
                }
                if (Size != value.Size)
                {
                    val = value.Size;
                    val.Deconstruct(out num2, out num);
                    m_width = num2;
                    m_height = num;
                    OnSizeChanged(EventArgs.Empty);
                }
            }
        }

        public bool Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                if (m_enabled != value)
                {
                    m_enabled = value;
                    OnEnabledChanged(EventArgs.Empty);
                }
            }
        }

        public bool Visible
        {
            get
            {
                return m_visible;
            }
            set
            {
                if (m_visible != value)
                {
                    m_visible = value;
                    OnVisibleChanged(EventArgs.Empty);
                }
            }
        }

        public int DrawOrder
        {
            get
            {
                return m_drawOrder;
            }
            set
            {
                if (m_drawOrder != value)
                {
                    m_drawOrder = value;
                    OnDrawOrderChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> LocationChanged;

        public event EventHandler<EventArgs> SizeChanged;

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> RenderRequested;

        public event EventHandler<EventArgs> Rendered;

        public Control(ControlEventSource eventSource)
            : this(eventSource, DefaultEnabledValue, DefaultVisibleValue, DefaultDrawOrder)
        {
        }

        public Control(ControlEventSource eventSource, bool enabled, bool visible, int drawOrder)
        {
            m_x = 0;
            m_y = 0;
            m_width = 0;
            m_height = 0;
            m_enabled = enabled;
            m_visible = visible;
            m_drawOrder = drawOrder;
            EventSource = eventSource;
            SubscribeToEventSource();
            Controls = new ControlCollection();
            Controls.ControlsAdded += OnChildControlCollectionChanged;
            Controls.ControlsRemoved += OnChildControlCollectionChanged;
            BuiltInControls = new ControlCollection();
            BuiltInControls.ControlsAdded += OnChildControlCollectionChanged;
            BuiltInControls.ControlsRemoved += OnChildControlCollectionChanged;
        }

        public virtual void LoadContent()
        {
            foreach (Control builtInControl in BuiltInControls)
            {
                builtInControl.LoadContent();
            }
            foreach (Control control in Controls)
            {
                control.LoadContent();
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual Rectangle GetContainerBounds()
        {
            //IL_000f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            return new Rectangle(0, 0, Width, Height);
        }

        protected virtual void InvalidateRenderCache()
        {
            OnRenderRequested(EventArgs.Empty);
        }

        protected virtual void InvalidateParentRenderCache()
        {
            OnRenderRequested(EventArgs.Empty);
        }

        protected virtual void OnLocationChanged(EventArgs e)
        {
            InvalidateParentRenderCache();
            LocationChanged?.Invoke(this, e);
        }

        protected virtual void OnSizeChanged(EventArgs e)
        {
            InvalidateRenderCache();
            SizeChanged?.Invoke(this, e);
        }

        protected virtual void OnEnabledChanged(EventArgs e)
        {
            InvalidateRenderCache();
            EnabledChanged?.Invoke(this, e);
        }

        protected virtual void OnVisibleChanged(EventArgs e)
        {
            InvalidateRenderCache();
            VisibleChanged?.Invoke(this, e);
        }

        protected virtual void OnDrawOrderChanged(EventArgs e)
        {
            InvalidateParentRenderCache();
            DrawOrderChanged?.Invoke(this, e);
        }

        protected virtual void OnRenderRequested(EventArgs e)
        {
            RenderRequested?.Invoke(this, e);
        }

        protected virtual void OnRendered(EventArgs e)
        {
            Rendered?.Invoke(this, e);
        }

        protected virtual void OnKeyDown(object sender, KeyboardEventArgs e)
        {
            InvokeChildControlEventMethod((Control control) => control.OnKeyDown, builtInControls: true, childControls: true, sender, e);
        }

        protected virtual void OnKeyPressed(object sender, KeyboardEventArgs e)
        {
            InvokeChildControlEventMethod((Control control) => control.OnKeyPressed, builtInControls: true, childControls: true, sender, e);
        }

        protected virtual void OnKeyReleased(object sender, KeyboardEventArgs e)
        {
            InvokeChildControlEventMethod((Control control) => control.OnKeyReleased, builtInControls: true, childControls: true, sender, e);
        }

        protected virtual void OnTextTyped(object sender, KeyboardEventArgs e)
        {
            InvokeChildControlEventMethod((Control control) => control.OnTextTyped, builtInControls: true, childControls: true, sender, e);
        }

        protected virtual void OnMouseMoved(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseMoved, sender, e);
        }

        protected virtual void OnMouseButtonDown(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseButtonDown, sender, e);
        }

        protected virtual void OnMouseButtonPressed(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseButtonPressed, sender, e);
        }

        protected virtual void OnMouseButtonReleased(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseButtonReleased, sender, e);
        }

        protected virtual void OnMouseButtonClicked(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseButtonClicked, sender, e);
        }

        protected virtual void OnMouseButtonDoubleClicked(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseButtonDoubleClicked, sender, e);
        }

        protected virtual void OnMouseScrollWheelMoved(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseScrollWheelMoved, sender, e);
        }

        protected virtual void OnMouseHorizontalScrollWheelMoved(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseHorizontalScrollWheelMoved, sender, e);
        }

        protected virtual void OnMouseDragStarted(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseDragStarted, sender, e);
        }

        protected virtual void OnMouseDragFinished(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseDragFinished, sender, e);
        }

        protected virtual void OnMouseDragMoved(object sender, MouseEventArgs e)
        {
            InvokeChildControlMouseEventMethod((Control control) => control.OnMouseDragMoved, sender, e);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                m_disposed = true;
                if (disposing)
                {
                    UnsubscribeFromEventSource();
                }
            }
        }

        private void OnKeyDownInternal(object sender, KeyboardEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnKeyDown, sender, e);
        }

        private void OnKeyPressedInternal(object sender, KeyboardEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnKeyPressed, sender, e);
        }

        private void OnKeyReleasedInternal(object sender, KeyboardEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnKeyReleased, sender, e);
        }

        private void OnTextTypedInternal(object sender, KeyboardEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnTextTyped, sender, e);
        }

        private void OnMouseMovedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseMoved, sender, e);
        }

        private void OnMouseButtonDownInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseButtonDown, sender, e);
        }

        private void OnMouseButtonPressedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseButtonPressed, sender, e);
        }

        private void OnMouseButtonReleasedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseButtonReleased, sender, e);
        }

        private void OnMouseButtonClickedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseButtonClicked, sender, e);
        }

        private void OnMouseButtonDoubleClickedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseButtonDoubleClicked, sender, e);
        }

        private void OnMouseScrollWheelMovedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseScrollWheelMoved, sender, e);
        }

        private void OnMouseHorizontalScrollWheelMovedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseHorizontalScrollWheelMoved, sender, e);
        }

        private void OnMouseDragStartedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseDragStarted, sender, e);
        }

        private void OnMouseDragFinishedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseDragFinished, sender, e);
        }

        private void OnMouseDragMovedInternal(object sender, MouseEventArgs e)
        {
            InvokeEventHandlerIfEnabled(OnMouseDragMoved, sender, e);
        }

        private void SubscribeToEventSource()
        {
            if (EventSource != null)
            {
                EventSource.KeyDown += OnKeyDownInternal;
                EventSource.KeyPressed += OnKeyPressedInternal;
                EventSource.KeyReleased += OnKeyReleasedInternal;
                EventSource.TextTyped += OnTextTypedInternal;
                EventSource.MouseMoved += OnMouseMovedInternal;
                EventSource.MouseButtonDown += OnMouseButtonDownInternal;
                EventSource.MouseButtonPressed += OnMouseButtonPressedInternal;
                EventSource.MouseButtonReleased += OnMouseButtonReleasedInternal;
                EventSource.MouseButtonClicked += OnMouseButtonClickedInternal;
                EventSource.MouseButtonDoubleClicked += OnMouseButtonDoubleClickedInternal;
                EventSource.MouseScrollWheelMoved += OnMouseScrollWheelMovedInternal;
                EventSource.MouseHorizontalScrollWheelMoved += OnMouseHorizontalScrollWheelMovedInternal;
                EventSource.MouseDragStarted += OnMouseDragStartedInternal;
                EventSource.MouseDragFinished += OnMouseDragFinishedInternal;
                EventSource.MouseDragMoved += OnMouseDragMovedInternal;
            }
        }

        private void UnsubscribeFromEventSource()
        {
            if (EventSource != null)
            {
                EventSource.KeyDown -= OnKeyDownInternal;
                EventSource.KeyPressed -= OnKeyPressedInternal;
                EventSource.KeyReleased -= OnKeyReleasedInternal;
                EventSource.TextTyped -= OnTextTypedInternal;
                EventSource.MouseMoved -= OnMouseMovedInternal;
                EventSource.MouseButtonDown -= OnMouseButtonDownInternal;
                EventSource.MouseButtonPressed -= OnMouseButtonPressedInternal;
                EventSource.MouseButtonReleased -= OnMouseButtonReleasedInternal;
                EventSource.MouseButtonClicked -= OnMouseButtonClickedInternal;
                EventSource.MouseButtonDoubleClicked -= OnMouseButtonDoubleClickedInternal;
                EventSource.MouseScrollWheelMoved -= OnMouseScrollWheelMovedInternal;
                EventSource.MouseHorizontalScrollWheelMoved -= OnMouseHorizontalScrollWheelMovedInternal;
                EventSource.MouseDragStarted -= OnMouseDragStartedInternal;
                EventSource.MouseDragFinished -= OnMouseDragFinishedInternal;
                EventSource.MouseDragMoved -= OnMouseDragMovedInternal;
            }
        }

        private void InvokeEventHandlerIfEnabled<TEventArgs>(EventHandler<TEventArgs> eventHandler, object sender, TEventArgs eventArgs)
        {
            if (Enabled)
            {
                eventHandler?.Invoke(sender, eventArgs);
            }
        }

        private void OnChildControlCollectionChanged(object sender, ControlCollectionChangedEventArgs e)
        {
            foreach (Control addedControl in e.AddedControls)
            {
                SubscribeToChildControlEvents(addedControl);
            }
            foreach (Control removedControl in e.RemovedControls)
            {
                UnsubscribeFromChildControlEvents(removedControl);
            }
        }

        private void SubscribeToChildControlEvents(Control control)
        {
            control.RenderRequested += OnChildControlRenderRequested;
        }

        private void UnsubscribeFromChildControlEvents(Control control)
        {
            control.RenderRequested -= OnChildControlRenderRequested;
        }

        private void InvokeChildControlEventMethod<TEventArgs>(ControlEventMethodSelector<TEventArgs> methodSelector, bool builtInControls, bool childControls, object sender, TEventArgs e)
        {
            if (builtInControls)
            {
                foreach (Control builtInControl in BuiltInControls)
                {
                    EventHandler<TEventArgs> eventHandler = methodSelector(builtInControl);
                    eventHandler(sender, e);
                }
            }
            if (!childControls)
            {
                return;
            }
            foreach (Control control in Controls)
            {
                EventHandler<TEventArgs> eventHandler2 = methodSelector(control);
                eventHandler2(sender, e);
            }
        }

        private void InvokeChildControlMouseEventMethod(ControlEventMethodSelector<MouseEventArgs> methodSelector, object sender, MouseEventArgs e)
        {
            //IL_0023: Unknown result type (might be due to invalid IL or missing references)
            //IL_0028: Unknown result type (might be due to invalid IL or missing references)
            Viewport currentControlViewport = e.Viewport;
            InvokeChildControlEventMethod(BuiltInControlMethodSelector, builtInControls: true, childControls: false, sender, e);
            InvokeChildControlEventMethod(ChildControlMethodSelector, builtInControls: false, childControls: true, sender, e);
            e.SetViewport(in currentControlViewport);
            EventHandler<MouseEventArgs> BuiltInControlMethodSelector(Control control)
            {
                //IL_0008: Unknown result type (might be due to invalid IL or missing references)
                //IL_000d: Unknown result type (might be due to invalid IL or missing references)
                //IL_0012: Unknown result type (might be due to invalid IL or missing references)
                Viewport viewport2 = GraphicsControlGeometryHelpers.CreateChildViewport(in currentControlViewport, control.Bounds);
                e.SetViewport(in viewport2);
                return methodSelector(control);
            }
            EventHandler<MouseEventArgs> ChildControlMethodSelector(Control control)
            {
                //IL_0008: Unknown result type (might be due to invalid IL or missing references)
                //IL_000d: Unknown result type (might be due to invalid IL or missing references)
                //IL_0012: Unknown result type (might be due to invalid IL or missing references)
                //IL_001b: Unknown result type (might be due to invalid IL or missing references)
                //IL_0020: Unknown result type (might be due to invalid IL or missing references)
                //IL_0025: Unknown result type (might be due to invalid IL or missing references)
                Viewport viewport = GraphicsControlGeometryHelpers.CreateChildViewport(in currentControlViewport, control.Bounds);
                viewport = GraphicsControlGeometryHelpers.CreateChildViewport(in viewport, GetContainerBounds());
                e.SetViewport(in viewport);
                return methodSelector(control);
            }
        }

        private void OnChildControlRenderRequested(object sender, EventArgs e)
        {
            InvalidateRenderCache();
        }
    }
}
