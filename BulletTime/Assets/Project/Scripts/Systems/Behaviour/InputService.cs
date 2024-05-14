using System;
using Systems.Intefaces;
using UnityEngine;
using Zenject;
using UniRx;

namespace Behaviour
{
    public class InputService : IInputService, ITickable, IDisposable
    {
        public event Action<Vector2> PointerPosition;
        public event Action<Vector2> PointerDelta;
        public event Action PointerPerformed;
        public event Action PointerHold;
        public event Action PointerCanceled;
        public event Action PauseButton;

        private IDisposable _disposable;
        private Input _input;
    
        public InputService(IGameStateController gameStateController)
        {
            _input = new Input();
            _input.Gameplay.Enable();
            InitActions();

            _disposable = gameStateController.CurrentState.Subscribe(OnStateChanged);
        }

        private void OnStateChanged(GameStateType state)
        {
            switch (state)
            {
                case GameStateType.BootStrap:
                case GameStateType.TitleScreen:
                case GameStateType.RestartLevel:
                case GameStateType.LoseGame:
                case GameStateType.WinGame:
                case GameStateType.Pause:
                    DisableGameplayInput();
                    break;
                case GameStateType.Core:
                    EnableGameplayInput();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void InitActions()
        {
            _input.Gameplay.PointerUse.performed += (_) => PointerPerformed?.Invoke();
            _input.Gameplay.PointerUse.canceled += (_) => PointerCanceled?.Invoke();
            _input.Gameplay.Pause.performed += (_) => PauseButton?.Invoke();
        }

        public void Tick()
        {
            var position = _input.Gameplay.PointerPosition.ReadValue<Vector2>();
            PointerPosition?.Invoke(position);

            var delta = _input.Gameplay.PointerDelta.ReadValue<Vector2>();
            PointerDelta?.Invoke(delta);
            
            if(_input.Gameplay.PointerUse.IsPressed())
                PointerHold?.Invoke();
                
        }

        private void EnableGameplayInput()
        {
            _input.Gameplay.Enable();
            Debug.Log($"<color=pink> GameplayInput enabled </color>");
        }

        private void DisableGameplayInput()
        {
            _input.Gameplay.Disable();
            Debug.Log($"<color=pink> GameplayInput disabled </color>");
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}