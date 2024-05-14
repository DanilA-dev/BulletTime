using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Behaviour
{
    public class HitParticleHandler : MonoBehaviour
    {
        [SerializeField] private HitParticleInvoker[] _invokers;
        [Space]
        [SerializeField] private ParticleSystem _hitParticlePrefab;
        [SerializeField] private int _capacity;
        [SerializeField] private int _maxParticles;

        private const float RELEASE_TIME = 3;

        private ParticleSystem _createdParticle;
        private ObjectPool<ParticleSystem> _particlePool;
        private WaitForSeconds _yieldSeconds;

        private void Awake()
        {
            CreatePool();
            SubToInvokers();
        }

        private void OnDestroy()
        {
            foreach (var invoker in _invokers)
                invoker.OnHit -= CreateParticleAt;
        }

        private void SubToInvokers()
        {
            foreach (var invoker in _invokers)
                invoker.OnHit += CreateParticleAt;
        }

        private void CreatePool()
        {
            _particlePool = new ObjectPool<ParticleSystem>(CreateParticle,
                p => p.gameObject.SetActive(true),
                p => p.gameObject.SetActive(false),
                p => Destroy(p.gameObject),
                false,
                _capacity,
                _maxParticles);
        }

        private void CreateParticleAt(Vector3 pos)
        {
            _createdParticle = _particlePool.Get();
            _createdParticle.transform.position = pos;
            _createdParticle.transform.eulerAngles = pos;

            StartCoroutine(AutoRelease());
        }

        private IEnumerator AutoRelease()
        {
            yield return _yieldSeconds ??= new WaitForSeconds(RELEASE_TIME);
            _particlePool.Release(_createdParticle);
        }
        
        private ParticleSystem CreateParticle()
        {
            return Instantiate(_hitParticlePrefab);
        }
    }
}