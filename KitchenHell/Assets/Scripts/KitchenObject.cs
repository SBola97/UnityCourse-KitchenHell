using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;//To know who owns the KitchenObject (eg. a clearCounter)

    public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObjectSO; 
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent){
        if(this.kitchenObjectParent != null){
            //Cleans up the former parent
            this.kitchenObjectParent.ClearKitchenObject();
        }
        //Sets the new parent
        this.kitchenObjectParent = kitchenObjectParent;
        //Sets the kitchenObject
        if(kitchenObjectParent.HasKitchenObject()){
            Debug.LogError("IKitchenObjectParent already has a KitchenObject!");
        }
        kitchenObjectParent.SetKitchenObject(this);

        //Updates the visual
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent(){
        return kitchenObjectParent;
    }

}
