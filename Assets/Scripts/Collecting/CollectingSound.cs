using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collecting))]
public class CollectingSound : MonoBehaviour
{
    [SerializeField] private AudioSource _collectAudioSource;

    private Collecting _collecting;

    private void Start()
    {
        _collecting = GetComponent<Collecting>();
        _collecting.OnCollected += Collected;
    }

    private void Collected()
    {
        _collectAudioSource.Play();
    }
}