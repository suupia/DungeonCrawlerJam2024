#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.PlayerAssembly.Interfaces;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DungeonCrawler.PlayerMonoAssembly
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