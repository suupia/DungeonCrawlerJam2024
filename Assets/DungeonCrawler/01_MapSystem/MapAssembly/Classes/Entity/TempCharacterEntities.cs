#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;

namespace DungeonCrawler.MapAssembly.Classes.Entity
{
    public class CharacterWall: IGridTile
    {
        public override string ToString()
        {
            return "W";
        }
    }
    
    public class CharacterPath: IGridTile
    {
        public override string ToString()
        {
            return "P";
        }
    }
    
    public class CharacterRoom: IGridTile
    {
        public override string ToString()
        {
            return "R";
        }
    }
    
}