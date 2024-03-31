#nullable enable
using UnityEngine;

namespace DungeonCrawler._10_UpgradeSystem.UpgradeAssembly
{
    public class HighScore
    {
        const string SaveKey = "HighScore";
        public int HighScoreValue
        {
            get =>  PlayerPrefs.GetInt(SaveKey, 0);
            set =>  PlayerPrefs.SetInt(SaveKey, value);
        }

        public void GainFlamePoint(int value)
        {
            Debug.Log($"Gain HighScore {value}");
            HighScoreValue += value;
        }
    }}