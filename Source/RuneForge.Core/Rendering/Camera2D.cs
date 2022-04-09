using System;

using Microsoft.Xna.Framework;

namespace RuneForge.Core.Rendering
{
    public class Camera2D
    {
        private readonly Camera2DParameters m_cameraParameters;
        private Point m_location;
        private float m_scale;
        private Matrix m_worldToScreenTransformationMatrix;
        private Matrix m_screenToWorldTransformationMatrix;
        private bool m_cacheInvalidated;

        public Camera2D(Camera2DParameters cameraParameters)
        {
            m_cameraParameters = cameraParameters;
            m_location = Point.Zero;
            m_scale = cameraParameters.DefaultScale;
            m_worldToScreenTransformationMatrix = Matrix.Identity;
            m_screenToWorldTransformationMatrix = Matrix.Identity;
            m_cacheInvalidated = true;
        }

        public Point Location
        {
            get => m_location;
            set
            {
                if (m_location != value)
                {
                    m_location = value;
                    InvalidateCache();
                }
            }
        }
        public float Scale
        {
            get => m_scale;
            set
            {
                value = MathF.Max(value, m_cameraParameters.MinScale);
                value = MathF.Min(value, m_cameraParameters.MaxScale);
                if (m_scale != value)
                {
                    m_scale = value;
                    InvalidateCache();
                }
            }
        }

        public Vector2 TranslateWorldToScreen(Vector2 vector)
        {
            if (m_cacheInvalidated)
                RecalculateTransformationMatrices();
            (float x, float y, _) = Vector3.Transform(new Vector3(vector, 0), m_worldToScreenTransformationMatrix);
            return new Vector2(x, y);
        }
        public Vector2 TranslateScreenToWorld(Vector2 vector)
        {
            if (m_cacheInvalidated)
                RecalculateTransformationMatrices();
            (float x, float y, _) = Vector3.Transform(new Vector3(vector, 0), m_screenToWorldTransformationMatrix);
            return new Vector2(x, y);
        }

        public Matrix GetWorldToScreenTransformationMatrix()
        {
            if (m_cacheInvalidated)
                RecalculateTransformationMatrices();
            return m_worldToScreenTransformationMatrix;
        }
        public Matrix GetScreenToWorldTransformationMatrix()
        {
            if (m_cacheInvalidated)
                RecalculateTransformationMatrices();
            return m_screenToWorldTransformationMatrix;
        }

        private void RecalculateTransformationMatrices()
        {
            m_worldToScreenTransformationMatrix =
                Matrix.CreateTranslation(new Vector3(-m_location.X, -m_location.Y, 0))
                * Matrix.CreateScale(new Vector3(Scale, Scale, 1))
                * Matrix.CreateTranslation(new Vector3(MathF.Floor(m_cameraParameters.Viewport.Width * 0.5f), MathF.Floor(m_cameraParameters.Viewport.Height * 0.5f), 0));
            m_screenToWorldTransformationMatrix = Matrix.Invert(m_worldToScreenTransformationMatrix);
            m_cacheInvalidated = false;
        }

        private void InvalidateCache()
        {
            m_cacheInvalidated = true;
        }
    }
}
