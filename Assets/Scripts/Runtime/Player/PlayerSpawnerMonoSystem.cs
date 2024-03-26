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

        public void SpawnPlayer(int x, int y)
        {
            Debug.Log($"Player spawn position: {x}, {y}");
            var instancePos = new Vector2(3, 3); // convert grid pos to world pos
        }
    }
}