using Systems.Intefaces;
using Core.Signals;
using Entity_Data;
using Factories;
using Model;
using Types;
using UniRx;
using UnityEngine;
using Zenject;

namespace Behaviour
{
    public class PlayerGetService : MonoBehaviour, IPlayerGetService
    {
        [SerializeField] private PlayerEntityData _data;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Color _gizmosColor;
        
        private PlayerEntityFactory _playerFactory;
        private PlayerEntity _playerEntity;


        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _playerFactory = new PlayerEntityFactory(diContainer);
        }
        
        public PlayerEntity Create()
        {
            var player = new Player(_data.MaxHealth, _data.DefaultJumpForce, _spawnPoint.position);
            _playerEntity = _playerFactory.Create(_data.Prefab, _spawnPoint.position, Quaternion.identity);
            _playerEntity.Init(player);
            MessageBroker.Default.Publish(new PlayerSignals.Spawn(_playerEntity));
            return _playerEntity;
        }

        public void Reset() => _playerEntity?.ResetPlayer();

        public void OnDrawGizmos()
        {
            if(_spawnPoint == null)
                return;

            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(_spawnPoint.position, 0.5f);
        }
    }
}