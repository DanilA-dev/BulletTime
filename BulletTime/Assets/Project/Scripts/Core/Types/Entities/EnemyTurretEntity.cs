using System;
using Core.Behaviour;
using CustomFSM.Preicate;
using CustomFSM.StateMachine;
using EnemyTurretFSM;
using Model;
using UnityEngine;

namespace Types
{
    public class EnemyTurretEntity : Entity
    {
        [SerializeField] private WeaponHandler _weaponHandler;

        private EnemyTurretIdleState _idleState;
        private EnemyTurretShootState _shootState;
        private StateMachine _stateMachine;

        private TargetDetector _targetDetector;
        private EnemyTurret _enemyTurret;
        public EnemyTurret EnemyTurret => _enemyTurret;

        public void Init(EnemyTurret enemyTurret)
        {
            _enemyTurret = enemyTurret;
            _targetDetector = new TargetDetector(this.transform, enemyTurret.DetectRadius);
            InitFSM();
            InitTransitions();
        }

        private void InitFSM()
        {
            _stateMachine = new StateMachine();
            _idleState = new EnemyTurretIdleState(this);
            _shootState = new EnemyTurretShootState(this, _weaponHandler);
        }

        private void InitTransitions()
        {
            _stateMachine.AddTransition(_idleState, _shootState, new FuncPredicate(IsTargetNear));
            _stateMachine.AddTransition(_shootState, _idleState, new FuncPredicate((() => !IsTargetNear())));
            
            _stateMachine.SetState(_idleState);
        }

        private void Update()
        {
            _stateMachine.OnUpdate();
        }
        
        private void FixedUpdate()
        {
            _stateMachine.OnFixedUpdate();
            _targetDetector.TryFindTarget();
            _shootState.UpdateTarget(_targetDetector.Target);
        }


        private bool IsTargetNear() => _targetDetector.Target != null 
                                       && _targetDetector.Target.Damagable.CanBeDamaged;

        public void ResetEnemy() => _enemyTurret.Reset();

        private void OnDrawGizmos()
        {
            _targetDetector.OnDrawGizmos();
        }
        
    }
}