using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TouchCheckController : MonoBehaviour
{
    public bool isTouched = false;
    [SerializeField] private string _tagCheckObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_tagCheckObject))
        {
            isTouched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_tagCheckObject))
        {
            isTouched = false;
        }
    }
}
