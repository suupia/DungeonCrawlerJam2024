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
    EnemyControllerMono? _enemyController;
    
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
                if(_enemyController != null) Destroy(_enemyController.gameObject);
                foreach (var (x, y) in _dungeonSwitcher.CurrentDungeon.InitEnemyPositions)
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
        _enemyController = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        _enemyController.Construct(enemy);
    }
}
