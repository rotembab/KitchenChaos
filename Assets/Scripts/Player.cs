using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayer;
    private float playerRadius = 0.7f;
    private float playerHeight = 2f;
    private float interactDistance = 2f;
    private bool isWalking = false;
    private Vector3 lastInteractionDir;
    
    
    
    private void Update()
    {
        HandleMovement();
        HandleInteractions();

    }

    private void HandleInteractions()
    {
        Vector3 moveDir = CalcMoveDir();
        if(moveDir!= Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactDistance, countersLayer))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }
    
    public bool IsWalking() 
    {
        return isWalking;   
    }
    private void HandleMovement()
    {
        Vector3 moveDir = CalcMoveDir();
        float moveDistance =  (Time.deltaTime * speed);
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up * playerHeight,playerRadius,  moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3( moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up * playerHeight,playerRadius,  moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3( 0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up * playerHeight,playerRadius,  moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //cant move at any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        
        transform.forward = Vector3.Slerp( transform.forward ,moveDir, Time.deltaTime * rotationSpeed); 
        isWalking = moveDir != Vector3.zero;
    }
    
    private Vector3 CalcMoveDir()
    {
        Vector2 direction = gameInput.GetMovementVectorNormalized();
        return new Vector3(direction.x, 0f, direction.y);
    }   
}

