using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingMono : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = volumeSlider.value;
    }
}
