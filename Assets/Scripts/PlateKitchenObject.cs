using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<IngredientAddedEventArgs> OnIngrediantAdded;
    
    public class IngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    
    private List<KitchenObjectSO> kitchenSOList;
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    
    private void Awake()
    {
        kitchenSOList = new List<KitchenObjectSO>();
    }   
    
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        if(kitchenSOList.Contains(kitchenObjectSO) || kitchenObjectSO == null)
        {
            return false;
        }
        kitchenSOList.Add(kitchenObjectSO);
        OnIngrediantAdded?.Invoke(this, new IngredientAddedEventArgs { kitchenObjectSO = kitchenObjectSO }); 
        return true;
    }
    
    
    
    public List<KitchenObjectSO> GetKitchenSOList()
    {
        return kitchenSOList;
    }
}
