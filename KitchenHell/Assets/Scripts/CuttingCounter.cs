using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player){
        //There is no KitchenObject here
        if(!HasKitchenObject()){
            //Checks if player is carrying a KitchenObject
            if(player.HasKitchenObject()){
                //Checks if the KitchenObject belongs to a recipe
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //Drop it on the counter to cut it
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else{
                //Player is not carrying anything or KitchenObject can't be cut
            }
        }
        //There is a KitchenObject here
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

    public override void InteractAlternate(Player player){
        //Check if there is a KitchenObject here AND if it can be cut
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
            KitchenObjectSO outputKitchenObjectSO = getOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO){
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray){
            if(cuttingRecipeSO.input == kitchenObjectSO){
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO getOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray){
            if(cuttingRecipeSO.input == inputKitchenObjectSO){
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
