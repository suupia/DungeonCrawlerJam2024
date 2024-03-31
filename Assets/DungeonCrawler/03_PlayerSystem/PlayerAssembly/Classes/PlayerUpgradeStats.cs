#nullable enable
using DungeonCrawler._10_UpgradeSystem.UpgradeAssembly;

namespace DungeonCrawler
{
    public class PlayerUpgradeStats
    {
        public int Atk => CalcAtk();
        int _atkUpgradeCount;

        public void OnCompleteUpgrade(UpgradeKind kind)
        {
            switch (kind)
            {
                case UpgradeKind.Test1:
                    _atkUpgradeCount++;
                    break;
                default:
                    break;
            }
        }

        public int UpgradeCost(UpgradeKind kind)
        {
            return 100;
        }
        

        int CalcAtk()
        {
            // UpgradeCount -> Upgrade Amount
            return  _atkUpgradeCount;
        }
    }
}