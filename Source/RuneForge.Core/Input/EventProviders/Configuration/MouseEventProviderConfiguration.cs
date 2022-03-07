using System;

namespace RuneForge.Core.Input.EventProviders.Configuration
{
    public class MouseEventProviderConfiguration
    {
        public static readonly TimeSpan DefaultDoubleClickTimeSpan = TimeSpan.FromMilliseconds(500.0);

        public static readonly int DefaultDragDistanceThreshold = 2;

        public TimeSpan DoubleClickTimeSpan { get; set; }

        public int DragDistanceThreshold { get; set; }

        public MouseEventProviderConfiguration()
        {
        }

        public MouseEventProviderConfiguration(TimeSpan doubleClickTimeSpan, int dragDistanceThreshold)
        {
            DoubleClickTimeSpan = doubleClickTimeSpan;
            DragDistanceThreshold = dragDistanceThreshold;
        }
    }
}
