using System;

using RuneForge.Game.Maps.Interfaces;

namespace RuneForge.Game.Maps
{
    public class MapProvider : IMapProvider
    {
        private Map m_map;
        private bool m_mapInitialized;

        public MapProvider()
        {
            m_map = null;
            m_mapInitialized = false;
        }

        public Map Map
        {
            get
            {
                if (m_mapInitialized)
                    return m_map;
                else
                    throw new InvalidOperationException("Unable to retrieve the map before it is initialized.");
            }
            set
            {
                if (m_mapInitialized)
                    throw new InvalidOperationException("Unable to change the map once it has been initialized.");
                else if (value == null)
                    throw new InvalidOperationException("Unable to initialize the map with a 'null' value.");
                else
                {
                    m_map = value;
                    m_mapInitialized = true;
                }
            }
        }
    }
}
