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
                    var dungeon = _dungeonSwitcher.CurrentDungeon;
                    SpawnStairs(8, 8); // todo
                }); 
        }

        public void SpawnStairs(int x, int y)
        {
            Debug.Log($"Key spawn position: {x}, {y}");
            var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var spawnPosition = new Vector3(spawnGridPosition.x, KeySpawnHeight, spawnGridPosition.z);
            Instantiate(stairsPrefab, spawnPosition, Quaternion.identity);
        }
    }   
}
