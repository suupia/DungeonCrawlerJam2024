#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIMono : MonoBehaviour
{
    [SerializeField] GameObject titleView = null!;
    
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
