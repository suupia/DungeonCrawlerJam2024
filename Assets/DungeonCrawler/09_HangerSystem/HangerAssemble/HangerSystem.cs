#nullable enable
using System;
using Codice.Client.GameUI.Explorer;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class HangerSystem
    {
        HangerMeter _hangerMeter;

        const int TurnDecrease = 1;

        public Action GameOver = () => {Debug.Log("GameOver with HangerMeter 0");};

        // Getter
        public float CurrentHangerRate()
        {
            return (float)_hangerMeter.Value/_hangerMeter.MaxValue;
            
        }
        
        [Inject]
        public HangerSystem(HangerMeter hangerMeter)
        {
            _hangerMeter = hangerMeter;
        }

        public void UpdateTurn() // from PlayerController.Move()?
        {
            _hangerMeter.Value -= TurnDecrease;
            
            Debug.Log($"hangerMeter.value = {_hangerMeter.Value}");

            if (_hangerMeter.Value <= 0)
            {
                GameOver();
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
