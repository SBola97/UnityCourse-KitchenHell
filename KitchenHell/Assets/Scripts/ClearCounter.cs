using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    public void Interact(){
        Debug.Log("Interact!");
        //Spawns the object and puts it exactly on top of the counter
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab,counterTopPoint); 

        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
        
    }
}
