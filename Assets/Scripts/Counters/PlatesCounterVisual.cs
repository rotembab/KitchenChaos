using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    private List<GameObject> plateVisualGameObjectList = new List<GameObject>();

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateTaken += PlatesCounter_OnPlateTaken;
    }

    private void PlatesCounter_OnPlateTaken(object sender, EventArgs e)
    {
        GameObject lastPlateVisual = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(lastPlateVisual);
        Destroy(lastPlateVisual);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
       Transform plateVisualTransform =  Instantiate(plateVisualPrefab, counterTopPoint);
       float plateOffsetY = .1f;
       plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
         plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
