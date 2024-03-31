#nullable enable
using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using Observable = R3.Observable;

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

        public void SetUp(string upgradeName, Action upgrade, Func<int> upgradeCost, Func<int> currentValue, Func<int> nextValue, Func<int> currentFlamePoint)
        {
            _upgradeName = upgradeName;
            UpgradeCost = upgradeCost;
            Upgrade = upgrade;
            CurrentValue = currentValue;
            NextValue = nextValue;
            CurrentFlamePoint = currentFlamePoint;
            
            Observable.EveryValueChanged(this, _ => CurrentFlamePoint())
                .Subscribe(_ =>
                {
                    var isEnbale = CurrentFlamePoint() >= UpgradeCost();
                    Debug.Log($"upgrade {_upgradeName} isEnable {isEnbale}");
                    upgradeCustomButton.ChangeEnableState(isEnbale);

                    if (isEnbale)
                    {
                        upgradeCustomButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        upgradeCustomButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    }
                }); 

            
            SetValuesToUI();
            upgradeCustomButton.AddListener(() =>
            {
                if (CurrentFlamePoint()<UpgradeCost())
                {
                    Debug.Log(
                        $"cancel upgrade {_upgradeName}, cost = {UpgradeCost()}, flamePoint = {CurrentFlamePoint()}");
                    return;
                }
                
                Debug.Log($"upgrade {_upgradeName} from {CurrentValue()} to {NextValue()}");
                Upgrade();
                SetValuesToUI();
            });
        }

        void SetValuesToUI()
        {
            upgradeText.text = _upgradeName;
            valueDiffText.text = $"{CurrentValue()} to {NextValue()}";
            costText.text = $"cost: {UpgradeCost()}";
        }
    }
}