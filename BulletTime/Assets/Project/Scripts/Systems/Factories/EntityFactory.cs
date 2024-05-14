using UnityEngine;

namespace Factories
{
    public abstract class EntityFactory<T> where T : Entity
    {
        public virtual T Create(T prefab, Vector3 pos, Quaternion rot)
        {
            return GameObject.Instantiate(prefab, pos, rot);
        }
    }
}