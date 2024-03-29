#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class DungeonSwitcher
    {
        public int Floor { get; private set; }
        public PlainDungeonGridMap CurrentPlainDungeon => _currentPlainDungeon;
        PlainDungeonGridMap _currentPlainDungeon;
        readonly DungeonBuilder _dungeonBuilder;
        
        public DungeonSwitcher(
            DungeonBuilder dungeonBuilder
            )
        {
            _dungeonBuilder = dungeonBuilder;
        }

        public PlainDungeonGridMap SwitchNextDungeon()
        {
            Debug.Log("SwitchNextDungeon");
            _currentPlainDungeon = _dungeonBuilder.CreateDungeon();
            Floor++;
            return _currentPlainDungeon;
        }
    }
}