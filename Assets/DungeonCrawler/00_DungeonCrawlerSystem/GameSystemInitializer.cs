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
        
        [Inject]
        public void Construct(
            DungeonBuilder dungeonBuilder,
            DungeonSwitcher dungeonSwitcher
            )
        {
            _dungeonBuilder = dungeonBuilder;
            _dungeonSwitcher = dungeonSwitcher;

            SetUp();
        }
        void SetUp()
        {
            Debug.Log("GameSystemInitializer.SetUp()");

        }
    }
}
