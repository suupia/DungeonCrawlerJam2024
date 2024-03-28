#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class MapSwitcher
    {
        public EntityGridMap CurrentMap => _currentMap;
        EntityGridMap _currentMap;
        IGridCoordinate _coordinate = null!;
        DivideAreaExecutor _divideAreaExecutor = null!;
        readonly DungeonBuilder _dungeonBuilder;
        
        public MapSwitcher(
            IGridCoordinate coordinate,
            DivideAreaExecutor divideAreaExecutor,
            DungeonBuilder dungeonBuilder)
        {
            _coordinate = coordinate;
            _divideAreaExecutor = divideAreaExecutor;
            _dungeonBuilder = dungeonBuilder;
        }

        public EntityGridMap SwitchNextDungeon()
        {
            Debug.Log("SwitchNextDungeon");
            var nextMap = new EntityGridMap(_coordinate);
            _currentMap = _dungeonBuilder.CreateDungeon(nextMap);
            return _currentMap;
        }
    }
}