#nullable enable
using System.Collections.Generic;
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.MapSystem.Scripts;
using DungeonCrawler.MapSystem.Scripts.Entity;


namespace DungeonCrawler.MapSystem.Scripts
{
    public class DungeonBuilder
    {
        readonly IEntity _wall = new CharacterWall();
        readonly IGridCoordinate _coordinate;
        public DungeonBuilder(IGridCoordinate coordinate)
        {
            _coordinate = coordinate;
        }
        
        public EntityGridMap CreateDungeon()
        {
            // Step1: Fill the map with walls
            var map = InitMap();
            // Step2: Divide the map into two areas
            var divideMap = DivideMap(map);
            // Step3: Create the room in each area
            var roomMap = CreateRoom(divideMap);
            // Step4: Connect the rooms
            var connectMap = ConnectRoom(roomMap);
            return connectMap;
        }
        
        EntityGridMap InitMap()
        {
            var map = new EntityGridMap(_coordinate);
            map.FillAll(_wall);
            return map;
        }
        
        EntityGridMap DivideMap(EntityGridMap map)
        {
            return map;
        }
        
        EntityGridMap CreateRoom(EntityGridMap map)
        {
            return map;
        }
        
        EntityGridMap ConnectRoom(EntityGridMap map)
        {
            return map;
        }
    }
}