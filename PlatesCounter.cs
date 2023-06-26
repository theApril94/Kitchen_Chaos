using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;



    private float spawnPlatesTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update()
    {
        spawnPlatesTimer += Time.deltaTime;
        if (spawnPlatesTimer > spawnPlateTimerMax)
        {
            spawnPlatesTimer = 0f;

            if (KitchenGameManager.Instance.IsGamePlaying() && platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is empty handed

            if (platesSpawnedAmount > 0)
            {
                // there is at least one plate here
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);  
            }
        }
    }
}


