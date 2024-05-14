using CustomFSM.State;
using Types;

namespace EnemyTurretFSM
{
    public class BaseEnemyTurretState : IState
    {
        protected EnemyTurretEntity _enemyTurret;

        public BaseEnemyTurretState(EnemyTurretEntity enemyTurret)
        {
            _enemyTurret = enemyTurret;
        }
        
        public virtual void OnEnter() {}

        public virtual void OnUpdate() {}

        public virtual void OnFixedUpdate() {}

        public virtual void OnExit() {}
    }
}