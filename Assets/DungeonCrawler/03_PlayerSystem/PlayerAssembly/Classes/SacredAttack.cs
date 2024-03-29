using DungeonCrawler.PlayerAssembly.Interfaces;

namespace DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes
{
    public class SacredAttack : IPlayerAttack
    {
        int _amount;

        public SacredAttack(int amount)
        {
            _amount = amount;
        }
        public void Attack(EnemyDomain enemy)
        {
            enemy.Hp -= _amount;
        }
    }
}