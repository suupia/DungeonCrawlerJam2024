using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitcherMono : MonoBehaviour
{
    [SerializeField] BattleUI battleUI;
    
    void Start()
    {
        battleUI.Hide();
    }
}
