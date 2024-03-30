#nullable enable
using System;
using VContainer;

namespace DungeonCrawler
{
    public class HangerSystem
    {
        HangerMeter _hangerMeter;

        int _turnDecrease = 1;
        
        public HangerSystem()
        {
            _hangerMeter = new HangerMeter(100);
        }

        public void UpdateTurn(Action action) // from PlayerController.Move()?
        {
            _hangerMeter.Value -= _turnDecrease;

            if (_hangerMeter.Value <= 0)
            {
                GameOver(action);
            }
        }

        void GameOver(Action action)
        {
            action();
        }
    }
}
