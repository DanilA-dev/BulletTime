using Model;
using UnityEngine;

namespace Entity_Data
{
    [CreateAssetMenu(menuName = "Data/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [field: SerializeField] public BulletData BulletData { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public int MaxAmmo { get; private set; }
        [field: SerializeField] public WeaponSpread Spread { get; private set; }
        [field: SerializeField] public float RateOfFire { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }
    }
}