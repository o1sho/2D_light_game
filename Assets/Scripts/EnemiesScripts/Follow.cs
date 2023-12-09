using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _minDistance;
    [SerializeField] Transform _target;

    private void Update()
    {
        if (Vector2.Distance(transform.position, _target.position) > _minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Attack!");
        }
    }
}
