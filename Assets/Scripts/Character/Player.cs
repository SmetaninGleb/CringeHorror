using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private LoseChecker _loseChecker;
    private bool _isAttacked = false;

    public bool IsAttacked => _isAttacked;

    private void Start()
    {
        _loseChecker = FindObjectOfType<LoseChecker>();
    }

    public void Attack()
    {
        _isAttacked = true;
        _loseChecker.Lose();
    }
}