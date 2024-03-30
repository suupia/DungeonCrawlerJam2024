#nullable enable
using System;
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
        
        public (int x, int y) InitPlayerPosition { get; set; }
        public (int x, int y) InitStairsPosition { get; set; }
        public IReadOnlyList<(int x, int y)> InitEnemyPositions { get; set; }
        public IReadOnlyList<(int x, int y)> InitTorchPositions { get; set; } = new List<(int x, int y)>();

        public DungeonGridMap(PlainDungeonGridMap plainDungeon)
        {
            _plainDungeon = plainDungeon;
        }
    }
}