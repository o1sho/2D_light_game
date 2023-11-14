using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;

    public void Initialize()
    {
        //inputController.Player.Attack.performed += context => OnAttack();
        //inputController.Player.Block.performed += context => OnBlock();
    }
    private void Start()
    {
        isGrounded = false;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnAttack()
    {

    }

    private void OnBlock()
    {

    }

}
