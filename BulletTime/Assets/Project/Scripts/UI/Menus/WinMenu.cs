using System;
using Systems.Intefaces;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class WinMenu : BaseMenu
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _winLabel;
        [Header("Animations")]
        [SerializeField] private float _winLabelFadeDuration;
        [SerializeField] private float _restartButtonFadeInterval;
        [SerializeField] private float _restartButtonFadeDuration;

        private IGameStateController _gameStateController;
        private CanvasGroup _restartButtonCanvasGroup;

        public override MenuType Type => MenuType.Win;

        [Inject]
        private void Construct(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartLevel);
            _restartButtonCanvasGroup = _restartButton.AddComponent<CanvasGroup>();

        }
        private void OnEnable() => PlayFadeAnimation();

        private void OnDestroy() => _restartButton.onClick.RemoveAllListeners();

        private void RestartLevel()
        {
            _gameStateController.ChangeState(GameStateType.RestartLevel);
        }

        #region Tween Animations

        private void PlayFadeAnimation()
        {
            var seq = DOTween.Sequence();
            seq.Restart();
            seq.Append(_winLabel.DOFade(1, _winLabelFadeDuration).From(0));
            seq.AppendInterval(_restartButtonFadeInterval);
            seq.Append(_restartButtonCanvasGroup.DOFade(1, _restartButtonFadeDuration).From(0));
        }

        #endregion
    }
}