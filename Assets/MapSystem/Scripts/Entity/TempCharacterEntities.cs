#nullable enable
using DungeonCrawler.MapSystem.Interfaces;

namespace DungeonCrawler.MapSystem.Scripts.Entity
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
    
    public class CharacterArea: IEntity
    {
        public override string ToString()
        {
            return "A";
        }
    }

    public class CharacterPlayerSpawnPosition : IEntity
    {
        public override string ToString()
        {
            return "S";
        }
    }
}