#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.MapSystem.Scripts;
using DungeonCrawler.MapSystem.Scripts.Entity;
using Unity.Plastic.Newtonsoft.Json;
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

        DivideAreaExecutor _divideAreaExecutor;
        DungeonBuilder _dungeonBuilder;
            
        void Awake()
        {
            // Domain
            _divideAreaExecutor = new DivideAreaExecutor();
            _dungeonBuilder = new DungeonBuilder(new SquareGridCoordinate(30, 30), _divideAreaExecutor);
        }

        void Start()
        {
            buildButton.onClick.AddListener(() =>
            {
                Debug.Log("Build Dungeon");
                
                // Domain
                var map = new EntityGridMap(new SquareGridCoordinate(30, 30));
                var dungeonBuilder = new DungeonBuilder(new SquareGridCoordinate(30, 30), _divideAreaExecutor);
                var areas = CreateTestAreas(map);
                
                var divideCoord = _divideAreaExecutor.RandomizeCoord(true, dungeonBuilder.GetInitArea(map));
                Debug.Log($"DivideCoord: {divideCoord}");
                Debug.Log($"map.Width / 2 : {map.Width / 2}");
                var path = _divideAreaExecutor.CreatePath(areas[0], areas[1],  divideCoord, true);
                areas[0].AdjacentAreas.Add((areas[1], path));
                areas[1].AdjacentAreas.Add((areas[0], path));
                
                // place paths and walls 
                var paths = areas.SelectMany(area => area.AdjacentAreas.Select(tuple => tuple.path)).ToList();
                map = dungeonBuilder.PlaceRooms(map, areas);
                map = dungeonBuilder.PlacePath(map, paths);
                map = dungeonBuilder.PlaceWall(map);  // this should be last
                
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
            

            
            buildButton2.onClick.AddListener(() =>
            {
                Debug.Log("Build Dungeon");
                var map = _dungeonBuilder.CreateDungeonDivideByStep();

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

                        if (map.GetSingleTypeList<IEntity>(x, y) is { } entities)
                        {
                            string frontEntityName = entities.First().GetType().Name;
                            Debug.Log($"frontEntityName: {frontEntityName}");
                            // 先頭から5文字を削除
                            string typeName = frontEntityName.Length > 9 ? frontEntityName.Substring(9) : frontEntityName;
                            string count = entities.Count.ToString();
                            tile.SetDebugText($"{typeName}\n{count}");
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
                _dungeonBuilder.Reset();   
            });
        }

        List<Area> CreateTestAreas(EntityGridMap map)
        {
            var area1 = new Area(
                X: 0,
                Y: 0,
                Width: map.Width/2,
                Height: map.Height,
                Room: new Room(
                    X: DivideAreaExecutor.MinRoomMargin,
                    Y: DivideAreaExecutor.MinRoomMargin,
                    Width: map.Width/2 - DivideAreaExecutor.MinRoomMargin * 2,
                    Height: map.Height - DivideAreaExecutor.MinRoomMargin * 2
                ),
                AdjacentAreas: new List<(Area area, Path path)>()
            );
            var area2 = new Area(
                X: map.Width/2,
                Y: 0,
                Width: map.Width/2,
                Height: map.Height/2,
                Room: new Room(
                    X: DivideAreaExecutor.MinRoomMargin + map.Width/2,
                    Y: DivideAreaExecutor.MinRoomMargin,
                    Width: map.Width/2 - DivideAreaExecutor.MinRoomMargin * 2,
                    Height: map.Height/2 - DivideAreaExecutor.MinRoomMargin * 2
                ),
                AdjacentAreas: new List<(Area area, Path path)>()
            );
            var area3 = new Area(
                X: map.Width/2,
                Y: map.Height/2,
                Width: map.Width/2,
                Height: map.Height/2,
                Room: new Room(
                    X: DivideAreaExecutor.MinRoomMargin + map.Width/2,
                    Y: DivideAreaExecutor.MinRoomMargin + map.Height/2,
                    Width: map.Width/2 - DivideAreaExecutor.MinRoomMargin * 2,
                    Height: map.Height/2 - DivideAreaExecutor.MinRoomMargin * 2
                ),
                AdjacentAreas: new List<(Area area, Path path)>()
            );
            return new List<Area>() {area1, area2, area3};
                
        }
        
    }
}
