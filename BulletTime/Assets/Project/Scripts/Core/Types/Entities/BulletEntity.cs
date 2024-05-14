using System.Collections;
using Core.Behaviour;
using Model;
using UnityEngine;
using UnityEngine.Pool;

namespace Types
{
    [RequireComponent(typeof(Rigidbody),
        typeof(Collider))]
    public class BulletEntity : Entity
    {
        private const float RELEASE_TIME = 4f;
        
        private Rigidbody _rigidbody;
        private float _speed;
        private float _damage;
        private ObjectPool<BulletEntity> _pool;
        private WaitForSeconds _yieldSeconds;
        private Vector3 _spread;
        private WeaponSpread _weaponSpread;

        private Collider _collider;
        
        public Vector3 Direction { get; set; }
        
        
        public void Init(float speed, float damage, 
            ObjectPool<BulletEntity> pool, WeaponSpread weaponSpread)
        {
            _speed = speed;
            _damage = damage;
            _pool = pool;
            _weaponSpread = weaponSpread;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
            StartCoroutine(AutoRelease());
        }

        private void OnEnable() => _spread = _weaponSpread.GetSpread();

        private void OnDisable() => _rigidbody.velocity = Vector3.zero;
        private void FixedUpdate() => Move();

        private void Move() => _rigidbody.velocity = (Direction + _spread) * (_speed * Time.fixedDeltaTime);

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IDamagable damagable))
                damagable.Damage(_damage);
            
            if(other.TryGetComponent(out HitParticleInvoker hitParticleHandler))
                hitParticleHandler.InvokeHitParticle(other.ClosestPoint(transform.position));
            
            _pool.Release(this);
        }
        
        private IEnumerator AutoRelease()
        {
            yield return _yieldSeconds ??= new WaitForSeconds(RELEASE_TIME);
            _pool.Release(this);
        }
    }
}