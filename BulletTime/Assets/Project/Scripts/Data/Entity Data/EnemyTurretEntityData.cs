using Types;
using UnityEngine;

namespace Entity_Data
{
    [CreateAssetMenu(menuName = "Data/Entity Data/Enemy Data")]
    public class EnemyTurretEntityData : EntityData<EnemyTurretEntity>
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; private set; }
        [field: SerializeField] public float DetectRadius { get; private set; }


    }
}