#nullable enable
using DungeonCrawler.Core;
using DungeonCrawler.MapSystem.Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DungeonCrawler.Runtime.Player
{
    public class PlayerSpawnerMonoSystem : MonoBehaviour, IPlayerSpawnerMonoSystem
    {
        [SerializeField] PlayerController playerPrefab = null!;
        const float PlayerSpawnHeight = 1.0f;    
        
        public void SpawnPlayer(int x, int y)
        {
            Debug.Log($"Player spawn position: {x}, {y}");
            var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var spawnPosition = new Vector3(spawnGridPosition.x, PlayerSpawnHeight, spawnGridPosition.z);
            Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}