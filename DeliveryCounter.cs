using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }


    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // only accept plates


                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);


                player.GetKitchenObject().DestroySelf();
            }       
        }
    }
}
