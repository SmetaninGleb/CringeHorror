using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectablePlacer : MonoBehaviour
{
    [SerializeField] private List<Collectable> _collectablePrefabs = new List<Collectable>();
    [SerializeField] private LevelConfigs _levelConfigs;

    private int _numOfCollactableSpawned;
    private List<CollectablePlace> _placeList;

    private void Start()
    {
        _numOfCollactableSpawned = _levelConfigs.GetCollectableToSpawn(PlayerPrefs.GetInt(LevelPrefsName.Name));
        _placeList = FindObjectsOfType<CollectablePlace>().ToList();
        int collectableIndex = 0;
        List<int> usedPlaces = new List<int>();
        for (int i = 0; i < _numOfCollactableSpawned; i++)
        {
            int placeIndex = Random.Range(0, _placeList.Count);
            while (usedPlaces.Contains(placeIndex))
            {
                placeIndex = (placeIndex + 1) % _placeList.Count;
            }
            usedPlaces.Add(placeIndex);
            CollectablePlace place = _placeList[placeIndex];
            Instantiate(
                _collectablePrefabs[collectableIndex],
                place.transform.position,
                place.transform.rotation, transform
            );
            collectableIndex = (collectableIndex + 1) % _collectablePrefabs.Count;
        }
    }
}