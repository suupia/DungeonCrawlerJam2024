#nullable enable
using System;
using DungeonCrawler._10_UpgradeSystem.UpgradeAssembly;
using PlasticGui.WebApi.Responses;
using TMPro;
using UnityEngine;

namespace DungeonCrawler
{
    public class UpgradeContentUIMono : MonoBehaviour
    {
        [SerializeField] CustomButton upgradeCustomButton = null!;
        [SerializeField] TextMeshProUGUI upgradeText = null!;
        
        string _upgradeName = null!;
        int _upgradeCost = 0;

        Action Upgrade = () => { };
        Func<int> CurrentValue = () => 0;
        Func<int> NextValue = () => 0;

        FlamePoint _flamePoint;

        public void SetFlamePoint(FlamePoint flamePoint)
        {
            _flamePoint = flamePoint;
        }
        public void SetUp(string upgradeName, int cost, Action upgrade, Func<int> currentValue, Func<int> nextValue)
        {
            _upgradeName = upgradeName;
            _upgradeCost = cost;
            Upgrade = upgrade;
            CurrentValue = currentValue;
            NextValue = nextValue;

            upgradeText.text = _upgradeName;
            upgradeCustomButton.AddListener(() =>
            {
                if (_flamePoint.FlamePointValue<cost) Debug.Log($"cancel upgrade {_upgradeName}, cost = {cost}, flamePoint = {_flamePoint.FlamePointValue}");
                
                Debug.Log($"upgrade {_upgradeName} from {CurrentValue()} to {NextValue()}");
                Upgrade();
            });
        }
    }
}