#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes
{

    public class GridEntityPlacer
    {
        readonly IGridEntity _wall;
        readonly IGridEntity _path;
        readonly IGridEntity _room;
        
        public GridEntityPlacer()
        {
            _wall = new CharacterWall();
            _path = new CharacterPath();
            _room = new CharacterRoom();
        }
        
        public DungeonGridMap PlaceEntities(DungeonGridMap dungeon)
        {
            dungeon = PlaceRooms(dungeon);
            dungeon = PlacePath(dungeon);
            dungeon = PlaceWall(dungeon);  // this should be last
            return dungeon;
        }
        
        public DungeonGridMap PlaceRooms(DungeonGridMap dungeon)
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
        
        public DungeonGridMap PlaceWall(DungeonGridMap dungeon)
        {
            for (int y = 0; y < dungeon.Map.Height; y++)
            {
                for (int x = 0; x < dungeon.Map.Width; x++)
                {
                    if (dungeon.Map.GetSingleTypeList<IGridEntity>(x, y).Count == 0)
                    {
                        dungeon.Map.AddEntity(x,y, _wall);
                    }
                }
            }
            return dungeon;
        }
        
        public DungeonGridMap PlacePath(DungeonGridMap dungeon)
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

    }
}