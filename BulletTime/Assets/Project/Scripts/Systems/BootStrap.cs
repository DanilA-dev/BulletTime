using Systems.Intefaces;
using UnityEngine;
using Zenject;

public class BootStrap : MonoBehaviour
{
    private IGameStateController _gameStateController;
    
    [Inject]
    private void Construct(IGameStateController gameStateController)
    {
        _gameStateController = gameStateController;
    }
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        _gameStateController.ChangeScene(SceneType.Main);
    }
}
