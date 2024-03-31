#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

public class EnemySpawnerMono : MonoBehaviour
{
    [SerializeField] EnemyControllerMono enemyPrefab = null!;
    const float EnemySpawnHeight = 1.0f;
    
    DungeonSwitcher _dungeonSwitcher = null!;
    readonly List<EnemyControllerMono> _enemyControllers = new ();
    
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
                foreach (var enemyController in _enemyControllers)
                {
                    if (enemyController == null) continue;
                    Destroy(enemyController.gameObject);
                }
                _enemyControllers.Clear();
                var positions = _dungeonSwitcher.CurrentDungeon.InitEnemyPositions;
                foreach (var (x,y) in positions)
                {
                    SpawnEnemy(x,y);
                }
            }); 
    }
    void SpawnEnemy(int x, int y)
    {
        Debug.Log($"Enemy spawn position: {x}, {y}");
        var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
        var enemy = _dungeonSwitcher.CurrentDungeon.Map.GetSingleEntity<Enemy>(x, y);
        Assert.IsNotNull(enemy);
        var spawnPosition = new Vector3(spawnGridPosition.x, EnemySpawnHeight, spawnGridPosition.z);
        var enemyController = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemyController.Construct(enemy);
        _enemyControllers.Add(enemyController);
    }
}
