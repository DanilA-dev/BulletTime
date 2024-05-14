using Behaviour;
using Zenject;

namespace Installers
{
    public class BootStrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindGameStateController();
            BindSceneHandler();
        }

        private void BindGameStateController() => Container.BindInterfacesAndSelfTo<GameStateController>().FromNew().AsSingle().NonLazy();
        private void BindSceneHandler() => Container.BindInterfacesAndSelfTo<SceneHandler>().FromNew().AsSingle().NonLazy();

        private void BindInput() => Container.BindInterfacesAndSelfTo<InputService>().FromNew().AsSingle().NonLazy();

    }
}