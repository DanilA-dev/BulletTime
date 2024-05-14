using System;
using Systems.Intefaces;
using UniRx;

namespace Behaviour
{
    public class GamePauseHandler : IDisposable
    {
        private IGameStateController _gameStateController;
        private IInputService _inputService;
        private ITimeController _timeController;
        private IDisposable _disposable;

        public GamePauseHandler(IInputService inputService, ITimeController timeController,IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            _inputService = inputService;
            _timeController = timeController;

            _disposable = _gameStateController.CurrentState.Subscribe(OnResume);
            _inputService.PauseButton += OnPauseButtonButton;
        }

        private void OnResume(GameStateType gameStateType)
        {
            if(gameStateType != GameStateType.Pause)
                _timeController.SetDefaultTime();
        }

        private void OnPauseButtonButton()
        {
            _gameStateController.ChangeState(GameStateType.Pause);
            _timeController.FreezeTime();
        }

        public void Dispose()
        {
            _inputService.PauseButton -= OnPauseButtonButton;
            _disposable?.Dispose();
        }
    }
}