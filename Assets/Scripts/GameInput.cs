using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    
    private PlayerInputSystem playerInputSystem;
    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Enable();
        playerInputSystem.Player.Interact.performed += InteractOnperformed;
        playerInputSystem.Player.InteractAlternate.performed += InteractAlternateOnperformed;
    }

    private void InteractAlternateOnperformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this,EventArgs.Empty);
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
