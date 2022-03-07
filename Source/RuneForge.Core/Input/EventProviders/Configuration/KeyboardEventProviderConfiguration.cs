using System;

namespace RuneForge.Core.Input.EventProviders.Configuration
{
    public class KeyboardEventProviderConfiguration
    {
        public static readonly TimeSpan DefaultInitialEventDelay = TimeSpan.FromMilliseconds(450.0);

        public static readonly TimeSpan DefaultRepeatedEventDelay = TimeSpan.FromMilliseconds(30.0);

        public TimeSpan InitialEventDelay { get; set; }

        public TimeSpan RepeatedEventDelay { get; set; }

        public KeyboardEventProviderConfiguration()
        {
        }

        public KeyboardEventProviderConfiguration(TimeSpan initialEventDelay, TimeSpan repeatedEventDelay)
        {
            InitialEventDelay = initialEventDelay;
            RepeatedEventDelay = repeatedEventDelay;
        }
    }
}
