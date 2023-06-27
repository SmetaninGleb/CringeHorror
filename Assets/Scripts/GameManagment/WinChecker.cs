using System.Collections;
using UnityEngine;


public class WinChecker : MonoBehaviour
{
    [SerializeField] private LevelConfigs _levelConfigs;

    private Collecting _collecting;
    private WinUI _winUI;
    private int _level;
    private int _numFoundToWin;
    private int _collectedNum = 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(LevelPrefsName.Name))
        {
            PlayerPrefs.SetInt(LevelPrefsName.Name, 1);
            PlayerPrefs.Save();
            _level = 1;
        }
        else
        {
            _level = PlayerPrefs.GetInt(LevelPrefsName.Name);
        }
        _numFoundToWin = _levelConfigs.GetCollectableToFound(_level);
        _collecting = FindObjectOfType<Collecting>();
        _collecting.OnCollected += Collected;
        _winUI = FindObjectOfType<WinUI>();
        _winUI.gameObject.SetActive(false);
    }

    private void Collected()
    {
        _collectedNum++;
        if (_collectedNum >= _numFoundToWin)
        {
            Win();
        }
    }

    private void Win()
    {
        Time.timeScale = 0f;
        _winUI.gameObject.SetActive(true);
        PlayerPrefs.SetInt(LevelPrefsName.Name, _level + 1);
        PlayerPrefs.Save();
        Cursor.lockState = CursorLockMode.Confined;
    }
}