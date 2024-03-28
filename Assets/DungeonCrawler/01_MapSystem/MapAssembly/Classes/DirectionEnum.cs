#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class DirectionEnum : IComparable
    {
        public static DirectionEnum North = new DirectionEnum(0);
        public static DirectionEnum East = new DirectionEnum(1);
        public static DirectionEnum South = new DirectionEnum(2);
        public static DirectionEnum West = new DirectionEnum(3);
        public int Id { get; set; }
        public (int x, int y) Vector { get; private set; }
        static readonly List<DirectionEnum> List = new List<DirectionEnum>();
        public static IEnumerable<DirectionEnum> GetAll() => List;

        DirectionEnum(int id)
        {
            Id = id;
            Vector = id switch
            {
                0 => (0, 1),
                1 => (1, 0),
                2 => (0, -1),
                3 => (-1, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
            List.Add(this);
        }

        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType()) return false;
            var other = (DirectionEnum)obj;
            return Id == other.Id;
        }

        public int CompareTo(object other) => Id.CompareTo(((DirectionEnum)other).Id);

        // Other utility methods ...
    }
}