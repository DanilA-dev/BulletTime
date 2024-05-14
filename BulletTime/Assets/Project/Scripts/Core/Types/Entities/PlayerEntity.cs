using Model;
using UnityEngine;
using View;

namespace Types
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerEntity : Entity, IDamagable, IEnemyTarget
    {
        [SerializeField] private PlayerView _view;
        
        private Player _player;
        private Camera _cam;
        private Rigidbody _rigidbody;
        private Transform _transform;
        
        private Vector3 _pointerDirection;
        private Vector3 _jumpDirection;

        public Transform TargetTransform => _transform;
        public IDamagable Damagable => this;
        public bool CanBeDamaged => !_player.IsDead;

        
        public Player Player => _player;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _cam = Camera.main;
            _transform = transform;
        }

        public void Init(Player player)
        {
            _player = player;
        }

        #region Input Callbacks

        public void OnPointerPositionUpdate(Vector2 dir)
        {
            UpdateJumpDirection(dir);
        }

        public void OnPointerClick(Vector2 dir)
        {
            _view.EnalbeLine();
        }

       
        public void OnPointerRelease()
        {
            _view.DisableLine();
            PerformJump();
        }

        #endregion
       

        private void PerformJump()
        {
            var jumpStrength = Mathf.Clamp01(_pointerDirection.sqrMagnitude);
            _rigidbody.velocity = Vector3.zero;
            _jumpDirection.Normalize();
            _rigidbody.AddForce(_jumpDirection * (jumpStrength * _player.DefaultJumpForce), ForceMode.Impulse);
        }

        private void UpdateJumpDirection(Vector2 dir)
        {
            _pointerDirection = (_cam.ScreenToWorldPoint(dir) - _transform.position);
            _pointerDirection.z = 0;

            _jumpDirection = _pointerDirection;
            var jumpDirSqr = Mathf.Clamp01((_pointerDirection).sqrMagnitude);
            var fromPlayerDir = _transform.position + (_jumpDirection.normalized) * (jumpDirSqr * 2f);
            _view.SetArrowLine(_transform.position, fromPlayerDir);
        }
      
        public void Damage(float value)
        {
            _player.Damage(value);
        }

        public void ResetPlayer()
        {
            _player.Reset();
            _rigidbody.velocity = Vector3.zero;
            _transform.position = _player.SpawnPosition;
        }

        private void OnDrawGizmos()
        {
            if (_transform == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(_transform.position, _jumpDirection);
        }
    }
}