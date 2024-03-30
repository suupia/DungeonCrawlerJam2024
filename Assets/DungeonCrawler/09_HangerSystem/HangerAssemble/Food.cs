#nullable enable

using Codice.Client.GameUI.Explorer;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler
{
    public class Food : IGridEntity
    {
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
            
            // need to be destroyed
        }
    }
}
