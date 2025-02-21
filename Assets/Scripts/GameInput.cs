using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    
    private PlayerInputSystem playerInputSystem;
    private void Awake()
    {
        Instance = this;
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Enable();
        playerInputSystem.Player.Interact.performed += InteractOnperformed;
        playerInputSystem.Player.InteractAlternate.performed += InteractAlternateOnperformed;
        playerInputSystem.Player.Pause.performed += PauseOnperformed;
    }
    
    private void OnDestroy()
    {
        playerInputSystem.Player.Interact.performed -= InteractOnperformed;
        playerInputSystem.Player.InteractAlternate.performed -= InteractAlternateOnperformed;
        playerInputSystem.Player.Pause.performed -= PauseOnperformed;
        
        playerInputSystem.Dispose();
    }

    private void PauseOnperformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this,EventArgs.Empty);
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
