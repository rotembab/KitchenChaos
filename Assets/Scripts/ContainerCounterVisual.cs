using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounterOnOnPlayerGrabbedObject;
    }

    private void ContainerCounterOnOnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
