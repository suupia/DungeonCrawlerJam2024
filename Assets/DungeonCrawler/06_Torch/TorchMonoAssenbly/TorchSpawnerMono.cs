#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using System.Collections;
using System.Collections.Generic;
using PlasticGui;
using UnityEngine;

namespace  DungeonCrawler
{
    public class TorchSpawnerMono : MonoBehaviour
    {
        [SerializeField] TorchControllerMono torchPrefab;
        const float TorchSpawnHeight = 1.0f;

        public void SpawnTorch(int x, int y)
        {
            Debug.Log($"Torch spawn position: {x}, {y}");
            var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var spawnPosition = new Vector3(spawnGridPosition.x, TorchSpawnHeight, spawnGridPosition.z);
            Instantiate(torchPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

