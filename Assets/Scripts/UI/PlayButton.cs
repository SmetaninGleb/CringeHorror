using Agava.WebUtility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    [SerializeField] private int _playSceneIndex = 1;
    //private YandexGame _yandexGame;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartPlaying);
    //    _yandexGame = FindObjectOfType<YandexGame>();
    //    _yandexGame.OpenFullscreenAd.AddListener(AdTurnOn);
    //    _yandexGame.CloseFullscreenAd.AddListener(AdTurnOff);
        WebApplication.InBackgroundChangeEvent += InBackgroundBeh;
    }

    private void OnDestroy()
    {
        WebApplication.InBackgroundChangeEvent -= InBackgroundBeh;
    }

    private void StartPlaying()
    {
        SceneManager.LoadScene(_playSceneIndex);
    }

    private void InBackgroundBeh(bool isBack)
    {
        if (isBack)
        {
            AudioListener.volume = 0f;
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.volume = 1f;
            AudioListener.pause = false;
        }
    }

    //private void AdTurnOn()
    //{
    //    AudioListener.volume = 0f;
    //    AudioListener.pause = true;
    //}
    //
    //private void AdTurnOff()
    //{
    //    AudioListener.volume = 1f;
    //    AudioListener.pause = false;
    //}
}