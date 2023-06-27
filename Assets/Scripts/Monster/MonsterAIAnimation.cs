using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MonsterAI))]
public class MonsterAIAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Header("Animation Bools Names")]
    [SerializeField] private string _idleBoolName = "IsIdle";
    [SerializeField] private string _crawlingBoolName = "IsCrawling";
    [SerializeField] private string _fastCrawlingBoolName = "IsFastCrawling";
    [SerializeField] private string _jumpBoolName = "IsJump";

    private MonsterAI _ai;
    private MonsterAIState _previousState = MonsterAIState.IDLE;

    void Start()
    {
        _ai = GetComponent<MonsterAI>();
    }

    void Update()
    {
        if (_ai.State != _previousState)
        {
            _previousState = _ai.State;
            TurnOffAllBools();
            if (_ai.State == MonsterAIState.IDLE)
            {
                _animator.SetBool(_idleBoolName, true);
            }
            if (_ai.State == MonsterAIState.CRAWLING)
            {
                _animator.SetBool(_crawlingBoolName, true);
            }
            if (_ai.State == MonsterAIState.FAST_CRAWLING)
            {
                _animator.SetBool(_fastCrawlingBoolName, true);
            }
            if (_ai.State == MonsterAIState.JUMP)
            {
                _animator.SetBool(_jumpBoolName, true);
            }
        }
    }

    private void TurnOffAllBools()
    {
        _animator.SetBool(_idleBoolName, false);
        _animator.SetBool(_crawlingBoolName, false);
        _animator.SetBool(_fastCrawlingBoolName, false);
        _animator.SetBool(_jumpBoolName, false);
    }
}