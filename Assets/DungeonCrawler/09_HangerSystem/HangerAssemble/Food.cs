#nullable enable

using System;
using Codice.Client.GameUI.Explorer;
using CodiceApp.EventTracking;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DungeonCrawler
{
    public class Food : IGridEntity
    {
        public event EventHandler OnEaten = (sender, e) => { };
        public Func<(int x, int y)> GridPosition = () => (0, 0);
        public int HangerMeterIncreaseAmount { get; set; } = 11;
        HangerSystem _hangerSystem;

        public Food(HangerSystem hangerSystem, DungeonSwitcher dungeonSwitcher)
        {
            _hangerSystem = hangerSystem;
            OnEaten += (sender, e) =>
            {
                Debug.Log("Food.OnEaten()");
                dungeonSwitcher.CurrentDungeon.Map.RemoveEntity(GridPosition().x,GridPosition().y, this);
            };
        }
        public void GotOn()
        {
            Debug.Log("Food.GotOn()");
            _hangerSystem.EatFood(this);
            
            OnEaten(this, EventArgs.Empty);
        }
    }
}
