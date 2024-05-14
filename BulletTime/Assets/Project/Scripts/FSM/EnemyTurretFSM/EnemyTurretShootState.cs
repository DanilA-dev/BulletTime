using Extensions;
using Types;
using UnityEngine;

namespace EnemyTurretFSM
{
    public class EnemyTurretShootState : BaseEnemyTurretState
    {

        private Transform _transform;
        private WeaponHandler _weaponHandler;
        private IEnemyTarget _target;
        private LayerMask _obstacleLayer;
        
        public EnemyTurretShootState(EnemyTurretEntity enemyTurret, WeaponHandler weaponHandler)
            : base(enemyTurret)
        {
            _transform = enemyTurret.transform;
            _weaponHandler = weaponHandler;
            _obstacleLayer = LayerMask.GetMask("Default");
        }
        public override void OnUpdate()
        {
            RotateTowardsTarget();
        }

        public override void OnFixedUpdate()
        {
            TryShootTarget();
        }

        private void TryShootTarget()
        {
            var gunPoint = _weaponHandler.FirePoint.position;
            var targetPoint = _target.TargetTransform.position;

            var hasCollision = Physics.Linecast(gunPoint, targetPoint, out var hitinfo, _obstacleLayer);
            if (hasCollision)
                return;
           
            var dir = (_target.TargetTransform.position - _transform.position).normalized;
           _weaponHandler.Fire(dir);
        }

        private void RotateTowardsTarget()
        {
            if(_target == null)
                return;
            
            _transform.RotateTowardsDirection(_target.TargetTransform.position, Vector3.right, _enemyTurret.EnemyTurret.RotateSpeed);
        }

        public void UpdateTarget(IEnemyTarget target) => _target = target;
        
    }
}