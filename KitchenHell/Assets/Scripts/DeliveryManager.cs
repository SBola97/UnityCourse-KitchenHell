using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSuccess;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private float waitingRecipesMax = 4f;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update(){
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f){
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipesMax){
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0,recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
       for (int i = 0; i < waitingRecipeSOList.Count; i++){
        RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
        //Looks for same number of ingredients
        if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.getKitchenSOList().Count){
            bool plateContentMatchesRecipe = true;
            //Cycles through all ingredients in the Recipe
            foreach (KitchenObjectSO recipeIngredient in waitingRecipeSO.kitchenObjectSOList){
                bool ingredientFound = false;
                //Cycles through all ingredients in the Plate
                foreach (KitchenObjectSO plateIngredient in plateKitchenObject.getKitchenSOList()){
                    if(recipeIngredient == plateIngredient){
                        ingredientFound = true;
                        break;
                    }
                }
                if(!ingredientFound){
                    plateContentMatchesRecipe = false;
                }
            }
            if(plateContentMatchesRecipe){
                Debug.Log("[Recipe] " + waitingRecipeSO.recipeName + " was successfully delivered");
                waitingRecipeSOList.RemoveAt(i);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
       }
       OnRecipeFailed?.Invoke(this, EventArgs.Empty);
       Debug.Log("No recipe matches, recipe rejected!");

    }

    public List<RecipeSO> GetWaitingRecipeSOList(){
        return waitingRecipeSOList;
    }

}
