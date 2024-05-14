using System;
using Systems.Intefaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PauseMenu : BaseMenu
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Transform _panelTransform;
        [SerializeField] private Image _blackScreen;
        [Space] 
        [SerializeField] private float _blackScreenFadeDuration;
        [SerializeField] private float _popUpDuration;


        private IGameStateController _gameStateController;
        
        public override MenuType Type => MenuType.Pause;

        [Inject]
        private void Construct(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }
        
        private void Awake()
        {
            _resumeButton.onClick.AddListener(ResumeGame);
            _exitButton.onClick.AddListener(ExitGame);
        }

        public void OnEnable()
        {
            PlayFadeBlackScreen(0.7f, 0);
            PlayPopUpAnimation(Vector3.one, Vector3.zero);
        }

        private void OnDestroy()
        {
            _resumeButton.onClick.RemoveListener(ResumeGame);
            _exitButton.onClick.RemoveListener(ExitGame);
        }

        private async void ResumeGame()
        {
            PlayFadeBlackScreen(0, 0.7f);
            await PlayPopUpAnimation(Vector3.zero, Vector3.zero).AsyncWaitForCompletion();
            _gameStateController.ChangeState(GameStateType.Core);
        }
        
        private void ExitGame()
        {
            _gameStateController.ExitApp();
        }

        #region Tween Animations

        private Sequence PlayFadeBlackScreen(float endValue, float from)
        {
            var seq = DOTween.Sequence();
            seq.Restart();
            seq.Append(_blackScreen.DOFade(endValue, _blackScreenFadeDuration).From(from));
            seq.SetUpdate(true);
            seq.SetAutoKill(true);

            return seq;
        }
        
        private Sequence PlayPopUpAnimation(Vector3 endValue, Vector3 from)
        {
            var seq = DOTween.Sequence();
            seq.Restart();
            seq.Append(_panelTransform.DOScale(endValue, _popUpDuration).From(from));
            seq.SetUpdate(true);
            seq.SetAutoKill(true);

            return seq;
        }

        #endregion
        
       
    }
}