#nullable enable
using System.Text.RegularExpressions;
using UnityEngine;

namespace DungeonCrawler
{
    public class PlayerStats
    {
        const string SaveKey = "PlayerExp";

        public int PlayerExp
        {
            get =>  PlayerPrefs.GetInt(SaveKey, 0);
            set =>  PlayerPrefs.SetInt(SaveKey, value);
        }
        public int PlayerLevel => CalcLevel(PlayerExp);
        public int PlayerMaxHp => CalcMaxHp(PlayerLevel);
        public int PlayerAttack => CalcAttack(PlayerLevel);

        int CalcMaxHp(int level)
        {
            return level * 10;
        }
        int CalcAttack(int level)
        {
            return (int)(level * 1.5);
        }
        int CalcLevel(int exp)
        {
            return (int)Mathf.Sqrt(exp);
        }
        
        
    }
}