﻿#nullable enable
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
        }
        
        public void GotOn()
        {
            Debug.Log($"Enemy.GotOn()");
            _battleGameConnector.OnEnemyLose( () => OnDead(this, EventArgs.Empty));
            _battleGameConnector.StartBattle();
            
        }
  
    }
}