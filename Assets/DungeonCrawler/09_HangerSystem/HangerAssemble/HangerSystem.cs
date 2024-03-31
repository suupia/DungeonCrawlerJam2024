#nullable enable
using System;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class HangerSystem
    {
        HangerMeter _hangerMeter;

        const int TurnDecrease = 1;

        GameStateSwitcher _gameStateSwitcher;

        // Getter
        public float CurrentHangerRate()
        {
            return (float)_hangerMeter.Value/_hangerMeter.MaxValue;
            
        }
        
        [Inject]
        public HangerSystem(HangerMeter hangerMeter, GameStateSwitcher gameStateSwitcher)
        {
            _hangerMeter = hangerMeter;
            _gameStateSwitcher = gameStateSwitcher;
            
            _gameStateSwitcher.OnGameStateChange += (sender, e) =>
            {
                Debug.Log($"GameState Changed: {e.PrevGameState} -> {e.PostGameState}");
                if (e.PrevGameState == GameStateSwitcher.GameStateEnum.Exploring 
                    && e.PostGameState == GameStateSwitcher.GameStateEnum.AtTitle)
                {
                    _hangerMeter.ResetHangerMeter();
                }
            };
        }

        public void UpdateTurn()
        {
            _hangerMeter.Value -= TurnDecrease;
            
            Debug.Log($"hangerMeter.value = {_hangerMeter.Value}");

            if (_hangerMeter.Value <= 0)
            {
                _gameStateSwitcher.EnterTitle();
            }
        }

        public void EatFood(Food food)
        {
            _hangerMeter.Value += food.HangerMeterIncreaseAmount;
        }

        public void SetHangerMeterMaxValue(int maxValue)
        {
            _hangerMeter.MaxValue = maxValue;
        }
    }
}
