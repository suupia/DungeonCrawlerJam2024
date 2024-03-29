#nullable enable
using System.Linq;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes
{

    public class GridEntityPlacer
    {
        readonly IGridTile _wall;
        readonly IGridTile _path;
        readonly IGridTile _room;
        readonly IGridEntity _stairs;

        public GridEntityPlacer()
        {
            _wall = new CharacterWall();
            _path = new CharacterPath();
            _room = new CharacterRoom();
            _stairs = new CharacterStairs();
        }
        
        public DungeonGridMap PlaceEntities(PlainDungeonGridMap plainDungeon)
        {
            var dungeon = new DungeonGridMap(plainDungeon);
            // Tiles
            dungeon = PlaceRooms(dungeon);
            dungeon = PlacePath(dungeon);
            dungeon = PlaceWall(dungeon);  // this should be last
            
            // Entities
            dungeon = PlaceStairs(dungeon);
            
            return dungeon;
        }
        
        DungeonGridMap PlaceRooms(DungeonGridMap dungeon)
        {
            var areas = dungeon.Areas;  
            foreach (var area in areas)
            {
                var room = area.Room;
                for (int y = room.Y; y < room.Y + room.Height; y++)
                {
                    for (int x = room.X; x < room.X + room.Width; x++)
                    {
                        dungeon.Map.AddEntity(x, y, _room);
                    }
                }
            }

            return dungeon;
        }
        
        DungeonGridMap PlaceWall(DungeonGridMap dungeon)
        {
            for (int y = 0; y < dungeon.Map.Height; y++)
            {
                for (int x = 0; x < dungeon.Map.Width; x++)
                {
                    if (dungeon.Map.GetSingleTypeList<IGridTile>(x, y).Count == 0)
                    {
                        dungeon.Map.AddEntity(x,y, _wall);
                    }
                }
            }
            return dungeon;
        }
        
        DungeonGridMap PlacePath(DungeonGridMap dungeon)
        {
            var paths = dungeon.Paths;
            foreach (var path in paths)
            {
                foreach (var (x, y) in path.Points)
                {
                    dungeon.Map.AddEntity(x, y, _path);
                }
            }
            return dungeon;
        }

        DungeonGridMap PlaceStairs(DungeonGridMap dungeon)
        {
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");
            
            var area = areas[Random.Range(0,areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            dungeon.Map.AddEntity(spawnX, spawnY, _stairs);
            return dungeon;
        }
        

    }
}