using System;
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
        
        public event EventHandler  OnBattleStart;
        public event EventHandler  OnBattleEnd;
        
        public event EventHandler OnPlyerWin;
        public event EventHandler OnPlyerLose;

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
        
        public void StartBattle()
        {
            Debug.Log("Battle Start");
            _battleState = BattleState.InBattle;
            OnBattleStart?.Invoke(this, EventArgs.Empty);
        }
        
        public void UpdateTurn(IPlayerAttack playerAttack)
        {
            if(_battleState != BattleState.InBattle)
            {
                _battleState = BattleState.InBattle;
            }
            
            playerAttack.Attack(Enemy);

            if (Enemy.IsDead)
            {
                EndBattle(true);
            }
            
            Enemy.Attack(Player);

            if (Player.IsDead)
            {
                // Game Over
                Debug.Log("player was defeated by enemy");
                EndBattle(false);
            }
            
            Debug.Log($"In Battle player._hp = {Player.CurrentHp}, enemy._hp = {Enemy.CurrentHp}");
        }
        
        void EndBattle(bool isPlayerWin)
        {
            Debug.Log("Battle End " + (isPlayerWin ? "Player Win" : "Player Lose"));
            _battleState = BattleState.None;
            OnBattleEnd?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
