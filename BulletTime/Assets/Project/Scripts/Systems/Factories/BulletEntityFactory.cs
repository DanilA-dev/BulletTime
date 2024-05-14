using Types;
using UnityEngine;

namespace Factories
{
    public class BulletEntityFactory : EntityFactory<BulletEntity>
    {
        public override BulletEntity Create(BulletEntity prefab, Vector3 pos, Quaternion rot)
        {
            return GameObject.Instantiate(prefab, pos, rot);
        }
    }
}