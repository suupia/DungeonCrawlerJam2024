#nullable enable
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapAssembly.Classes.Entity;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace DungeonCrawler.MapMonoAssembly
{
    public class TileMaterialMono : MonoBehaviour
    {
        [SerializeField] Material wallMaterial = null!;
        [SerializeField] Material pathMaterial = null!;
        [SerializeField] Material roomMaterial = null!;
        
        [SerializeField] MeshRenderer floorMeshRenderer = null!;
        [SerializeField] MeshRenderer[] wallMeshRenderers = null!;
        [SerializeField] TextMeshPro debugText = null!;

        public void ResetSprites()
        {
            floorMeshRenderer.material = null;
            foreach(var wallSpriteRenderer in wallMeshRenderers)
            {
                wallSpriteRenderer.material = null;
            }
        }
        public void SetFloorSprite(IGridTile gridTile)
        {
            if(gridTile is CharacterWall)
            {
                floorMeshRenderer.material = wallMaterial;
            }
            else if(gridTile is CharacterPath)
            {
                floorMeshRenderer.material = pathMaterial;
            }
            else if(gridTile is CharacterRoom)
            {
                floorMeshRenderer.material = roomMaterial;
            }
            else
            {
                Debug.LogError("Unknown entity");
            }
            
        }
        
        public void SetWallSprite(IGridTile gridTile, DirectionEnum direction)
        {
            if(gridTile is CharacterWall)
            {
                wallMeshRenderers[direction.Id].material = wallMaterial;
            }
            else if(gridTile is CharacterPath)
            {
                wallMeshRenderers[direction.Id].material = null;
            }
            else if(gridTile is CharacterRoom)
            {
                wallMeshRenderers[direction.Id].material = null;
            }
            else
            {
                wallMeshRenderers[direction.Id].material = null;
            }
        }
        
        public void SetDebugText(string text)
        {
            debugText.text = text;
        }

    }
}