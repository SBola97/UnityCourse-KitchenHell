using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutkitchenObjectSO;
    public override void Interact(Player player){
        //There is no kitchenObject here
        if(!HasKitchenObject()){
            //Player is carrying a kitchenObject
            if(player.HasKitchenObject()){
                //Drop it to the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else{
                //Player is not carrying anything
            }
            

        }
        //There is a kitchenObject here
        else{
            if(player.HasKitchenObject()){
                //Player's carrying something
            }
            //Player isn't carrying anything
            else{
                //Give it to the player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        //Check if there is something on top of it
        if(HasKitchenObject()){
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutkitchenObjectSO, this);
        }
    }
}
