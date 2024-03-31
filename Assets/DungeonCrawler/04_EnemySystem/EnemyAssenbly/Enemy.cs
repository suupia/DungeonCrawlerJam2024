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
        public event EventHandler OnDead = (sender, e) => { }; 
        public Func<(int x, int y)> GridPosition = () => (0, 0);
        readonly BattleGameConnector _battleGameConnector;
        readonly DungeonSwitcher _dungeonSwitcher;
        readonly EnemyDomain _enemyDomain;
        
        public Enemy(
            BattleGameConnector battleGameConnector,
            DungeonSwitcher dungeonSwitcher
            )
        {
            _battleGameConnector = battleGameConnector;
            _dungeonSwitcher = dungeonSwitcher;
            OnDead += (sender, e) =>
            {
                Debug.Log("Enemy.OnDead()");
                _dungeonSwitcher.CurrentDungeon.Map.RemoveEntity(GridPosition().x,GridPosition().y, this);
            };
            var enemyStats = new EnemyStats( () => _dungeonSwitcher.Floor+1);
            _enemyDomain = new EnemyDomain(enemyStats.MaxHp, enemyStats.Atk);
        }
        
        public void GotOn()
        {
            Debug.Log($"Enemy.GotOn()");
            _battleGameConnector.RegisterPlayerWinAction( () => OnDead(this, EventArgs.Empty));
            _battleGameConnector.StartBattle(_enemyDomain);
            
        }
  
    }
}