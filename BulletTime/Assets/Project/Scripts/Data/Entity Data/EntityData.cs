using UnityEngine;

namespace Entity_Data
{
    public abstract class EntityData<T> : ScriptableObject where T : Entity
    {
        [field: SerializeField] public T Prefab { get; private set; }
    }
}