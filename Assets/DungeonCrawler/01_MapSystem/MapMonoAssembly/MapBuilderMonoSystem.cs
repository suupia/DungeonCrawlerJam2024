#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace DungeonCrawler.MapMonoAssembly
{
    public class MapBuilderMonoSystem : MonoBehaviour, IMapBuilderMonoSystem
    {
        [SerializeField] TileMono tilePrefab = null!;

        readonly List<TileMono> _pool = new();

        IGridCoordinate _coordinate = null!;
        DivideAreaExecutor _divideAreaExecutor = null!;
        DungeonBuilder _dungeonBuilder = null!;

        EntityGridMap? _map;
            
        // void Awake()
        // {
        //     // Domain
        //     _coordinate = new SquareGridCoordinate(50, 50);
        //     _divideAreaExecutor = new DivideAreaExecutor();
        //     _dungeonBuilder = new DungeonBuilder(
        //         _divideAreaExecutor
        //     );
        // }
        [Inject]
        public void Construct(
            IGridCoordinate coordinate,
            DivideAreaExecutor divideAreaExecutor,
            DungeonBuilder dungeonBuilder)
        {
            _coordinate = coordinate;
            _divideAreaExecutor = divideAreaExecutor;
            _dungeonBuilder = dungeonBuilder;
            
            for(int i = 0; i<_coordinate.Length ; i++)
            {
                var vector = _coordinate.ToVector(i);
                var tile = Instantiate(tilePrefab, GridConverter.GridPositionToWorldPosition(vector), Quaternion.identity);
                tile.transform.localScale = new Vector3(GridConverter.GridSize, GridConverter.GridSize, GridConverter.GridSize);
                _pool.Add(tile);
            }
        }

        public void CreateDungeon()
        {
            Debug.Log("CreateDungeon");
            DestroyAllTiles();
            _map ??= new EntityGridMap(_coordinate);
            _map = _dungeonBuilder.CreateDungeon(_map);
            UpdateSprites(_map);
        }
        void CreateDungeonByStep()
        {
            Debug.Log("CreateDungeonByStep");
            DestroyAllTiles();
            _map ??= new EntityGridMap(_coordinate);
            _map = _dungeonBuilder.CreateDungeonByStep(_map);
            UpdateSprites(_map);
        }

        void DestroyAllTiles()
        {
            foreach(var tile in _pool)
            {
                Destroy(tile);
            }
        }

        void UpdateSprites(EntityGridMap map)
        {
            Debug.Log($"Length:{_coordinate.Length}");
            for(int i = 0; i<_coordinate.Length ; i++)
            {
                var vector = _coordinate.ToVector(i);
                var (x, y) = (vector.x, vector.y);
                var tile = _pool[i];
                if(map.GetSingleEntity<IEntity>(x,y) is {} entity)
                {
                    tile.SetFloorSprite(entity);
                }
                    
                
                if (map.GetAllTypeList(x, y).Count() != 0)
                {
                    string result = "";
                    var allEntityList = map.GetAllTypeList(x, y).ToList();
                    foreach (var entity1 in allEntityList)
                    {
                        int count = allEntityList.Count(e => e.ToString() == entity1.ToString());
                
                        result += entity1.ToString() + $"({count})\n";
                    }
                
                    tile.SetDebugText(result);
                }
            }
        }

        // void Start()
        // {
        //     
        //     for(int i = 0; i<_coordinate.Length ; i++)
        //     {
        //         var vector = _coordinate.ToVector(i);
        //         var tile = Instantiate(tilePrefab, GridConverter.GridPositionToWorldPosition(vector), Quaternion.identity);
        //         tile.transform.localScale = new Vector3(GridConverter.GridSize, GridConverter.GridSize, GridConverter.GridSize);
        //         _pool.Add(tile);
        //     }
        //
        //     CreateDungeon();
        //     
        //     // spawn player
        //     var (spawnX, spawnY) = _dungeonBuilder.PlacePlayerSpawnPosition();
        //     GameManager.GetMonoSystem<IPlayerSpawnerMonoSystem>().SpawnPlayer(spawnX, spawnY);
        //
        // }
        //

        
    }
}
