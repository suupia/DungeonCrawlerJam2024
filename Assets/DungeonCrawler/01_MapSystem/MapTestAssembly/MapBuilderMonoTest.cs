#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DungeonCrawler.MapTestAssembly
{
    // This script is for TESTING purposes only.
    public class MapBuilderMonoTest : MonoBehaviour
    {
        // [SerializeField] TileMonoTest tilePrefab = null!;
        // [SerializeField] Button divideButton = null!;
        // [SerializeField] Button resetButton = null!;
        //
        // readonly List<TileMonoTest> _pool = new List<TileMonoTest>();
        //
        // IGridCoordinate _coordinate = null!;
        // DivideAreaExecutor _divideAreaExecutor = null!;
        // DungeonBuilder _dungeonBuilder = null!;
        //
        // EntityGridMap? _map;
        //     
        // void Awake()
        // {
        //     // Domain
        //     _coordinate = new SquareGridCoordinate(50, 50);
        //     _divideAreaExecutor = new DivideAreaExecutor();
        //     _dungeonBuilder = new DungeonBuilder(
        //         _divideAreaExecutor, _coordinate, new GridEntityPlacer()
        //     );
        // }
        //
        // void Start()
        // {
        //     divideButton.onClick.AddListener(
        //         CreateDungeonByStep
        //     );
        //
        //     resetButton.onClick.AddListener(() =>
        //     {
        //         DestroyAllTiles();
        //     });
        //     
        //     for(int i = 0; i<_coordinate.Length ; i++)
        //     {
        //         var vector = _coordinate.ToVector(i);
        //         Debug.Log($"subscript:{i} x:{vector.x} y:{vector.y}");
        //         var tile = Instantiate(tilePrefab, new Vector3(vector.x, 0, vector.y), Quaternion.identity);
        //         _pool.Add(tile);
        //     }
        // }
        //
        // void CreateDungeonByStep()
        // {
        //     Debug.Log("CreateDungeonByStep");
        //     DestroyAllTiles();
        //     _map ??= new EntityGridMap(_coordinate);
        //     var dungeon = new PlainDungeonGridMap(_map, new List<Area>(), new List<Path>());
        //     _map = _dungeonBuilder.CreateDungeonByStep(dungeon).Map;
        //     UpdateSprites(_map);
        // }
        //
        // void DestroyAllTiles()
        // {
        //     foreach(var tile in _pool)
        //     {
        //         Destroy(tile);
        //     }
        // }
        //
        // void UpdateSprites(EntityGridMap map)
        // {
        //
        //     for(int i = 0; i<_coordinate.Length ; i++)
        //     {
        //         var vector = _coordinate.ToVector(i);
        //         var (x, y) = (vector.x, vector.y);
        //         var tile = _pool[i];
        //         if(map.GetSingleEntity<IGridTile>(x,y) is {} entity)
        //         {
        //             tile.SetSprite(entity);
        //         }
        //             
        //         
        //         if (map.GetAllTypeList(x, y).Count() != 0)
        //         {
        //             string result = "";
        //             var allEntityList = map.GetAllTypeList(x, y).ToList();
        //             foreach (var entity1 in allEntityList)
        //             {
        //                 int count = allEntityList.Count(e => e.ToString() == entity1.ToString());
        //         
        //                 result += entity1.ToString() + $"({count})\n";
        //             }
        //         
        //             tile.SetDebugText(result);
        //         }
        //     }
        // }
        //
        // List<Area> CreateTestAreas(EntityGridMap map)
        // {
        //     var area1 = new Area(
        //         X: 0,
        //         Y: 0,
        //         Width: map.Width/2,
        //         Height: map.Height,
        //         Room: new Room(
        //             X: DivideAreaExecutor.MinRoomMargin,
        //             Y: DivideAreaExecutor.MinRoomMargin,
        //             Width: map.Width/2 - DivideAreaExecutor.MinRoomMargin * 2,
        //             Height: map.Height - DivideAreaExecutor.MinRoomMargin * 2
        //         ),
        //         AdjacentAreas: new List<(Area area, Path path)>()
        //     );
        //     var area2 = new Area(
        //         X: map.Width/2,
        //         Y: 0,
        //         Width: map.Width/2,
        //         Height: map.Height/2,
        //         Room: new Room(
        //             X: DivideAreaExecutor.MinRoomMargin + map.Width/2,
        //             Y: DivideAreaExecutor.MinRoomMargin,
        //             Width: map.Width/2 - DivideAreaExecutor.MinRoomMargin * 2,
        //             Height: map.Height/2 - DivideAreaExecutor.MinRoomMargin * 2
        //         ),
        //         AdjacentAreas: new List<(Area area, Path path)>()
        //     );
        //     var area3 = new Area(
        //         X: map.Width/2,
        //         Y: map.Height/2,
        //         Width: map.Width/2,
        //         Height: map.Height/2,
        //         Room: new Room(
        //             X: DivideAreaExecutor.MinRoomMargin + map.Width/2,
        //             Y: DivideAreaExecutor.MinRoomMargin + map.Height/2,
        //             Width: map.Width/2 - DivideAreaExecutor.MinRoomMargin * 2,
        //             Height: map.Height/2 - DivideAreaExecutor.MinRoomMargin * 2
        //         ),
        //         AdjacentAreas: new List<(Area area, Path path)>()
        //     );
        //     return new List<Area>() {area1, area2, area3};
        //         
        // }
        
    }
}
