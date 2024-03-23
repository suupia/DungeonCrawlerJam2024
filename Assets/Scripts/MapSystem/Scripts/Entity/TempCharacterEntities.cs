#nullable enable
using DungeonCrawler.MapSystem.Interfaces;

namespace DungeonCrawler.MapSystem.Scripts.Entity
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