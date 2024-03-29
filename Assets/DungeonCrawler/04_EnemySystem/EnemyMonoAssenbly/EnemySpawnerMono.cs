#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
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
                var(x,y) = _dungeonSwitcher.CurrentDungeon.InitEnemyPosition;
                SpawnEnemy(x,y);
            }); 
    }
    void SpawnEnemy(int x, int y)
    {
        Debug.Log($"Enemy spawn position: {x}, {y}");
        var spawnGridPosition = GridConverter.GridPositionToWorldPosition(new Vector2Int(x, y));
        var spawnPosition = new Vector3(spawnGridPosition.x, EnemySpawnHeight, spawnGridPosition.z);
        _enemyController = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
