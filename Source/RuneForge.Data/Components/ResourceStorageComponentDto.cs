using System;

namespace RuneForge.Data.Components
{
    [Serializable]
    public class ResourceStorageComponentDto : ComponentDto
    {
        public int AcceptedResourceTypes { get; set; }

        public TimeSpan TransferTime { get; set; }
    }
}
