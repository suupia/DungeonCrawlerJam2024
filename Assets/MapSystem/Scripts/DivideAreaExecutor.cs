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
        const int MaxRoomSize = 20;
        const int MinAreaSize = MinRoomSize + MinRoomMargin * 2;
        public List<Area> DivideAreaOnce(List<Area> result)
        {
            var frontArea = result.First();
            var (area1, area2, isDivided) = DivideArea(frontArea);
            if (isDivided)
            {
                result.RemoveAt(0);
                result.Add(area1);
                result.Add(area2);
            }
            else
            {
                // what should I do?
            }
            
            return result;
        }
        
        (Area area1, Area area2, bool idDivided) DivideArea(Area area)
        {
            if(!CanDivideArea(area))
            {
                return (area, area, false);
            }
            
            // Preconditions
            Assert.IsTrue(area.Width > MinAreaSize*2 || area.Height > MinAreaSize*2);


            bool isDivideByVertical = Random.Range(0, 2) == 0;
            if(area.Width > MinAreaSize*2)
            {
                isDivideByVertical = true;
            }else if (area.Height > MinAreaSize*2)
            {
                isDivideByVertical = false;
            }
            int areaSize = isDivideByVertical ? area.Width : area.Height;
            var minX = MinAreaSize; // Ensure that the room can be placed in the left area
            var maxX = areaSize - MinAreaSize; // Ensure that the room can be placed in the right area
            Assert.IsTrue(minX < maxX, $"minX: {minX}, maxX: {maxX}, areaSize: {areaSize}");
            var divideX = Random.Range(minX, maxX);
            var (dividedArea1, dividedArea2) = isDivideByVertical
                ? (
                    new Area(area.X, area.Y, divideX, area.Height, null, new List<(Area area, Path path)>()),
                    new Area(area.X + divideX, area.Y, area.Width - divideX, area.Height, null,new List<(Area area, Path path)>())
                )
                : (
                    new Area(area.X, area.Y, area.Width, divideX, null,new List<(Area area, Path path)>()),
                    new Area(area.X, area.Y + divideX, area.Width, area.Height - divideX, null,new List<(Area area, Path path)>())
                );

            

            Debug.Log($"dividedArea1: X: {dividedArea1.X}, Y: {dividedArea1.Y}, Width: {dividedArea1.Width}, Height: {dividedArea1.Height}");
            Debug.Log($"dividedArea2: X: {dividedArea2.X}, Y: {dividedArea2.Y}, Width: {dividedArea2.Width}, Height: {dividedArea2.Height}");
            var (area1, area2) = AddRoomEach(dividedArea1, dividedArea2);

            var path = new Path(new List<(int x, int y)>(),divideX);
            var connectPath = CreatePath(area1, area2, divideX, isDivideByVertical);
            path.Points.AddRange(connectPath.Points);
            area1.AdjacentAreas.Add((area2, connectPath));  // This process must be done after AddRoomEach
            area2.AdjacentAreas.Add((area1, connectPath));
            
            // Add area's AdjacentAreas to area1
            foreach (var (adjacentArea, adjacentPath) in area.AdjacentAreas)
            {
                area1.AdjacentAreas.Add((adjacentArea, CreatePath(area1,adjacentArea, adjacentPath.DivideX,isDivideByVertical)));
                
            }
            
            foreach (var (adjacentArea, adjacentPath) in area.AdjacentAreas)
            {
                // adjacentArea から areaへのパスを削除して、
                // adjacentArea から area1へ貼りなおす必要がある
                adjacentArea.AdjacentAreas.Remove((area, adjacentPath));
                adjacentArea.AdjacentAreas.Add((area1, CreatePath(adjacentArea, area1, adjacentPath.DivideX, isDivideByVertical)));


            }

            Assert.IsTrue(dividedArea1.Width >= MinAreaSize);
            Assert.IsTrue(dividedArea2.Width >= MinAreaSize);
            Assert.IsTrue(dividedArea1.Height >= MinAreaSize);
            Assert.IsTrue(dividedArea2.Height >= MinAreaSize);
            
            // Debug
            Debug.Log($"DivideArea: X: {area.X}, Y: {area.Y}, Width: {area.Width}, Height: {area.Height}");
            Debug.Log($"dividedArea1: X: {dividedArea1.X}, Y: {dividedArea1.Y}, Width: {dividedArea1.Width}, Height: {dividedArea1.Height}");
            Debug.Log($"dividedArea2: X: {dividedArea2.X}, Y: {dividedArea2.Y}, Width: {dividedArea2.Width}, Height: {dividedArea2.Height}");
            
            return (area1, area2, true);

        }
        
        

        
        // todo : public for the test
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
        
        (Area area1, Area area2) AddRoomEach(Area area1, Area area2)
        {
            return (AddRoom(area1), AddRoom(area2));
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