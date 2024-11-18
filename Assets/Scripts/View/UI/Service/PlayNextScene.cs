using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class PlayNextScene : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private MapDataConfigurator _configurator;

    private const string SCENE_NAME = "LoadingMainToGame";

    [Inject]
    private void Construct(MapDataConfigurator mapDataConfigurator)
    {
        _configurator = mapDataConfigurator;
    }

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
        _configurator.ConfigureMapData();
        SceneManager.LoadSceneAsync(SCENE_NAME);
    }
}
