#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIMono : MonoBehaviour
{
    [SerializeField] GameObject titleView = null!;
    [SerializeField] UpgradeUIMono upgradeUI = null!;
    
    public void Init()
    {
        // Hide upgrade ui when the game started
        upgradeUI.HideUpgradeUI();
    }
    public void ShowTitleUI()
    {
        Debug.Log("TitleUI.Show()");
        titleView.SetActive(true);
    }
    
    public void HideTitleUI()
    {
        Debug.Log("TitleUI.Hide()");
        titleView.SetActive(false);
    }


}
