﻿using System;
using System.Collections.ObjectModel;

using RuneForge.Data.Buildings;
using RuneForge.Data.Maps;
using RuneForge.Data.Players;
using RuneForge.Data.Units;

namespace RuneForge.Core.Helpers
{
    [Serializable]
    public class SerializableGameState
    {
        public ReadOnlyCollection<PlayerDto> Players { get; set; }

        public ReadOnlyCollection<UnitDto> Units { get; set; }
        public ReadOnlyCollection<BuildingDto> Buildings { get; set; }

        public ReadOnlyCollection<MapDecorationDto> MapDecorations { get; set; }
    }
}
