using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HpGaugeMono : MonoBehaviour
{
    [SerializeField] Image fillGauge = null!;
    
    public void FillRate(float rate)
    {
        fillGauge.fillAmount = rate;
    }
}
