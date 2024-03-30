#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapMonoAssembly;
using DungeonCrawler.PlayerAssembly.Interfaces;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class GameSystemInitializer : MonoBehaviour
    {
        GameStateSwitcher _gameStateSwitcher = null!;
        DungeonSwitcher _dungeonSwitcher = null!;
        MapBuilderMono _mapBuilderMono = null!;
        
        [Inject]
        public void Construct(
            GameStateSwitcher gameStateSwitcher,
            DungeonSwitcher dungeonSwitcher,
            MapBuilderMono mapBuilderMono
            )
        {
            _gameStateSwitcher = gameStateSwitcher;
            _dungeonSwitcher = dungeonSwitcher;
            _mapBuilderMono = mapBuilderMono;

            SetUp();
        }
        void SetUp()
        {
            Debug.Log("GameSystemInitializer.SetUp()");
            _gameStateSwitcher.OnGameStateChange += (sender, e) =>
            {
                Debug.Log($"GameState Changed: {e.NextGameState}");
                _mapBuilderMono.BuildFirstDungeon();
                // _dungeonSwitcher.Reset();
            };
            _gameStateSwitcher.EnterTitle();
        }
    }
}
