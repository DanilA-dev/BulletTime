using System.Collections;
using Systems.Intefaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image _fillBar;

    private float _progress;
    private IGameStateController _gameStateController;
    
    [Inject]
    private void Construct(IGameStateController gameStateController)
    {
        _gameStateController = gameStateController;
    }
    private void Start() => StartCoroutine(LoadAsync(_gameStateController.CurrentScene.Value));

    private void Update()
    {
        _fillBar.fillAmount = Mathf.MoveTowards(_fillBar.fillAmount, _progress, Time.deltaTime * 3);
    }
   
    private IEnumerator LoadAsync(SceneType sceneType)
    {
        yield return null;
        var asyncOperation = SceneManager.LoadSceneAsync(sceneType.ToString());
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            _progress = asyncOperation.progress;
            if (asyncOperation.progress >= 0.9f)
                break;

            yield return new WaitForSeconds(0.5f);
        }
        asyncOperation.allowSceneActivation = true;
    }

}
