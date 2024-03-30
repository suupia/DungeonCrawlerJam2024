#nullable enable

namespace DungeonCrawler
{
    public class HangerMeter
    {
        public int MaxValue { get; set; }
        public int Value { get; set; }

        public HangerMeter(int maxValue)
        {
            MaxValue = maxValue;
            Value = MaxValue;
        }
    }
}
