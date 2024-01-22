using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    private void Awake()
    {
        _inputManager.Initialize();
        Debug.Log("InputManager initialized!");
    }
}
