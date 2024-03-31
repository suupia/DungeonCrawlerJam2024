using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using DungeonCrawler.MapAssembly.Classes;
using R3;
using TMPro;
using UnityEngine;
using VContainer;

public class TorchInventoryUIMono : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI torchInventoryText = null!;

    public void SetTorchInventoryNum(int num)
    {
        torchInventoryText.text = $"Torch: {num}";
    }
}
