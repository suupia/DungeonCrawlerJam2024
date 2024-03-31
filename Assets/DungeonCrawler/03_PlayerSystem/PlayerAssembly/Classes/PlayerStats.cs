#nullable enable
using System.Text.RegularExpressions;
using UnityEngine;

namespace DungeonCrawler
{
    public class PlayerStats
    {
        const string SaveKey = "PlayerExp";

        public int Exp
        {
            get =>  PlayerPrefs.GetInt(SaveKey, 10);
            set =>  PlayerPrefs.SetInt(SaveKey, value);
        }
        public int Level => CalcLevel(Exp);
        public int MaxHp => CalcMaxHp(Level);
        public int Atk => CalcAtk(Level);

        int CalcMaxHp(int level)
        {
            return level * 10;
        }
        int CalcAtk(int level)
        {
            return (int)(level * 1.5);
        }
        int CalcLevel(int exp)
        {
            return (int)Mathf.Sqrt(exp);
        }
        
        
    }
}