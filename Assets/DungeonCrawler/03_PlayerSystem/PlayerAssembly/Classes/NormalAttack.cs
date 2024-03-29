using DungeonCrawler.PlayerAssembly.Interfaces;

namespace DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes
{
    public class NormalAttack : IPlayerAttack
    {
        int _amount;

        public NormalAttack(int amount)
        {
            _amount = amount;
        }
        public void Attack(EnemyDomain enemy)
        {
            enemy.Hp -= _amount;
        }
    }
}