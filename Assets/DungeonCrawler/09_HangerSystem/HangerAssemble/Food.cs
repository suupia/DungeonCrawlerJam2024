#nullable enable

using System;
using Codice.Client.GameUI.Explorer;
using CodiceApp.EventTracking;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DungeonCrawler
{
    public class Food : IGridEntity
    {
        public event EventHandler OnEaten = (sender, e) => { };
        public int HangerMeterIncreaseAmount { get; set; } = 11;
        HangerSystem _hangerSystem;

        public Food(HangerSystem hangerSystem)
        {
            _hangerSystem = hangerSystem;
        }
        public void GotOn()
        {
            Debug.Log("Food.GotOn()");
            _hangerSystem.EatFood(this);
            
            OnEaten(this, EventArgs.Empty);
        }
    }
}
