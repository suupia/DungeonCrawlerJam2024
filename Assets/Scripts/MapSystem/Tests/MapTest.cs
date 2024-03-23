using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DungeonCrawler.Map.Scripts;
using MapSystem.Scripts.Entity;

public class MapTest
{
    [Test]
    public void TestEntityGridMap()
    {
        // Arrange
        var entityGridMap = new EntityGridMap(new SquareGridCoordinate(10, 10));
        
        // Act
        entityGridMap.FillAll(new CharacterWall());
        
        // Debug
        entityGridMap.DebugPrint();
        
        // Assert
        Assert.AreEqual(10, entityGridMap.Width);
        Assert.AreEqual(10, entityGridMap.Height);
        Assert.AreEqual(100, entityGridMap.Length);
    }
}
