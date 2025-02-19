using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipiesDeliveredText;
    
    
    
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        SetSelfActive(false);
    }

    private void HandleGameStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOverActive())
        {
            SetSelfActive( true);
            recipiesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulDeliveriesAmount().ToString();
        }
        else
        {
            SetSelfActive(false);
        }
    }


    private void SetSelfActive(bool active)
    {
        gameObject.SetActive(active);
    }
    
}
