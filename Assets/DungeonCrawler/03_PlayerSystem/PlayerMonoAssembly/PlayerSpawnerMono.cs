#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.PlayerAssembly.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.PlayerLoop;
using VContainer;

namespace DungeonCrawler.PlayerMonoAssembly
{
    public class PlayerSpawnerMono : MonoBehaviour
    {
        [SerializeField] PlayerController playerPrefab = null!;
        const float PlayerSpawnHeight = 1.0f;  
        
        DungeonSwitcher _dungeonSwitcher = null!;
        PlayerController? _playerController;
        
        [Inject]
        public void Construct(DungeonSwitcher dungeonSwitcher)
        {
            _dungeonSwitcher = dungeonSwitcher;
            SetUp();
        }
        void SetUp()
        {
            Observable.EveryValueChanged(this, _ => _dungeonSwitcher.Floor)
                .Subscribe(_ =>
                {
                    if(_playerController != null) Destroy(_playerController.gameObject);
                    var(x,y) = _dungeonSwitcher.CurrentDungeon.PlayerPosition;
                    SpawnPlayer(x,y);
                }); 
        }
        public void SpawnPlayer(int x, int y)
        {
            Debug.Log($"Player spawn position: {x}, {y}");
            var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var spawnPosition = new Vector3(spawnGridPosition.x, PlayerSpawnHeight, spawnGridPosition.z);
            _playerController = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            _playerController.Construct(_dungeonSwitcher);
        }
    }
}