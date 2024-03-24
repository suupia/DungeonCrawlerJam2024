#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace DungeonCrawler.MapSystem.Scripts
{
    public class DivideAreaExecutor
    {
        public const int MinRoomMargin = 2;  // This should be 2 or greater, because the rooms are connected to each other by a path.
        const int MinRoomSize = 3;
        const int MaxRoomSize = 25;
        const int MinAreaSize = MinRoomSize + MinRoomMargin * 2;
        public List<Area> DivideAreaOnce(List<Area> areas)
        {
            var frontArea = areas.First(); // todo : Pop the largest area
            if (CanDivideArea(frontArea))
            {
                var (area1, area2) = DivideArea(frontArea); 
                areas.RemoveAt(0);
                areas.Add(area1);
                areas.Add(area2);
            }
            
            return areas;
        }
        
        (Area area1, Area area2) DivideArea(Area area)
        {
            // [Precondition]
            Assert.IsTrue(CanDivideArea(area));

            bool isDivideByVertical = area.Width >= area.Height;  // todo : Randomize this may be more interesting
            
            var divideCoord = RandomizeCoord(isDivideByVertical, area);
            
            var (area1, area2) = DivideAreaByCoord(area, divideCoord, isDivideByVertical);

            var path = new Path(new List<(int x, int y)>(),divideCoord);
            var connectPath = CreatePath(area1, area2, divideCoord, isDivideByVertical);
            path.Points.AddRange(connectPath.Points);
            area1.AdjacentAreas.Add((area2, connectPath));  // This process must be done after AddRoomEach
            area2.AdjacentAreas.Add((area1, connectPath));
            
            // Update path of area <-> adjacentArea
            foreach (var (adjacentArea, adjacentPath) in area.AdjacentAreas)
            {
                // 1. Update path of area -> adjacentArea to area1 -> adjacentArea
                // (Caution) you don't have to area.AdjacentAreas.Remove((adjacentArea, adjacentPath)); , because area is never used after this process.
                // And you cannot remove the element from the list while iterating the list.
                area1.AdjacentAreas.Add((adjacentArea, CreatePath(area1,adjacentArea, adjacentPath.DivideX,isDivideByVertical)));
                
                // 2. Update path of adjacentArea -> area to adjacentArea -> area1
                adjacentArea.AdjacentAreas.Remove((area, adjacentPath));
                adjacentArea.AdjacentAreas.Add((area1, CreatePath(adjacentArea, area1, adjacentPath.DivideX, isDivideByVertical)));
                
            }
            
            // Debug
            Debug.Log($"DivideArea: X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");

            return (area1, area2);

        }
        
        public int RandomizeCoord(bool isDivideByVertical, Area area)
        {
            int areaSize = isDivideByVertical ? area.Width : area.Height;
            var minX = MinAreaSize; // Ensure that the room can be placed in the left area
            var maxX = areaSize - MinAreaSize; // Ensure that the room can be placed in the right area
            Assert.IsTrue(minX < maxX, $"minX: {minX}, maxX: {maxX}, areaSize: {areaSize}");
            var divideCoord = Random.Range(minX, maxX);
            return divideCoord;
        }

        public (Area area1, Area area2) DivideAreaByCoord(Area area, int divideCoord, bool isDivideByVertical)
        {
            var (dividedArea1, dividedArea2) = isDivideByVertical
                ? (
                    new Area(area.X, area.Y, divideCoord, area.Height, null, new List<(Area area, Path path)>()),
                    new Area(area.X + divideCoord, area.Y, area.Width - divideCoord, area.Height, null,new List<(Area area, Path path)>())
                )
                : (
                    new Area(area.X, area.Y, area.Width, divideCoord, null,new List<(Area area, Path path)>()),
                    new Area(area.X, area.Y + divideCoord, area.Width, area.Height - divideCoord, null,new List<(Area area, Path path)>())
                );
            return (AddRoom(dividedArea1), AddRoom(dividedArea2));
        }
        
        public Path CreatePath(Area area1, Area area2, int divideX, bool isDivideByVertical)
        {
            Assert.IsNotNull(area1.Room);
            Assert.IsNotNull(area2.Room);
            var path = new Path(new List<(int x, int y)>(),divideX);
            if (isDivideByVertical)
            {
                var randY1 = Random.Range(area1.Room.Y + 1, area1.Room.Y + area1.Room.Height - 1);  // Excluding both ends
                var (x1, y1) = (area1.Room.X + area1.Room.Width, randY1);
                var randY2 = Random.Range(area2.Room.Y + 1, area2.Room.Y + area2.Room.Height - 1);  // Excluding both ends
                var (x2, y2) = (area2.Room.X, randY2);
                while (x1 < divideX)
                {
                    path.Points.Add((x1, y1));
                    x1++;
                }
                while (x2 > divideX)
                {
                    path.Points.Add((x2, y2));
                    x2--;
                }
                var minY = Mathf.Min(y1, y2);
                var maxY = Mathf.Max(y1, y2);
                while (minY <= maxY)
                {
                    path.Points.Add((divideX, minY));
                    minY++;
                }
            }
            else
            {
                var randX1 = Random.Range(area1.Room.X + 1, area1.Room.X + area1.Room.Width - 1);  // Excluding both ends
                var (x1, y1) = (randX1, area1.Room.Y + area1.Room.Height);
                var randX2 = Random.Range(area2.Room.X + 1, area2.Room.X + area2.Room.Width - 1);  // Excluding both ends
                var (x2, y2) = (randX2, area2.Room.Y);
                while (y1 < divideX)
                {
                    path.Points.Add((x1, y1));
                    y1++;
                }
                while (y2 > divideX)
                {
                    path.Points.Add((x2, y2));
                    y2--;
                }
                var minX = Mathf.Min(x1, x2);
                var maxX = Mathf.Max(x1, x2);
                while (minX <= maxX)
                {
                    path.Points.Add((minX, divideX));
                    minX++;
                }
            }
            Debug.Log($"area1: ({area1.X}, {area1.Y}) -> area2: ({area2.X}, {area2.Y})");
            Debug.Log($"paths: {string.Join(',', path.Points.Select(p => $"({p.x},{p.y})"))}");
            return path;
        }
        
        bool CanDivideArea(Area area)
        {
            return area.Width > MinAreaSize*2 || area.Height > MinAreaSize*2;            
        }

        Area AddRoom(Area area)
        {
            var room = GenerateRandomRoom(area);
            return area with { Room = room };
        }
        Room GenerateRandomRoom(Area area)
        {
            var roomX = Random.Range(area.X + MinRoomMargin, area.X + area.Width - (MinRoomSize + MinRoomMargin * 2));
            var roomY = Random.Range(area.Y + MinRoomMargin, area.Y + area.Height - (MinRoomSize + MinRoomMargin * 2));
            var roomWidth = Random.Range(MinRoomSize, Mathf.Min(MaxRoomSize, area.Width - (roomX - area.X)));
            var roomHeight = Random.Range(MinRoomSize, Mathf.Min(MaxRoomSize, area.Height - (roomY - area.Y)));
    
            return new Room(roomX, roomY, roomWidth, roomHeight);
        }

    }
}