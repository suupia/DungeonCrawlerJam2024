using System;
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using R3;
using UnityEditor.Experimental.Licensing;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class MiniMapManagerMono : MonoBehaviour
    {
        [SerializeField] MiniMapTileMono _miniMapTileMono;
        [SerializeField] Camera cameraPrefab;

        List<MiniMapTileMono> _backgroundTiles;
        List<MiniMapTileMono> _immovableEntityTiles;
        MiniMapTileMono _playerTile;
        Camera _camera;

        Vector3 _offset = new Vector3(300, 0, 300);
        Quaternion _rotateOffset = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
        Vector3 _cameraOffset;

        DungeonSwitcher _dungeonSwitcher;
        
        [Inject]
        public void Construct(
            DungeonSwitcher dungeonSwitcher)
        {
            _dungeonSwitcher = dungeonSwitcher;
            
            SetUp();
        }

        void SetUp()
        {
            _cameraOffset = _offset + Vector3.up * 20;
            _camera = Instantiate(cameraPrefab, _cameraOffset, _rotateOffset);
            
            Observable.EveryValueChanged(this, _ => _dungeonSwitcher.Floor)
                .Subscribe(_ =>
                {
                    InitMiniMap(_dungeonSwitcher.CurrentDungeon.Map);
                });
        }

        void InstantiateShortageTiles(EntityGridMap entityGridMap)
        {
            var shortage = entityGridMap.Length - _backgroundTiles.Count;
            for(int i = 0; i<shortage; i++)
            {
                var vector = entityGridMap.ToVector(i);
                var tile = Instantiate(_miniMapTileMono, GridConverter.GridPositionToWorldPosition(vector) + _offset, _rotateOffset);
                tile.transform.localScale = new Vector3(GridConverter.GridSize, GridConverter.GridSize, GridConverter.GridSize);
                
                _backgroundTiles.Add(tile);
            }
        }

        void ResetAllTIiles()
        {
            foreach (var tile in _backgroundTiles)
            {
                tile.ResetSprite();
            }
        }

        void InitMiniMap(EntityGridMap entityGridMap)
        {
            ResetAllTIiles();
            InstantiateShortageTiles(entityGridMap);
            Debug.Log($"Mini map background tiles num = {_backgroundTiles.Count}");
            for (int i = 0; i < entityGridMap.Length; i++)
            {
                var gridObjects = entityGridMap.GetAllTypeList(i);

                foreach (var obj in gridObjects)
                {
                    if (obj is IGridEntity)
                    {
                    }
                    else
                    {
                        _backgroundTiles[i].SetTileSprite(obj);
                    }
                }
            }
        }

        void ChasePlayer()
        {
            // can use R3?
            var playerPosition = new Vector3(10, 1, 10);

            _camera.transform.position = playerPosition + _cameraOffset;
        }
    }
}
