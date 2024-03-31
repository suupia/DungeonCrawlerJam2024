#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace DungeonCrawler.MapMonoAssembly
{
    public class MapBuilderMono : MonoBehaviour
    {
        [SerializeField] TileMono tilePrefab = null!;
        readonly List<TileMono> _pool = new();

        IGridCoordinate _coordinate = null!;
        DungeonSwitcher _dungeonSwitcher = null!;
            
        DivideAreaExecutor _divideAreaExecutor = null!;
        DungeonBuilder _dungeonBuilder = null!;
        
        [Inject]
        public void Construct(
            IGridCoordinate coordinate,
            DungeonSwitcher dungeonSwitcher)
        {
            _coordinate = coordinate;
            _dungeonSwitcher = dungeonSwitcher;
            
            SetUp();
        }

        void SetUp()
        {
            InstantiateTiles();
            
            // Observable.EveryValueChanged(this, _ => _dungeonSwitcher.Floor)
            //     .Subscribe(_ =>
            //     {
            //         UpdateSprites(_dungeonSwitcher.CurrentDungeon);
            //     }); 
            _dungeonSwitcher.RegisterOnFloorChangedAction(15, () =>
            {
                UpdateSprites(_dungeonSwitcher.CurrentDungeon);
            });
        }

        public void BuildFirstDungeon()
        {
            Debug.Log("SwitchNextDungeon");
            var nextMap = _dungeonSwitcher.SwitchNextDungeon();
            UpdateSprites(nextMap);
        }
        void ResetAllTiles()
        {
            foreach(var tile in _pool)
            {
                tile.ResetSprites();
            }
        }
        
        void InstantiateTiles()
        {
            for(int i = 0; i<_coordinate.Length ; i++)
            {
                var vector = _coordinate.ToVector(i);
                var tile = Instantiate(tilePrefab, GridConverter.GridPositionToWorldPosition(vector), Quaternion.identity);
                tile.transform.localScale = new Vector3(GridConverter.GridSize, GridConverter.GridSize, GridConverter.GridSize);
                _pool.Add(tile);
            }
        }

        void UpdateSprites(DungeonGridMap plainDungeon)
        {
            ResetAllTiles();

            Debug.Log($"Length:{_coordinate.Length}");
            for(int i = 0; i<_coordinate.Length ; i++)
            {
                var vector = _coordinate.ToVector(i);
                var (x, y) = (vector.x, vector.y);
                var tile = _pool[i];
                if(plainDungeon.Map.GetSingleEntity<IGridTile>(x,y) is {} entity)
                {
                    tile.SetFloorSprite(entity);
                }

                foreach (var direction in DirectionEnum.GetAll())
                {
                    var aroundVector = new Vector2Int(x + direction.Vector.x, y + direction.Vector.y);
                    if(_coordinate.IsInDataArea(aroundVector.x, aroundVector.y))
                    {
                        if(plainDungeon.Map.GetSingleEntity<IGridTile>(aroundVector.x, aroundVector.y) is {} aroundEntity)
                        {
                            tile.SetWallSprite(aroundEntity, direction);
                        }
                    }
                }
                    
                
                if (plainDungeon.Map.GetAllTypeList(x, y).Count() != 0)
                {
                    string result = "";
                    var allEntityList = plainDungeon.Map.GetAllTypeList(x, y).ToList();
                    foreach (var entity1 in allEntityList)
                    {
                        int count = allEntityList.Count(e => e.ToString() == entity1.ToString());
                
                        result += entity1.ToString() + $"({count})\n";
                    }
                
                    tile.SetDebugText(result);
                }
            }
        }

        void Update()
        {
            UnityEditorDebugProcess();
            
        }

        void UnityEditorDebugProcess()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Debug SwitchNextDungeon()");
                BuildFirstDungeon();
            }
#endif
        }



    }
}
