using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using R3;
using UnityEngine;
using VContainer;

public class TorchUIMono : MonoBehaviour
{
    [SerializeField] TorchInventoryUIMono _torchInventoryUIMono;
    
    TorchSystem _torchSystem = null!;

    [Inject]
    public void Construct(TorchSystem torchSystem)
    {
        _torchSystem = torchSystem;
        SetUp();
    }

    void SetUp()
    {
        Observable.EveryValueChanged(this, _ => _torchSystem.TorchInventoryNum())
            .Subscribe(_ =>
            {
                _torchInventoryUIMono.SetTorchInventoryNum( _torchSystem.TorchInventoryNum());
            }); 
    }
}
