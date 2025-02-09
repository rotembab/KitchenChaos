using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputSystem playerInputSystem;
    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Enable();
        playerInputSystem.Player.Interact.performed += InteractOnperformed;
    }

    private void InteractOnperformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }
 

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 direction = playerInputSystem.Player.Move.ReadValue<Vector2>();
        return direction.normalized;
    }
}
