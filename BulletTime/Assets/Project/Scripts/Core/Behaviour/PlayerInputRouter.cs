using Systems.Intefaces;
using Types;
using UnityEngine;
using Zenject;

namespace Core.Behaviour
{
    [RequireComponent(typeof(PlayerEntity))]
    public class PlayerInputRouter : MonoBehaviour
    {
        private PlayerEntity _playerEntity;
        private IInputService _inputService;
        private ITimeController _timeController;

        private bool _isPointerPressed;
        private bool _isPointerReleased;
        private Vector2 _lastPos;

        [Inject]
        private void Construct(IInputService inputService, ITimeController timeController)
        {
            _inputService = inputService;
            _timeController = timeController;
        }
        
        private void Awake()
        {
            _playerEntity = GetComponent<PlayerEntity>();
        }

        private void Start()
        {
            _inputService.PointerPosition += UpdateDirection;
            _inputService.PointerPerformed += PointerPerform;
            _inputService.PointerCanceled += PointerCancel;
        }

        private void OnDestroy()
        {
            _inputService.PointerPosition -= UpdateDirection;
            _inputService.PointerPerformed -= PointerPerform;
            _inputService.PointerCanceled -= PointerCancel;
        }

        private void PointerPerform()
        {
            _isPointerPressed = true;
            _isPointerReleased = false;
            
            _playerEntity.OnPointerClick(_lastPos);
            _timeController.SetSlowTime();
        }
        
        private void PointerCancel()
        {
            _isPointerReleased = true;
            _isPointerPressed = false;
            
            _playerEntity.OnPointerRelease();
            _timeController.SetDefaultTime();
        }
        
        private void UpdateDirection(Vector2 pos)
        {
            _lastPos = pos;
            
            if (_isPointerPressed)
                _playerEntity.OnPointerPositionUpdate(pos);
        }
    }
}