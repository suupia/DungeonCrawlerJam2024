using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.PlayerAssembly.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace DungeonCrawler
{
    public class BattleSimulator
    {
        public PlayerDomain Player { get; }
        public EnemyDomain Enemy { get; }

        public BattleSimulator(PlayerDomain player, EnemyDomain enemy)
        {
            Player = player;
            Enemy = enemy;
        }
        
        public void UpdateTurn(IPlayerAttack playerAttack)
        {
            playerAttack.Attack(Enemy);

            if (Enemy.IsDead)
            {
                FinishBattle();
            }
            
            Enemy.Attack(Player);

            if (Player.IsDead)
            {
                // Game Over
                Debug.Log("player was defeated by enemy");
                FinishBattle();
            }
            
            Debug.Log($"In Battle player._hp = {Player.CurrentHp}, enemy._hp = {Enemy.CurrentHp}");
        }

        void FinishBattle()
        {
            Debug.Log("The battle finished");
        }
    }
}
