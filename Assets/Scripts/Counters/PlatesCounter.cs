using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private float platesSpawnTime;
    private float spawnPlateTimer;
    private int platesCount;
    private int platesMaxCount = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer >= platesSpawnTime)
        {
            spawnPlateTimer = 0f;
            if (platesCount < platesMaxCount)
            {
                platesCount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(platesCount > 0)
            {
                platesCount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateTaken?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            
        }
    }
}
