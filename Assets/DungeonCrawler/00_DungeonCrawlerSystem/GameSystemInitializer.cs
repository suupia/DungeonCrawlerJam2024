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
            _gameStateSwitcher.EnterTitle();

            _gameStateSwitcher.OnGameStateChange += (sender, e) =>
            {
                Debug.Log($"GameState Changed: {e.PrevGameState} -> {e.PostGameState}");
                if (e.PrevGameState == GameStateSwitcher.GameStateEnum.Battling 
                    && e.PostGameState == GameStateSwitcher.GameStateEnum.AtTitle)
                {
                    Debug.Log($"_dungeonSwitcher.Reset()");
                    _dungeonSwitcher.Reset(0);
                    _mapBuilderMono.BuildFirstDungeon();
                    _mapBuilderMono.BuildFirstDungeon(); // WHY??
                    // if you call BuildFirstDungeon once, an error occurs. 

                }
            };
            
            
        }
    }
}
