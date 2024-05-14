using System;
using Systems.Intefaces;
using Core.Signals;
using UniRx;
using Zenject;

namespace Behaviour
{
    public class GameOverHandler : IDisposable
    {
        private IGameStateController _gameStateController;
        private IDisposable _disposable;
        
        [Inject]
        private void Construct(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            
            _disposable = MessageBroker.Default.Receive<PlayerSignals.Die>()
                .Subscribe(_ => OnPlayerDie());
        }

        private void OnPlayerDie()
        {
            _gameStateController.ChangeState(GameStateType.LoseGame);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}