using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private int _playSceneIndex = 1;
    [SerializeField] private int _menuSceneIndex = 0;

    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }

    private void Restart()
    {
        SceneManager.LoadScene(_playSceneIndex);
    }

    private void Exit()
    {
        SceneManager.LoadScene(_menuSceneIndex);
    }
}