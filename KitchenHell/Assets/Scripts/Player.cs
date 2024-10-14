using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs{
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake(){
        if(Instance != null){
            Debug.LogError("There is more than 1 Player instance");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start(){
        //Listens the event for interactions
        gameInput.OnInteractAction += GameInput_OnInteraction; 
    }

    private void GameInput_OnInteraction(object sender, EventArgs e){
        if(selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }

    // Update is called once per frame
    private void Update(){
        HandleMovement();
        HandleInteractions();
    }
    public bool IsWalking(){
        return isWalking;
    }

    private void HandleInteractions(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if(moveDir != Vector3.zero){
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)){
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                //Has a Counter
                if(baseCounter != selectedCounter){
                    SetSelectedCounter(baseCounter);
                }
            }
            else{
                SetSelectedCounter(null);   
            }
        }
        else{
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement(){
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
        //Debug.Log(inputVector);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter){
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
            selectedCounter = selectedCounter   
        });
    }

    public Transform GetKitchenObjectFollowTransform(){
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
}
