#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.BaseCommands;
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.MapSystem.Scripts;
using DungeonCrawler.MapSystem.Scripts.Entity;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;


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
        const int MinRoomMargin = 2;  // This should be larger than 2, because the rooms are connected to each other by a path.

        const int MinAreaSize = MinRoomSize + MinRoomMargin * 2;
        public DungeonBuilder(IGridCoordinate coordinate)
        {
            _coordinate = coordinate;
        }
        
        public EntityGridMap CreateDungeonDivide()
        {
            var map = new EntityGridMap(_coordinate);
            var area = new Area(
                X: 0,
                Y: 0,
                Width: map.Width,
                Height: map.Height,
                Room: new Room(
                    X: MinRoomMargin,
                    Y: MinRoomMargin,
                    Width: map.Width - MinRoomMargin * 2,
                    Height: map.Height - MinRoomMargin * 2
                )
            );
            var (areas, paths) = LoopDivideArea(area);   
            // map = PlaceRooms(map, rooms);
            map = PlaceRoomsRe(map, areas);
            map = PlacePath(map, paths);
            // map = PlacePath(map, CreatePathNaive(rooms[0], rooms[1]));
            map = PlaceWall(map);  // this should be last
            
            return map;
        }

        (Area area1, Area area2, Path path, bool idDivided) DivideArea(Area area)
        {
            if(!CanDivideArea(area))
            {
                return (area, area, new Path(new List<(int x, int y)>()), false);
            }
            
            // Preconditions
            Assert.IsTrue(area.Width > MinAreaSize*2 || area.Height > MinAreaSize*2);


            bool isDivideByVertical = Random.Range(0, 2) == 0;
            if(area.Width > MinAreaSize*2)
            {
                isDivideByVertical = true;
            }else if (area.Height > MinAreaSize*2)
            {
                isDivideByVertical = false;
            }
            int areaSize = isDivideByVertical ? area.Width : area.Height;
            var minX = MinAreaSize; // Ensure that the room can be placed in the left area
            var maxX = areaSize - MinAreaSize; // Ensure that the room can be placed in the right area
            Assert.IsTrue(minX < maxX, $"minX: {minX}, maxX: {maxX}, areaSize: {areaSize}");
            var divideX = Random.Range(minX, maxX);
            var (dividedArea1, dividedArea2) = isDivideByVertical
                ? (
                    new Area(area.X, area.Y, divideX, area.Height, null),
                    new Area(area.X + divideX, area.Y, area.Width - divideX, area.Height, null)
                )
                : (
                    new Area(area.X, area.Y, area.Width, divideX, null),
                    new Area(area.X, area.Y + divideX, area.Width, area.Height - divideX, null)
                );
            
            Debug.Log($"dividedArea1: X: {dividedArea1.X}, Y: {dividedArea1.Y}, Width: {dividedArea1.Width}, Height: {dividedArea1.Height}");
            Debug.Log($"dividedArea2: X: {dividedArea2.X}, Y: {dividedArea2.Y}, Width: {dividedArea2.Width}, Height: {dividedArea2.Height}");
            var (area1, area2) = AddRoomEach(dividedArea1, dividedArea2);
            var path = CreatePath(area1, area2, divideX,isDivideByVertical);

            Assert.IsTrue(dividedArea1.Width >= MinAreaSize);
            Assert.IsTrue(dividedArea2.Width >= MinAreaSize);
            Assert.IsTrue(dividedArea1.Height >= MinAreaSize);
            Assert.IsTrue(dividedArea2.Height >= MinAreaSize);
            
            // Debug
            Debug.Log($"DivideArea: X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");
            Debug.Log($"dividedArea1: X: {dividedArea1.X}, Y: {dividedArea1.Y}, Width: {dividedArea1.Width}, Height: {dividedArea1.Height}");
            Debug.Log($"dividedArea2: X: {dividedArea2.X}, Y: {dividedArea2.Y}, Width: {dividedArea2.Width}, Height: {dividedArea2.Height}");
            
            return (area1, area2 , path, true);
            
            // Local Functions
            bool CanDivideArea(Area area)
            {
                return area.Width > MinAreaSize*2 || area.Height > MinAreaSize*2;            
            }
            
            (Area area1, Area area2) AddRoomEach(Area area1, Area area2)
            {
                return (AddRoom(area1), AddRoom(area2));
            }
            

        }
        Path CreatePath(Area area1, Area area2, int divideX, bool isDivideByVertical)
        {
            var path = new Path(new List<(int x, int y)>());
            if (isDivideByVertical)
            {
                var randY1 = Random.Range(area1.Room.Y + 1, area1.Room.Y + area1.Room.Height - 1);  // Excluding both ends
                var (x1, y1) = (area1.Room.X + area1.Room.Width, randY1);
                var randY2 = Random.Range(area2.Room.Y + 1, area2.Room.Y + area2.Room.Height - 1);  // Excluding both ends
                var (x2, y2) = (area2.Room.X, randY2);
                while (x1 < divideX)
                {
                    path.Points.Add((x1, y1));
                    x1++;
                }
                while (x2 > divideX)
                {
                    path.Points.Add((x2, y2));
                    x2--;
                }
                var minY = Mathf.Min(y1, y2);
                var maxY = Mathf.Max(y1, y2);
                while (minY <= maxY)
                {
                    path.Points.Add((divideX, minY));
                    minY++;
                }
            }
            else
            {
                var randX1 = Random.Range(area1.Room.X + 1, area1.Room.X + area1.Room.Width - 1);  // Excluding both ends
                var (x1, y1) = (randX1, area1.Room.Y + area1.Room.Height);
                var randX2 = Random.Range(area2.Room.X + 1, area2.Room.X + area2.Room.Width - 1);  // Excluding both ends
                var (x2, y2) = (randX2, area2.Room.Y);
                while (y1 < divideX)
                {
                    path.Points.Add((x1, y1));
                    y1++;
                }
                while (y2 > divideX)
                {
                    path.Points.Add((x2, y2));
                    y2--;
                }
                var minX = Mathf.Min(x1, x2);
                var maxX = Mathf.Max(x1, x2);
                while (minX <= maxX)
                {
                    path.Points.Add((minX, divideX));
                    minX++;
                }
            }
            Debug.Log($"paths: {string.Join(',', path.Points.Select(p => $"({p.x},{p.y})"))}");
            return path;
        }

        // (List<Area> areas, List<Path> paths) RecursiveDivideArea(Area initArea,List<Path> paths ,int counter = 0)
        // {
        //     Debug.Log($"initArea: X: {initArea.X}, Y: {initArea.Y}, Width: {initArea.Width}, Height: {initArea.Height}");
        //     var (area1, area2, path ,isDivided ) = DivideArea(initArea);
        //     Debug.Log($"isDivided: {isDivided}");
        //     var areas = new List<Area>();
        //     paths.Add(path);
        //     if (isDivided)
        //     {
        //         var (areas1, paths1) = RecursiveDivideArea(area1, paths,counter + 1);
        //         var (areas2, paths2) = RecursiveDivideArea(area2, paths, counter + 1);
        //         areas.AddRange(areas1);
        //         areas.AddRange(areas2);
        //         paths.AddRange(paths1);
        //         paths.AddRange(paths2);
        //     }
        //     else
        //     {
        //         areas.Add(area1);
        //     }
        //     Debug.Log($"areas.Count: {areas.Count}");
        //     Debug.Log($"paths.Count: {paths.Count}");
        //     return (areas, paths);
        // }
        
        (List<Area> areas, List<Path> paths) LoopDivideArea(Area initArea ,int counter = 0)
        {
            const int divideLimit = 2;
            var areas = new List<Area>(){initArea}; // todo : use priority queue
            var paths = new List<Path>();
            for(int i = 0; i< divideLimit; i++)
            {
                Debug.Log($"counter: {counter}");
                var frontArea = areas.First();
                var (area1, area2, path, isDivided) = DivideArea(frontArea);
                if (isDivided)
                {
                    areas.RemoveAt(0);
                    areas.Add(area1);
                    areas.Add(area2);
                    paths.Add(path);
                }
                else
                {
                    // what should I do?
                }
            }
            return (areas, paths);
            
            
            //
            // Debug.Log($"initArea: X: {initArea.X}, Y: {initArea.Y}, Width: {initArea.Width}, Height: {initArea.Height}");
            // var (area1, area2, path ,isDivided ) = DivideArea(initArea);
            // Debug.Log($"isDivided: {isDivided}");
            // paths.Add(path);
            // if (isDivided)
            // {
            //     var (areas1, paths1) = RecursiveDivideArea(area1, paths,counter + 1);
            //     var (areas2, paths2) = RecursiveDivideArea(area2, paths, counter + 1);
            //     areas.AddRange(areas1);
            //     areas.AddRange(areas2);
            //     paths.AddRange(paths1);
            //     paths.AddRange(paths2);
            // }
            // else
            // {
            //     areas.Add(area1);
            // }
            // Debug.Log($"areas.Count: {areas.Count}");
            // Debug.Log($"paths.Count: {paths.Count}");
            // return (areas, paths);
        }

        bool CanDivideArea(Area area)
        {
            return area.Width >= MinAreaSize*2 || area.Height >= MinAreaSize*2;            
        }

        record Area(int X, int Y, int Width, int Height, Room Room);
        record Room(int X, int Y, int Width, int Height);

        Area AddRoom(Area area)
        {
            var room = GenerateRandomRoom(area);
            return area with { Room = room };
        }

        Room GenerateRandomRoom(Area area)
        {
            var roomX = UnityEngine.Random.Range(area.X + MinRoomMargin, area.X + area.Width - (MinRoomSize + MinRoomMargin * 2));
            var roomY = UnityEngine.Random.Range(area.Y + MinRoomMargin, area.Y + area.Height - (MinRoomSize + MinRoomMargin * 2));
            var roomWidth = UnityEngine.Random.Range(MinRoomSize, Mathf.Min(MaxRoomSize, area.Width - (roomX - area.X)));
            var roomHeight = UnityEngine.Random.Range(MinRoomSize, Mathf.Min(MaxRoomSize, area.Height - (roomY - area.Y)));
    
            return new Room(roomX, roomY, roomWidth, roomHeight);
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
        EntityGridMap PlaceRoomsRe(EntityGridMap map, List<Area> areas)
        {
            foreach (var area in areas)
            {
                var room = area.Room;
                for (int y = room.Y; y < room.Y + room.Height; y++)
                {
                    for (int x = room.X; x < room.X + room.Width; x++)
                    {
                        map.AddEntity(x, y, _path);
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
        
        EntityGridMap PlacePath(EntityGridMap map, List<Path> paths)
        {
            foreach (var path in paths)
            {
                foreach (var (x, y) in path.Points)
                {
                    map.AddEntity(x, y, _path);
                    Debug.Log($"PlacePath: X: {x}, Y: {y}");
                }
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
            
            return new Path(points);
        }

    }
    
    
    record Area(int X, int Y, int Width, int Height, Room Room);
    record Room(int X, int Y, int Width, int Height);
    record Path(List<(int x, int y)> Points);
}