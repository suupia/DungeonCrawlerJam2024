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

    [Inject]
    public void Construct(FlamePoint flamePoint)
    {
        _flamePoint = flamePoint;
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
        foreach (var upgradeKind in Enum.GetValues(typeof(UpgradeKind)))
        {
            var upgradeContentUIMono = Instantiate(upgradeContentPrefab, Vector3.zero, Quaternion.identity, upgradeContentsParent.transform);
            upgradeContentUIMono.SetFlamePoint(_flamePoint);
            switch (upgradeKind)
            {
                case UpgradeKind.Test1:
                    upgradeContentUIMono.SetUp("test1", 10,
                        () => {Debug.Log("upgrade test1");},
                        () => 0,
                        () => 1);
                    break;
                case UpgradeKind.Test2:
                    break;
                default:
                    Debug.LogWarning($"Unsupported UpgradeKind {upgradeKind}");
                    break;
            }
        }
        
    }
}
