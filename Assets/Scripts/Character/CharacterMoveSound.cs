using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMove))]
public class CharacterMoveSound : MonoBehaviour
{
    [SerializeField] float _timeBetweenSteps = 0.6f;
    [SerializeField] private AudioSource _stepSource;
    [SerializeField] private AudioClip _rightStepClip;
    [SerializeField] private AudioClip _leftStepClip;

    private float _startStepTime;
    private CharacterMove _move;
    private StepType _nextStepType;

    void Start()
    {
        _move = GetComponent<CharacterMove>();
        _startStepTime = Time.timeSinceLevelLoad;
    }


    void Update()
    {
        if (!_move.IsMoving)
        {
            _startStepTime = Time.timeSinceLevelLoad;
            _nextStepType = StepType.RIGHT;
            return;
        }
        if (Time.timeSinceLevelLoad - _startStepTime >= _timeBetweenSteps)
        {
            if (_nextStepType == StepType.RIGHT)
            {
                PlayRightStep();
                _nextStepType = StepType.LEFT;
                _startStepTime = Time.timeSinceLevelLoad;
            } 
            else if (_nextStepType == StepType.LEFT)
            {
                PlayLeftStep();
                _nextStepType = StepType.RIGHT;
                _startStepTime = Time.timeSinceLevelLoad;
            }
        }
    }

    private void PlayRightStep()
    {
        _stepSource.clip = _rightStepClip;
        _stepSource.Play();
    }

    private void PlayLeftStep()
    {
        _stepSource.clip = _leftStepClip;
        _stepSource.Play();
    }

    private enum StepType
    {
        RIGHT,
        LEFT,
    }
}