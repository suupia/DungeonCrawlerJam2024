#nullable enable

namespace DungeonCrawler
{
    public class HangerMeter
    {
        int _maxValue;
        public int Value { get; set; }

        public HangerMeter(int maxValue)
        {
            _maxValue = maxValue;
            Value = _maxValue;
        }
    }
}
