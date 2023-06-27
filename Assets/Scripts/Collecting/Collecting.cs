using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Collecting : MonoBehaviour
{
    private List<Collectable> _allCollectables;
    private List<Collectable> _collectedList = new List<Collectable>();

    private int _level;

    public Action OnCollected;

    void Start()
    {
        _allCollectables = FindObjectsOfType<Collectable>().ToList();
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Collectable collectable))
        {
            _collectedList.Add(collectable);
            collectable.Collect();
            OnCollected?.Invoke();
        }
    }
}