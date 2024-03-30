#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace DungeonCrawler
{
    public class FoodSpawnerMono : MonoBehaviour
    {
        [SerializeField] TorchControllerMono foodPrefab = null!;
        const float FoodSpawnHeight = 1.0f;
        
        DungeonSwitcher _dungeonSwitcher = null!;
        readonly List<TorchControllerMono> _foodControllers = new ();
        
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
                    foreach (var torchController in _foodControllers)
                    {
                        Destroy(torchController.gameObject);
                    }
                    var positions = _dungeonSwitcher.CurrentDungeon.InitTorchPositions;
                    foreach (var (x,y) in positions)
                    {
                        SpawnFood(x,y);
                    }
                }); 
        }

        void SpawnFood(int x, int y)
        {
            Debug.Log($"Torch spawn position: {x}, {y}");
            var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
            var food = _dungeonSwitcher.CurrentDungeon.Map.GetSingleEntity<Food>(x, y);
            Assert.IsNotNull(food);
            var spawnPosition = new Vector3(spawnGridPosition.x, FoodSpawnHeight, spawnGridPosition.z);
            var foodController = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            foodController.Construct(food);
            _foodControllers.Add(foodController);
            
        }
    }
}
