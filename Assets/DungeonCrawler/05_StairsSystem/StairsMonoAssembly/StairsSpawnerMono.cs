#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace  DungeonCrawler.KeyMonoAssenbly
{
    public class StairsSpawnerMono : MonoBehaviour
    {
        [FormerlySerializedAs("keyPrefab")] [SerializeField] StairsControllerMono stairsPrefab = null!;
        const float KeySpawnHeight = 1.0f;

        public void SpawnStairs(int x, int y)
        {
            Debug.Log($"Key spawn position: {x}, {y}");
            var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var spawnPosition = new Vector3(spawnGridPosition.x, KeySpawnHeight, spawnGridPosition.z);
            Instantiate(stairsPrefab, spawnPosition, Quaternion.identity);
        }
    }   
}
