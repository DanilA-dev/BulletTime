using System;
using Core.Signals;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class CoreMenu : BaseMenu
    {
        [SerializeField] private Image _playerHealthBar;
        [Header("Animations")]
        [SerializeField] private float _shakeStrength;
        [SerializeField] private float _shakeDuration;
        
        public override MenuType Type => MenuType.Core;

        [Inject]
        private void Construct()
        {
            MessageBroker.Default.Receive<PlayerSignals.ChangeHealth>()
                .Subscribe(OnPlayerDamaged).AddTo(gameObject);
        }

        private void OnEnable()
        {
            _playerHealthBar.fillAmount = 1;
        }

        private void OnPlayerDamaged(PlayerSignals.ChangeHealth changeHealth)
        {
            PlayShakeAnimation();
            _playerHealthBar.fillAmount = (changeHealth.CurrentHealth) / (changeHealth.MaxHealth);
        }

        #region Tween Animations
        
        private void PlayShakeAnimation()
        {
            var parent = _playerHealthBar.transform.parent;
            var seq = DOTween.Sequence();
            seq.Restart();
            seq.Append(parent.transform.DOShakePosition(_shakeDuration, _shakeStrength));
        }

        #endregion
        
    }
}