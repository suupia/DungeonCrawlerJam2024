using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.EnemyAssenbly.Classes;
using DungeonCrawler.PlayerAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler.BattleSystem.BattleAssenbly
{
    public class BattaleManager
    {
        PlayerDomain _player;
        EnemyDomain _enemy;

        public BattaleManager(PlayerDomain player, EnemyDomain enemy)
        {
            _player = player;
            _enemy = enemy;
        }
        
        public void UpdateTurn()
        {
            // player attacks to enemy
            
            _enemy.Attack(_player);
        }
    }
}
