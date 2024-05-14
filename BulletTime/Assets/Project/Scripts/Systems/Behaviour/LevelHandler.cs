using Systems.Intefaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Behaviour
{
    public class LevelHandler : MonoBehaviour
    {
        private IEnemyTurretGetService _enemyTurretGetService;
        private IPlayerGetService _playerGetService;
        private IGameStateController _gameStateController;

        [Inject]
        private void Construct(IGameStateController gameStateController,IPlayerGetService playerGetService,
            IEnemyTurretGetService enemyTurretGetService)
        {
            _gameStateController = gameStateController;
            _playerGetService = playerGetService;
            _enemyTurretGetService = enemyTurretGetService;

            gameStateController.CurrentState.Subscribe(OnGameState).AddTo(gameObject);
        }

        private void OnGameState(GameStateType gameStateType)
        {
            if(gameStateType == GameStateType.TitleScreen)
                CreateEntities();

            if (gameStateType == GameStateType.RestartLevel)
                ResetEntities();
        }

        private void CreateEntities()
        {
            _playerGetService.Create();
            _enemyTurretGetService.CreateEnemies();
        }

        private void ResetEntities()
        {
            _playerGetService.Reset();
            _enemyTurretGetService.Reset();
            _gameStateController.ChangeState(GameStateType.Core);
        }
        
    }
}