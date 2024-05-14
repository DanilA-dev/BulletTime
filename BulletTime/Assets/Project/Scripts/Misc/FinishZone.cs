using System;
using Systems.Intefaces;
using Types;
using UnityEngine;
using Zenject;

namespace Misc
{
    [RequireComponent(typeof(Collider))]
    public class FinishZone : MonoBehaviour
    {
        private Collider _collider;
        private IGameStateController _gameStateController;

        [Inject]
        private void Construct(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerEntity player))
                FinishLevel();
        }

        private void FinishLevel()
        {
            _gameStateController.ChangeState(GameStateType.WinGame);
        }
    }
}