using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace DungeonCrawler
{
    public class BattleManager
    {
        PlayerDomain _player;
        EnemyDomain _enemy;
        bool inBattle = false;

        public void StartBattle(PlayerDomain player, EnemyDomain enemy)
        {
            Assert.IsFalse(inBattle);
            
            _player = player;
            _enemy = enemy;
            inBattle = true;
        }

        public void UpdateTurn()
        {
            Assert.IsTrue(inBattle);
            
            
            // How should I do about SacredAttack()
            _player.Attack((_enemy));

            if (_enemy.Hp <= 0)
            {
                FinishBattle();
            }
            
            _enemy.Attack(_player);

            if (_player.Hp <= 0)
            {
                // Game Over
                Debug.Log("player was defeated by enemy");
                FinishBattle();
            }
            
            Debug.Log($"In Battle player._hp = {_player.Hp}, enemy._hp = {_enemy.Hp}");
        }

        void FinishBattle()
        {
            Debug.Log("The battle finished");
            inBattle = false;
        }
    }
}
