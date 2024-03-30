using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using UnityEditor;
using UnityEngine;

namespace DungeonCrawler
{
    public class TorchControllerMono : MonoBehaviour
    {
        Torch _torch;
        
        public void Construct(Torch torch)
        {
            _torch = torch;
            _torch.GridPosition = () => GridConverter.WorldPositionToGridPosition(transform.position);
            _torch.OnPicked += (sender, e) =>
            {
                Destroy(gameObject);
            };
        }
    }
}
