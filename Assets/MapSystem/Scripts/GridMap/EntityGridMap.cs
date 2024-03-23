#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonCrawler.MapSystem.Interfaces;
using UnityEngine;



namespace DungeonCrawler.MapSystem.Scripts
{
    public class EntityGridMap : IGridCoordinate, IGridMap
    {
        public int Width  => _coordinate.Width;
        public int Height => _coordinate.Height;
        public int Length => _coordinate.Length; 
        public int ToSubscript(int x, int y) => _coordinate.ToSubscript(x, y);
        public Vector2Int ToVector(int subscript) => _coordinate.ToVector(subscript);
        
        public bool IsInDataArea(int x, int y) => _coordinate.IsInDataArea(x, y);
        public bool IsInDataOrEdgeArea(int x, int y) => _coordinate.IsInDataOrEdgeArea(x, y);

        
        readonly List<IEntity>[] _entityMaps;
        readonly IGridCoordinate _coordinate;
        
        public EntityGridMap(IGridCoordinate coordinate)
        {
            _coordinate = coordinate;
            _entityMaps = new List<IEntity>[Length];
            for (int i = 0; i < Length; i++)
            {
                _entityMaps[i] = new List<IEntity>();
            }
        }
        
        public bool IsInDataArea(Vector2Int vector)
        {
            return _coordinate.IsInDataArea(vector);
        }

        public EntityGridMap ClearMap()
        {
            for (int i = 0; i < Length; i++)
            {
                _entityMaps[i] = new List<IEntity>();
            }
            // _blockPresenterは初期化していなことに注意
            return this;
        }
        
        public void FillAll(IEntity entity)
        {
            for (int i = 0; i < Length; i++)
            {
                _entityMaps[i].Add(entity);
            }
        }

        public void DebugPrint()
        {
            StringBuilder sb = new StringBuilder();
            int maxStringLength = 0;

            // Calculate maxStringLength
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var entities = GetSingleTypeList<IEntity>(new Vector2Int(x, y));
                    if (entities.Any())
                    {
                        int length = entities[0].ToString().Length;
                        if (length > maxStringLength)
                            maxStringLength = length;
                    }
                    else
                    {
                        if (4 > maxStringLength)  // "null" の文字列長
                            maxStringLength = 4;
                    }
                }
            }

            // Output string with fixed width
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var entities = GetSingleTypeList<IEntity>(new Vector2Int(x, y));
                    if (entities.Any())
                    {
                        sb.Append(entities[0].ToString().PadRight(maxStringLength));
                    }
                    else
                    {
                        sb.Append("null".PadRight(maxStringLength));
                    }
                }
                sb.Append("\n");
            }
            Debug.Log(sb.ToString());
        }

        //Getter
        public T? GetSingleEntity<T>(Vector2Int vector) where T : IEntity
        {
            int x = vector.x;
            int y = vector.y;

            return GetSingleEntity<T>(x, y);
        }
        public T? GetSingleEntity<T>(int x, int y) where T : IEntity
        {
            if (_coordinate.IsOutOfDataArea(x, y)) {return default(T);}

            return GetSingleEntity<T>(ToSubscript(x, y));
        }
        
        public T? GetSingleEntity<T>(int index) where T : IEntity
        {
            if (index < 0 || index > Length)
            {
                Debug.LogError("領域外の値を習得しようとしました");
                return default(T);
            }
            
            var entity = _entityMaps[index].OfType<T>().FirstOrDefault();

            if (entity == null)
            {
                // Debug.Log($"_entityMaps[{index}].OfType<TEntity>().FirstOrDefault()がnullです");
                return default(T);
            }
            else
            {
                //探しているEntityTypeの先頭のものを返す（存在しない場合はdefault）
                return entity;
            }
        }
        public List<T> GetSingleTypeList<T>(Vector2Int vector) where T : IEntity
        {
            var x = vector.x;
            var y = vector.y;

            return GetSingleTypeList<T>(ToSubscript(x, y));
        }
        public List<T> GetSingleTypeList<T>(int x, int y) where T : IEntity
        {
            if (_coordinate.IsOutOfDataArea(x, y)) return new List<T>();

            return GetSingleTypeList<T>(ToSubscript(x, y));
        }

        public List<T> GetSingleTypeList<T>(int index)  // IDisposableを受け取る場合があるので、制約は設けない。　たぶんよくない設計
        {
            
            if (index < 0 || index > Length)
            {
                Debug.LogError("領域外の値を習得しようとしました");
                return new List<T>(); // 空のリストを返す
            }

            var filteredEntities = _entityMaps[index].OfType<T>().ToList();
    
            if (!filteredEntities.Any())
            {
                //Debug.Log($"_entityMaps[{index}]の{typeof(EntityType)}のCountが0です");
                return  new List<T>(); // 空のリストを返す
            }

            return filteredEntities;
        }


        public IEnumerable<IEntity> GetAllTypeList(Vector2Int vector)
        {
            int x, y;
            x = vector.x;
            y = vector.y;

            if (_coordinate.IsOutOfDataArea(x, y)) return  new List<IEntity>();
            
            return GetSingleTypeList<IEntity>(ToSubscript(x, y));

        }
        
        public IEnumerable<IEntity> GetAllTypeList(int  index)
        {
            if (index < 0 || index > Length)
            {
                Debug.LogError("領域外の値を習得しようとしました");
                return new List<IEntity>(); // 空のリストを返す
            }

            return _entityMaps[index];
        }
        
        // AddEntity
        public void AddEntity(Vector2Int vector, IEntity entity)
        {
            var x = vector.x;
            var y = vector.y;

            AddEntity(x,y, entity);
        }
        public void AddEntity(int x, int y, IEntity entity)
        {

            if (_coordinate.IsOutOfDataArea(x, y))
            {
                Debug.LogWarning($"IsOutOfDataRange({x},{y}) is true");
                return;
            }

            AddEntity(ToSubscript(x, y), entity);
        }

        public void AddEntity(int index, IEntity entity)
        {
            if (index < 0 || index > Length)
            {
                Debug.LogError("You are trying to get a value out of range.");
                return;
            }

            var entities = _entityMaps[index];

            if (entities.Any())
            {
                Debug.LogWarning($"[Warning] {typeof(IEntity)} is already in. Current Count: {entities.Count}");
            }

            // domain
            _entityMaps[index].Add(entity);
            
        }

        // RemoveEntity
        public void RemoveEntity<T>(int x, int y, T entity) where T : IEntity
        {
            if (_coordinate. IsOutOfDataArea(x, y))
            {
                Debug.LogWarning($"IsOutOfDataRange({x},{y}) is true");
                return;
            }

            var index = ToSubscript(x, y);
            
            // domain
            _entityMaps[index].Remove(entity);

        }

        public void RemoveEntity<T>(Vector2Int vector, T entity) where T : IEntity
        {
            RemoveEntity(vector.x, vector.y, entity);
        }
        


    }

}