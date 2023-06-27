using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private int _mainSceneIndex = 1;
    [SerializeField] private int _menuSceneIndex = 0;

    private void Start()
    {
        _nextButton.onClick.AddListener(NextButtonClicked);
        _exitButton.onClick.AddListener(ExitButtonClicked);
    }

    private void NextButtonClicked()
    {
        SceneManager.LoadScene(_mainSceneIndex);
    }

    private void ExitButtonClicked()
    {
        SceneManager.LoadScene(_menuSceneIndex);
    }
}