using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO KitchenObjectSO;

    public event EventHandler OnPlayerGrabObject;


    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            // if player doesn't have kitchen object
            KitchenObject.SpawnKitchenObject(KitchenObjectSO, player);     

            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
