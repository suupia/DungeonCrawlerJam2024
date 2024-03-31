using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class HangerGaugeMono : MonoBehaviour
{
    [SerializeField] Image fillGauge = null!;
    
    public void FillRate(float rate) => fillGauge.fillAmount = rate;
}
