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

        public void ResetSprites()
        {
            floorSpriteRenderer.sprite = null;
            foreach(var wallSpriteRenderer in wallSpriteRenderers)
            {
                wallSpriteRenderer.sprite = null;
            }
        }
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
            if(entity is CharacterWall)
            {
                wallSpriteRenderers[direction.Id].sprite = wallSprite;
            }
            else if(entity is CharacterPath)
            {
                wallSpriteRenderers[direction.Id].sprite = null;
            }
            else if(entity is CharacterRoom)
            {
                wallSpriteRenderers[direction.Id].sprite = null;
            }
            else
            {
                wallSpriteRenderers[direction.Id].sprite = null;
            }
        }
        
        public void SetDebugText(string text)
        {
            debugText.text = text;
        }

    }
}