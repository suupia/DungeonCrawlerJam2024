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

        int _divideCount;
        List<Area> _areas = new ();


        public DungeonBuilder(
            DivideAreaExecutor divideAreaExecutor)
        {
            _divideAreaExecutor = divideAreaExecutor;
            _wall = new CharacterWall();
            _path = new CharacterPath();
            _room = new CharacterRoom();
        }

        
        public DungeonGridMap CreateDungeon(EntityGridMap map)
        {
            for(int i = 0; i < DivideCount; i++)
            {
                map = CreateDungeonByStep(map);
            }
            _divideCount = 0;
            return new DungeonGridMap(map, _areas, new List<Path>());
        }
        
        public EntityGridMap CreateDungeonByStep(EntityGridMap map)
        {
            var areas =_divideCount == 0 ? new List<Area>{GetInitArea(map)} : _divideAreaExecutor.DivideAreaOnce(_areas);
            var paths = areas.SelectMany(area => area.AdjacentAreas.Select(tuple => tuple.path)).ToList();
            map.ClearMap();
            map = PlaceRooms(map, areas);
            map = PlacePath(map, paths);
            map = PlaceWall(map);  // this should be last
            _divideCount++;
            _areas = areas;
            return map;
        }

        public void Reset()
        {
            _divideCount = 0;
            _areas = new List<Area>();
        }

        public Area GetInitArea(EntityGridMap map)
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

        public EntityGridMap PlaceRooms(EntityGridMap map, List<Area> areas)
        {
            foreach (var area in areas)
            {
                var room = area.Room;
                for (int y = room.Y; y < room.Y + room.Height; y++)
                {
                    for (int x = room.X; x < room.X + room.Width; x++)
                    {
                        map.AddEntity(x, y, _room);
                    }
                }
            }

            return map;
        }
        
        public EntityGridMap PlaceWall(EntityGridMap map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.GetSingleTypeList<IGridEntity>(x, y).Count == 0)
                    {
                        map.AddEntity(x,y, _wall);
                    }
                }
            }
            return map;
        }
        
        public EntityGridMap PlacePath(EntityGridMap map, List<Path> paths)
        {
            foreach (var path in paths)
            {
                foreach (var (x, y) in path.Points)
                {
                    map.AddEntity(x, y, _path);
                }
            }
            return map;
        }

        // Following functions should be written in other class.
        public (int x, int y) CalculatePlayerSpawnPosition()
        {
            // [pre-condition] _areas should not be empty
            Assert.IsTrue(_areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", _areas.Select(area => area.Room))}");
            
            var area = _areas[Random.Range(0,_areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            return (spawnX, spawnY);
        }
        public (int x, int y) CalculateEnemySpawnPosition()
        {
            // [pre-condition] _areas should not be empty
            Assert.IsTrue(_areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", _areas.Select(area => area.Room))}");
            
            var area = _areas[Random.Range(0,_areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            return (spawnX, spawnY);
        }

        public (int x, int y) CalculateKeySpawnPosition()
        {
            // [pre-condition] _areas should not be empty
            Assert.IsTrue(_areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", _areas.Select(area => area.Room))}");
            
            var area = _areas[Random.Range(0,_areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            return (spawnX, spawnY);
        }
    }
    public record Area(int X, int Y, int Width, int Height, Room Room, List<(Area area, Path path)> AdjacentAreas);
    public record Room(int X, int Y, int Width, int Height);
    public record Path(List<(int x, int y)> Points, (bool isDivideByVertical, int coord) DivideInfo);
}


