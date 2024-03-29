using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using DungeonCrawler;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

public class MiniMapTileMono : MonoBehaviour
{
    [SerializeField] Sprite wallSprite;
    [SerializeField] Sprite groundSprite;
    
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite enemySprite;
    [SerializeField] Sprite itemSprite;

    [SerializeField] SpriteRenderer _spriteRenderer;

    public void ResetSprite()
    {
        _spriteRenderer.sprite = null;
    }

    public void SetTileSprite(IGridTile gridTile)
    {
        if (gridTile is CharacterWall)
        {
            _spriteRenderer.sprite = wallSprite;
            _spriteRenderer.sortingOrder = 0;
        }
        else if (gridTile is CharacterRoom || gridTile is CharacterPath)
        {
            _spriteRenderer.sprite = groundSprite;
            _spriteRenderer.sortingOrder = 0;
        }
        else if (gridTile is Stairs)
        {
            _spriteRenderer.sprite = enemySprite;
            _spriteRenderer.sortingOrder = 1;
        }
        else if (gridTile is Enemy)
        {
            _spriteRenderer.sprite = enemySprite;
            _spriteRenderer.sortingOrder = 1;
        }
        else if (gridTile is Torch)
        {
            _spriteRenderer.sprite = itemSprite;
            _spriteRenderer.sortingOrder = 1;
        }
        else if (gridTile is Player)
        {
            _spriteRenderer.sprite = playerSprite;
            _spriteRenderer.sortingOrder = 10;
        }
        else
        {
            Debug.LogError("Unknown entity");
        }
    }
}
