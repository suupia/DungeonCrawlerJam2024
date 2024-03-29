﻿#nullable enable
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Sprites;
using VContainer;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes
{
    public class GridEntityPlacer
    {
        readonly CharacterWall _wall;
        readonly CharacterPath _path;
        readonly CharacterRoom _room;
        readonly Stairs _stairs;
        readonly Enemy _enemy;

        [Inject]
        public GridEntityPlacer(
            CharacterWall wall,
            CharacterPath path,
            CharacterRoom room,
            Stairs stairs,
            Enemy enemy
            )
        {
            _wall = wall;
            _path = path;
            _room = room;
            _stairs = stairs;
            _enemy = enemy;
        }

        public DungeonGridMap PlaceEntities(PlainDungeonGridMap plainDungeon)
        {
            var dungeon = new DungeonGridMap(plainDungeon);
            // Tiles
            dungeon = PlaceRooms(dungeon);
            dungeon = PlacePath(dungeon);
            dungeon = PlaceWall(dungeon); // this should be last

            // Entities
            dungeon = PlaceStairs(dungeon);
            dungeon = PlaceEnemies(dungeon);
            dungeon = PlaceTorches(dungeon);

            return dungeon;
        }

        DungeonGridMap PlaceRooms(DungeonGridMap dungeon)
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

        DungeonGridMap PlaceWall(DungeonGridMap dungeon)
        {
            for (int y = 0; y < dungeon.Map.Height; y++)
            {
                for (int x = 0; x < dungeon.Map.Width; x++)
                {
                    if (dungeon.Map.GetSingleTypeList<IGridTile>(x, y).Count == 0)
                    {
                        dungeon.Map.AddEntity(x, y, _wall);
                    }
                }
            }

            return dungeon;
        }

        DungeonGridMap PlacePath(DungeonGridMap dungeon)
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

        DungeonGridMap PlaceStairs(DungeonGridMap dungeon)
        {
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");

            var area = areas[Random.Range(0, areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            dungeon.Map.AddEntity(spawnX, spawnY, _stairs);
            dungeon.StairsPosition = (spawnX, spawnY);
            return dungeon;
        }

        DungeonGridMap PlaceEnemies(DungeonGridMap dungeon)
        {
            // [pre-condition] _areas should not be empty
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");

            var area = areas[Random.Range(0, areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            dungeon.Map.AddEntity(spawnX, spawnY, _enemy);
            dungeon.EnemyPosition = (spawnX, spawnY);
            return dungeon;
        }


        public DungeonGridMap PlaceTorches(DungeonGridMap dungeon)
        {
            // [pre-condition] _areas should not be empty
            const int torchCount = 3;
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");

            var result = new List<(int x, int y)>();
            while (result.Count() < torchCount)
            {
                var area = areas[Random.Range(0, areas.Count())];
                var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
                var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
                var spawnPosition = (spawnX, spawnY);
                if (!result.Contains(spawnPosition)) result.Add(spawnPosition);
            }
            
            foreach (var (x, y) in result)
            {
                dungeon.Map.AddEntity(x, y, new Torch());
            }
            dungeon.TorchPositions = result;
            return dungeon;
        }
    }
}