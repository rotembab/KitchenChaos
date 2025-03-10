using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour , IKitchenObjectParent
{
    public event EventHandler OnPickup;
   // public static Player Instance { get; private set; }
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f; 
    [SerializeField] private LayerMask countersLayer;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private float playerRadius = 0.7f;
    private float playerHeight = 2f;
    private float interactDistance = 2f; 
    private bool isWalking = false;
    private Vector3 lastInteractionDir;
    private BaseCounter selectedCounter;
    private float MinSitckInput = 0.5f;
    
    private KitchenObject kitchenObject;
    
    
    public class SelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
        
    }

    public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    
    
    private void Awake()
    { 
        //Instance = this;
    }
    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInputOnOnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInputOnOnInteractAlternateAction;
    }

    private void GameInputOnOnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        } 
    }

    private void GameInputOnOnInteractAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        if(!IsOwner) return;
        HandleMovementServerAuth();
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    SetSelectCounter(baseCounter);
                   
                }
            }
            else
            {
                SetSelectCounter(null);
            }
        }
        else
        {
            SetSelectCounter(null);
        }
    }
    
    public bool IsWalking() 
    {
        return isWalking;   
    }

    private void HandleMovementServerAuth()
    {
        Vector2 direction =  GameInput.Instance.GetMovementVectorNormalized();
        HandleMovementServerRpc(direction);
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void HandleMovementServerRpc(Vector2 direction)
    {
        Vector3 moveDir = new Vector3(direction.x, 0f, direction.y);
        float moveDistance =  (Time.deltaTime * speed);
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up * playerHeight,playerRadius,  moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3( moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x> Mathf.Abs(MinSitckInput)  && !Physics.CapsuleCast(transform.position,transform.position +Vector3.up * playerHeight,playerRadius,  moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3( 0f, 0f, moveDir.z).normalized;
                canMove =  moveDir.z> Mathf.Abs(MinSitckInput) && !Physics.CapsuleCast(transform.position,transform.position +Vector3.up * playerHeight,playerRadius,  moveDirZ, moveDistance);
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
    
    private void HandleMovement()
    {
        Vector3 moveDir = CalcMoveDir();
        float moveDistance =  (Time.deltaTime * speed);
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up * playerHeight,playerRadius,  moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3( moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x> Mathf.Abs(MinSitckInput)  && !Physics.CapsuleCast(transform.position,transform.position +Vector3.up * playerHeight,playerRadius,  moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3( 0f, 0f, moveDir.z).normalized;
                canMove =  moveDir.z> Mathf.Abs(MinSitckInput) && !Physics.CapsuleCast(transform.position,transform.position +Vector3.up * playerHeight,playerRadius,  moveDirZ, moveDistance);
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
        Vector2 direction =  GameInput.Instance.GetMovementVectorNormalized();
        return new Vector3(direction.x, 0f, direction.y);
    }   
    
    private void SetSelectCounter(BaseCounter clearCounter)
    {
        this.selectedCounter = clearCounter;
        OnSelectedCounterChanged?.Invoke(this, new SelectedCounterChangedEventArgs{SelectedCounter = selectedCounter}); 
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickup?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    
    
    
    
    
}

