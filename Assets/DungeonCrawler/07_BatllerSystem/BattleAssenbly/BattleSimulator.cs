using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.PlayerAssembly.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace DungeonCrawler
{
    public class BattleSimulator
    {
        PlayerDomain _player;
        EnemyDomain _enemy;

        public BattleSimulator(PlayerDomain player, EnemyDomain enemy)
        {
            _player = player;
            _enemy = enemy;
        }
        
        public void UpdateTurn(IPlayerAttack playerAttack)
        {
            // How should I do about SacredAttack()
            playerAttack.Attack(_enemy);

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
        }
    }
}
