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

        public Action GameOver = () => {};

        public HangerSystem()
        {
            _hangerMeter = new HangerMeter(100);
        }

        public void UpdateTurn() // from PlayerController.Move()?
        {
            _hangerMeter.Value -= _turnDecrease;

            if (_hangerMeter.Value <= 0)
            {
                GameOver();
            }
        }
    }
}
