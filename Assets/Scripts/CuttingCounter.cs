using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[]  cuttingRecipesSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSo()))
        {
            KitchenObjectSO outputKitchenObjectSO =  GetOutputForInput(GetKitchenObject().GetKitchenObjectSo());
            GetKitchenObject().DestorySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }
    
    private  KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSOArray)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO.output;
            }
        }

        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSOArray)
        {
            if (cuttingRecipeSO.input == input)
            {
                return true;
            }
        }

        return false;
    }
    
}
