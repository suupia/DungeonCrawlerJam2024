#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler._10_UpgradeSystem.UpgradeAssembly;
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
        MapBuilderMaterialMono _mapBuilderMono = null!;
        
        PlayerSpawnerMono _playerSpawnerMono = null!;

        FlamePoint _flamePoint = null!;
        HighScore _highScore = null!;
        
        [Inject]
        public void Construct(
            GameStateSwitcher gameStateSwitcher,
            DungeonSwitcher dungeonSwitcher,
            MapBuilderMaterialMono mapBuilderMono,
            PlayerSpawnerMono playerSpawnerMono,
            FlamePoint flamePoint,
            HighScore highScore
            )
        {
            _gameStateSwitcher = gameStateSwitcher;
            _dungeonSwitcher = dungeonSwitcher;
            _mapBuilderMono = mapBuilderMono;
            _playerSpawnerMono = playerSpawnerMono;
            _flamePoint = flamePoint;
            _highScore = highScore;

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
                    _flamePoint.GainFlamePoint(_dungeonSwitcher.Floor);
                    _highScore.SetHighScore(_dungeonSwitcher.Floor);
                        
                    Debug.Log($"_dungeonSwitcher.Reset()");
                    _dungeonSwitcher.Reset(0);
                    _mapBuilderMono.BuildFirstDungeon();

                }
            };
            
            
        }
    }
}
