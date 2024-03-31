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
        Instantiate(upgradeContentPrefab, Vector3.zero, Quaternion.identity, upgradeContentsParent.transform);    }
}
