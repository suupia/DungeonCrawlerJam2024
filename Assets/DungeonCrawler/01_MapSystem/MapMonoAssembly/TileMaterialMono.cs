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

        public void ResetSprites()
        {
            floorMeshRenderer.gameObject.SetActive(false);
            foreach(var wallSpriteRenderer in wallMeshRenderers)
            {
                wallSpriteRenderer.gameObject.SetActive(false);
            }
        }
        public void SetFloorSprite(IGridTile gridTile)
        {
            floorMeshRenderer.gameObject.SetActive(true);

            if(gridTile is CharacterWall)
            {
                // floorMeshRenderer.gameObject.SetActive(true);
                floorMeshRenderer.material = wallMaterial;
            }
            else if(gridTile is CharacterPath)
            {
                // floorMeshRenderer.gameObject.SetActive(true);
                floorMeshRenderer.material = pathMaterial;
            }
            else if(gridTile is CharacterRoom)
            {
                // floorMeshRenderer.gameObject.SetActive(true);
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
                wallMeshRenderers[direction.Id].gameObject.SetActive(true);
                wallMeshRenderers[direction.Id].material = wallMaterial;
            }
            else if(gridTile is CharacterPath)
            {
                wallMeshRenderers[direction.Id].gameObject.SetActive(false);
            }
            else if(gridTile is CharacterRoom)
            {
                wallMeshRenderers[direction.Id].gameObject.SetActive(false);
            }
            else
            {
                wallMeshRenderers[direction.Id].gameObject.SetActive(false);
            }
        }
        
        public void SetDebugText(string text)
        {
            
        }

    }
}