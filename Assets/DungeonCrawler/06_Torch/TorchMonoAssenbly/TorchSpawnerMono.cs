#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
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
                        if (torchController == null) continue;
                        Destroy(torchController.gameObject);
                    }
                    _torchControllers.Clear();
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
            var torch = _dungeonSwitcher.CurrentDungeon.Map.GetSingleEntity<Torch>(x, y);
            Assert.IsNotNull(torch);
            var spawnPosition = new Vector3(spawnGridPosition.x, TorchSpawnHeight, spawnGridPosition.z);
            var torchController = Instantiate(torchPrefab, spawnPosition, Quaternion.identity);
            torchController.Construct(torch);
            _torchControllers.Add(torchController);
            
        }
    }
}

