#nullable enable
using System;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using VContainer;

namespace DungeonCrawler._04_EnemySystem.EnemyAssembly
{
    public class Enemy : IGridEntity
    {
        public Func<(int x, int y)> GridPosition = () => (0, 0);
        readonly BattleGameConnector _battleGameConnector;
        readonly DungeonSwitcher _dungeonSwitcher;
        
        public Enemy(
            BattleGameConnector battleGameConnector,
            DungeonSwitcher dungeonSwitcher
            )
        {
            _battleGameConnector = battleGameConnector;
            _dungeonSwitcher = dungeonSwitcher;
        }
        
        public void GotOn()
        {
            Debug.Log($"Enemy.GotOn()");
            _battleGameConnector.OnEnemyLose(OnLose);
            _battleGameConnector.StartBattle();
            
        }
        
        void OnLose()
        {
            Debug.Log($"Enemy.Die()");
            _dungeonSwitcher.CurrentDungeon.Map.RemoveEntity(GridPosition().x,GridPosition().y, this);
        }
    }
}