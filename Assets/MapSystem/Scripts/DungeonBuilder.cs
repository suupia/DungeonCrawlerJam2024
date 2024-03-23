﻿#nullable enable
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
        const int MinRoomMargin = 1;
        const int MinAreaSize = MinRoomSize + MinRoomMargin * 2;
        public DungeonBuilder(IGridCoordinate coordinate)
        {
            _coordinate = coordinate;
        }
        
        public EntityGridMap CreateDungeonDivide()
        {
            var map = new EntityGridMap(_coordinate);
            var area = new Area
            {
                X = 0,
                Y = 0,
                Width = map.Width,
                Height = map.Height,
                Rooms =  new Room[]
                {
                    new Room
                    {
                        X = 0,
                        Y = 0,
                        Width = map.Width - MinRoomMargin * 2,
                        Height = map.Height - MinRoomMargin * 2
                    }
                },
            };
            // map = PlaceRooms(map, rooms);
            map = PlaceRoomsRe(map, area);
            // map = PlacePath(map, CreatePathNaive(rooms[0], rooms[1]));
            map = PlaceWall(map);  // this should be last
            
            return map;
        }

        (Area area, bool idDivided) DivideArea(Area area)
        {
            if(!CanDivideArea(area))
            {
                return (area, false);
            }
            
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
            
            Debug.Log($"result.Item1: X: {result.Item1.X}, Y: {result.Item1.Y}, Width: {result.Item1.Width}, Height: {result.Item1.Height}");
            Debug.Log($"result.Item2: X: {result.Item2.X}, Y: {result.Item2.Y}, Width: {result.Item2.Width}, Height: {result.Item2.Height}");
            var finalResult = AddRoomEach(result.Item1, result.Item2);
            var mergedArea = MergeArea(finalResult.area1, finalResult.area2);

            Assert.IsTrue(result.Item1.Width >= MinRoomSize);
            Assert.IsTrue(result.Item2.Width >= MinRoomSize);
            Assert.IsTrue(result.Item1.Height >= MinRoomSize);
            Assert.IsTrue(result.Item2.Height >= MinRoomSize);
            
            // Debug
            Debug.Log($"DivideArea: X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");
            Debug.Log($"result.Item1: X: {result.Item1.X}, Y: {result.Item1.Y}, Width: {result.Item1.Width}, Height: {result.Item1.Height}");
            Debug.Log($"result.Item2: X: {result.Item2.X}, Y: {result.Item2.Y}, Width: {result.Item2.Width}, Height: {result.Item2.Height}");
            
            return (mergedArea, true);
            
            // Local Functions
            bool CanDivideArea(Area area)
            {
                return area.Width >= MinAreaSize*2 || area.Height >= MinAreaSize*2;            
            }
            
            (Area area1, Area area2) AddRoomEach(Area area1, Area area2)
            {
                return (AddRoom(area1), AddRoom(area2));
            }
            
            Area MergeArea(Area area1, Area area2)
            {
                return new Area
                {
                    X = Mathf.Min(area1.X, area2.X),
                    Y = Mathf.Min(area1.Y, area2.Y),
                    Width = area1.Width + area2.Width,
                    Height = area1.Height + area2.Height,
                };
            }
        }

        Area RecursiveDivideArea(Area initArea ,int counter = 0)
        {
            Debug.Log($"initArea: X: {initArea.X}, Y: {initArea.Y}, Width: {initArea.Width}, Height: {initArea.Height}");
            var (dividedArea, idDivided) = DivideArea(initArea);
            Debug.Log($"isDivided: {idDivided}");
            if (idDivided)
            {
               return RecursiveDivideArea(dividedArea, counter+1);
            }
            return dividedArea;
            
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

        Area AddRoom(Area area)
        {
            var room = new Room();
            room.X = Random.Range(area.X + MinRoomMargin, area.X + area.Width - (MinRoomSize + MinRoomMargin * 2));
            room.Y = Random.Range(area.Y + MinRoomMargin, area.Y + area.Height - (MinRoomSize + MinRoomMargin * 2));
            room.Width = Random.Range(MinRoomSize, Mathf.Min( MaxRoomSize, area.Width - (room.X-area.X)));
            room.Height = Random.Range(MinRoomSize, Mathf.Min( MaxRoomSize, area.Height - (room.Y-area.Y)));
            Debug.Log($"area.Rooms.Length: {area.Rooms.Length}");
            Debug.Log($"area.Rooms: {area.Rooms}");
            var result = new Area()
            {
                X = area.X,
                Y = area.Y,
                Width = area.Width,
                Height = area.Height,
                Rooms = area.Rooms.Length == 0 ? new Room[]{room} : area.Rooms.Append(room).ToArray()
            };
            return result;
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
        EntityGridMap PlaceRoomsRe(EntityGridMap map, Area area)
        {
            foreach (var room in area.Rooms)
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
    
    
    record Area
    {
        public int X, Y, Width, Height;
        public Room[] Rooms = Array.Empty<Room>();
    }
    record Room
    {
        public int X, Y, Width, Height;
        public (int x, int y) Leader;
    }

    record Path
    {
        public (int x, int y)[] Points;
    }
}