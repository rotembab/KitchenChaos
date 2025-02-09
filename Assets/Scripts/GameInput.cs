using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputSystem playerInputSystem;
    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 direction = playerInputSystem.Player.Move.ReadValue<Vector2>();
        return direction.normalized;
    }
}
