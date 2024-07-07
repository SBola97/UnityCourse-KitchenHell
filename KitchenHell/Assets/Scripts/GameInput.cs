using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized(){
        
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

/*         if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Pressing the W button");
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Pressing the A button");
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Pressing the S button");
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Pressing the D button");
            inputVector.x = +1;
        } */
        Debug.Log(inputVector);
        return inputVector;
    }
}
