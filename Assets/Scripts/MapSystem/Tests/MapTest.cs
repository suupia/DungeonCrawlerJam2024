using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapSystem.Scripts;
using DungeonCrawler.MapSystem.Scripts.Entity;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
    
    [Test]
    public void TestDungeonBuilder()
    {
        // Arrange
        var dungeonBuilder = new DungeonBuilder(new SquareGridCoordinate(10, 10));
    
        // Act
        var map = dungeonBuilder.CreateDungeon();

        // Debug
        map.DebugPrint();
        
        // Assert
        Assert.AreEqual(10, map.Width);
        Assert.AreEqual(10, map.Height);
        Assert.AreEqual(100, map.Length);
    }
}
