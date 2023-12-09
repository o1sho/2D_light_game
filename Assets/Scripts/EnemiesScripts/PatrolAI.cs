using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Transform[] _patrolPoints;
    [SerializeField] float _waitTime;
    [SerializeField] private int currentPointIndex;

    private bool once;

    private void Update()
    {
        if (transform.position != _patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[currentPointIndex].position, _speed * Time.deltaTime);
        } else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(WaitingBetweenPoints());
            }
        }
    }

    private IEnumerator WaitingBetweenPoints()
    {
        yield return new WaitForSeconds(_waitTime);
        currentPointIndex = currentPointIndex + 1 < _patrolPoints.Length ? currentPointIndex++ : 0;
        Debug.Log("puj");
        once = false;
    }
}

//_wallJumpingDirection = transform.rotation.y == 0 ? -1 : 1;
