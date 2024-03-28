﻿#nullable enable
using DungeonCrawler.MapAssembly.Classes;

namespace DungeonCrawler.MapAssembly.Interfaces
{
    public interface IMapBuilderMonoSystem
    {
        public void CreateDungeon();
        
        // this should be change
        public EntityGridMap _Map();
    }
}