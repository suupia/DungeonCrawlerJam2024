#nullable enable
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.MapSystem.Scripts.Entity;
using UnityEngine;
using TMPro;

namespace DungeonCrawler
{
    // This script is for TESTING purposes only.
    public class TileMono : MonoBehaviour
    {
        [SerializeField] Sprite wallSprite = null!;
        [SerializeField] Sprite pathSprite = null!;
        [SerializeField] Sprite roomSprite = null!;
        [SerializeField] SpriteRenderer floorSpriteRenderer = null!;
        [SerializeField] TextMeshProUGUI debugText = null!;

        public void SetSprite(IEntity entity)
        {
            Debug.Log("SetSprite");
            if(entity is CharacterWall)
            {
                floorSpriteRenderer.sprite = wallSprite;
            }
            else if(entity is CharacterPath)
            {
                floorSpriteRenderer.sprite = pathSprite;
            }
            else if(entity is CharacterRoom)
            {
                floorSpriteRenderer.sprite = roomSprite;
            }
            else
            {
                Debug.LogError("Unknown entity");
            }
        }

    }
}