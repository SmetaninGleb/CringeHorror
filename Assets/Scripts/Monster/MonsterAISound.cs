using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MonsterAI))]
public class MonsterAISound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _walkingClip;
    [SerializeField] private float _walkingVolume = 0.1f;
    [SerializeField] private AudioClip _runClip;
    [SerializeField] private float _runVolume = 1f;
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private float _attackVolume = 1f;

    private MonsterAI _ai;

    private void Start()
    {
        _ai = GetComponent<MonsterAI>();
        _ai.OnStateChanged += StateChanged;
    }

    private void OnDestroy()
    {
        _ai.OnStateChanged -= StateChanged;
    }

    private void StateChanged(MonsterAIState state)
    {
        if (state == MonsterAIState.CRAWLING)
        {
            _audioSource.clip = _walkingClip;
            _audioSource.volume = _walkingVolume;
            _audioSource.Play();
        }
        if (state == MonsterAIState.FAST_CRAWLING)
        {
            _audioSource.clip = _runClip;
            _audioSource.volume = _runVolume;
            _audioSource.Play();
        }
        if (state == MonsterAIState.JUMP)
        {
            _audioSource.clip = _attackClip;
            _audioSource.volume = _attackVolume;
            _audioSource.Play();
        }
    }
}