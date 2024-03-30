#nullable enable
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.PlayerMonoAssembly;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Sprites;
using VContainer;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes
{
    public class GridTilePlacer
    {
        readonly CharacterWall _wall;
        readonly CharacterPath _path;
        readonly CharacterRoom _room;

        readonly GridEntityFactory _entityFactory; 
        
        [Inject]
        public GridTilePlacer(
            GridEntityFactory entityFactory
            
        )
        {
            _wall = new CharacterWall();
            _path = new CharacterPath();
            _room = new CharacterRoom();
            _entityFactory = entityFactory;
        }

        public DungeonGridMap PlaceEntities(PlainDungeonGridMap plainDungeon , DungeonSwitcher dungeonSwitcher)
        {
            var dungeon = new DungeonGridMap(plainDungeon);
            // Tiles
            dungeon = PlaceRooms(dungeon);
            dungeon = PlacePath(dungeon);
            dungeon = PlaceWall(dungeon); // this should be last

            // Entities
            dungeon = PlacePlayer(dungeon, dungeonSwitcher);
            dungeon = PlaceStairs(dungeon, dungeonSwitcher);
            dungeon = PlaceEnemies(dungeon, dungeonSwitcher);
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

        DungeonGridMap PlaceStairs(DungeonGridMap dungeon, DungeonSwitcher dungeonSwitcher)
        {
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");

            var area = areas[Random.Range(0, areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            dungeon.Map.AddEntity(spawnX, spawnY, _entityFactory.CreateEntity<Stairs>(dungeonSwitcher));
            dungeon.InitStairsPosition = (spawnX, spawnY);
            return dungeon;
        }

        DungeonGridMap PlaceEnemies(DungeonGridMap dungeon, DungeonSwitcher dungeonSwitcher)
        {
            // [pre-condition] _areas should not be empty
            
            const int enemyCount = 1;
            var spawnPositions = GetSpawnPositions(dungeon, enemyCount);
            foreach (var (x, y) in spawnPositions)
            {
                dungeon.Map.AddEntity(x, y, _entityFactory.CreateEntity<Enemy>(dungeonSwitcher));
            }
            dungeon.InitEnemyPositions = spawnPositions;
            return dungeon;
        }


        public DungeonGridMap PlaceTorches(DungeonGridMap dungeon)
        {
            const int torchCount = 3;
            var spawnPositions = GetSpawnPositions(dungeon, torchCount);
            
            foreach (var (x, y) in spawnPositions)
            {
                dungeon.Map.AddEntity(x, y, new Torch());
            }
            dungeon.InitTorchPositions = spawnPositions;
            return dungeon;
        }

        List<(int, int)> GetSpawnPositions(DungeonGridMap dungeon, int need)
        {
            // [pre-condition] _areas should not be empty
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");

            var result = new List<(int x, int y)>();
            while (result.Count() < need)
            {
                var area = areas[Random.Range(0, areas.Count())];
                var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
                var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
                var spawnPosition = (spawnX, spawnY);
                var isInFrontOfPath = IsInFrontOfPath(dungeon, spawnX, spawnY);
                if (!result.Contains(spawnPosition) && !isInFrontOfPath) result.Add(spawnPosition);
            }

            return result;
        }
        bool IsInFrontOfPath(DungeonGridMap dugeon, int x, int y)
        {
            bool isInFrontOfPath = false;
            foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                if (dugeon.Map.GetSingleEntity<CharacterPath>(x + dx, y + dy) is CharacterPath)
                {
                    Debug.Log($"cannot place entity at ({x}, {y}) because ({x+dx}, {y+dy}) is path");
                    isInFrontOfPath = true;
                    break;
                }
            }

            return isInFrontOfPath;
        }

        DungeonGridMap PlacePlayer(DungeonGridMap dungeon, DungeonSwitcher dungeonSwitcher)
        {
            // [pre-condition] _areas should not be empty
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");
            
            var area = areas[Random.Range(0,areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            var player = _entityFactory.CreateEntity<Player>(dungeonSwitcher);
            dungeon.Map.AddEntity(spawnX, spawnY, player);
            Debug.Log($"Player spawn position: {spawnX}, {spawnY}");
            dungeon.InitPlayerPosition = (spawnX, spawnY);
            return dungeon;
        }
    }
}