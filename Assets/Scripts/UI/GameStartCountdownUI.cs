using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
 [SerializeField] private TextMeshProUGUI countdownText;

 private void Start()
 {
  GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
  SetSelfActive(false);
 }

 private void HandleGameStateChanged(object sender, EventArgs e)
 {
  if (GameManager.Instance.IsCountdownToStartActive())
  {
   SetSelfActive( true);
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

 private void Update()
 {
  countdownText.SetText(Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString());
 }
}
