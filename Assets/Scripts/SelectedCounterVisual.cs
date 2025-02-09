 using System;
 using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedCounterVisual;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += PlayerOnOnSelectedCounterChanged;
    }

    private void PlayerOnOnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        if(e.SelectedCounter == clearCounter)
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
        selectedCounterVisual.SetActive(value);
    }
}
