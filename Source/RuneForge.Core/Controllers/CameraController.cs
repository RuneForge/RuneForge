using Microsoft.Xna.Framework;

using RuneForge.Core.Rendering;
using RuneForge.Game.Maps;
using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Core.Controllers
{
    public class CameraController
    {
        private readonly IMapProvider m_mapProvider;
        private readonly Camera2D m_camera;

        public CameraController(IMapProvider mapProvider, Camera2D camera)
        {
            m_mapProvider = mapProvider;
            m_camera = camera;
        }

        public void MoveCamera(Point relativeLocation)
        {
            SetCameraLocation(m_camera.Location + relativeLocation);
        }

        public void SetCameraLocation(Point absoluteLocation)
        {
            Map map = m_mapProvider.Map;
            if (absoluteLocation.X < 0)
                absoluteLocation.X = 0;
            if (absoluteLocation.X > (map.Width + 1) * Map.CellWidth)
                absoluteLocation.X = (map.Width + 1) * Map.CellWidth;
            if (absoluteLocation.Y < 0)
                absoluteLocation.Y = 0;
            if (absoluteLocation.Y > (map.Height + 1) * Map.CellHeight)
                absoluteLocation.Y = (map.Height + 1) * Map.CellHeight;
            m_camera.Location = absoluteLocation;
        }

        public void SetCameraScale(float scale)
        {
            m_camera.Scale = scale;
        }
    }
}
