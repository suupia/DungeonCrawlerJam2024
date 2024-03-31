using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUIMono : MonoBehaviour
{
    [SerializeField] GameObject view;
    [SerializeField] CustomButton closeButton;

    void Start()
    {
        closeButton.AddListener(HideSettingUI);
    }
    public void ShowSettingUI()
    {
        view.SetActive(true);
    }
    public void HideSettingUI()
    {
        view.SetActive(false);
    }
} 
