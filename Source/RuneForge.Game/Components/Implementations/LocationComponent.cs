using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework;

using RuneForge.Game.Maps;

namespace RuneForge.Game.Components.Implementations
{
    public class LocationComponent : Component
    {
        private float m_x;
        private float m_y;
        private int m_width;
        private int m_height;
        private ReadOnlyCollection<Point> m_occupiedCellsCache;
        private ReadOnlyCollection<Point> m_surroundingCellsCache;
        private bool m_occupiedCellsCacheInvalidated;
        private bool m_surroundingCellsCacheInvalidated;

        public LocationComponent(float x, float y, int width, int height)
        {
            m_x = x;
            m_y = y;
            m_width = width;
            m_height = height;
            m_occupiedCellsCache = null;
            m_surroundingCellsCache = null;
            m_occupiedCellsCacheInvalidated = true;
            m_surroundingCellsCacheInvalidated = true;
        }

        public float X
        {
            get => m_x;
            set
            {
                if (m_x != value)
                {
                    m_x = value;
                    InvalidateCache();
                }
            }
        }
        public float Y
        {
            get => m_y;
            set
            {
                if (m_y != value)
                {
                    m_y = value;
                    InvalidateCache();
                }
            }
        }
        public int XCells
        {
            get => (int)MathF.Floor(X / Map.CellWidth);
            set => X = value * Map.CellWidth;
        }
        public int YCells
        {
            get => (int)MathF.Floor(Y / Map.CellHeight);
            set => Y = value * Map.CellHeight;
        }

        public int Width
        {
            get => m_width;
            set
            {
                if (m_width != value)
                {
                    m_width = value;
                    InvalidateCache();
                }
            }
        }
        public int Height
        {
            get => m_height;
            set
            {
                if (m_height != value)
                {
                    m_height = value;
                    InvalidateCache();
                }
            }
        }
        public int WidthCells
        {
            get => Width / Map.CellWidth;
            set => Width = value * Map.CellWidth;
        }
        public int HeightCells
        {
            get => Height / Map.CellHeight;
            set => Height = value * Map.CellHeight;
        }

        public Vector2 Location
        {
            get => new Vector2(X, Y);
            set => (X, Y) = value;
        }
        public Point LocationCells
        {
            get => new Point(XCells, YCells);
            set => (XCells, YCells) = value;
        }

        public Point Size
        {
            get => new Point(Width, Height);
            set => (Width, Height) = value;
        }
        public Point SizeCells
        {
            get => new Point(WidthCells, HeightCells);
            set => (WidthCells, HeightCells) = value;
        }

        public static LocationComponent CreateFromLocation(float x, float y, int width, int height)
        {
            return new LocationComponent(x, y, width, height);
        }
        public static LocationComponent CreateFromCellLocation(int xCells, int yCells, int widthCells, int heightCells)
        {
            return new LocationComponent(0, 0, 0, 0)
            {
                XCells = xCells,
                YCells = yCells,
                WidthCells = widthCells,
                HeightCells = heightCells,
            };
        }

        public ReadOnlyCollection<Point> GetOccupiedCells()
        {
            return !m_occupiedCellsCacheInvalidated ? m_occupiedCellsCache : m_occupiedCellsCache = RebuildOccupiedCellsCache();
        }
        public ReadOnlyCollection<Point> GetSurroundingCells()
        {
            return !m_surroundingCellsCacheInvalidated ? m_surroundingCellsCache : m_surroundingCellsCache = RebuildSurroundingCellsCache();
        }

        private ReadOnlyCollection<Point> RebuildOccupiedCellsCache()
        {
            List<Point> occupiedCells = new List<Point>(WidthCells * HeightCells);
            for (int i = 0; i < occupiedCells.Capacity; i++)
            {
                int xCells = XCells + (i % WidthCells);
                int yCells = YCells +  (i / HeightCells);
                occupiedCells.Add(new Point(xCells, yCells));
            }
            m_occupiedCellsCacheInvalidated = false;
            return occupiedCells.AsReadOnly();
        }
        private ReadOnlyCollection<Point> RebuildSurroundingCellsCache()
        {
            List<Point> surroundingCells = new List<Point>((WidthCells * 2) + (HeightCells * 2) + 4);
            for (int x = 0; x < WidthCells; x++)
            {
                surroundingCells.Add(new Point(x + XCells, YCells - 1));
                surroundingCells.Add(new Point(x + XCells, YCells + HeightCells));
            }
            for (int y = 0; y < HeightCells; y++)
            {
                surroundingCells.Add(new Point(XCells - 1, y + YCells));
                surroundingCells.Add(new Point(XCells + WidthCells, y + YCells));
            }
            surroundingCells.Add(new Point(XCells - 1, YCells - 1));
            surroundingCells.Add(new Point(XCells + WidthCells, YCells - 1));
            surroundingCells.Add(new Point(XCells - 1, YCells + HeightCells));
            surroundingCells.Add(new Point(XCells + WidthCells, YCells + HeightCells));
            m_surroundingCellsCacheInvalidated = false;
            return surroundingCells.AsReadOnly();
        }

        private void InvalidateCache()
        {
            m_occupiedCellsCacheInvalidated = true;
            m_surroundingCellsCacheInvalidated = true;
        }
    }
}
