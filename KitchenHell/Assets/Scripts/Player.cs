using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    // Start is called before the first frame update
    private void Start(){

    }
    // Update is called once per frame
    private void Update(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        //Sets the movement of the player on the 3 axes with proper speed and FPS synchronization
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        //Sets de size of the player
        float playerRadius = .7f;
        float playerHeight = 2f;
        //Verifies if there is something on his way preventing from moving
        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDir,
            moveDistance);
        
        //Handles the diagonal movement when collides with something
        if (!canMove){
            //Cannot move towards moveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

            canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDirX,
            moveDistance);

            if (canMove){
                //Player can move only on X
                moveDir = moveDirX;
            }
            else{
                //Cannot move on X

                //Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;

                canMove = !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius,
                moveDirZ,
                moveDistance);
                if (canMove){
                    //Player can move only on Z
                    moveDir = moveDirZ;
                }
                else{
                    //Cannot move in any direction
                }
            }
        }
        if (canMove){
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        //Makes the player rotate towards the direction he moves smoothly
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        Debug.Log(inputVector);

    }

    public bool IsWalking(){
        return isWalking;
    }
}
