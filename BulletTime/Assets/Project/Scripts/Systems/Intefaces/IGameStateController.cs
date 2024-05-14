using System;
using UI;
using UniRx;

namespace Systems.Intefaces
{
    public interface IGameStateController
    {
        public ReactiveProperty<GameStateType> CurrentState { get; }
        public ReactiveProperty<SceneType> CurrentScene { get; }
        public MenuType CurrentMenu { get;}

        public void ChangeState(GameStateType newState);
        public void ChangeScene(SceneType newScene);
        public void ExitApp();
    }
}