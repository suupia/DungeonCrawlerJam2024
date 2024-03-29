#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;

namespace DungeonCrawler.MapAssembly.Classes.Entity
{
    public class CharacterWall: IGridEntity
    {
        public override string ToString()
        {
            return "W";
        }
    }
    
    public class CharacterPath: IGridEntity
    {
        public override string ToString()
        {
            return "P";
        }
    }
    
    public class CharacterRoom: IGridEntity
    {
        public override string ToString()
        {
            return "R";
        }
    }
    
    public class CharacterStairs: IGridEntity
    {
        public override string ToString()
        {
            return "S";
        }
    }
}