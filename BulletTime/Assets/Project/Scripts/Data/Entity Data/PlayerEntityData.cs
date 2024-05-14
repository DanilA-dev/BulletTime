using Types;
using UnityEngine;

namespace Entity_Data
{
    [CreateAssetMenu(menuName = "Data/Entity Data/Player Data")]
    public class PlayerEntityData : EntityData<PlayerEntity>
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float DefaultJumpForce { get; private set; }
    }
}