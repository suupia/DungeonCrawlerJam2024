#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class DivideAreaExecutor
    {
        public const int MinRoomMargin = 2;  // This should be 2 or greater, because the rooms are connected to each other by a path.
        const int MinRoomSize = 5;
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
            // [pre-conditions]
            Assert.IsTrue(CanDivideArea(area));

            bool isDivideByVertical = area.Width >= area.Height;  // todo : Randomize this may be more interesting
            
            var divideCoord = RandomizeCoord(isDivideByVertical, area);
            var (area1, area2) = DivideAreaByCoord(area, divideCoord, isDivideByVertical);
            var path = new Path(new List<(int x, int y)>(),(isDivideByVertical, divideCoord));
            var connectPath = CreatePath(area1, area2, divideCoord, isDivideByVertical);
            path.Points.AddRange(connectPath.Points);
            area1.AdjacentAreas.Add((area2, connectPath));  // This process must be done after AddRoomEach
            area2.AdjacentAreas.Add((area1, connectPath));
            
            // Update path of area <-> adjacentArea
            foreach (var (adjacentArea, adjacentPath) in area.AdjacentAreas)
            {
                // Step0. Decide which area is connected to adjacentArea
                var leaderArea = CalculateAreaDistance(area1, adjacentArea) < CalculateAreaDistance(area2, adjacentArea) ? area1 : area2;
                
                // Step1. Prepare path connecting area1 and adjacentArea
                var pathArea1ToAdjacent = CreatePath(leaderArea, adjacentArea, adjacentPath.DivideInfo.coord , adjacentPath.DivideInfo.isDivideByVertical);
                
                // Step2. Update path of area -> adjacentArea to area1 -> adjacentArea
                // (Caution) you don't have to area.AdjacentAreas.Remove((adjacentArea, adjacentPath)); , because area is never used after this process.
                // And you cannot remove the element from the list while iterating the list.
                leaderArea.AdjacentAreas.Add((adjacentArea, pathArea1ToAdjacent));
                
                // Step3. Update path of adjacentArea -> area to adjacentArea -> area1
                adjacentArea.AdjacentAreas.Remove((area, adjacentPath));
                adjacentArea.AdjacentAreas.Add((leaderArea, pathArea1ToAdjacent));
                
            }
            
            // Debug
            Debug.Log($"DivideArea: X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");

            return (area1, area2);

        }
        
        int CalculateAreaDistance(Area area1, Area area2)
        {
            // [pre-conditions]
            Assert.IsTrue(area1.X != area2.X || area1.Y != area2.Y, $"area1: ({area1.X}, {area1.Y}), area2: ({area2.X}, {area2.Y})");
            
            if(area1.X > area2.X) (area1, area2) = (area2, area1);
            int distance = area1.Y > area2.Y
                ? GridDistance((area1.X + area1.Width, area1.Y), (area2.X, area2.Y + area2.Height)) 
                : GridDistance((area1.X + area1.Width, area1.Y + area1.Height), (area2.X, area2.Y));

            return distance;
        }
        
        int GridDistance((int x, int y) point1, (int x, int y) point2)
        {
            return Mathf.Abs(point1.x - point2.x) + Mathf.Abs(point1.y - point2.y);
        }
        
        public int RandomizeCoord(bool isDivideByVertical, Area area)
        {
            int areaSize = isDivideByVertical ? area.Width : area.Height;
            int areaCoord = isDivideByVertical ? area.X : area.Y;
            var minX = areaCoord + MinAreaSize; // Ensure that the room can be placed in the left area
            var maxX = areaCoord + areaSize - MinAreaSize; // Ensure that the room can be placed in the right area
            Assert.IsTrue(minX < maxX, $"minX: {minX}, maxX: {maxX}, areaSize: {areaSize}"); 
            var divideCoord = Random.Range(minX, maxX);
            return divideCoord;
        }

        (Area area1, Area area2) DivideAreaByCoord(Area area, int divideCoord, bool isDivideByVertical)
        {
            var (dividedArea1, dividedArea2) = isDivideByVertical
                ? (
                    new Area(area.X, area.Y, (divideCoord-area.X), area.Height, null, new List<(Area area, Path path)>()),
                    new Area(area.X + (divideCoord - area.X), area.Y, area.Width - (divideCoord - area.X), area.Height, null,new List<(Area area, Path path)>())
                )
                : (
                    new Area(area.X, area.Y, area.Width, (divideCoord-area.Y), null,new List<(Area area, Path path)>()),
                    new Area(area.X, area.Y + (divideCoord - area.Y), area.Width, area.Height - (divideCoord - area.Y), null,new List<(Area area, Path path)>())
                );
            
            // [post-conditions]
            Assert.IsTrue(dividedArea1.Width >= MinAreaSize);
            Assert.IsTrue(dividedArea1.Height >= MinAreaSize);
            Assert.IsTrue(dividedArea2.Width >= MinAreaSize);
            Assert.IsTrue(dividedArea2.Height >= MinAreaSize);
            Assert.IsTrue(dividedArea1.Width <= area.Width);
            Assert.IsTrue(dividedArea1.Height <= area.Height);
            Assert.IsTrue(dividedArea2.Width <= area.Width);
            Assert.IsTrue(dividedArea2.Height <= area.Height);
            for(int x = dividedArea1.X; x < dividedArea1.X + dividedArea1.Width; x++)
            {
                for(int y = dividedArea1.Y; y < dividedArea1.Y + dividedArea1.Height; y++)
                {
                    Assert.IsTrue(area.X <= x && x <= area.X + area.Width, $"x: {x}, area.X: {area.X}, area.Width: {area.Width}, dividedArea1.X: {dividedArea1.X}, dividedArea1.Width: {dividedArea1.Width}");
                    Assert.IsTrue(area.Y <= y && y <= area.Y + area.Height, $"y: {y}, area.Y: {area.Y}, area.Height: {area.Height}, dividedArea1.Y: {dividedArea1.Y}, dividedArea1.Height: {dividedArea1.Height}");
                }
            }
            for(int x = dividedArea2.X; x < dividedArea2.X + dividedArea2.Width; x++)
            {
                for(int y = dividedArea2.Y; y < dividedArea2.Y + dividedArea2.Height; y++)
                {
                    Assert.IsTrue(area.X <= x && x <= area.X + area.Width, $"x: {x}, area.X: {area.X}, area.Width: {area.Width}, dividedArea2.X: {dividedArea2.X}, dividedArea2.Width: {dividedArea2.Width}");
                    Assert.IsTrue(area.Y <= y && y <= area.Y + area.Height, $"y: {y}, area.Y: {area.Y}, area.Height: {area.Height}, dividedArea2.Y: {dividedArea2.Y}, dividedArea2.Height: {dividedArea2.Height}");
                }
            }
            
            return (AddRoom(dividedArea1), AddRoom(dividedArea2));
        }
        
        public Path CreatePath(Area area1, Area area2, int divideCoord, bool isDivideByVertical)
        {
            // [pre-conditions]
            Assert.IsNotNull(area1.Room);
            Assert.IsNotNull(area2.Room);
            if (isDivideByVertical)
            {
                if(area1.X > area2.X) (area1, area2) = (area2, area1);
                Assert.IsTrue(area1.X < area2.X, $" area1: ({area1.X}, {area1.Y}), area2: ({area2.X}, {area2.Y})");
            }
            else
            {
                if(area1.Y > area2.Y) (area1, area2) = (area2, area1);
                Assert.IsTrue(area1.Y < area2.Y, $" area1: ({area1.X}, {area1.Y}), area2: ({area2.X}, {area2.Y})");
            }
            
            var path = new Path(new List<(int x, int y)>(),(isDivideByVertical, divideCoord));
            if (isDivideByVertical)
            {
                var randY1 = Random.Range(area1.Room.Y + 1, area1.Room.Y + area1.Room.Height - 1);  // Excluding both ends
                var (x1, y1) = (area1.Room.X + area1.Room.Width, randY1);
                var randY2 = Random.Range(area2.Room.Y + 1, area2.Room.Y + area2.Room.Height - 1);  // Excluding both ends
                var (x2, y2) = (area2.Room.X-1, randY2);
                Assert.IsTrue(x1 < x2, $"x1: {x1}, x2: {x2}");
                while (x1 < divideCoord)
                {
                    path.Points.Add((x1, y1));
                    x1++;
                }
                while (x2 > divideCoord)
                {
                    path.Points.Add((x2, y2));
                    x2--;
                }
                var minY = Mathf.Min(y1, y2);
                var maxY = Mathf.Max(y1, y2);
                while (minY <= maxY)
                {
                    path.Points.Add((divideCoord, minY));
                    minY++;
                }
            }
            else
            {
                var randX1 = Random.Range(area1.Room.X + 1, area1.Room.X + area1.Room.Width - 1);  // Excluding both ends
                var (x1, y1) = (randX1, area1.Room.Y + area1.Room.Height);
                var randX2 = Random.Range(area2.Room.X + 1, area2.Room.X + area2.Room.Width - 1);  // Excluding both ends
                var (x2, y2) = (randX2, area2.Room.Y-1);
                Assert.IsTrue(y1 < y2, $"y1: {y1}, y2: {y2}");
                while (y1 < divideCoord)
                {
                    path.Points.Add((x1, y1));
                    y1++;
                }
                while (y2 > divideCoord)
                {
                    path.Points.Add((x2, y2));
                    y2--;
                }
                var minX = Mathf.Min(x1, x2);
                var maxX = Mathf.Max(x1, x2);
                while (minX <= maxX)
                {
                    path.Points.Add((minX, divideCoord));
                    minX++;
                }
            }
            Debug.Log($"CreatePath: isDivideByVertical: {isDivideByVertical}, divideCoord: {divideCoord}");
            Debug.Log($"area1: X: {area1.X}, Y: {area1.Y}, Width: {area1.Width}, Height: {area1.Height} -> area2: X: {area2.X}, Y: {area2.Y}, Width: {area2.Width}, Height: {area2.Height}");
            Debug.Log($"paths: {string.Join(',', path.Points.Select(p => $"({p.x},{p.y})"))}");
            return path;
        }
        
        bool CanDivideArea(Area area)
        {
            if(area.Width < MinAreaSize && area.Height < MinAreaSize) return false;
            if (area.Width <= MinAreaSize * 2 && area.Height <= MinAreaSize * 2) return false;
            return true;
        }

        Area AddRoom(Area area)
        {
            var room = GenerateRandomRoom(area);
            return area with { Room = room };
        }
        Room GenerateRandomRoom(Area area)
        {
            // [pre-conditions]
            Assert.IsTrue(area.Width >= MinRoomSize + MinRoomMargin * 2);
            Assert.IsTrue(area.Height >= MinRoomSize + MinRoomMargin * 2);
            
            var roomX = Random.Range(area.X + MinRoomMargin, area.X + area.Width - (MinRoomSize + MinRoomMargin));
            var roomY = Random.Range(area.Y + MinRoomMargin, area.Y + area.Height - (MinRoomSize + MinRoomMargin));
            var roomWidth = Random.Range(MinRoomSize, Mathf.Min(MaxRoomSize, area.Width - ((roomX - area.X) + MinRoomMargin)));
            var roomHeight = Random.Range(MinRoomSize, Mathf.Min(MaxRoomSize, area.Height - ((roomY - area.Y) + MinRoomMargin)));
            
            // [post-conditions]
            Assert.IsTrue(roomWidth >= MinRoomSize);
            Assert.IsTrue(roomHeight >= MinRoomSize);
            Assert.IsTrue(roomX + roomWidth <= area.X + area.Width - MinRoomMargin);
            Assert.IsTrue(roomY + roomHeight <= area.Y + area.Height - MinRoomMargin);
            Assert.IsTrue(roomX <= area.X + area.Width - MinRoomSize - MinRoomMargin);
            Assert.IsTrue(roomY <= area.Y + area.Height - MinRoomSize - MinRoomMargin);
            return new Room(roomX, roomY, roomWidth, roomHeight);
        }

    }
}