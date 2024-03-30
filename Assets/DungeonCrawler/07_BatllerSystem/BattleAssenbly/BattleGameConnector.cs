#nullable enable
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
    }
}