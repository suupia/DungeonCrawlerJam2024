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
        Dictionary<BattleState, List<BattleState>> _stateTransitionMap = new() // (Current State) -> (List of Possible Transition States)
        {
            { BattleState.None, new() { BattleState.None, BattleState.InBattle,BattleState.InResult}}, 
            { BattleState.InBattle, new() { BattleState.InResult}},
            { BattleState.InResult, new() { BattleState.None}},
        };
        public bool IsInBattle => _battleState == BattleState.InBattle;
        public bool IsInResult => _battleState == BattleState.InResult;

        public void StartBattle(PlayerDomain player, EnemyDomain enemy)
        {
            Player = player;
            Enemy = enemy;
            Debug.Log("Battle Start");
            ChangeState(BattleState.InBattle);
            OnBattleStart?.Invoke(this, EventArgs.Empty);
        }
        
        public void EndBattle()
        {
            _battleState = BattleState.None;
            OnBattleEnd?.Invoke(this, EventArgs.Empty);
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
                StartResult(true);
            }
            
            Enemy.Attack(Player);

            if (Player.IsDead)
            {
                // Game Over
                Debug.Log("player was defeated by enemy");
                StartResult(false);
            }
            
            Debug.Log($"In Battle player._hp = {Player.CurrentHp}, enemy._hp = {Enemy.CurrentHp}");
        }
        void ChangeState(BattleState nextState)
        {
            Assert.IsTrue(_stateTransitionMap.ContainsKey(_battleState));  // Current State
            Assert.IsTrue(_stateTransitionMap[_battleState].Contains(nextState)); // Next State
            // OnStart(_battleState);
            _battleState = nextState;
            // OnEnd(_battleState);
        }

        void StartResult(bool isPlayerWin)
        {
            Debug.Log("Battle End " + (isPlayerWin ? "Player Win" : "Player Lose"));
            ChangeState(BattleState.InResult);
            if (isPlayerWin)
            {
                OnPlyerWin?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPlyerLose?.Invoke(this, EventArgs.Empty);
            }
        }
        

    }
}
