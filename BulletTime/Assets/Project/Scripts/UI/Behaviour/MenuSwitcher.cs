using System;
using Systems.Intefaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MenuSwitcher : MonoBehaviour
    {
        [SerializeField] private BaseMenu[] _menus;

        private IGameStateController _gameStateController;
        
        [Inject]
        private void Construct(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            _gameStateController.CurrentState.Subscribe(ChangeTabByState).AddTo(gameObject);
        }

        private void ChangeTabByState(GameStateType state)
        {
            switch (state)
            {
                case GameStateType.BootStrap:
                    break;
                case GameStateType.TitleScreen:
                    ToggleMenu(MenuType.Menu);
                    break;
                case GameStateType.RestartLevel:
                case GameStateType.Core:
                    ToggleMenu(MenuType.Core);
                    break;
                case GameStateType.LoseGame:
                    ToggleMenu(MenuType.Lose);
                    break;
                case GameStateType.WinGame:
                    ToggleMenu(MenuType.Win);
                    break;
                case GameStateType.Pause:
                    OpenMenuOnTop(MenuType.Pause);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        

        public void ToggleMenu(MenuType type)
        {
            foreach (var menu in _menus)
                menu.gameObject.SetActive(menu.Type == type);
        }

        public void OpenMenuOnTop(MenuType type)
        {
            foreach (var menu in _menus)
            {
                if(menu.Type == type)
                    menu.gameObject.SetActive(true);
            }
        }
    }
}