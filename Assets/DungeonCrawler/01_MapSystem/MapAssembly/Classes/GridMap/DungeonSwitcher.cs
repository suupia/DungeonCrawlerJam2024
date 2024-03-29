#nullable enable
using System;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class DungeonSwitcher
    {
        public int Floor { get; private set; }
        public DungeonGridMap CurrentDungeon => _currentDungeon ?? _defaultDungeonGridMap;
        DungeonGridMap? _currentDungeon;
        readonly DungeonBuilder _dungeonBuilder;
        readonly GridTilePlacer _gridTilePlacer;
        readonly DefaultDungeonGridMap _defaultDungeonGridMap;
        
        public DungeonSwitcher(
            DungeonBuilder dungeonBuilder,
            DefaultDungeonGridMap defaultDungeonGridMap,
            GridTilePlacer gridTilePlacer
            )
        {
            _dungeonBuilder = dungeonBuilder;
            _defaultDungeonGridMap = defaultDungeonGridMap;
            _gridTilePlacer = gridTilePlacer;
        }

        public DungeonGridMap SwitchNextDungeon()
        {
            Debug.Log("SwitchNextDungeon");
            var nextPlainDungeon = _dungeonBuilder.CreateDungeon();
            _currentDungeon = _gridTilePlacer.PlaceEntities(nextPlainDungeon, this);
            Floor++;
            return _currentDungeon;
        }
    }
}