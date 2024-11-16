using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayNextScene : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private const string SCENE_NAME = "Game";

    private void OnEnable()
    {
        _playButton.onClick.AddListener(PlayScene);
    }

    private void OnDisable()
    {
        _playButton.onClick?.RemoveListener(PlayScene);
    }

    private void PlayScene()
    {
        SceneManager.LoadScene(SCENE_NAME);
    }
}
