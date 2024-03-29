using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] GameObject view;
    
    public void Show()
    {
        Debug.Log("BattleUI.Show()");
        view.SetActive(true);
    }
    public void Hide()
    {
        Debug.Log("BattleUI.Hide()");
        view.SetActive(false);
    }
}
