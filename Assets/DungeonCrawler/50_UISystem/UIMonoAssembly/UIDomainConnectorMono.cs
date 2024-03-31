using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using R3;
using UnityEngine;
using VContainer;

public class UIDomainConnectorMono : MonoBehaviour
{
    [SerializeField] BattleUI battleUI;
    [SerializeField] TitleUIMono titleUI;
    
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
        SetUp();
    }
    void SetUp()
    {
        Debug.Log($"UIDomainConnectorMono battleUI:{battleUI}");
        Debug.Log($"UIDomainConnectorMono titleUI:{titleUI}");
        Debug.Log($"UIDomainConnectorMono _gameStateSwitcher:{_gameStateSwitcher}");
        Debug.Log($"UIDomainConnectorMono _battleSimulator:{_battleSimulator}");
        
        // TitleUI
        titleUI.Init();
        Observable.EveryValueChanged(this, _ => _gameStateSwitcher.IsInTitle())
            .Subscribe(isOn =>
            {
                if(isOn) titleUI.ShowTitleUI();
                else titleUI.HideTitleUI();
            });

        // BattleUI
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
