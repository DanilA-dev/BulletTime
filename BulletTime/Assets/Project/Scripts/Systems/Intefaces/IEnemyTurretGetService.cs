using Types;

namespace Systems.Intefaces
{
    public interface IEnemyTurretGetService
    {
        public EnemyTurretEntity[] CreateEnemies();
        public void Reset();
    }
}