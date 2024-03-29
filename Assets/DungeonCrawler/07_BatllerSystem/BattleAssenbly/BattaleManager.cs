using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
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
            // How should I do about SacredAttack()
            _player.Attack((_enemy));

            if (_enemy._hp <= 0)
            {
                FinishBattle();
            }
            
            _enemy.Attack(_player);

            if (_player._hp <= 0)
            {
                // Game Over
                Debug.Log("player was defeated by enemy");
                FinishBattle();
            }
            
            Debug.Log($"In Battle player._hp = {_player._hp}, enemy._hp = {_enemy._hp}");
        }

        void FinishBattle()
        {
            Debug.Log("The battle finished");
        }
    }
}
