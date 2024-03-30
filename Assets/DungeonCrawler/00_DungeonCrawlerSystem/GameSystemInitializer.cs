#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapMonoAssembly;
using DungeonCrawler.PlayerAssembly.Interfaces;
using DungeonCrawler.PlayerMonoAssembly;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class GameSystemInitializer : MonoBehaviour
    {
        GameStateSwitcher _gameStateSwitcher = null!;
        DungeonSwitcher _dungeonSwitcher = null!;
        MapBuilderMono _mapBuilderMono = null!;
        
        PlayerSpawnerMono _playerSpawnerMono = null!;
        
        [Inject]
        public void Construct(
            GameStateSwitcher gameStateSwitcher,
            DungeonSwitcher dungeonSwitcher,
            MapBuilderMono mapBuilderMono,
            PlayerSpawnerMono playerSpawnerMono
            )
        {
            _gameStateSwitcher = gameStateSwitcher;
            _dungeonSwitcher = dungeonSwitcher;
            _mapBuilderMono = mapBuilderMono;
            _playerSpawnerMono = playerSpawnerMono;

            SetUp();
        }
        void SetUp()
        {
            Debug.Log("GameSystemInitializer.SetUp()");
            
            _mapBuilderMono.BuildFirstDungeon(); // This process is required to run regardless of game state.
            _gameStateSwitcher.OnGameStateChange += (sender, e) =>
            {
                Debug.Log($"GameState Changed: {e.PrevGameState} -> {e.PostGameState}");
                // if (e.PrevGameState == GameStateSwitcher.GameStateEnum.AtTitle)
                // {
                //     //_dungeonSwitcher.Reset();
                //     _mapBuilderMono.BuildFirstDungeon();
                // }
                // if(e.PostGameState == GameStateSwitcher.GameStateEnum.Exploring)
                // {
                //     _mapBuilderMono.BuildFirstDungeon();
                //     _playerSpawnerMono.SpawnPlayer();
                // }
            };
            
            
            _gameStateSwitcher.EnterTitle();
        }
    }
}
