﻿#nullable enable
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap
{
    public class DungeonGridMap
    {
        public EntityGridMap Map => _plainDungeon.Map;
        public IReadOnlyList<Area> Areas => _plainDungeon.Areas;
        public IReadOnlyList<Path> Paths => _plainDungeon.Paths;
        readonly PlainDungeonGridMap _plainDungeon;
        
        public (int x, int y) StairsPosition { get; set; }
        public (int x, int y) EnemyPosition { get; set; }
        
        public DungeonGridMap(PlainDungeonGridMap plainDungeon)
        {
            _plainDungeon = plainDungeon;
        }
    }
}