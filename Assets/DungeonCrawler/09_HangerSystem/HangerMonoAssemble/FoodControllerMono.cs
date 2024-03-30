#nullable enable
using System.Collections;
using System.Collections.Generic;
using CodiceApp.EventTracking;
using DungeonCrawler.MapAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler
{
    public class FoodControllerMono : MonoBehaviour
    {
        Food _food;
        
        public void Construct(Food food)
        {
            _food = food;
            _food.GridPosition = () => GridConverter.WorldPositionToGridPosition(transform.position);
            _food.OnEaten += (sender, e) =>
            {
                Destroy(gameObject);
            };
        }
    }
}
