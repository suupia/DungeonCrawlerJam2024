#nullable enable

using UnityEngine;

namespace DungeonCrawler
{
    public class HangerMeter
    {
        public int MaxValue { get; set; }
        int _value = 0;
        public int Value
        {
            get { return _value;}
            set { _value = Mathf.Min(MaxValue + 1, value); }
        }

        public HangerMeter(int maxValue)
        {
            MaxValue = maxValue;
            Value = MaxValue;
        }

        public void ResetHangerMeter()
        {
            Value = MaxValue;
        }
    }
}
