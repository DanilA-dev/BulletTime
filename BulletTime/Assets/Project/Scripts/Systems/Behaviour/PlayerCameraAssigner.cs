using Systems.Intefaces;
using Cinemachine;
using Core.Signals;
using Types;
using UniRx;
using UnityEngine;
using Zenject;

namespace Behaviour
{
    public class PlayerCameraAssigner : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCameraPrefab;

        private Vector3 _initPosition;
        private CinemachineVirtualCamera _createdCam;
        private IGameStateController _gameStateController;
        
        [Inject]
        private void Construct(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            MessageBroker.Default.Receive<PlayerSignals.Spawn>()
                .Subscribe(_ => AssignCameraToPlayer(_.Player)).AddTo(gameObject);

            _gameStateController.CurrentState
                .Where(s => s == GameStateType.RestartLevel)
                .Subscribe(_ => ResetCamPosition()).AddTo(gameObject);
        }

        private void ResetCamPosition()
        {
            _createdCam.ForceCameraPosition(_initPosition, Quaternion.identity);
        }

        private void AssignCameraToPlayer(PlayerEntity playerEntity)
        {
            _createdCam = Instantiate(_virtualCameraPrefab);
            var camT = _createdCam.transform;
            var cameraTarget = playerEntity.GetComponentInChildren<PlayerCameraTarget>();

            if (cameraTarget == null)
            {
                Debug.LogError($"Can't assign camera to player, no CameraTarget on {playerEntity.name} childs");
                return;
            }

            _createdCam.Follow = cameraTarget.transform;
            _createdCam.LookAt = cameraTarget.transform;
            camT.position = new Vector3(camT.position.x, camT.position.y, - 10);
            _initPosition = camT.position;
        }
    }
}