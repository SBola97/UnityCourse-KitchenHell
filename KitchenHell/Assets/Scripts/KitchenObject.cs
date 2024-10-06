using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;//To know if it's on the clearCounter

    public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObjectSO; 
    }

    public void setClearCounter(ClearCounter clearCounter){
        if(this.clearCounter != null){
            //Cleans up the former clearCounter parent
            this.clearCounter.clearKitchenObject();
        }
        //Sets the new clearCounter parent
        this.clearCounter = clearCounter;
        //Sets the kitchenObject
        if(clearCounter.hasKitchenObject()){
            Debug.LogError("Counter already has a KitchenObject!");
        }
        clearCounter.setKitchenObject(this);

        //Updates the visual
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter(){
        return clearCounter;
    }

}
