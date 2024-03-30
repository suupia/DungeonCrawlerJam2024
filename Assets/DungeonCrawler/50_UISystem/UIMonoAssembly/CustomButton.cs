using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomButton : UIBehaviour
{
    Button Button
    {
        get
        {
            if (_button == null) _button = GetComponent<Button>();
            return _button;
        }
    }
    Button _button;
    
    public void ChangeEnableState(bool enableState)
    {
        Button.enabled = enableState;
    }

    public bool Interactable
    {
        get => Button.interactable;
        set => Button.interactable = value;
    }
        
    public void AddListener(Action action)
    {
        Button.onClick.AddListener(() =>
        {
            action();
        });
    }
        
    public void AddListener<T>(Action<T> action,T value)
    {
        Button.onClick.AddListener(() =>
        {
            action(value);
        });
    }
        
    public void ClearListeners()
    {
        Button.onClick.RemoveAllListeners();
    }


}
