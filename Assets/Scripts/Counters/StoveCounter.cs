using UnityEngine;

public class StoveCounter : BaseCounter
{
   [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

   public override void Interact(Player player)
   {
      if (!HasKitchenObject())
      {
         if (player.HasKitchenObject())
         {
            if (HasFryingWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
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
   
   
   private  KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
   {
      FryingRecipeSO cuttingRecipeSO =  GetFryingRecipeSOWithInput(input);
      if (cuttingRecipeSO != null)
      {
         return cuttingRecipeSO.output;
      }

      return null;
   }

   private bool HasFryingWithInput(KitchenObjectSO input)
   {
      FryingRecipeSO cuttingRecipeSO =  GetFryingRecipeSOWithInput(input);
      return cuttingRecipeSO != null;
   }


   private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (FryingRecipeSO fryingRecipeSo in fryingRecipeSOArray)
      {
         if (fryingRecipeSo.input == inputKitchenObjectSO)
         {
            return fryingRecipeSo;
         }
      }
      return null;
   }
}
