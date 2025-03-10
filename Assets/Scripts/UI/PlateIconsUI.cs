using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKitchenObject.OnIngrediantAdded += PlateKitchenObject_OnIngrediantAdded;
    }

    private void PlateKitchenObject_OnIngrediantAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e)
    {
        UpdateVisual();
    }


    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            
            Destroy(child.gameObject);
            
        
        }
        
        foreach( KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenSOList())
        {
           Transform iconTransform =  Instantiate(iconTemplate, transform);
           iconTransform.gameObject.SetActive(true);
           iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
