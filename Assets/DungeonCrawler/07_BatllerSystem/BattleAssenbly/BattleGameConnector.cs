#nullable enable
using System;
using DungeonCrawler.MapAssembly.Classes;

namespace DungeonCrawler
{
    // This class is responsible for connecting the BattleSimulator 
    public class BattleGameConnector
    {
        readonly BattleSimulator _battleSimulator;  
        readonly GameStateSwitcher _gameStateSwitcher;
        public BattleGameConnector(
            BattleSimulator battleSimulator,
            GameStateSwitcher gameStateSwitcher
        )
        {
            _battleSimulator = battleSimulator;
            _gameStateSwitcher = gameStateSwitcher;
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

        public void StartBattle()
        {
            var player = new PlayerDomain(100);
            var enemy = new EnemyDomain(100, 1);
            _battleSimulator.StartBattle(player, enemy);
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