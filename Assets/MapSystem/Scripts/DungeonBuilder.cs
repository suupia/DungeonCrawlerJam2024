#nullable enable
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.MapSystem.Scripts;
using DungeonCrawler.MapSystem.Scripts.Entity;
using UnityEngine;


namespace DungeonCrawler.MapSystem.Scripts
{
    public class DungeonBuilder
    {
        readonly CharacterWall _wall = new CharacterWall();
        readonly IEntity _path = new CharacterPath();
        readonly IEntity _area = new CharacterArea();
        readonly IGridCoordinate _coordinate;
                
        const int MinRoomSize = 3;
        const int MaxRoomSize = 7;
        const int MinRoomMargin = 1;
        public DungeonBuilder(IGridCoordinate coordinate)
        {
            _coordinate = coordinate;
        }
        
        public EntityGridMap CreateDungeon()
        {
            var map = new EntityGridMap(_coordinate);
            var areas = DivideMap(map);
            var rooms = new List<Room>();
            foreach (var area in areas)
            {
                rooms.Add( CreateRoom(area));
            }
            // Debug
            foreach (var (room, i) in rooms.Select((v, i) => (v, i)))
            {
                Debug.Log($"Room i: {i}, X: {room.X}, Y: {room.Y}, Width: {room.Width}, Height: {room.Height}");
            }
            

            map = PlaceRooms(map, rooms);
            map = PlacePath(map, CreatePathNaive(rooms[0], rooms[1]));
            map = PlaceWall(map);  // this should be last
            
            return map;
        }
        
        
        List<Area> DivideMap(EntityGridMap map)
        {
            var minX = (MinRoomSize + MinRoomMargin * 2);
            var maxX = map.Width - (MinRoomSize + MinRoomMargin * 2);
            var divideX = Random.Range(minX, maxX);
            var areas = new List<Area>();
            areas.Add(new Area{X = 0, Y = 0, Width = divideX, Height = map.Height});
            areas.Add(new Area{X = divideX, Y = 0, Width = map.Width - divideX, Height = map.Height});
            
            // Debug
            foreach (var (area, i) in areas.Select((v, i) => (v, i)))
            {
                Debug.Log($"Area i: {i}, X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");
            }
            
            return areas;
        }

        
        Room CreateRoom(Area area)
        {
            var room = new Room();
            room.X = Random.Range(area.X + MinRoomMargin, area.X + area.Width - MinRoomSize - MinRoomMargin);
            room.Y = Random.Range(area.Y + MinRoomMargin, area.Y + area.Height - MinRoomSize - MinRoomMargin);
            room.Width = Random.Range(MinRoomSize, Mathf.Min( MaxRoomSize, area.Width - (room.X-area.X)));
            room.Height = Random.Range(MinRoomSize, Mathf.Min( MaxRoomSize, area.Height - (room.Y-area.Y)));
            return room;
        }
        
        EntityGridMap PlaceRooms(EntityGridMap map, List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                for (int y = room.Y; y < room.Y + room.Height; y++)
                {
                    for (int x = room.X; x < room.X + room.Width; x++)
                    {
                        map.AddEntity(x,y, _path);
                    }
                }
            }
            return map;
        }

        EntityGridMap PlaceWall(EntityGridMap map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.GetSingleTypeList<IEntity>(x, y).Count == 0)
                    {
                        map.AddEntity(x,y, _wall);
                    }
                }
            }
            return map;
        }
        
        EntityGridMap PlacePath(EntityGridMap map, Path path)
        {
            foreach (var (x, y) in path.Points)
            {
                map.AddEntity(x, y, _path);
            }
            return map;
        }
        
        EntityGridMap FillEntity<TEntity>(EntityGridMap map) where TEntity : IEntity , new()
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.GetSingleEntity<TEntity>(x, y) != null)
                    {
                        map.AddEntity(x,y, new TEntity());
                    }
                }
            }
            return map;
        }
        
        Path CreatePathNaive(Room start, Room end)
        {
            var points = new List<(int x, int y)>();
            var x = start.X;
            var y = start.Y;
            while (x != end.X)
            {
                points.Add((x, y));
                x += x < end.X ? 1 : -1;
            }
            while (y != end.Y)
            {
                points.Add((x, y));
                y += y < end.Y ? 1 : -1;
            }
            
            // Debug
            foreach (var (point, i) in points.Select((v, i) => (v, i)))
            {
                Debug.Log($"Point i: {i}, X: {point.x}, Y: {point.y}");
            }
            
            return new Path{Points = points.ToArray()};
        }

    }
    
    
    struct Area
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
    }
    struct Room
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
    }

    struct Path
    {
        public (int x, int y)[] Points;
    }
}