using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class HangerUIMono : MonoBehaviour
{
    [SerializeField] HangerGaugeMono _hangerGauge;

    HangerSystem _hangerSystem;

    [Inject]
    public void Construct(HangerSystem hangerSystem)
    {
        _hangerSystem = hangerSystem;
        
        SetUp();
    }

    void SetUp()
    {
        Observable.EveryValueChanged(this, _ => _hangerSystem.CurrentHangerRate())
            .Subscribe(_ =>
            {
                _hangerGauge.FillRate(_hangerSystem.CurrentHangerRate());
            });
    }

}
