#nullable enable
using DungeonCrawler.Core;
using DungeonCrawler.MapSystem.Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DungeonCrawler.Runtime.Player
{
    public class PlayerSpawner : MonoBehaviour, IMonoSystem
    {
        [SerializeField] PlayerController playerPrefab = null!;

        DungeonBuilder? _dungeonBuilder;

        public void Init(DungeonBuilder dungeonBuilder)
        {
            _dungeonBuilder = dungeonBuilder;
        }
        
        public void SpawnPlayer(EntityGridMap map)
        {
            var position = _dungeonBuilder.PlacePlayerSpawnPosition();
            Debug.Log($"Player spawn position: {position}");
            var instancePos = new Vector2(3, 3); // convert grid pos to world pos
        }

    }
}