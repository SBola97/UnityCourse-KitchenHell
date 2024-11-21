using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeArray;

    private float fryingTimer;

    FryingRecipeSO fryingRecipeSO;
    private void Update(){
        if(HasKitchenObject()){
            fryingTimer += Time.deltaTime;
            if(fryingTimer > fryingRecipeSO.fryingTimerMax){
                //It's fried
                fryingTimer = 0f;
                Debug.Log("Fried");
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
            }
            Debug.Log(fryingTimer);
        }
    }

    public override void Interact(Player player)
    {
        //There is no KitchenObject here
        if(!HasKitchenObject()){
            //Checks if player is carrying a KitchenObject
            if(player.HasKitchenObject()){
                //Checks if the KitchenObject belongs to a recipe
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //Drop it on the counter to fry it
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                }
            }
            else{
                //Player is not carrying anything or KitchenObject can't be fry
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
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO){
        var fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO getOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if(fryingRecipeSO != null){
            return fryingRecipeSO.output;
        }
        return null;
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeArray){
            if(fryingRecipeSO.input == inputKitchenObjectSO){
                return fryingRecipeSO;
            }
        }
        return null;
    }
}
