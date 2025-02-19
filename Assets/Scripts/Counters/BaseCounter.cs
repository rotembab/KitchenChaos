using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectDrop;
    [SerializeField] private Transform CounterTopPoint;
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        
    }
    public virtual void InteractAlternate(Player player)
    {
        
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return CounterTopPoint;
    }
    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnAnyObjectDrop?.Invoke(this, EventArgs.Empty);
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
