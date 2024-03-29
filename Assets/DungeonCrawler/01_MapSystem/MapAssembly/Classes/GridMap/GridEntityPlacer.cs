#nullable enable
using System.Linq;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes
{

    public class GridEntityPlacer
    {
        readonly IGridTile _wall;
        readonly IGridTile _path;
        readonly IGridTile _room;
        readonly IGridEntity _stairs;

        public GridEntityPlacer()
        {
            _wall = new CharacterWall();
            _path = new CharacterPath();
            _room = new CharacterRoom();
        }
        
        public PlainDungeonGridMap PlaceEntities(PlainDungeonGridMap plainDungeon)
        {
            // Tiles
            plainDungeon = PlaceRooms(plainDungeon);
            plainDungeon = PlacePath(plainDungeon);
            plainDungeon = PlaceWall(plainDungeon);  // this should be last
            
            // Entities
            plainDungeon = PlaceStairs(plainDungeon);
            
            return plainDungeon;
        }
        
        PlainDungeonGridMap PlaceRooms(PlainDungeonGridMap plainDungeon)
        {
            var areas = plainDungeon.Areas;  
            foreach (var area in areas)
            {
                var room = area.Room;
                for (int y = room.Y; y < room.Y + room.Height; y++)
                {
                    for (int x = room.X; x < room.X + room.Width; x++)
                    {
                        plainDungeon.Map.AddEntity(x, y, _room);
                    }
                }
            }

            return plainDungeon;
        }
        
        PlainDungeonGridMap PlaceWall(PlainDungeonGridMap plainDungeon)
        {
            for (int y = 0; y < plainDungeon.Map.Height; y++)
            {
                for (int x = 0; x < plainDungeon.Map.Width; x++)
                {
                    if (plainDungeon.Map.GetSingleTypeList<IGridTile>(x, y).Count == 0)
                    {
                        plainDungeon.Map.AddEntity(x,y, _wall);
                    }
                }
            }
            return plainDungeon;
        }
        
        PlainDungeonGridMap PlacePath(PlainDungeonGridMap plainDungeon)
        {
            var paths = plainDungeon.Paths;
            foreach (var path in paths)
            {
                foreach (var (x, y) in path.Points)
                {
                    plainDungeon.Map.AddEntity(x, y, _path);
                }
            }
            return plainDungeon;
        }

        PlainDungeonGridMap PlaceStairs(PlainDungeonGridMap plainDungeon)
        {
            var areas = plainDungeon.Areas;
            Assert.IsTrue(areas.Count > 0);
            Debug.Log($"ares: {string.Join(",", areas.Select(area => area.Room))}");
            
            var area = areas[Random.Range(0,areas.Count())];
            var spawnX = Random.Range(area.Room.X, area.Room.X + area.Room.Width);
            var spawnY = Random.Range(area.Room.Y, area.Room.Y + area.Room.Height);
            plainDungeon.Map.AddEntity(spawnX, spawnY, _stairs);
            return plainDungeon;
        }
        

    }
}