using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using R3;
using TMPro;
using UnityEngine;
using VContainer;

public class FloorUIMono : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI floorText = null!;

    DungeonSwitcher _dungeonSwitcher = null!;

    [Inject]
    public void Construct(DungeonSwitcher dungeonSwitcher)
    {
        _dungeonSwitcher = dungeonSwitcher;
        SetUp();
    }

    void SetUp()
    {
        Observable.EveryValueChanged(this, _ => _dungeonSwitcher.Floor)
            .Subscribe(_ =>
            {
                floorText.text = $"Floor: B{_dungeonSwitcher.Floor}F";
            }); 
    }
}
