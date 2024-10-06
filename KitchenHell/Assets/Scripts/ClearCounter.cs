using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject; //To know if there is a kitchenObject on top of it

    private void Update(){
        if(testing && Input.GetKeyDown(KeyCode.T)){
            if(kitchenObject != null){
                //Set the kitchenObject to a different parent (clearCounter)
                kitchenObject.setClearCounter(secondClearCounter);
            }
        }
    }
    public void Interact(){
        if(kitchenObject == null){
            //Spawns the object and puts it exactly on top of a counter
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab,counterTopPoint); 
            kitchenObjectTransform.GetComponent<KitchenObject>().setClearCounter(this);
        }
        else{
            Debug.Log(kitchenObject.GetClearCounter()); 
        }
        
    }

    public Transform GetKitchenObjectFollowTransform(){
        return counterTopPoint;
    }

    public void setKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void clearKitchenObject(){
        kitchenObject = null;
    }

    public bool hasKitchenObject(){
        return kitchenObject != null;
    }
}
