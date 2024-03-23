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
        const int MinAreaSize = MinRoomSize + MinRoomMargin * 2;
        public DungeonBuilder(IGridCoordinate coordinate)
        {
            _coordinate = coordinate;
        }
        
        public EntityGridMap CreateDungeonDivide()
        {
            var map = new EntityGridMap(_coordinate);
            var areas = RecursiveDivideArea(new Area()
            {
                X = 0,
                Y = 0,
                Width = map.Width,
                Height = map.Height
            });
            Debug.Log($"areas.count: {areas.Count}");
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
            // map = PlacePath(map, CreatePathNaive(rooms[0], rooms[1]));
            map = PlaceWall(map);  // this should be last
            
            return map;
        }

        (Area area1, Area area2) DivideArea(Area area)
        {
            // Preconditions
            Assert.IsTrue(area.Width >= MinAreaSize*2 || area.Height >= MinAreaSize*2);
            
            bool isDivideByVertical = Random.Range(0, 2) == 0;
            if(area.Width < MinAreaSize*2)
            {
                isDivideByVertical = false;
            }else if (area.Height < MinAreaSize*2)
            {
                isDivideByVertical = true;
            }
            int areaSize = isDivideByVertical ? area.Width : area.Height;
            var minX = MinAreaSize; // Ensure that the room can be placed in the left area
            var maxX = areaSize - MinAreaSize; // Ensure that the room can be placed in the right area
            var divideX = Random.Range(minX, maxX);
            var result = isDivideByVertical ?
                (
                    new Area{X = area.X, Y = area.Y, Width = divideX, Height = area.Height},
                    new Area{X = area.X + divideX, Y = area.Y, Width = area.Width - divideX, Height = area.Height}
                )
                :
                (
                    new Area{X = area.X, Y = area.Y, Width = area.Width, Height = divideX},
                    new Area{X = area.X, Y = area.Y + divideX, Width = area.Width, Height = area.Height - divideX}
                );

            Assert.IsTrue(result.Item1.Width >= MinRoomSize);
            Assert.IsTrue(result.Item2.Width >= MinRoomSize);
            Assert.IsTrue(result.Item1.Height >= MinRoomSize);
            Assert.IsTrue(result.Item2.Height >= MinRoomSize);
            
            // Debug
            Debug.Log($"DivideArea: X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");
            Debug.Log($"result.Item1.: X: {result.Item1.X}, Y: {result.Item1.Y}, Width: {result.Item1.Width}, Height: {result.Item1.Height}");
            Debug.Log($"result.Item2: X: {result.Item2.X}, Y: {result.Item2.Y}, Width: {result.Item2.Width}, Height: {result.Item2.Height}");
            
            return result;
        }

        List<Area> RecursiveDivideArea(Area initArea ,int counter = 0)
        {
            Debug.Log($"initArea: X: {initArea.X}, Y: {initArea.Y}, Width: {initArea.Width}, Height: {initArea.Height}");
            var result = new List<Area>();
            if (CanDivideArea(initArea))
            {
                var (area1,area2) = DivideArea(initArea);
                result.AddRange(RecursiveDivideArea(area1,counter+1));
                result.AddRange(RecursiveDivideArea(area2,counter+1));
            }
            else
            {
                result.Add(initArea);
            }
            if (counter >= 3) return result;

            return result;
        }

        bool CanDivideArea(Area area)
        {
            return area.Width >= MinAreaSize*2 || area.Height >= MinAreaSize*2;            
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
        public int X, Y, Width, Height;
    }
    struct Room
    {
        public int X, Y, Width, Height;
        public (int x, int y) Leader;
    }

    struct Path
    {
        public (int x, int y)[] Points;
    }
}