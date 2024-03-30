#nullable enable
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
        public PlayerDomain? Player { get; private set; }
        public EnemyDomain? Enemy { get; private set; }

        public event EventHandler OnBattleStart = (sender, e) => { };
        public event EventHandler  OnBattleEnd = (sender, e) => { };
        
        public event EventHandler OnPlyerWin = (sender, e) => { };
        public event EventHandler OnPlyerLose = (sender, e) => { };

        enum BattleState
        {
            None,
            InBattle,
            InResult,
        }

        BattleState _battleState = BattleState.None;
        public bool IsInBattle => _battleState == BattleState.InBattle;
        public bool IsInResult => _battleState == BattleState.InResult;

        public void StartBattle(PlayerDomain player, EnemyDomain enemy)
        {
            Assert.IsTrue(_battleState == BattleState.None);
            Player = player;
            Enemy = enemy;
            Debug.Log("Battle Start");
            _battleState = BattleState.InBattle;
            OnBattleStart?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateTurn(IPlayerAttack playerAttack)
        {
            Assert.IsNotNull(Player);
            Assert.IsNotNull(Enemy);
            
            if(_battleState != BattleState.InBattle)
            {
                _battleState = BattleState.InBattle;
            }
            else
            {
                Debug.LogWarning($" Battle is not in progress. Current State = {_battleState}");
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
