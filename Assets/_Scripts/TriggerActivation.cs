using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerActivation : MonoBehaviour
{
    [SerializeField] private UnityEvent _component;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _component.Invoke();
        }
    }
}
