using Types;
using UnityEngine;

namespace Entity_Data
{
    [CreateAssetMenu(menuName = "Data/Entity Data/Bullet Data")]
    public class BulletData : EntityData<BulletEntity>
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}