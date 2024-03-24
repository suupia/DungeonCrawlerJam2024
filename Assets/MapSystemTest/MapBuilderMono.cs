#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.MapSystem.Scripts;
using DungeonCrawler.MapSystem.Scripts.Entity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DungeonCrawler
{
    // This script is for TESTING purposes only.
    public class MapBuilderMono : MonoBehaviour
    {
        [SerializeField] TileMono tilePrefab = null!;
        [SerializeField] Button buildButton = null!;
        [SerializeField] Button buildButton2 = null!;
        [SerializeField] Button button3 = null!;

        readonly List<GameObject> _pool = new List<GameObject>();

        void Start()
        {
            // buildButton.onClick.AddListener(() =>
            // {
            //     Debug.Log("Build Dungeon");
            //     
            //     // Domain
            //     var map = new EntityGridMap(new SquareGridCoordinate(20, 10));
            //     var dungeonBuilder = new DungeonBuilder(new SquareGridCoordinate(20, 10));
            //     var area1 = new Area(
            //         X: 0,
            //         Y: 0,
            //         Width: map.Width/2,
            //         Height: map.Height,
            //         Room: new Room(
            //             X: DungeonBuilder.MinRoomMargin,
            //             Y: DungeonBuilder.MinRoomMargin,
            //             Width: map.Width/2 - DungeonBuilder.MinRoomMargin * 2,
            //             Height: map.Height - DungeonBuilder.MinRoomMargin * 2
            //         ),
            //         AdjacentAreas: new List<(Area area, Path path)>()
            //     );
            //     var area2 = new Area(
            //         X: map.Width/2,
            //         Y: 0,
            //         Width: map.Width/2,
            //         Height: map.Height,
            //         Room: new Room(
            //             X: DungeonBuilder.MinRoomMargin + map.Width/2,
            //             Y: DungeonBuilder.MinRoomMargin,
            //             Width: map.Width/2 - DungeonBuilder.MinRoomMargin * 2,
            //             Height: map.Height - DungeonBuilder.MinRoomMargin * 2
            //         ),
            //         AdjacentAreas: new List<(Area area, Path path)>()
            //     );
            //     var path = dungeonBuilder.CreatePath(area1, area2, map.Width / 2, true);
            //     area1.AdjacentAreas.Add((area2, path));
            //     area2.AdjacentAreas.Add((area1, path));
            //     
            //     // place paths and walls 
            //     var areas = new List<Area> {area1, area2};
            //     var paths = areas.SelectMany(area => area.AdjacentAreas.Select(tuple => tuple.path)).ToList();
            //     map = dungeonBuilder.PlaceRooms(map, areas);
            //     map = dungeonBuilder.PlacePath(map, paths);
            //     map = dungeonBuilder.PlaceWall(map);  // this should be last
            //     
            //     // Mono
            //     foreach(var tile in _pool)
            //     {
            //         Destroy(tile);
            //     }
            //     for(int y = 0; y < map.Height; y++)
            //     {
            //         for(int x = 0; x < map.Width; x++)
            //         {
            //             var tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
            //             _pool.Add(tile.gameObject);
            //             if(map.GetSingleEntity<IEntity>(x,y) is {} entity)
            //             {
            //                 tile.SetSprite(entity);
            //             }
            //             # region Comment
            //             // This has the same meaning as the following code:
            //             
            //             // var entity = map.GetSingleEntity<IEntity>(x,y);
            //             // if(entity != null)
            //             // {
            //             //     tile.SetSprite(entity);
            //             // }
            //             # endregion
            //             
            //         }
            //     }
            // });
            
            // Domain
            var map = new EntityGridMap(new SquareGridCoordinate(15, 15));
            var divideAreaExecutor = new DivideAreaExecutor();
            var dungeonBuilder = new DungeonBuilder(new SquareGridCoordinate(15, 15), divideAreaExecutor);
            
            buildButton2.onClick.AddListener(() =>
            {
                Debug.Log("Build Dungeon");
                map = dungeonBuilder.CreateDungeonDivideByStep();

                // Mono
                foreach(var tile in _pool)
                {
                    Destroy(tile);
                }
                for(int y = 0; y < map.Height; y++)
                {
                    for(int x = 0; x < map.Width; x++)
                    {
                        var tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                        _pool.Add(tile.gameObject);
                        if(map.GetSingleEntity<IEntity>(x,y) is {} entity)
                        {
                            tile.SetSprite(entity);
                        }

                    }
                }
            });
            
            button3.onClick.AddListener(() =>
            {
                Debug.Log($"Reset Dungeon");
                // Mono
                foreach(var tile in _pool)
                {
                    Destroy(tile);
                }
                dungeonBuilder.Reset();   
            });
        }
        
    }
}
