#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerMono : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab = null!;
    const float EnemySpawnHeight = 1.0f;
        
    public void SpawnEnemy(int x, int y)
    {
        Debug.Log($"Enemy spawn position: {x}, {y}");
        var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
        var spawnPosition = new Vector3(spawnGridPosition.x, EnemySpawnHeight, spawnGridPosition.z);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
