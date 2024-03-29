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
        DungeonBuilder _dungeonBuilder = null!;
        DungeonSwitcher _dungeonSwitcher = null!;
        GameStateSwitcher _gameStateSwitcher = null!;
        
        [Inject]
        public void Construct(
            DungeonBuilder dungeonBuilder,
            DungeonSwitcher dungeonSwitcher,
            GameStateSwitcher gameStateSwitcher
            )
        {
            _dungeonBuilder = dungeonBuilder;
            _dungeonSwitcher = dungeonSwitcher;
            _gameStateSwitcher = gameStateSwitcher;

            SetUp();
        }
        void SetUp()
        {
            Debug.Log("GameSystemInitializer.SetUp()");
            _gameStateSwitcher.EnterExploring();

        }
    }
}
