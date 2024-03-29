using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] GameObject view;
    
    public void Show()
    {
        view.SetActive(true);
    }
    public void Hide()
    {
        view.SetActive(false);
    }
}
