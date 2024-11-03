using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player){
        //Checks if player is carrying something already
        if(!player.HasKitchenObject()){
            //Spawns the object and gives it to the Player
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            //Fires off the event
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
