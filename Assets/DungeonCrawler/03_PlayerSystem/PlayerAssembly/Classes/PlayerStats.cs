﻿#nullable enable
using System.Text.RegularExpressions;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using UnityEngine;
using VContainer;

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
        PlayerUpgradeStats _playerUpgradeStats;

        [Inject]
        public PlayerStats(PlayerUpgradeStats upgradeStats)
        {
            _playerUpgradeStats = upgradeStats;
        }
        int CalcMaxHp(int level)
        {
            return level * 10 + _playerUpgradeStats.maxHp;
        }
        int CalcAtk(int level)
        {
            return (int)(level * 1.5) + _playerUpgradeStats.Atk;
        }
        int CalcLevel(int exp)
        {
            return (int)Mathf.Sqrt(exp);
        }
        
        
    }
}