using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using R3;
using UnityEngine;
using VContainer;

public class UISwitcherMono : MonoBehaviour
{
    [SerializeField] BattleUI battleUI;
    
    GameStateSwitcher _gameStateSwitcher;
    
    [Inject]
    public void Construct( GameStateSwitcher gameStateSwitcher)
    {
        _gameStateSwitcher = gameStateSwitcher;
    }
    void Start()
    {
        Observable.EveryValueChanged(this, _ => _gameStateSwitcher.IsInBattle())
            .Subscribe(isOn =>
            {
                if(isOn) battleUI.Show();
                else battleUI.Hide();
            }); 
    }
}
