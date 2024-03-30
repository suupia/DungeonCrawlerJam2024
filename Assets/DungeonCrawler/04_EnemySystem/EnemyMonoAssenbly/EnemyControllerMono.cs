using System.Collections;
using System.Collections.Generic;
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using DungeonCrawler.MapAssembly.Classes;
using UnityEngine;

public class EnemyControllerMono : MonoBehaviour
{
    Enemy _enemy;
    (int x, int y) GridPosition => GridConverter.WorldPositionToGridPosition(transform.position);

    public void Construct(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.GridPosition = () => GridPosition;
    }
}
