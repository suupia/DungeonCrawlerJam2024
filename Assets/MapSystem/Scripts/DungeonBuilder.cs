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
        readonly IEntity _room = new CharacterRoom();
        readonly IGridCoordinate _coordinate;

        int _divideCount;
        List<Area> _areas = new ();

        readonly DivideAreaExecutor _divideAreaExecutor;
        public DungeonBuilder(IGridCoordinate coordinate , DivideAreaExecutor divideAreaExecutor)
        {
            _coordinate = coordinate;
            _divideAreaExecutor = divideAreaExecutor;
        }

        public EntityGridMap CreateDungeonDivideByStep()
        {
            var map = new EntityGridMap(_coordinate);
            var areas =_divideCount == 0 ? new List<Area>{GetInitArea(map)} : _divideAreaExecutor.DivideAreaOnce(_areas);
            var paths = areas.SelectMany(area => area.AdjacentAreas.Select(tuple => tuple.path)).ToList();
            map = PlaceRooms(map, areas);
            map = PlacePath(map, paths);
            map = PlaceWall(map);  // this should be last
            DebugAllAreasAdjacentAreas(areas);
            _divideCount++;
            _areas = areas;
            return map;
        }
        
        public void Reset()
        {
            _divideCount = 0;
            _areas = new List<Area>();
        }

        void DebugAllAreasAdjacentAreas(List<Area> areas)
        {
            foreach (var area in areas)
            {
                Debug.LogWarning($"Area: X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");
                foreach (var (adjacentArea, path) in area.AdjacentAreas)
                {
                    Debug.LogWarning($">> AdjacentArea: X: {adjacentArea.X}, Y: {adjacentArea.Y}, Width: {adjacentArea.Width}, Height: {adjacentArea.Height}");
                    Debug.LogWarning($">> Path: {string.Join(',', path.Points.Select(p => $"({p.x},{p.y})"))}");
                }
            }
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
                    // Debug.Log($"PlacePath: X: {x}, Y: {y}");
                }
            }
            return map;
        }


    }

}


public record Area(int X, int Y, int Width, int Height, Room Room, List<(Area area, Path path)> AdjacentAreas);
public record Room(int X, int Y, int Width, int Height);
public record Path(List<(int x, int y)> Points, (bool isDivideByVertical, int coord) DivideInfo);