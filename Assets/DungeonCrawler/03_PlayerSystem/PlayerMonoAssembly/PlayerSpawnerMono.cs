#nullable enable
using System.Data.Common;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.PlayerAssembly.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.PlayerLoop;
using VContainer;

namespace DungeonCrawler.PlayerMonoAssembly
{
    public class PlayerSpawnerMono : MonoBehaviour
    {
        [SerializeField] PlayerController playerPrefab = null!;
        const float PlayerSpawnHeight = 1.0f;  
        
        DungeonSwitcher _dungeonSwitcher = null!;
        GameStateSwitcher _gameStateSwitcher = null!;
        PlayerController? _playerController;
        HangerSystem _hangerSystem;
        
        [Inject]
        public void Construct(
            DungeonSwitcher dungeonSwitcher,
            GameStateSwitcher gameStateSwitcher,
            HangerSystem hangerSystem
            )
        {
            _dungeonSwitcher = dungeonSwitcher;
            _gameStateSwitcher = gameStateSwitcher;
            _hangerSystem = hangerSystem;
            SetUp();
        }

        void SetUp()
        {
            Observable.EveryValueChanged(this, _ => _dungeonSwitcher.Floor)
                .Subscribe(_ =>
                {
                    Reset();
                    if (_dungeonSwitcher.CurrentDungeon is DefaultDungeonGridMap)
                    {
                        Debug.LogWarning($" _dungeonSwitcher.CurrentDungeon is DefaultDungeonGridMap");
                        return;
                    }
                    SpawnPlayer();
                });

            _gameStateSwitcher.OnGameStateChange += (sender, e) =>
            {
                Debug.Log($"GameState Changed: {e.PrevGameState} -> {e.PostGameState}");
                if (e.PrevGameState == GameStateSwitcher.GameStateEnum.Battling 
                    && e.PostGameState == GameStateSwitcher.GameStateEnum.AtTitle)
                {
                    Reset();
                }
            };
        }
        public void SpawnPlayer()
        {
            var(x,y) = _dungeonSwitcher.CurrentDungeon.InitPlayerPosition;
            Debug.Log($"Player spawn position: {x}, {y}");
            var spawnWorldPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var player = _dungeonSwitcher.CurrentDungeon.Map.GetSingleEntity<Player>(x, y);
                Assert.IsNotNull(player, $"spawnGridPosition: {spawnWorldPosition} player: {player}");
            var spawnPosition = new Vector3(spawnWorldPosition.x, PlayerSpawnHeight, spawnWorldPosition.z);
            _playerController = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            _playerController.Init(player, _dungeonSwitcher,_gameStateSwitcher, _hangerSystem);
        }

        void Reset()
        {
            if(_playerController != null) Destroy(_playerController.gameObject);

        }
    }
}