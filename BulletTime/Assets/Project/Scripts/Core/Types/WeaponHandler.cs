using System;
using Entity_Data;
using Factories;
using Model;
using UnityEngine;
using UnityEngine.Pool;

namespace Types
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private Transform _firePoint;

        private const int CAPACITY = 30;
        private const int MAX_SIZE = 100;

        private Weapon _weapon;
        private ObjectPool<BulletEntity> _pool;
        private BulletEntityFactory _bulletEntityFactory;

        private Vector3 _fireDirection;
        
        public Transform FirePoint => _firePoint;


        private void Awake()
        {
            _bulletEntityFactory = new BulletEntityFactory();
            _pool = new ObjectPool<BulletEntity>(CreateBullet,
                b => b.gameObject.SetActive(true),
                b => b.gameObject.SetActive(false),
                b => Destroy(b.gameObject),
                false,
                CAPACITY,
                MAX_SIZE);
            
            InitWeapon();
        }


        private void InitWeapon()
        {
            _weapon = new Weapon(_weaponData.MaxAmmo, _weaponData.Damage,_weaponData.ReloadTime,
                _weaponData.RateOfFire, _weaponData.Spread, FireBullet);
        }

        private void Update() => _weapon.UpdateTimers();
        public void Fire(Vector3 fireDir)
        {
            _fireDirection = fireDir;
            _weapon.StartFire();
        }

        private void FireBullet()
        {
            var bullet = _pool.Get();
            bullet.transform.position = _firePoint.position;
            bullet.Direction = _fireDirection;
        }
        
        private BulletEntity CreateBullet()
        {
            var bullet = _bulletEntityFactory.Create(_weaponData.BulletData.Prefab, _firePoint.position, Quaternion.identity);
            bullet.Init(_weaponData.BulletData.Speed, _weapon.Damage, _pool, _weapon.Spread);
            return bullet;
        }
    }
}