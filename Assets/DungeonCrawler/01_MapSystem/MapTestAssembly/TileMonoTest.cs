#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapAssembly.Classes.Entity;
using UnityEngine;
using TMPro;

namespace DungeonCrawler
{
    // This script is for TESTING purposes only.
    public class TileMonoTest : MonoBehaviour
    {
        [SerializeField] Sprite wallSprite = null!;
        [SerializeField] Sprite pathSprite = null!;
        [SerializeField] Sprite roomSprite = null!;
        [SerializeField] SpriteRenderer floorSpriteRenderer = null!;
        [SerializeField] TextMeshPro debugText = null!;

        public void SetSprite(IGridEntity gridEntity)
        {
            if(gridEntity is CharacterWall)
            {
                floorSpriteRenderer.sprite = wallSprite;
            }
            else if(gridEntity is CharacterPath)
            {
                floorSpriteRenderer.sprite = pathSprite;
            }
            else if(gridEntity is CharacterRoom)
            {
                floorSpriteRenderer.sprite = roomSprite;
            }
            else
            {
                Debug.LogError("Unknown entity");
            }
        }
        
        public void SetDebugText(string text)
        {
            debugText.text = text;
        }

    }
}