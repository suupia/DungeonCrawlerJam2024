#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.Runtime.Player;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class GameSystemInitializer : MonoBehaviour
    {
        IMapBuilderMonoSystem _mapBuilderMonoSystem = null!;
        IPlayerSpawnerMonoSystem _playerSpawnerMonoSystem = null!;
        [Inject]
        public void Construct(
            IMapBuilderMonoSystem mapBuilderMonoSystem,
            IPlayerSpawnerMonoSystem playerSpawnerMonoSystem
            )
        {
            _mapBuilderMonoSystem = mapBuilderMonoSystem;
            _playerSpawnerMonoSystem = playerSpawnerMonoSystem;
        }
        void Start()
        {
            // Create map
            // _mapBuilderMonoSystem.CreateDungeon();  // todo : build map here 
            
            // Spawn player
            _playerSpawnerMonoSystem.SpawnPlayer(3,3);  // todo : spawn player here
        }

    }
}
