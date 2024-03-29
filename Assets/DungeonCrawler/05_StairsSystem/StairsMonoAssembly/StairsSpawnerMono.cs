#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace  DungeonCrawler
{
    public class StairsSpawnerMono : MonoBehaviour
    {
        [FormerlySerializedAs("keyPrefab")] [SerializeField] StairsControllerMono stairsPrefab = null!;
        const float KeySpawnHeight = 1.0f;
        
        DungeonSwitcher _dungeonSwitcher = null!;
        StairsControllerMono? _stairsController;
        
        [Inject]
        public void Construct(DungeonSwitcher dungeonSwitcher)
        {
            _dungeonSwitcher = dungeonSwitcher;
            SetUp();
        }

        void SetUp()
        {
            Observable.EveryValueChanged(this, _ => _dungeonSwitcher.Floor)
                .Subscribe(_ =>
                {
                    if(_stairsController != null) Destroy(_stairsController.gameObject);
                    var(x,y) = _dungeonSwitcher.CurrentDungeon.InitStairsPosition;
                    SpawnStairs(x,y);
                }); 
        }

        void SpawnStairs(int x, int y)
        {
            Debug.Log($"Stair spawn position: {x}, {y}");
            var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var spawnPosition = new Vector3(spawnGridPosition.x, KeySpawnHeight, spawnGridPosition.z);
            _stairsController = Instantiate(stairsPrefab, spawnPosition, Quaternion.identity);
        }
    }   
}
