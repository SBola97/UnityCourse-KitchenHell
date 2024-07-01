using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    [SerializeField] private float moveSpeed = 7f;
    // Update is called once per frame
    private void Update(){

        Vector2 inputVector = new Vector2(0,0);

        if(Input.GetKey(KeyCode.W)){
            Debug.Log("Pressing the W button");
            inputVector.y =+1; 
        }
        if(Input.GetKey(KeyCode.S)){
            Debug.Log("Pressing the A button");
            inputVector.y =-1; 
        }
        if(Input.GetKey(KeyCode.A)){
            Debug.Log("Pressing the S button");
            inputVector.x =-1; 
        }
        if(Input.GetKey(KeyCode.D)){
            Debug.Log("Pressing the D button");
            inputVector.x =+1; 
        }
        inputVector = inputVector.normalized;
        
        //Sets the movement of the player on the 3 axes with proper speed and FPS synchronization
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        //Makes the player rotate towards the direction he moves smoothly
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDir, Time.deltaTime * rotateSpeed);
        Debug.Log(inputVector);
         
    }
}
