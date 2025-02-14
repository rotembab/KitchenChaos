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
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
        if (HasKitchenObject())
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
}
