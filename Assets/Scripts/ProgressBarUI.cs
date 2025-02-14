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
        ChangeVisiblity(false);
    }

    private void CuttingCounterOnOnCuttingProgressChanged(object sender, CuttingCounter.CuttingProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.cuttingProgressNormalized;
        if(e.cuttingProgressNormalized==0f || e.cuttingProgressNormalized==1f)
        {
            ChangeVisiblity(false);
        }
        else
        {
            ChangeVisiblity(true);
        }
    }

    private void ChangeVisiblity(bool visible)
    {
       gameObject.SetActive(visible);
    }
}
