#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;

namespace DungeonCrawler.MapAssembly.Classes.Entity
{
    public class CharacterWall: IEntity
    {
        public override string ToString()
        {
            return "W";
        }
    }
    
    public class CharacterPath: IEntity
    {
        public override string ToString()
        {
            return "P";
        }
    }
    
    public class CharacterRoom: IEntity
    {
        public override string ToString()
        {
            return "R";
        }
    }
    
    public class CharacterStairs: IEntity
    {
        public override string ToString()
        {
            return "S";
        }
    }
}