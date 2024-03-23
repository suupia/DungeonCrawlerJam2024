#nullable enable
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.MapSystem.Scripts.Entity;
using UnityEngine;

namespace DungeonCrawler
{
    // This script is for TESTING purposes only.
    public class TileMono : MonoBehaviour
    {
        [SerializeField] Sprite wallSprite = null!;
        [SerializeField] Sprite pathSprite = null!;
        [SerializeField] SpriteRenderer _floorSpriteRenderer = null!;

        public void SetSprite(IEntity entity)
        {
            Debug.Log("SetSprite");
            if(entity is CharacterWall)
            {
                _floorSpriteRenderer.sprite = wallSprite;
            }
            else if(entity is CharacterPath)
            {
                _floorSpriteRenderer.sprite = pathSprite;
            }
        }

    }
}