#nullable enable
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.MapSystem.Scripts;
using DungeonCrawler.MapSystem.Scripts.Entity;
using UnityEngine;
using UnityEngine.Assertions;


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
        
        public EntityGridMap CreateDungeonDivideByX()
        {
            var map = new EntityGridMap(_coordinate);
            var areas = DivideMapByX(map);
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
        public EntityGridMap CreateDungeonDivideByY()
        {
            var map = new EntityGridMap(_coordinate);
            var areas = DivideMapByY(map);
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

        // List<Area> DivideArea(Area area)
        // {
        //     bool isDivideVertically = Random.Range(0, 2) == 0;
        //
        //     var minX = (MinRoomSize + MinRoomMargin * 2);
        //     var maxX = (isDivideVertically ? area.Width : area.Height) - (MinRoomSize + MinRoomMargin * 2);
        //     var divideX = Random.Range(minX, maxX);
        //     var areas = new List<Area>();
        //     areas.Add(new Area{X = 0, Y = 0, Width = divideX, Height = area.Height});
        //     areas.Add(new Area{X = divideX, Y = 0, Width = area.Width - divideX, Height = area.Height});
        //     
        //     // Debug
        //     foreach (var (area, i) in areas.Select((v, i) => (v, i)))
        //     {
        //         Debug.Log($"Area i: {i}, X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");
        //     }
        //     
        //     return areas;
        // }
        
        
        List<Area> DivideMapByX(EntityGridMap map)
        {
            var minX = MinRoomSize + MinRoomMargin * 2; // Ensure that the room can be placed in the left area
            var maxX = map.Width - (MinRoomSize + MinRoomMargin * 2); // Ensure that the room can be placed in the right area
            var divideX = Random.Range(minX, maxX);
            var areas = new List<Area>();
            areas.Add(new Area{X = 0, Y = 0, Width = divideX, Height = map.Height});
            areas.Add(new Area{X = divideX, Y = 0, Width = map.Width - divideX, Height = map.Height});
            
            Assert.IsTrue(areas[0].Width >= MinRoomSize);
            Assert.IsTrue(areas[1].Width >= MinRoomSize);
            
            // Debug
            foreach (var (area, i) in areas.Select((v, i) => (v, i)))
            {
                Debug.Log($"Area i: {i}, X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");
            }
            
            return areas;
        }
        
        List<Area> DivideMapByY(EntityGridMap map)
        {
            var minY = (MinRoomSize + MinRoomMargin * 2);
            var maxY = map.Height - (MinRoomSize + MinRoomMargin * 2);
            var divideY = Random.Range(minY, maxY);
            var areas = new List<Area>();
            areas.Add(new Area{X = 0, Y = 0, Width = map.Width, Height = divideY});
            areas.Add(new Area{X = 0, Y = divideY, Width = map.Width, Height = map.Height - divideY});
            
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
            room.X = Random.Range(area.X + MinRoomMargin, area.X + area.Width - (MinRoomSize + MinRoomMargin * 2));
            room.Y = Random.Range(area.Y + MinRoomMargin, area.Y + area.Height - (MinRoomSize + MinRoomMargin * 2));
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