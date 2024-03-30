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

        int _turnDecrease = 1;

        public Action GameOver = () => {Debug.Log("GameOver with HangerMeter 0");};

        public HangerSystem(HangerMeter hangerMeter)
        {
            _hangerMeter = hangerMeter;
        }

        public void UpdateTurn() // from PlayerController.Move()?
        {
            _hangerMeter.Value -= _turnDecrease;
            
            Debug.Log($"hangerMeter.value = {_hangerMeter.Value}");

            if (_hangerMeter.Value <= 0)
            {
                GameOver();
            }
        }
    }
}
