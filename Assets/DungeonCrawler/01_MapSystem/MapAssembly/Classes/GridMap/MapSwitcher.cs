#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class MapSwitcher
    {
        public DungeonGridMap CurrentDungeon => _currentDungeon;
        DungeonGridMap _currentDungeon;
        readonly IGridCoordinate _coordinate;
        readonly DungeonBuilder _dungeonBuilder;
        readonly DivideAreaExecutor _divideAreaExecutor;
        
        public MapSwitcher(
            IGridCoordinate coordinate,
            DivideAreaExecutor divideAreaExecutor,
            DungeonBuilder dungeonBuilder)
        {
            _coordinate = coordinate;
            _divideAreaExecutor = divideAreaExecutor;
            _dungeonBuilder = dungeonBuilder;
        }

        public DungeonGridMap SwitchNextDungeon()
        {
            Debug.Log("SwitchNextDungeon");
            var nextMap = new EntityGridMap(_coordinate);
            _currentDungeon = _dungeonBuilder.CreateDungeon(nextMap);
            return _currentDungeon;
        }
    }
}