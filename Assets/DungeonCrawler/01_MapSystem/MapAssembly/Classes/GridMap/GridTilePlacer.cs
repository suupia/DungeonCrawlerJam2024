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
            var areas = dungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");

            var area = areas[Random.Range(0, areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            dungeon.Map.AddEntity(spawnX, spawnY, _entityFactory.CreateEntity<Enemy>(dungeonSwitcher));
            dungeon.InitEnemyPosition = (spawnX, spawnY);
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
            dungeon.InitTorchPositions = result;
            return dungeon;
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