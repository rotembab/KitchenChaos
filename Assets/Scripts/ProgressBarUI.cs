using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start()
    {
        cuttingCounter.OnCuttingProgressChanged += CuttingCounterOnOnCuttingProgressChanged;
        barImage.fillAmount = 0;
    }

    private void CuttingCounterOnOnCuttingProgressChanged(object sender, CuttingCounter.CuttingProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.cuttingProgressNormalized;
    }
}
