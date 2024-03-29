﻿#nullable enable
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Interfaces;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class DungeonGridMap
    {
        public EntityGridMap GridMap => _map; // Aggregating and publishing in PUBLIC is a compromise.
        public IReadOnlyList<Area> Areas => _areas;
        public IReadOnlyList<Path> Paths => _paths;
        readonly EntityGridMap _map;
        readonly List<Area> _areas;
        readonly List<Path> _paths;

        public DungeonGridMap(
            EntityGridMap map,
            List<Area> areas,
            List<Path> paths)
        {
            _map = map;
            _areas = areas;
            _paths = paths;
        }
        
    }
}