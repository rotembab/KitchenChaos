using System;
using UnityEngine;

public class StoveCounter : BaseCounter
{
   
   public event EventHandler<StoveCounterEventArgs> OnStoveCounterStateChanged;
   public class StoveCounterEventArgs : EventArgs
   {
       public  State state;
   }
    
   public  enum State
   {
      Idle,
      Frying,
      Fried,
      Burned
   }
   [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
   [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
   
   private State state;   
   private float fryingTimer;
   private float burningTimer;
   private FryingRecipeSO fryingRecipeSo;
   private BurningRecipeSO burningRecipeSo;

   public void Start()
   {
      state = State.Idle;
   }

   private void Update()
   {
      if (HasKitchenObject())
      {
      switch (state)
      {
         case State.Idle:
            break; 
         case State.Frying:
            fryingTimer += Time.deltaTime;
         
            if (fryingTimer >= fryingRecipeSo.fryingTimerMax)
            {
               GetKitchenObject().DestorySelf();
               KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);
               
               burningRecipeSo = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());
               state = State.Fried;
               OnStoveCounterStateChanged?.Invoke(this, new StoveCounterEventArgs{state = state});
               burningTimer = 0f;
            }
            break;
         case State.Fried:
            burningTimer += Time.deltaTime;
         
            if (burningTimer >= burningRecipeSo.burningTimerMax)
            {
               GetKitchenObject().DestorySelf();
               KitchenObject.SpawnKitchenObject(burningRecipeSo.output, this);
               state = State.Burned;
               OnStoveCounterStateChanged?.Invoke(this, new StoveCounterEventArgs{state = state});
            }
            break;
         case State.Burned:
            break;
         
      }
      }
   }

   public override void Interact(Player player)
   {
      if (!HasKitchenObject())
      {
         if (player.HasKitchenObject())
         {
            if (HasFryingWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
            {
               player.GetKitchenObject().SetKitchenObjectParent(this);
               fryingRecipeSo = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());
               state = State.Frying;
               OnStoveCounterStateChanged?.Invoke(this, new StoveCounterEventArgs{state = state});
               fryingTimer = 0f;
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
            state = State.Idle;
            OnStoveCounterStateChanged?.Invoke(this, new StoveCounterEventArgs{state = state});
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
   
   
   private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (BurningRecipeSO burningRecipeSo in burningRecipeSOArray)
      {
         if (burningRecipeSo.input == inputKitchenObjectSO)
         {
            return burningRecipeSo;
         }
      }
      return null;
   }
}
