#nullable enable
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class DungeonSwitcher
    {
        public int Floor { get; private set; }
        public DungeonGridMap CurrentDungeon => _currentDungeon;
        DungeonGridMap _currentDungeon;
        readonly DungeonBuilder _dungeonBuilder;
        
        public DungeonSwitcher(
            DungeonBuilder dungeonBuilder
            )
        {
            _dungeonBuilder = dungeonBuilder;
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