#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class FlamePoint
    {
        public int FlamePointValue
        {
            get =>  Save.GetFlamePoint(); 
            set =>  Save.SetFlamePoint(value);
        }

        public void GainFlamePoint(int value)
        {
            Debug.Log($"Gain FlamePoint {value}");
            FlamePointValue += value;
        }
    }
}