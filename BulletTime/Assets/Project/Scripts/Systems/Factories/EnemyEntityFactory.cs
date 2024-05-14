using Types;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EnemyEntityFactory : EntityFactory<EnemyTurretEntity>
    {
        private DiContainer _diContainer;
        
        public EnemyEntityFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public override EnemyTurretEntity Create(EnemyTurretEntity prefab, Vector3 pos, Quaternion rot)
        {
            var entity = _diContainer.InstantiatePrefab(prefab).GetComponent<EnemyTurretEntity>();
            entity.transform.position = pos;
            return entity;
        }
    }
}