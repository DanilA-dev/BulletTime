using Systems.Intefaces;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu : BaseMenu
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _labelText;
        [Header("Animations")]
        [SerializeField] private Vector3 _labelAnimScale;
        [SerializeField] private float _labelScaleDuration;
        [SerializeField] private float _labelFadeDuration;
        
        private IGameStateController _gameStateController;
        
        public override MenuType Type => MenuType.Menu;

        [Inject]
        private void Construct(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }
        
        private void Awake() => _playButton.onClick.AddListener(StartLevel);

        private void OnEnable() => PlayScaleAnimation();

        private void OnDestroy() => _playButton.onClick.RemoveAllListeners();

        private async void StartLevel()
        {
            await FadeOutToCore().AsyncWaitForCompletion();
            _gameStateController.ChangeState(GameStateType.Core);
        }

        #region Tween Animations

        private void PlayScaleAnimation()
        {
            _labelText.transform
                .DOScale(_labelAnimScale, _labelScaleDuration)
                .From(Vector3.one)
                .SetLoops(-1, LoopType.Yoyo)
                .SetUpdate(true)
                .SetAutoKill(true);
        }

        private Sequence FadeOutToCore()
        {
            var seq = DOTween.Sequence();
            seq.Append(_labelText
                .DOFade(0, _labelFadeDuration)
                .SetUpdate(true)
                .SetAutoKill(true));
            seq.Join(_titleText
                .DOFade(0, _labelFadeDuration)
                .SetUpdate(true)
                .SetAutoKill(true));

             return seq;
        }

        #endregion

    }
}