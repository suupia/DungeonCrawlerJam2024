#nullable enable
using System;
using DungeonCrawler._10_UpgradeSystem.UpgradeAssembly;
using PlasticGui.WebApi.Responses;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace DungeonCrawler
{
    public class UpgradeContentUIMono : MonoBehaviour
    {
        [SerializeField] CustomButton upgradeCustomButton = null!;
        [SerializeField] TextMeshProUGUI upgradeText = null!;
        [SerializeField] TextMeshProUGUI valueDiffText = null!;
        [SerializeField] TextMeshProUGUI costText = null!;
        
        string _upgradeName = null!;
        Func<int> UpgradeCost = () => 0;

        Action Upgrade = () => { };
        Func<int> CurrentValue = () => 0;
        Func<int> NextValue = () => 0;
        Func<int> CurrentFlamePoint = () => 0;        

        public void SetFlamePointGetter(Func<int> currentFlamePoint)
        {
            CurrentFlamePoint = currentFlamePoint;
        }
        public void SetUp(string upgradeName, Action upgrade, Func<int> upgradeCost, Func<int> currentValue, Func<int> nextValue)
        {
            _upgradeName = upgradeName;
            UpgradeCost = upgradeCost;
            Upgrade = upgrade;
            CurrentValue = currentValue;
            NextValue = nextValue;
            
            

            SetValuesToUI();
            upgradeCustomButton.AddListener(() =>
            {
                if (CurrentFlamePoint()<UpgradeCost()) Debug.Log($"cancel upgrade {_upgradeName}, cost = {UpgradeCost()}, flamePoint = {CurrentFlamePoint()}");
                
                Debug.Log($"upgrade {_upgradeName} from {CurrentValue()} to {NextValue()}");
                Upgrade();
                SetValuesToUI();
            });
        }

        void SetValuesToUI()
        {
            upgradeText.text = _upgradeName;
            valueDiffText.text = $"{CurrentValue()} -> {NextValue()}";
            costText.text = $"cost: {UpgradeCost()}";
        }
    }
}