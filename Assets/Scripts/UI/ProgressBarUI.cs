using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;
    private IHasProgress hasProgress;
    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("Game Object:" + hasProgressGameObject.name + " does not have IHasProgress component");
        }
        hasProgress.OnProgressChanged += OnProgressChanged;
        barImage.fillAmount = 0;
        ChangeVisiblity(false);
    }

    private void OnProgressChanged(object sender, IHasProgress.ProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized==0f || e.progressNormalized==1f)
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
