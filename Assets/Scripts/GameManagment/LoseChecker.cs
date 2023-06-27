using UnityEngine;

public class LoseChecker : MonoBehaviour
{
    [SerializeField] private LoseUI _loseUI;

    private void Start()
    {
        _loseUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Lose()
    {
        Time.timeScale = 0.2f;
        _loseUI.gameObject.SetActive(true);
    }
}