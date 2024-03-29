#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapAssembly.Classes;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;


namespace DungeonCrawler.MapAssembly.Classes
{
    public class DungeonBuilder
    {
        const int DivideCount = 5;
        readonly IGridEntity _wall;
        readonly IGridEntity _path;
        readonly IGridEntity _room;
        readonly DivideAreaExecutor _divideAreaExecutor;

        readonly IGridCoordinate _coordinate;

        public DungeonBuilder(
            DivideAreaExecutor divideAreaExecutor,
            IGridCoordinate coordinate)
        {
            _divideAreaExecutor = divideAreaExecutor;
            _coordinate = coordinate;
            _wall = new CharacterWall();
            _path = new CharacterPath();
            _room = new CharacterRoom();
        }

        
        public DungeonGridMap CreateDungeon()
        {
            var map = new EntityGridMap(_coordinate);
            var dungeon = new DungeonGridMap(map,new List<Area>(), new List<Path>());
            for(int i = 0; i < DivideCount; i++)
            {
                dungeon = CreateDungeonByStep(dungeon);
            }
            return dungeon;
        }
        
        public DungeonGridMap CreateDungeonByStep(DungeonGridMap dungeon)
        {
            var areas = !dungeon.Areas.Any()
                ? new List<Area> { GetInitArea(dungeon.Map) }
                : _divideAreaExecutor.DivideAreaOnce(new List<Area>(dungeon.Areas));
            var paths = areas.SelectMany(area => area.AdjacentAreas.Select(tuple => tuple.path)).ToList();
            dungeon.Map.ClearMap();
            var nextDungeon = new DungeonGridMap(dungeon.Map, areas, paths);
            var dungeon1 = PlaceRooms(nextDungeon);
            var dungeon2 = PlacePath(dungeon1);
            var dungeon3 = PlaceWall(dungeon2);  // this should be last
            return dungeon3;
        }

        public void Reset()
        {
        }

        Area GetInitArea(EntityGridMap map)
        {
            return new Area(
                X: 0,
                Y: 0,
                Width: map.Width,
                Height: map.Height,
                Room: new Room(
                    X: DivideAreaExecutor.MinRoomMargin,
                    Y: DivideAreaExecutor.MinRoomMargin,
                    Width: map.Width - DivideAreaExecutor.MinRoomMargin * 2,
                    Height: map.Height - DivideAreaExecutor.MinRoomMargin * 2
                ),
                AdjacentAreas: new List<(Area area, Path path)>()
            );
        }

        public DungeonGridMap PlaceRooms(DungeonGridMap dungeon)
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
        
        public DungeonGridMap PlaceWall(DungeonGridMap dungeon)
        {
            for (int y = 0; y < dungeon.Map.Height; y++)
            {
                for (int x = 0; x < dungeon.Map.Width; x++)
                {
                    if (dungeon.Map.GetSingleTypeList<IGridEntity>(x, y).Count == 0)
                    {
                        dungeon.Map.AddEntity(x,y, _wall);
                    }
                }
            }
            return dungeon;
        }
        
        public DungeonGridMap PlacePath(DungeonGridMap dungeon)
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

        // Following functions should be written in other class.
        public (int x, int y) CalculatePlayerSpawnPosition(DungeonGridMap dungeon)
        {
            // [pre-condition] _areas should not be empty
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");
            
            var area = areas[Random.Range(0,areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            return (spawnX, spawnY);
        }
        public (int x, int y) CalculateEnemySpawnPosition(DungeonGridMap dungeon)
        {
            // [pre-condition] _areas should not be empty
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");
            
            var area = areas[Random.Range(0,areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            return (spawnX, spawnY);
        }

        public (int x, int y) CalculateKeySpawnPosition(DungeonGridMap dungeon)
        {
            // [pre-condition] _areas should not be empty
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");
            
            var area = areas[Random.Range(0,areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            return (spawnX, spawnY);
        }
    }
    public record Area(int X, int Y, int Width, int Height, Room Room, List<(Area area, Path path)> AdjacentAreas);
    public record Room(int X, int Y, int Width, int Height);
    public record Path(List<(int x, int y)> Points, (bool isDivideByVertical, int coord) DivideInfo);
}


