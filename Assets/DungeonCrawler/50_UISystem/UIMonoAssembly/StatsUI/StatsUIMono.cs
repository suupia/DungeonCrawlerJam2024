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
        Observable.EveryValueChanged(this, _ => _playerStats.PlayerLevel)
            .Subscribe(_ =>
            {
                levelText.text = $"LEVEL: {_playerStats.PlayerLevel}";
            }); 
        // Observable.EveryValueChanged(this, _ => _playerStats.PlayerHp)
        //     .Subscribe(_ =>
        //     {
        //         levelText.text = $"LEVEL: {_playerStats.PlayerLevel}";
        //     }); 
        Observable.EveryValueChanged(this, _ => _playerStats.PlayerAttack)
            .Subscribe(_ =>
            {
                atkText.text = $"ATK: {_playerStats.PlayerAttack}";
            }); 
        Observable.EveryValueChanged(this, _ => _playerStats.PlayerExp)
            .Subscribe(_ =>
            {
                expText.text = $"LEVEL: {_playerStats.PlayerExp}";
            }); 
    }
}
