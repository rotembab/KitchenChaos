using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{

   public event EventHandler OnRecipeSpawned;
   public event EventHandler OnRecipeDelivered;

   public event EventHandler OnRecipeSuccess;
   public event EventHandler OnRecipeFailed;
   
    public static DeliveryManager Instance { get; private set; }
   [SerializeField] private RecipeListSO recipeListSO;
   
   private List<RecipeSO> waitingRecipeSOList;
   private float spawnRecipeTimer;
   private float spawnRecipeTimerMax = 4f;
   private int maxWaitingRecipeCount = 4;

   private void Awake()
   {
      Instance = this;
      waitingRecipeSOList = new List<RecipeSO>();
      
   }
   private void Update()
   {
      spawnRecipeTimer -= Time.deltaTime;
      if (spawnRecipeTimer <= 0)
      {
         spawnRecipeTimer = spawnRecipeTimerMax;
         if (waitingRecipeSOList.Count < maxWaitingRecipeCount)
         {
            RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
            waitingRecipeSOList.Add(waitingRecipeSO);
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
         }
      }
   }

   public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
   {
      for (int i = 0; i< waitingRecipeSOList.Count; i++)
      {
         RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
         if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenSOList().Count)
         {
            bool plateMatch = true;
            foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
            {
               bool ingrediantFound = false;
               foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenSOList())
               {
                  if (recipeKitchenObjectSO == plateKitchenObjectSO)
                  {
                     ingrediantFound = true;
                     break;
                  }
               }
               
               if(!ingrediantFound)
               {
                  plateMatch = false;
               }
            }

            if (plateMatch)
            {
               Debug.Log("Player delivered a correct recipe");
               waitingRecipeSOList.RemoveAt(i);
               OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
               OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
               return;
            }
         }
      }
      
      // If we reach here, the plate does not match any waiting recipe
      Debug.Log("Player failed to deliver a correct recipe");
      OnRecipeFailed?.Invoke(this, EventArgs.Empty);
   }
   
   
   public List<RecipeSO> GetWaitingRecipeSOList()
   {
      return waitingRecipeSOList;
   }
}
