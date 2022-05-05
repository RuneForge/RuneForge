using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using RuneForge.Core.Helpers.Interfaces;
using RuneForge.Data.Buildings;
using RuneForge.Data.Buildings.Interfaces;
using RuneForge.Data.Maps;
using RuneForge.Data.Maps.Interfaces;
using RuneForge.Data.Players;
using RuneForge.Data.Players.Interfaces;
using RuneForge.Data.Units;
using RuneForge.Data.Units.Interfaces;
using RuneForge.Game.Buildings.Interfaces;
using RuneForge.Game.Players.Interfaces;
using RuneForge.Game.Units.Interfaces;

namespace RuneForge.Core.Helpers
{
    public class GameStateSerializer : IGameStateSerializer
    {
        private readonly IPlayerService m_playerService;
        private readonly IPlayerRepository m_playerRepository;
        private readonly IUnitService m_unitService;
        private readonly IUnitRepository m_unitRepository;
        private readonly IBuildingService m_buildingService;
        private readonly IBuildingRepository m_buildingRepository;
        private readonly IMapDecorationRepository m_mapDecorationRepository;

        public GameStateSerializer(
            IPlayerService playerService,
            IPlayerRepository playerRepository,
            IUnitService unitService,
            IUnitRepository unitRepository,
            IBuildingService buildingService,
            IBuildingRepository buildingRepository,
            IMapDecorationRepository mapDecorationRepository
            )
        {
            m_playerService = playerService;
            m_playerRepository = playerRepository;
            m_unitService = unitService;
            m_unitRepository = unitRepository;
            m_buildingService = buildingService;
            m_buildingRepository = buildingRepository;
            m_mapDecorationRepository = mapDecorationRepository;
        }

        public string GetSaveFileName()
        {
            DateTime currentDateTime = DateTime.Now;
            return $"Save {currentDateTime:yyyy-MM-dd HH-mm-ss}.save";
        }

        public void SerializeGameState(Stream stream)
        {
            m_playerService.CommitChanges();
            ReadOnlyCollection<PlayerDto> players = m_playerRepository.GetPlayers();
            m_unitService.CommitChanges();
            ReadOnlyCollection<UnitDto> units = m_unitRepository.GetUnits();
            m_buildingService.CommitChanges();
            ReadOnlyCollection<BuildingDto> buildings = m_buildingRepository.GetBuildings();
            ReadOnlyCollection<MapDecorationDto> mapDecorations = m_mapDecorationRepository.GetMapDecorations();

            SerializableGameState serializableGameState = new SerializableGameState()
            {
                Players = players,
                Units = units,
                Buildings = buildings,
                MapDecorations = mapDecorations,
            };

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, serializableGameState);
        }
    }
}
