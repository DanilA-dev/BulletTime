using Systems.Intefaces;
using Behaviour;
using Core.Behaviour;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private EnemyTurretGetService _enemyTurretSpawner;
        [SerializeField] private PlayerGetService _playerSpawner;
        
        public override void InstallBindings()
        {
            BindPlayerSpawner();
            BindEnemySpawner();
            BindGameOverHandler();
            BindBulletTimeController();
            BindGamePauseHandler();
        }

        private void BindGamePauseHandler() => Container.BindInterfacesAndSelfTo<GamePauseHandler>().FromNew().AsSingle().NonLazy();

        private void BindBulletTimeController() => Container.BindInterfacesAndSelfTo<TimeController>().FromNew().AsSingle().NonLazy();

        private void BindGameOverHandler() => Container.BindInterfacesAndSelfTo<GameOverHandler>().FromNew().AsSingle().NonLazy();

        private void BindEnemySpawner() =>
            Container.Bind<IEnemyTurretGetService>().To<EnemyTurretGetService>()
                .FromInstance(_enemyTurretSpawner).AsSingle().NonLazy();

        private void BindPlayerSpawner() =>
            Container.Bind<IPlayerGetService>().To<PlayerGetService>()
                .FromInstance(_playerSpawner).AsSingle().NonLazy();
    }
}