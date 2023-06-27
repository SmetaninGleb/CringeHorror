using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CollectableCounter : MonoBehaviour
{
    [SerializeField] private LevelConfigs _levelConfigs;
    private TMP_Text _text;
    private int _notFoundedCollectableNum;
    private int _foundedCollectableNum = 0;
    private int _needToFoundCollectableNum;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        List<Collectable> collectables = FindObjectsOfType<Collectable>().ToList();
        _notFoundedCollectableNum = collectables.Count;
        _needToFoundCollectableNum = _levelConfigs.GetCollectableToFound(PlayerPrefs.GetInt(LevelPrefsName.Name));
        foreach (Collectable collectable in collectables)
        {
            collectable.OnCollected += Collected;
        }
        UpdateText();
    }

    private void Collected(Collectable collectable)
    {
        _notFoundedCollectableNum--;
        _foundedCollectableNum++;
        UpdateText();
        collectable.OnCollected -= Collected;
    }

    private void UpdateText()
    {
        _text.text = _foundedCollectableNum.ToString() + "/" + _needToFoundCollectableNum.ToString();
    }
}