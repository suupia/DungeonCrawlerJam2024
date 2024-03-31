using System;
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using DungeonCrawler._10_UpgradeSystem.UpgradeAssembly;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using VContainer;
using FlamePoint = DungeonCrawler._10_UpgradeSystem.UpgradeAssembly.FlamePoint;
using Observable = R3.Observable;


public class UpgradeUIMono : MonoBehaviour
{
    [SerializeField] GameObject view;
    [SerializeField] GameObject upgradeContentsParent;
    [SerializeField] UpgradeContentUIMono upgradeContentPrefab;
    [SerializeField] CustomButton closeButton;
    [SerializeField] TextMeshProUGUI flamePointText;

    FlamePoint _flamePoint;
    
    PlayerUpgradeStats _playerUpgradeStats;
    PlayerStats _playerStats;
    

    [Inject]
    public void Construct(
        FlamePoint flamePoint,
        PlayerUpgradeStats playerUpgradeStats,
        PlayerStats playerStats
        )
    {
        _flamePoint = flamePoint;
        _playerUpgradeStats = playerUpgradeStats;
        _playerStats = playerStats;
    }

    public void Init()
    {
        closeButton.AddListener(HideUpgradeUI);
        InstantiateUpgradeContentUIs();
        
        Observable.EveryValueChanged(this, _ => _flamePoint.FlamePointValue)
            .Subscribe(_ =>
            {
                flamePointText.text = $"Flame point : {_flamePoint.FlamePointValue}";
            }); 
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
                case UpgradeKind.PlayerAttack:
                    upgradeContentUIMono.SetUp(
                        "Attack",
                        () => _playerUpgradeStats.Upgrade(upgradeKind),
                        () => _playerUpgradeStats.UpgradeCost(upgradeKind),
                        () => _playerStats.Atk,
                        () => _playerStats.Atk + _playerUpgradeStats.AtkUpgradeDelta,
                        () => _flamePoint.FlamePointValue
                    );
                    break;
                case UpgradeKind.PlayerHp:
                    upgradeContentUIMono.SetUp("MaxHP",
                        () => _playerUpgradeStats.Upgrade(upgradeKind),
                        () => _playerUpgradeStats.UpgradeCost(upgradeKind),
                        () => _playerStats.MaxHp,
                        () => _playerStats.MaxHp + _playerUpgradeStats.MaxHpUpgradeDelta,
                        () => _flamePoint.FlamePointValue
                    );
                    break;
                default:
                    Debug.LogWarning($"Unsupported UpgradeKind {upgradeKind}");
                    break;
            }
        }
    }
}
