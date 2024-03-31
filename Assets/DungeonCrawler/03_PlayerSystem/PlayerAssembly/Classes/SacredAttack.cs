using DungeonCrawler.PlayerAssembly.Interfaces;

namespace DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes
{
    public class SacredAttack : IPlayerAttack
    {
        int _atk;

        public SacredAttack(int atk)
        {
            _atk = atk;
        }
        public void Attack(EnemyDomain enemy)
        {
            enemy.OnAttacked(_atk*5);
        }
    }
}