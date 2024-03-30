#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using VContainer;

namespace DungeonCrawler._04_EnemySystem.EnemyAssembly
{
    public class Enemy : IGridEntity
    {
        BattleGameConnector _battleGameConnector;
        
        public Enemy(
            BattleGameConnector battleGameConnector
            )
        {
            _battleGameConnector = battleGameConnector;
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
            // _dungeonSwitcher.SwitchToBattle();
        }
    }
}