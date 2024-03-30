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
    BattleSimulator _battleSimulator;
    
    [Inject]
    public void Construct(
        GameStateSwitcher gameStateSwitcher,
        BattleSimulator battleSimulator
        )
    {
        _gameStateSwitcher = gameStateSwitcher;
        _battleSimulator = battleSimulator;
    }
    void Start()
    {
        Observable.EveryValueChanged(this, _ => _gameStateSwitcher.IsInBattle())
            .Subscribe(isOn =>
            {
                if(isOn) battleUI.ShowBattleUI();
                else battleUI.HideBattleUI();
            });
        Observable.EveryValueChanged(this, _ => _battleSimulator.IsInResult)
            .Subscribe(isOn =>
            {
                if(isOn) battleUI.ShowResultUI();
                else battleUI.HideResultUI();
            });
    }
}
