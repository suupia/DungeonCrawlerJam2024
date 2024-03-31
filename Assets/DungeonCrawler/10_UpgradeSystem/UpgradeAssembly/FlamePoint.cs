#nullable enable
using UnityEngine;

namespace DungeonCrawler._10_UpgradeSystem.UpgradeAssembly
{
    public class FlamePoint
    {
        const string SaveKey = "FlamePoint";
        public int FlamePointValue
        {
            get =>  PlayerPrefs.GetInt(SaveKey, 0);
            set =>  PlayerPrefs.SetInt(SaveKey, value);
        }

        public void GainFlamePoint(int value)
        {
            Debug.Log($"Gain FlamePoint {value}");
            FlamePointValue += value;
        }
    }}