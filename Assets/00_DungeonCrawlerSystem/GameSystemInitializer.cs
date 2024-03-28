#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapSystem.Scripts;
using DungeonCrawler.Runtime.Player;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class GameSystemInitializer : MonoBehaviour
    {
        IMapBuilderMonoSystem _mapBuilderMonoSystem = null!;
        IPlayerSpawnerMonoSystem _playerSpawnerMonoSystem = null!;
        DungeonBuilder _dungeonBuilder = null!;
        
        [Inject]
        public void Construct(
            IMapBuilderMonoSystem mapBuilderMonoSystem,
            IPlayerSpawnerMonoSystem playerSpawnerMonoSystem,
            DungeonBuilder dungeonBuilder
            )
        {
            _mapBuilderMonoSystem = mapBuilderMonoSystem;
            _playerSpawnerMonoSystem = playerSpawnerMonoSystem;
            _dungeonBuilder = dungeonBuilder;

            SetUp();
        }
        void SetUp()
        {
            Debug.Log("GameSystemInitializer.SetUp()");
            
            // Create map
            _mapBuilderMonoSystem.CreateDungeon();  // todo : build map here 
            
            // Spawn player
            var (spawnX, spawnY) = _dungeonBuilder.PlacePlayerSpawnPosition();
            _playerSpawnerMonoSystem.SpawnPlayer(3,3);  // todo : spawn player here
        }

    }
}
