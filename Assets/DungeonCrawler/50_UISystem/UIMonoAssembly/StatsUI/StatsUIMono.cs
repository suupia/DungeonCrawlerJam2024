using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using R3;
using TMPro;
using UnityEngine;
using VContainer;

public class StatsUIMono : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI expText;

    PlayerStats _playerStats;
    [Inject]
    public void Init(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        SetUp();
    }

    void SetUp()
    {
        Observable.EveryValueChanged(this, _ => _playerStats.Level)
            .Subscribe(_ =>
            {
                levelText.text = $"LEVEL: {_playerStats.Level}";
            }); 
        Observable.EveryValueChanged(this, _ => _playerStats.MaxHp)
            .Subscribe(_ =>
            {
                hpText.text = $"HP: {_playerStats.MaxHp}";
            }); 
        Observable.EveryValueChanged(this, _ => _playerStats.Atk)
            .Subscribe(_ =>
            {
                atkText.text = $"ATK: {_playerStats.Atk}";
            }); 
        Observable.EveryValueChanged(this, _ => _playerStats.Exp)
            .Subscribe(_ =>
            {
                expText.text = $"EXP: {_playerStats.Exp}";
            }); 
    }
}
