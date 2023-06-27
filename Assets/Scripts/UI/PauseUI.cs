using Agava.WebUtility;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private Slider _mouseSensSlider;

    private bool _isPaused = false;
    private CharacterMove _characterMove;
    private YandexGame _yandexGame;
    private bool _wasTimeStopped = false;
    private bool _wasCursorLocked = true;
    
    private void Start()
    {
        _pauseUI.SetActive(false);
        _characterMove = FindObjectOfType<CharacterMove>();
        _mouseSensSlider.value = _characterMove.GetMouseSens();
        _mouseSensSlider.onValueChanged.AddListener(MouseSliderChanged);
        _yandexGame = FindObjectOfType<YandexGame>();
        _yandexGame.OpenFullscreenAd.AddListener(Pause);
        WebApplication.InBackgroundChangeEvent += Pause;
    }

    private void OnDestroy()
    {
        WebApplication.InBackgroundChangeEvent -= Pause;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                Pause();
            }
            else
            {
                _isPaused = false;
                _pauseUI.SetActive(false);
                if (_wasTimeStopped)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
                if (_wasCursorLocked == true)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Confined;
                }
                AudioListener.volume = 1f;
                AudioListener.pause = false;
            }
        }
    }

    private void Pause()
    {
        if (_isPaused) return;
        if (Time.timeScale < 1)
        {
            _wasTimeStopped = true;
        }
        else
        {
            _wasTimeStopped = false;
        }
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            _wasCursorLocked = true;
        }
        else
        {
            _wasCursorLocked = false;
        }
        _isPaused = true;
        _pauseUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        AudioListener.volume = 0f;
        AudioListener.pause = true;
    }

    private void Pause(bool isPause)
    {
        if (!isPause) return;
        Pause();
    }

    private void MouseSliderChanged(float value)
    {
        _characterMove.SetMouseSensitivity(value);
    }
}