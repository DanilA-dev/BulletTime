using System.Collections.Generic;
using Systems.Intefaces;
using Entity_Data;
using Factories;
using Model;
using Types;
using UnityEngine;
using Zenject;

namespace Behaviour
{
    public class EnemyTurretGetService : MonoBehaviour, IEnemyTurretGetService
    {
        [SerializeField] private EnemyTurretSpawnSettings[] _enemyTurretsSpawn;
        
        private EnemyEntityFactory _enemyFactory;
        private List<EnemyTurretEntity> _createdEnemyTurrets;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _enemyFactory = new EnemyEntityFactory(diContainer);
            _createdEnemyTurrets = new List<EnemyTurretEntity>();
        }
   
        public EnemyTurretEntity[] CreateEnemies()
        {

            foreach (var enemyTurretSpawn in _enemyTurretsSpawn)
            {
               var enemy = Create(enemyTurretSpawn.Data, enemyTurretSpawn.SpawnPoint.position);
               _createdEnemyTurrets.Add(enemy);
            }

            return _createdEnemyTurrets.ToArray();
        }

        public void Reset()
        {
            if(_createdEnemyTurrets.Count <= 0)
                return;
            
            _createdEnemyTurrets.ForEach(e => e.ResetEnemy());
        }

        private EnemyTurretEntity Create(EnemyTurretEntityData data, Vector3 pos)
        {
            var enemy = new EnemyTurret(data.RotateSpeed, data.MaxHealth, data.DetectRadius);
            var entity =  _enemyFactory.Create(data.Prefab, pos, Quaternion.identity);
            entity.Init(enemy);
            return entity;
        }

        public void OnDrawGizmos()
        {
            if(_enemyTurretsSpawn == null || _enemyTurretsSpawn.Length <= 0)
                return;

            foreach (var enemyTurretSpawn in _enemyTurretsSpawn)
            {
                if(enemyTurretSpawn.SpawnPoint == null)
                    continue;
                
                Gizmos.color = enemyTurretSpawn.GizmosColor;
                Gizmos.DrawSphere(enemyTurretSpawn.SpawnPoint.position, 0.5f);
            }
        }
    }

    [System.Serializable]
    public class EnemyTurretSpawnSettings
    {
        [field: SerializeField] public EnemyTurretEntityData Data { get; private set; }
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
        
        [field: SerializeField] public Color GizmosColor { get; private set; }
        
    }
}