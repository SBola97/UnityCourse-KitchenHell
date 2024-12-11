using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

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
                bool isIngredientAdded;
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //Player's holding a plate
                    isIngredientAdded = plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());
                    if(isIngredientAdded){
                        GetKitchenObject().DestroySelf();
                    } 
                }
                else{
                    //Player's not holding a plate but something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        //There's a plate on the counter
                        isIngredientAdded = plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()); 
                        if(isIngredientAdded){
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            //Player isn't carrying anything
            else{
                //Give it to the player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
