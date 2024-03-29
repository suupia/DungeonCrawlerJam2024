#nullable enable
using System.Collections.Generic;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using VContainer;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes
{
    // Compromise with inheritance because I don't want to create the interface just for NullObjects
    public class DefaultDungeonGridMap : DungeonGridMap
    {
        [Inject]   
        public DefaultDungeonGridMap(IGridCoordinate coordinate) : base(
            new PlainDungeonGridMap(new EntityGridMap(coordinate), new List<Area>(), new List<Path>()))
        {
        }
    }
}