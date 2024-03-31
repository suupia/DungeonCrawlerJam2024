#nullable enable
using System;
using DungeonCrawler._10_UpgradeSystem.UpgradeAssembly;
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class PlayerUpgradeStats
    {
        public int Atk => CalcAtk(_atkUpgradeCount);
        public int AtkUpgradeDelta => CalcAtk(_atkUpgradeCount+1) - CalcAtk(_atkUpgradeCount);
        int _atkUpgradeCount;

        public int maxHp => CalcMaxHp(_maxHpUpgradeCount);
        public int MaxHpUpgradeDelta => CalcMaxHp(_maxHpUpgradeCount+1) - CalcMaxHp(_maxHpUpgradeCount);
        int _maxHpUpgradeCount;

        FlamePoint _flamePoint;

        [Inject]
        public PlayerUpgradeStats(FlamePoint flamePoint)
        {
            _flamePoint = flamePoint;
        }
        
        public void Upgrade(UpgradeKind kind)
        {
            _flamePoint.FlamePointValue -= UpgradeCost(kind);
            switch (kind)
            {
                case UpgradeKind.PlayerAttack:
                    _atkUpgradeCount++;
                    break;
                case UpgradeKind.PlayerHp:
                    _maxHpUpgradeCount++;
                    break;
                default:
                    break;
            }
            
        }

        public int UpgradeCost(UpgradeKind kind)
        {
            switch (kind)
            {
                case UpgradeKind.PlayerAttack: return _atkUpgradeCount + 1;
                case UpgradeKind.PlayerHp: return _maxHpUpgradeCount + 1;
                default: 
                    Debug.LogWarning("Unsupported UpgradeKind");
                    return -1;
            }
        }
        
        // attack
        int CalcAtk(int upgradeCount)
        {
            // UpgradeCount -> Upgrade Amount
            return  upgradeCount;
        }
        // maxhp
        int CalcMaxHp(int upgradeCount)
        {
            return upgradeCount;
        }
    }
}