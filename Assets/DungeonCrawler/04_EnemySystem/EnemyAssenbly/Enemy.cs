#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using VContainer;

namespace DungeonCrawler._04_EnemySystem.EnemyAssembly
{
    public class Enemy : IGridEntity
    {
        DungeonSwitcher _dungeonSwitcher;
        BattleGameConnector _battleGameConnector;
        
        public Enemy(
            DungeonSwitcher dungeonSwitcher,
            BattleGameConnector battleGameConnector
            )
        {
            _dungeonSwitcher = dungeonSwitcher;
            _battleGameConnector = battleGameConnector;
        }
        
        public void GotOn()
        {
            Debug.Log($"Enemy.GotOn()");
            //_battleGameConnector
            
        }
        
        void Die()
        {
            Debug.Log($"Enemy.Die()");
            // _dungeonSwitcher.SwitchToBattle();
        }
    }
}