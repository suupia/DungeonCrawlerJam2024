#nullable enable
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
        readonly DefaultDungeonGridMap _defaultDungeonGridMap;
        
        public DungeonSwitcher(
            DungeonBuilder dungeonBuilder,
            DefaultDungeonGridMap defaultDungeonGridMap
            )
        {
            _dungeonBuilder = dungeonBuilder;
            _defaultDungeonGridMap = defaultDungeonGridMap;
        }

        public DungeonGridMap SwitchNextDungeon()
        {
            Debug.Log("SwitchNextDungeon");
            _currentDungeon = _dungeonBuilder.CreateDungeon();
            Floor++;
            return _currentDungeon;
        }
    }
}