namespace RuneForge.Core.Interface.Controls.Helpers
{
    internal class RectangularFrameValidationData
    {
        public string SideTextureRegionNameTemplate { get; set; }

        public string CornerTextureRegionNameTemplate { get; set; }

        public (Alignment side, Alignment corner)[] ValidationPairs { get; set; }
    }
}
