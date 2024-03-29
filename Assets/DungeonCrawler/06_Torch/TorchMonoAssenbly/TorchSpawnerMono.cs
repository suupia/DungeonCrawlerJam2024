#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using System.Collections;
using System.Collections.Generic;
using PlasticGui;
using R3;
using UnityEngine;
using VContainer;

namespace  DungeonCrawler
{
    public class TorchSpawnerMono : MonoBehaviour
    {
        [SerializeField] TorchControllerMono torchPrefab = null!;
        const float TorchSpawnHeight = 1.0f;
        
        DungeonSwitcher _dungeonSwitcher = null!;
        readonly List<TorchControllerMono> _torchControllers = new ();
        
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
                    foreach (var torchController in _torchControllers)
                    {
                        Destroy(torchController.gameObject);
                    }
                    var positions = _dungeonSwitcher.CurrentDungeon.InitTorchPositions;
                    foreach (var (x,y) in positions)
                    {
                        SpawnTorch(x,y);
                    }
                }); 
        }

        void SpawnTorch(int x, int y)
        {
            Debug.Log($"Torch spawn position: {x}, {y}");
            var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var spawnPosition = new Vector3(spawnGridPosition.x, TorchSpawnHeight, spawnGridPosition.z);
            var torchController = Instantiate(torchPrefab, spawnPosition, Quaternion.identity);
            _torchControllers.Add(torchController);
        }
    }
}

