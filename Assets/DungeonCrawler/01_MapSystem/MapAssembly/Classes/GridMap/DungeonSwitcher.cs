#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class DungeonSwitcher
    {
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
            return _currentDungeon;
        }
    }
}