 using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
 [SerializeField] private StoveCounter stoveCounter;
 [SerializeField] private GameObject stoveOnGameObject;
 [SerializeField] private GameObject particlesGameObject;
 
 
 private void Start()
 {
     stoveCounter.OnStoveCounterStateChanged += StoveCounterOnOnStoveCounterStateChanged;
     toggleStoveOn(false);
 }

 private void StoveCounterOnOnStoveCounterStateChanged(object sender, StoveCounter.StoveCounterEventArgs e)
 {
     if(e.state!=StoveCounter.State.Idle)
     {
         toggleStoveOn(true);
     }
     else
     {
         toggleStoveOn(false);
     }
 }
 
 
 
 private void toggleStoveOn(bool on)
 {
     stoveOnGameObject.SetActive(on);
     particlesGameObject.SetActive(on);
 }
}
