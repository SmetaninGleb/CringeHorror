using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterAI : MonoBehaviour
{
    [SerializeField] private float _hearRadius = 3f;
    [SerializeField] private float _lookAngle = 90f;
    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private float _runSpeed = 3f;
    [SerializeField] private float _attackDistance = 4f;
    [SerializeField] private float _attackSpeed = 10f;
    [SerializeField] private float _attackedDistance = 1.5f;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform _lookPoint;

    private NavMeshAgent _agent;
    private List<MonsterAIPoint> _points;
    private MonsterAIPoint _currentTargetPoint;
    private int _currentTargetPointIndex = 0;
    private MonsterAIState _state;
    private Player _player;
    private bool _isFollowPlayer = false;
    public MonsterAIState State => _state;

    public Action<MonsterAIState> OnStateChanged;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _points = FindObjectsOfType<MonsterAIPoint>().ToList();
        _points = _points.OrderBy(point => point.Index).ToList();
        _currentTargetPoint = _points[_currentTargetPointIndex];
        _agent.SetDestination(_currentTargetPoint.transform.position);
        _state = MonsterAIState.CRAWLING;
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (CanAttack())
        {
            _agent.speed = _attackSpeed;
            _agent.SetDestination(_player.transform.position);
            if (_state != MonsterAIState.JUMP)
            {
                _state = MonsterAIState.JUMP;
                OnStateChanged?.Invoke(MonsterAIState.JUMP);
            }
            if (Vector3.Distance(transform.position, _player.transform.position) <= _attackedDistance)
            {
                _player.Attack();
            }
            return;
        }
        if (CanSeeAndFollowPlayer())
        {
            _isFollowPlayer = true;
            _agent.speed = _runSpeed;
            _agent.SetDestination(_player.transform.position);
            if (_state != MonsterAIState.FAST_CRAWLING)
            {
                _state = MonsterAIState.FAST_CRAWLING;
                OnStateChanged?.Invoke(MonsterAIState.FAST_CRAWLING);
            }
            return;
        }
        else
        {
            _isFollowPlayer = false;
            _agent.speed = _walkSpeed;
            _agent.SetDestination(_currentTargetPoint.transform.position);
            if (_state != MonsterAIState.CRAWLING)
            {
                _state = MonsterAIState.CRAWLING;
                OnStateChanged?.Invoke(MonsterAIState.CRAWLING);
            }
        }

        if (transform.position.x == _currentTargetPoint.transform.position.x
            && transform.position.z == _currentTargetPoint.transform.position.z)
        {
            _currentTargetPointIndex = (_currentTargetPointIndex + 1) % _points.Count;
            _currentTargetPoint = _points[_currentTargetPointIndex];
            _agent.SetDestination(_currentTargetPoint.transform.position);
            if (_state != MonsterAIState.CRAWLING)
            {
                _state = MonsterAIState.CRAWLING;
                OnStateChanged?.Invoke(MonsterAIState.CRAWLING);
            }
        }
    }

    private bool CanSeeAndFollowPlayer()
    {
        if (_player.IsAttacked) return false;
        Vector3 playerPos = _player.transform.position;
        Vector3 curPos = transform.position;
        NavMeshPath pathToPlayer = new NavMeshPath();
        _agent.CalculatePath(playerPos, pathToPlayer);
        if (pathToPlayer.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        if (Vector3.Distance(playerPos, curPos) <= _hearRadius)
        {
            return true;
        }
        Vector3 lookVec = playerPos - _lookPoint.position;
        float angle = Vector3.Angle(_lookPoint.forward, lookVec);
        if (Mathf.Abs(angle) > Mathf.Abs(_lookAngle)) return false;
        if (Physics.Raycast(_lookPoint.position, lookVec,out RaycastHit hit ,lookVec.magnitude))
        {
            if (hit.collider.gameObject.layer != _playerLayer)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    private bool CanAttack()
    {
        if (!CanSeeAndFollowPlayer()) return false;
        Vector3 playerPos = _player.transform.position;
        if (Vector3.Distance(transform.position, playerPos) <= _attackDistance)
        {
            return true;
        }
        return false;
    }
}

public enum MonsterAIState
{
    IDLE,
    CRAWLING,
    FAST_CRAWLING,
    JUMP,
}