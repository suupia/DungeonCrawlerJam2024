#nullable enable
using System;

namespace DungeonCrawler
{
    public class BattleGameConnector
    {
        readonly BattleSimulator _battleSimulator;
        readonly GameStateSwitcher _gameStateSwitcher;
        
        public BattleGameConnector(BattleSimulator battleSimulator, GameStateSwitcher gameStateSwitcher)
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
               _gameStateSwitcher.EnterExploring();
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
        
        public void OnEnemyLose(Action action)
        {
            _battleSimulator.OnPlayerWin += (sender, e) =>
            {
                action();
            };
        }
    }
}