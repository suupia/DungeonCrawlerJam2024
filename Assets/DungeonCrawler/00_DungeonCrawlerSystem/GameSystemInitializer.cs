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
        IPlayerSpawnerMono _playerSpawnerMono = null!;
        DungeonBuilder _dungeonBuilder = null!;
        DungeonSwitcher _dungeonSwitcher = null!;
        
        [Inject]
        public void Construct(
            IPlayerSpawnerMono playerSpawnerMono,
            DungeonBuilder dungeonBuilder,
            DungeonSwitcher dungeonSwitcher
            )
        {
            _playerSpawnerMono = playerSpawnerMono;
            _dungeonBuilder = dungeonBuilder;
            _dungeonSwitcher = dungeonSwitcher;

            SetUp();
        }
        void SetUp()
        {
            Debug.Log("GameSystemInitializer.SetUp()");

            // Spawn player
            var (spawnX, spawnY) = _dungeonBuilder.CalculatePlayerSpawnPosition(_dungeonSwitcher.CurrentDungeon);
            _playerSpawnerMono.SpawnPlayer(spawnX, spawnY);
            
        }
    }
}
