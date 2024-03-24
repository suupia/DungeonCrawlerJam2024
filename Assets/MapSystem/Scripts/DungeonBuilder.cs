#nullable enable
using System;
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
        readonly IEntity _wall;
        readonly IEntity _path;
        readonly IEntity _room;
        readonly IGridCoordinate _coordinate;

        int _divideCount;
        List<Area> _areas = new ();

        readonly DivideAreaExecutor _divideAreaExecutor;

        public DungeonBuilder(
            IGridCoordinate coordinate,
            DivideAreaExecutor divideAreaExecutor,
            IEntity wall,
            IEntity path,
            IEntity room)
        {
            _coordinate = coordinate;
            _divideAreaExecutor = divideAreaExecutor;
            _wall = wall;
            _path = path;
            _room = room;
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
                    if (map.GetSingleTypeList<IEntity>(x, y).Count == 0)
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


    }
    public record Area(int X, int Y, int Width, int Height, Room Room, List<(Area area, Path path)> AdjacentAreas);
    public record Room(int X, int Y, int Width, int Height);
    public record Path(List<(int x, int y)> Points, (bool isDivideByVertical, int coord) DivideInfo);
}


