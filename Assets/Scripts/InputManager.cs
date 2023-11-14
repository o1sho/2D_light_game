using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputController inputController;

    public void Initialize()
    {
        inputController = new InputController();
        inputController.Enable();
    }

    private void OnDisable()
    {
        inputController.Disable();
    }
}
