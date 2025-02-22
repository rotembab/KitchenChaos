using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
 private const string NUMBER_POPUP = "NumberPopup";
 
 [SerializeField] private TextMeshProUGUI countdownText;
 private Animator animator;
 private int lastCountdownNumber;
 
 private void Awake()
 {
  animator = GetComponent<Animator>();
 }
 
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
  int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
  countdownText.SetText(countdownNumber.ToString());
  if (countdownNumber != lastCountdownNumber)
  {
   animator.SetTrigger(NUMBER_POPUP);
   lastCountdownNumber = countdownNumber;
   SoundManager.Instance.PlayCountdownSound();
  }
 }
}
