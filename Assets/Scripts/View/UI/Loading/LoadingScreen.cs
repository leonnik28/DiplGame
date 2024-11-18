using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private Text _progressText;

    private AsyncOperation _asyncOperation;

    private const string LOAD_SCENE_NAME = "Game";
    private const float FAKE_LOAD_TIME = 3f;

    private void Start()
    {
        FakeLoadAsync().Forget();
    }

    private async UniTaskVoid FakeLoadAsync()
    {
        _asyncOperation = SceneManager.LoadSceneAsync(LOAD_SCENE_NAME);
        _asyncOperation.allowSceneActivation = false;

        float startTime = Time.time;
        float endTime = startTime + FAKE_LOAD_TIME;

        while (Time.time < endTime)
        {
            float elapsedTime = Time.time - startTime;
            float progress = Mathf.Clamp01(elapsedTime / FAKE_LOAD_TIME);
            _loadingBar.value = progress;
            _progressText.text = (progress * 100f).ToString("F0") + "%";

            await UniTask.Yield();
        }

        _loadingBar.value = 1f;
        _progressText.text = "100%";

        _asyncOperation.allowSceneActivation = true;
    }
}
