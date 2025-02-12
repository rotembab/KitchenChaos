using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform CounterTopPoint;
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
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
