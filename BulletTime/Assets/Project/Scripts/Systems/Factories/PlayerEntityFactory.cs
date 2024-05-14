using Types;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class PlayerEntityFactory : EntityFactory<PlayerEntity>
    {
        private DiContainer _diContainer;
        
        public PlayerEntityFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public override PlayerEntity Create(PlayerEntity prefab, Vector3 pos, Quaternion rot)
        {
            var entity = _diContainer.InstantiatePrefab(prefab).GetComponent<PlayerEntity>();
            entity.transform.position = pos;
            return entity;
        }
    }
}