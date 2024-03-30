using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using UnityEngine;

public class UpgradeUIMono : MonoBehaviour
{
    [SerializeField] GameObject view;
    [SerializeField] UpgradeContentUIMono upgradeContentPrefab;
    [SerializeField] CustomButton closeButton;

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
}
