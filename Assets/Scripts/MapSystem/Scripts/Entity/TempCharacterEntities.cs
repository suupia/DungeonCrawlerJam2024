#nullable enable
using DungeonCrawlerMap.Interfaces;

namespace MapSystem.Scripts.Entity
{
    public class CharacterWall: IEntity
    {
        public override string ToString()
        {
            return "#";
        }
    }
    
    public class CharacterPath: IEntity
    {
        public override string ToString()
        {
            return ".";
        }
    }
}