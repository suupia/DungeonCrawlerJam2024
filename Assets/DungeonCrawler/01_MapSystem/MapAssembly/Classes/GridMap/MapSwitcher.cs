#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class MapSwitcher
    {
        public DungeonGridMap CurrentDungeon => _currentDungeon;
        DungeonGridMap _currentDungeon;
        readonly DungeonBuilder _dungeonBuilder;
        
        public MapSwitcher(
            DungeonBuilder dungeonBuilder
            )
        {
            _dungeonBuilder = dungeonBuilder;
        }

        public DungeonGridMap SwitchNextDungeon()
        {
            Debug.Log("SwitchNextDungeon");
            _currentDungeon = _dungeonBuilder.CreateDungeon();
            return _currentDungeon;
        }
    }
}