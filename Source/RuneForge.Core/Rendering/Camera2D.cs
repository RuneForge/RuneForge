using System;

using Microsoft.Xna.Framework;

namespace RuneForge.Core.Rendering
{
    public class Camera2D
    {
        private const float c_minScale = 0.25f;
        private const float c_maxScale = 4.0f;

        private readonly Point m_viewportSize;
        private Point m_location;
        private float m_scale;
        private Matrix m_worldToScreenTransformationMatrix;
        private Matrix m_screenToWorldTransformationMatrix;
        private bool m_cacheInvalidated;

        public Camera2D(Point viewportSize)
            : this(viewportSize, Point.Zero, 1.0f)
        {
        }
        public Camera2D(Point viewportSize, Point location, float scale)
        {
            m_viewportSize = viewportSize;
            m_location = location;
            m_scale = scale;
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
                value = MathF.Max(value, c_minScale);
                value = MathF.Min(value, c_maxScale);
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
                * Matrix.CreateTranslation(new Vector3(MathF.Floor(m_viewportSize.X * 0.5f), MathF.Floor(m_viewportSize.Y * 0.5f), 0));
            m_screenToWorldTransformationMatrix = Matrix.Invert(m_worldToScreenTransformationMatrix);
            m_cacheInvalidated = false;
        }

        private void InvalidateCache()
        {
            m_cacheInvalidated = true;
        }
    }
}
