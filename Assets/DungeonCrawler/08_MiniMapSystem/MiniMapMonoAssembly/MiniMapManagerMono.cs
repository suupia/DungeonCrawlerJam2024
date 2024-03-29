using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEditor.Experimental.Licensing;
using UnityEngine;

namespace DungeonCrawler
{
    public class MiniMapManagerMono : MonoBehaviour
    {
        [SerializeField] MiniMapTileMono _miniMapTileMono;
        List<MiniMapTileMono> _miniMapTileMonos;

        public void InitMiniMap(EntityGridMap entityGridMap)
        {
            for (int index = 0; i < entityGridMap.Length; i++)
            {
                var entities = entityGridMap.GetAllTypeList(index);

                foreach (var entity in entities)
                {
                    
                }
            }
        }
    }
}
