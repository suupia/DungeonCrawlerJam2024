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
        
        enum BattleState
        {
            None,
            InBattle,
            InResult,
        }

        BattleState _battleState = BattleState.None;

        public BattleSimulator(PlayerDomain player, EnemyDomain enemy)
        {
            Player = player;
            Enemy = enemy;
        }
        
        public bool IsInBattle => _battleState == BattleState.InBattle;
        public bool IsInResult => _battleState == BattleState.InResult;
        
        public void UpdateTurn(IPlayerAttack playerAttack)
        {
            if(_battleState != BattleState.InBattle)
            {
                _battleState = BattleState.InBattle;
            }
            
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
            _battleState = BattleState.InResult;
        }
    }
}
