using System.Xml.Serialization;

namespace RuneForge.Content.Pipeline.Game.Players
{
    public class PlayerColor
    {
        [XmlAttribute(AttributeName = "MainColor")]
        public string MainColor { get; set; }

        [XmlAttribute(AttributeName = "EntityColorShadeA")]
        public string EntityColorShadeA { get; set; }
        [XmlAttribute(AttributeName = "EntityColorShadeB")]
        public string EntityColorShadeB { get; set; }
        [XmlAttribute(AttributeName = "EntityColorShadeC")]
        public string EntityColorShadeC { get; set; }
        [XmlAttribute(AttributeName = "EntityColorShadeD")]
        public string EntityColorShadeD { get; set; }
    }
}
