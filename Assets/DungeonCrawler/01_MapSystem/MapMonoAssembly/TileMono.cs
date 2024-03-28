#nullable enable
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapAssembly.Classes.Entity;
using UnityEngine;
using TMPro;

namespace DungeonCrawler.MapMonoAssembly
{
    public class TileMono : MonoBehaviour
    {
        [SerializeField] Sprite wallSprite = null!;
        [SerializeField] Sprite pathSprite = null!;
        [SerializeField] Sprite roomSprite = null!;
        
        [SerializeField] SpriteRenderer floorSpriteRenderer = null!;
        [SerializeField] SpriteRenderer[] wallSpriteRenderers = null!;
        [SerializeField] TextMeshPro debugText = null!;

        public void SetFloorSprite(IEntity entity)
        {
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
        
        public void SetWallSprite(IEntity entity, DirectionEnum direction)
        {
            var directionId = direction.Id;
            wallSpriteRenderers[directionId].sprite = wallSprite;
        }
        
        public void SetDebugText(string text)
        {
            debugText.text = text;
        }

    }
}