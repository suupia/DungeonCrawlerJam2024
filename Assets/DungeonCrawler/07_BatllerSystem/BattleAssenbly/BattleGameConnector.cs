#nullable enable
using System;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler
{
    // This class is responsible for connecting the BattleSimulator 
    public class BattleGameConnector
    {
        readonly BattleSimulator _battleSimulator;  
        readonly GameStateSwitcher _gameStateSwitcher;
        readonly PlayerStats _playerStats;
        readonly EnemyStats _enemyStats;
        public BattleGameConnector(
            BattleSimulator battleSimulator,
            GameStateSwitcher gameStateSwitcher,
            PlayerStats playerStats,
            EnemyStats enemyStats
        )
        {
            _battleSimulator = battleSimulator;
            _gameStateSwitcher = gameStateSwitcher;
            _playerStats = playerStats;
            _enemyStats = enemyStats;
            Init();
        }

        void Init()
        {
            _battleSimulator.OnBattleStart += (sender, e) =>
            {
                _gameStateSwitcher.EnterBattling();
            };
            _battleSimulator.OnBattleEnd += (sender, e) =>
            {
                Debug.Log($"e.IsPlayerWin: {e.IsPlayerWin}");
                if(e.IsPlayerWin)
                {
                    _gameStateSwitcher.EnterExploring();
                }
                else
                {
                    _gameStateSwitcher.EnterTitle();
                }
            };
        }

        public void StartBattle(EnemyDomain enemyDomain)
        {
            var player = new PlayerDomain(_playerStats.MaxHp);
            _battleSimulator.StartBattle(player, enemyDomain);
        }
        
        public void EndBattle()
        {
            _battleSimulator.EndBattle();
        }
        
        public void RegisterPlayerWinAction(Action action)
        {
            _battleSimulator.OnPlayerWin += (sender, e) =>
            {
                action();
            };
        }

        public void RegisterPlayerLooseAction(Action action)
        {
            _battleSimulator.OnPlayerWin += (sender, e) =>
            {
                action();
            };
        }
    }
}