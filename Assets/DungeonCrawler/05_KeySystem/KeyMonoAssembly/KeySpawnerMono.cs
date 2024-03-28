#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  DungeonCrawler.KeyMonoAssenbly
{
    public class KeySpawnerMono : MonoBehaviour
    {
        [SerializeField] KeyController keyPrefab = null!;
        const float KeySpawnHeight = 1.0f;

        public void SpawnKey(int x, int y)
        {
            Debug.Log($"Key spawn position: {x}, {y}");
            var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var spawnPosition = new Vector3(spawnGridPosition.x, KeySpawnHeight, spawnGridPosition.z);
            Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
        }
    }   
}
