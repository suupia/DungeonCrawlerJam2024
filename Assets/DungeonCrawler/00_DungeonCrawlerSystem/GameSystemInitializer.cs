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
        IMapBuilderMono _mapBuilderMono = null!;
        IPlayerSpawnerMono _playerSpawnerMono = null!;
        DungeonBuilder _dungeonBuilder = null!;
        EnemySpawnerMono _enemySpawnerMono = null!;
        StairsSpawnerMono _stairsSpawnerMono = null!;
        
        [Inject]
        public void Construct(
            IMapBuilderMono mapBuilderMono,
            IPlayerSpawnerMono playerSpawnerMono,
            DungeonBuilder dungeonBuilder,
            EnemySpawnerMono enemySpawnerMono,
            StairsSpawnerMono stairsSpawnerMono
            )
        {
            _mapBuilderMono = mapBuilderMono;
            _playerSpawnerMono = playerSpawnerMono;
            _dungeonBuilder = dungeonBuilder;
            _enemySpawnerMono = enemySpawnerMono;
            _stairsSpawnerMono = stairsSpawnerMono;

            SetUp();
        }
        void SetUp()
        {
            Debug.Log("GameSystemInitializer.SetUp()");
            
            // Create map
            _mapBuilderMono.SwitchNextDungeon();
            
            // Spawn player
            var (spawnX, spawnY) = _dungeonBuilder.CalculatePlayerSpawnPosition();
            _playerSpawnerMono.SpawnPlayer(spawnX, spawnY);
            
            // Spawn enemy
            var (enemySpawnX, enemySpawnY) = _dungeonBuilder.CalculateEnemySpawnPosition();
            _enemySpawnerMono.SpawnEnemy(enemySpawnX, enemySpawnY);
            
            // Spawn key
            var (keySpawnX, keySpawnY) = _dungeonBuilder.CalculateKeySpawnPosition();
            _stairsSpawnerMono.SpawnStairs(keySpawnX, keySpawnY);
        }
    }
}
