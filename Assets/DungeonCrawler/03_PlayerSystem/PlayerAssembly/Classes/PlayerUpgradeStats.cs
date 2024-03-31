#nullable enable
using System;
using DungeonCrawler._10_UpgradeSystem.UpgradeAssembly;
using UnityEngine;

namespace DungeonCrawler
{
    public class PlayerUpgradeStats
    {
        public int Atk => CalcAtk();
        public int AtkUpgradeDelta => CalcAtkUpgradeDelta();
        int _atkUpgradeCount;
        
        public void Upgrade(UpgradeKind kind)
        {
            switch (kind)
            {
                case UpgradeKind.Test1:
                    _atkUpgradeCount++;
                    break;
                default:
                    break;
            }
        }

        public int UpgradeCost(UpgradeKind kind)
        {
            switch (kind)
            {
                case UpgradeKind.Test1: return 0;
                case UpgradeKind.Test2: return 200;
                default: 
                    Debug.LogWarning("Unsupported UpgradeKind");
                    return -1;
            }
        }
        
        int CalcAtk()
        {
            // UpgradeCount -> Upgrade Amount
            return  _atkUpgradeCount;
        }
        int CalcAtkUpgradeDelta()
        {
            return 1;
        }
    }
}