using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;



    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object 
            if (player.HasKitchenObject())
            {
                //player is carrying sth 
                player.GetKitchenObject().SetKicthenObjectParent(this); // we go into the player, we get kitchen  object that player is holding, we modify kitchen object parent ontio this object
            }
            else
            {
                // player not carring anything
            }
        }
        else
        { // there is a kitchenobject here
            if (player.HasKitchenObject())
            {
                // player is caryying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // if yes, player is holding a plate 

                    if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKicthenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //player is not holding a plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //counter is holding a plate
                        if (plateKitchenObject.TryAddIngridient(player.GetKitchenObject().GetKicthenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }           
                }
            }
            else
            {
                // player is not caryying anything 
                GetKitchenObject().SetKicthenObjectParent(player);
            }
        }
    }
}
