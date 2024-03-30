using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] GameObject battleView;
    [SerializeField] GameObject resultView;
    
    public void ShowBattleUI()
    {
        Debug.Log("BattleUI.Show()");
        battleView.SetActive(true);
    }
    public void HideBattleUI()
    {
        Debug.Log("BattleUI.Hide()");
        battleView.SetActive(false);
    }
    
    public void ShowResultUI()
    {
        Debug.Log("BattleUI.ShowResultUI()");
        resultView.SetActive(true);
    }
    
    public void HideResultUI()
    {
        Debug.Log("BattleUI.HideResultUI()");
        resultView.SetActive(false);
    }
}
