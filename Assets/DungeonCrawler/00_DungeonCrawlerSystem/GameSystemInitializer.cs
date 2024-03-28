#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.PlayerAssembly.Interfaces;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class GameSystemInitializer : MonoBehaviour
    {
        IMapBuilderMonoSystem _mapBuilderMonoSystem = null!;
        IPlayerSpawnerMonoSystem _playerSpawnerMonoSystem = null!;
        DungeonBuilder _dungeonBuilder = null!;
        EnemySpawnerMono _enemySpawnerMono = null!;
        
        [Inject]
        public void Construct(
            IMapBuilderMonoSystem mapBuilderMonoSystem,
            IPlayerSpawnerMonoSystem playerSpawnerMonoSystem,
            DungeonBuilder dungeonBuilder,
            EnemySpawnerMono enemySpawnerMono
            )
        {
            _mapBuilderMonoSystem = mapBuilderMonoSystem;
            _playerSpawnerMonoSystem = playerSpawnerMonoSystem;
            _dungeonBuilder = dungeonBuilder;
            _enemySpawnerMono = enemySpawnerMono;

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
            
            // Spawn enemy
            var (enemySpawnX, enemySpawnY) = _dungeonBuilder.PlacePlayerSpawnPosition();
            _enemySpawnerMono.SpawnEnemy(enemySpawnX, enemySpawnY);
        }

    }
}
