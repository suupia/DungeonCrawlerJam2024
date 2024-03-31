using System;
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using DungeonCrawler._10_UpgradeSystem.UpgradeAssembly;
using UnityEngine;
using VContainer;
using FlamePoint = DungeonCrawler._10_UpgradeSystem.UpgradeAssembly.FlamePoint;


public class UpgradeUIMono : MonoBehaviour
{
    [SerializeField] GameObject view;
    [SerializeField] GameObject upgradeContentsParent;
    [SerializeField] UpgradeContentUIMono upgradeContentPrefab;
    [SerializeField] CustomButton closeButton;

    FlamePoint _flamePoint;
    PlayerUpgradeStats _playerUpgradeStats;

    [Inject]
    public void Construct(
        FlamePoint flamePoint,
        PlayerUpgradeStats playerUpgradeStats
        )
    {
        _flamePoint = flamePoint;
        _playerUpgradeStats = playerUpgradeStats;
    }

    public void Init()
    {
        closeButton.AddListener(HideUpgradeUI);
        InstantiateUpgradeContentUIs();
    }
    
    public void ShowUpgradeUI()
    {
        view.SetActive(true);
    }
    public void HideUpgradeUI()
    {
        view.SetActive(false);
    }

    void InstantiateUpgradeContentUIs()
    {
        foreach (UpgradeKind upgradeKind in Enum.GetValues(typeof(UpgradeKind)))
        {
            var upgradeContentUIMono = Instantiate(upgradeContentPrefab, Vector3.zero, Quaternion.identity, upgradeContentsParent.transform);
            
            switch (upgradeKind)
            {
                case UpgradeKind.Test1:
                    upgradeContentUIMono.SetUp("test1",
                        () => { Debug.Log("upgrade test1"); },
                        () => 10,
                        () => 10,
                        () => 20,
                        () => 100); // the last arg is for test
                    break;
                case UpgradeKind.Test2:
                    upgradeContentUIMono.SetUp("test2",
                        () => {Debug.Log("upgrade test2");},
                        () => 1000,
                        () => 100,
                        () => 200,
                        () => _flamePoint.FlamePointValue);
                    break;
                default:
                    Debug.LogWarning($"Unsupported UpgradeKind {upgradeKind}");
                    break;
            }
            
            upgradeContentUIMono.SetUp(
                "ATK Attack",
                () => _playerUpgradeStats.OnCompleteUpgrade(upgradeKind),
                        () => _playerUpgradeStats.UpgradeCost(upgradeKind),
                
                
                );
        }
        
    }
}
