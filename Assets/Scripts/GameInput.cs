using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnBindChanged; 
    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
        Gamepad_Interact,
        Gamepad_InteractAlternate,
        Gamepad_Pause,
    }
    
    private const string PLAYER_PREFS_BINDINGS= "InputBindings";
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    
    private PlayerInputSystem playerInputSystem;
    private void Awake()
    {
        Instance = this;
        playerInputSystem = new PlayerInputSystem();
        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputSystem.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
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

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            case Binding.Move_Up:
                return playerInputSystem.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputSystem.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputSystem.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputSystem.Player.Move.bindings[4].ToDisplayString();
            case Binding.Pause:
                return playerInputSystem.Player.Pause.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputSystem.Player.InteractAlternate.bindings[0].ToDisplayString();
            
            case Binding.Gamepad_Interact:
                return playerInputSystem.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_InteractAlternate:
                return playerInputSystem.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Binding.Gamepad_Pause:
                return playerInputSystem.Player.Pause.bindings[1].ToDisplayString();
            default:
            case Binding.Interact:
                return playerInputSystem.Player.Interact.bindings[0].ToDisplayString();
        }
    }


    public void RebindBinding(Binding binding,Action onActionRebound)
    {
        playerInputSystem.Player.Disable();
        InputAction inputAction = null;
        int bindingIndex = 0;
        
        switch (binding)
        {
            case Binding.Move_Up:
                inputAction = playerInputSystem.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputSystem.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputSystem.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputSystem.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Pause:
                inputAction = playerInputSystem.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputSystem.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Interact:
                inputAction = playerInputSystem.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_Interact:
                inputAction = playerInputSystem.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_InteractAlternate:
                inputAction = playerInputSystem.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Pause:
                inputAction = playerInputSystem.Player.Pause;
                bindingIndex = 1;
                break;
        }
        
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete((callback) =>
        {
            callback.Dispose();
            playerInputSystem.Player.Enable();
            onActionRebound();
       
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS,     playerInputSystem.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            OnBindChanged?.Invoke(this, EventArgs.Empty); 
        }).Start();
    }
}
