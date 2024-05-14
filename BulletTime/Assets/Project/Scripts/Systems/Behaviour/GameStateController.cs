using System;
using Systems.Intefaces;
using UI;
using UniRx;
using UnityEngine;

public enum GameStateType
{
    BootStrap = 0,
    TitleScreen = 1,
    RestartLevel = 2,
    Core = 3,
    LoseGame = 4,
    WinGame = 5,
    Pause = 6
}

public class GameStateController : IGameStateController
{
    public ReactiveProperty<GameStateType> CurrentState { get; private set; }
    public ReactiveProperty<SceneType> CurrentScene { get; private set; }
    public MenuType CurrentMenu { get; private set; }

    
    public GameStateController()
    {
        CurrentState = new ReactiveProperty<GameStateType> {Value = GameStateType.BootStrap};
        CurrentScene = new ReactiveProperty<SceneType>() {Value = SceneType.None};

        CurrentState.Subscribe(_ => Debug.Log($"Current game state {_}"));
    }

    public void ChangeState(GameStateType newState)
    {
        if(CurrentState.Value != newState)
            CurrentState.Value = newState;
    }
  
    public void ChangeScene(SceneType newScene)
    {
        if (CurrentScene.Value != newScene)
            CurrentScene.Value = newScene;
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}

