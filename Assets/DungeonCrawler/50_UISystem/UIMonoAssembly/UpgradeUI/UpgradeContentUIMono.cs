#nullable enable
using System;
using PlasticGui.WebApi.Responses;
using UnityEngine;

namespace DungeonCrawler
{
    public class UpgradeContentUIMono : MonoBehaviour
    {
        string _upgradeName = null!;
        int _upgradeCost = 0;

        Action Upgrade = () => { };
        Func<int> CurrentValue = () => 0;
        Func<int> NextValue = () => 0;

        public void SetUp(string upgradeName, int cost, Action upgrade, Func<int> currentValue, Func<int> nextValue)
        {
            _upgradeName = upgradeName!;
            _upgradeCost = cost;
            Upgrade = upgrade;
            CurrentValue = currentValue;
            NextValue = nextValue;
        }
    }
}