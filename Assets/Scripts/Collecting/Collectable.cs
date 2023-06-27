using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectable : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 180;

    public Action<Collectable> OnCollected;

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, _rotateSpeed * Time.deltaTime, 0f));
    }

    public void Collect()
    {
        OnCollected?.Invoke(this);
        gameObject.SetActive(false);
    }
}