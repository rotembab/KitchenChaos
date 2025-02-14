 using System;
 using UnityEngine;
 using UnityEngine.Serialization;

 public class SelectedCounterVisual : MonoBehaviour
{ 
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectsArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += PlayerOnOnSelectedCounterChanged;
    }

    private void PlayerOnOnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        if(e.SelectedCounter == baseCounter)
        {
            SetVisual(true);
        }
        else
        {
            SetVisual(false);
        }
    }
    
    private void SetVisual(bool value)
    {
        foreach (GameObject visualGameObject in visualGameObjectsArray)
        {
            visualGameObject.SetActive(value);
        }
        
    }
}
