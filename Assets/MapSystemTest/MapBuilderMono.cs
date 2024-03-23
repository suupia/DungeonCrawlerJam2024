#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.MapSystem.Scripts;
using DungeonCrawler.MapSystem.Scripts.Entity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DungeonCrawler
{
    // This script is for TESTING purposes only.
    public class MapBuilderMono : MonoBehaviour
    {
        [SerializeField] TileMono tilePrefab = null!;
        [SerializeField] Button buildButton = null!;

        readonly List<GameObject> _pool = new List<GameObject>();
        void Start()
        {
            buildButton.onClick.AddListener(() =>
            {
                Debug.Log("Build Dungeon");
                
                // Domain
                var map = new EntityGridMap(new SquareGridCoordinate(10, 10));
                var dungeonBuilder = new DungeonBuilder(new SquareGridCoordinate(10, 10));
                map = dungeonBuilder.CreateDungeon();
                
                // Mono
                foreach(var tile in _pool)
                {
                    Destroy(tile);
                }
                for(int y = 0; y < map.Height; y++)
                {
                    for(int x = 0; x < map.Width; x++)
                    {
                        var tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                        _pool.Add(tile.gameObject);
                        if(map.GetSingleEntity<IEntity>(x,y) is {} entity)
                        {
                            tile.SetSprite(entity);
                        }
                        # region Comment
                        // This has the same meaning as the following code:
                        
                        // var entity = map.GetSingleEntity<IEntity>(x,y);
                        // if(entity != null)
                        // {
                        //     tile.SetSprite(entity);
                        // }
                        # endregion
                        
                    }
                }
            });
        }
        
    }
}
