using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    [SerializeField] private int _moveSpeed;
    private Vector2 _moveDirection;
    [SerializeField] GameObject _patrolPointA;
    [SerializeField] GameObject _patrolPointB;
    private Transform _currentPoint;

    private Rigidbody2D _rigidbody2D;
    private Animator _anim;

    private void Start()
    {
        _rigidbody2D= GetComponent<Rigidbody2D>();
        _anim= GetComponent<Animator>();

        _currentPoint = _patrolPointB.transform;
        _anim.SetBool("isRunning", true);
    }

    private void Update()
    {
        if (_currentPoint == _patrolPointB.transform)
        {
            _moveDirection.x = 1;
            if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f) _currentPoint = _patrolPointA.transform;
        } else if (_currentPoint == _patrolPointA.transform)
        {
            _moveDirection.x = -1;
            if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f) _currentPoint = _patrolPointB.transform;
        }

    }

    private void FixedUpdate()
    {
        Move(_moveDirection);
        Flip();
    }

    private void Move(Vector2 directionMove)
    {
        _rigidbody2D.velocity = new Vector2(directionMove.x * _moveSpeed, _rigidbody2D.velocity.y);
    }
    private void Flip()
    {
        if (_moveDirection.x < 0) transform.rotation = Quaternion.Euler(0, -180, 0);
        if (_moveDirection.x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

