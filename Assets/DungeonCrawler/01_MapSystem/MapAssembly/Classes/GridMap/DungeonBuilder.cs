#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
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
        readonly DivideAreaExecutor _divideAreaExecutor;
        readonly IGridCoordinate _coordinate;
        readonly GridTilePlacer _gridTilePlacer;
        public DungeonBuilder(
            DivideAreaExecutor divideAreaExecutor,
            IGridCoordinate coordinate,
            GridTilePlacer gridTilePlacer
            )
        {
            _divideAreaExecutor = divideAreaExecutor;
            _coordinate = coordinate;
            _gridTilePlacer = gridTilePlacer;
        }

        
        public DungeonGridMap CreateDungeon()
        {
            var map = new EntityGridMap(_coordinate);
            var plainDungeon = new PlainDungeonGridMap(map,new List<Area>(), new List<Path>());
            for(int i = 0; i < DivideCount; i++)
            {
                plainDungeon = CreateDungeonByStep(plainDungeon);
            }
            var dungeon = _gridTilePlacer.PlaceEntities(plainDungeon);
            return dungeon;
        }
        
        public PlainDungeonGridMap CreateDungeonByStep(PlainDungeonGridMap plainDungeon)
        {
            var areas = !plainDungeon.Areas.Any()
                ? new List<Area> { GetInitArea(plainDungeon.Map) }
                : _divideAreaExecutor.DivideAreaOnce(new List<Area>(plainDungeon.Areas));
            var paths = areas.SelectMany(area => area.AdjacentAreas.Select(tuple => tuple.path)).ToList();
            plainDungeon.Map.ClearMap();
            var nextDungeon = new PlainDungeonGridMap(plainDungeon.Map, areas, paths);
            
            return nextDungeon;
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

    }
    public record Area(int X, int Y, int Width, int Height, Room Room, List<(Area area, Path path)> AdjacentAreas);
    public record Room(int X, int Y, int Width, int Height);
    public record Path(List<(int x, int y)> Points, (bool isDivideByVertical, int coord) DivideInfo);
}


