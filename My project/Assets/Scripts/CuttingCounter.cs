using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]
    private KitchenObjectsSO cutKitchenObjectSO; 
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // doesn't have anything here 
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetkitchenObjectParent(this);
            }
        }
        else
        {
            // has a kitchen object here
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetkitchenObjectParent(player);
                // player doesn't have something in his hands
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            // there is object here!
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);

        }
    }
}
