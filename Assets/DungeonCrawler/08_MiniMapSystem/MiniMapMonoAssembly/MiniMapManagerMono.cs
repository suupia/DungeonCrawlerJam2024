#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.PlayerAssembly.Classes;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace DungeonCrawler
{
    public class MiniMapManagerMono : MonoBehaviour
    {
        [FormerlySerializedAs("_miniMapTileMono")] [SerializeField] MiniMapTileMono _miniMapTilePrefab;
        [SerializeField] Camera cameraPrefab;

        List<MiniMapTileMono> _miniMapTiles;
        MiniMapTileMono _playerTile;
        Camera _camera;

        readonly Vector3 _offset = new Vector3(300, 0, 300);
        readonly Quaternion _rotateOffset = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
        Vector3 _cameraOffset;

        DungeonSwitcher _dungeonSwitcher;
        Player? _player;

        
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
            
            _playerTile = InstantiateTile();

            Observable.EveryValueChanged(this, _ => _dungeonSwitcher.Floor)
                .Subscribe(_ =>
                {
                    InitMiniMap();
                });
                Observable.EveryValueChanged(this, _ => _player?.GridPosition())
                .Subscribe(_ =>
                {
                    if (_dungeonSwitcher.CurrentDungeon is DefaultDungeonGridMap)
                    {
                        Debug.LogWarning($" _dungeonSwitcher.CurrentDungeon is DefaultDungeonGridMap in MiniMapManagerMono");
                        return;
                    }
                    ChasePlayerPosition();
                });
            Observable.EveryValueChanged(this, _ => _player?.CurrentRotation())
                .Subscribe(_ =>
                {
                    if (_dungeonSwitcher.CurrentDungeon is DefaultDungeonGridMap)
                    {
                        Debug.LogWarning(
                            $" _dungeonSwitcher.CurrentDungeon is DefaultDungeonGridMap in MiniMapManagerMono");
                        return;
                    }
                    ChaisePlayerRotation();
                });
        }

        MiniMapTileMono InstantiateTile(int index = 0)
        {
            var vector = _dungeonSwitcher.CurrentDungeon.Map.ToVector(index);
            var tile = Instantiate(_miniMapTilePrefab, GridConverter.GridPositionToWorldPosition(vector) + _offset, _rotateOffset);
            tile.transform.localScale = new Vector3(GridConverter.GridSize, GridConverter.GridSize, GridConverter.GridSize);
            
            return tile;
        }

        void SetTilePosition(MiniMapTileMono tile, int index)
        {
            var vector = _dungeonSwitcher.CurrentDungeon.Map.ToVector(index);
            tile.transform.position = GridConverter.GridPositionToWorldPosition(vector) + _offset;
        }

        void ResetAllTIiles()
        {
            foreach (var tile in _miniMapTiles)
            {
                tile.ResetSprite();
            }
        }

        void InitMiniMap()
        {
            ResetAllTIiles();

            EntityGridMap entityGridMap = _dungeonSwitcher.CurrentDungeon.Map;

            int nonPlayerCount = 0;
            for (int i = 0; i < entityGridMap.Length; i++)
            {
                var gridObjects = entityGridMap.GetAllTypeList(i);

                foreach (var obj in gridObjects)
                {
                    if (obj is Player)
                    {
                        _player = obj as Player;;
                        Debug.Log("find player and set playerTile");
                        _playerTile.SetTileSprite(obj);
                        SetTilePosition(_playerTile, i);
                    }
                    else
                    {
                        if (nonPlayerCount >= _miniMapTiles.Count)
                        {
                            _miniMapTiles.Add(InstantiateTile(i));
                        }
                        
                        _miniMapTiles[nonPlayerCount].SetTileSprite(obj);
                        SetTilePosition(_miniMapTiles[nonPlayerCount], i);
                        nonPlayerCount++;
                    }
                }
            }
            Debug.Log($"Mini map tiles num = {_miniMapTiles.Count}");
        }

        void ChasePlayerPosition()
        {
            var (x, y) = _player.GridPosition();
            SetTilePosition(_playerTile, _dungeonSwitcher.CurrentDungeon.Map.ToSubscript(x, y));

            _camera.transform.position =
                _cameraOffset + GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
        }

        void ChaisePlayerRotation()
        {
            if (_player != null) return;
            _playerTile.transform.rotation = _player.CurrentRotation() * _rotateOffset;
        }

        // void Update()
        // {
        //     if(_player != null)
        //     {
        //         Debug.Log($"player grid position: {_player.GridPosition()}"); 
        //     }
        // }
    }
}
