using Systems.Intefaces;
using UnityEngine;
using Zenject;

public class LevelInitializer : MonoBehaviour
{
    private IGameStateController _gameStateController;

    [Inject]
    private void Construct(IGameStateController gameStateController)
    {
        _gameStateController = gameStateController;
    }

    private void Awake()
    {
        _gameStateController.ChangeState(GameStateType.TitleScreen);
    }
}
