using System;
using Systems.Intefaces;
using UniRx;
using UnityEngine.SceneManagement;


public enum SceneType
{
    None = 0,
    Loader = 1,
    Main = 2,
}
    
public class SceneHandler : IDisposable, ISceneHandler
{
    private IDisposable _disposable;
    private IGameStateController _gameStateController;
        
    public SceneHandler(IGameStateController gameStateController)
    {
        _gameStateController = gameStateController;
        _disposable = gameStateController.CurrentScene.Subscribe(ChangeScene);
    }

    public void ChangeScene(SceneType sceneType)
    {
        SceneManager.LoadScene(SceneType.Loader.ToString());
    }

    public void RestartScene()
    {
        var currentScene = _gameStateController.CurrentScene.Value;
        SceneManager.LoadScene(currentScene.ToString());
    }

    public void Dispose()
    {
        _disposable?.Dispose();
    }
}
