using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] JoyStick aimStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float turnSpeed = 20f;
    [SerializeField] float animTurnSpeed = 20f;

    [Header("Inventory")]
    [SerializeField] InventoryComponent inventoryComponent;
    

    Animator animator;
    float animatorTurnSpeed;

    


    Camera mainCamera;
    CameraController cameraController;

    Vector2 moveInput;
    Vector2 aimInput;


    // Start is called before the first frame update
    void Start()
    {
        moveStick.OnStickValueUpdated += moveStickUpdated;
        aimStick.OnStickValueUpdated += aimStickUpdated;
        aimStick.OnStickTabed += StartSwitchWeapon;
        mainCamera = Camera.main;
        cameraController = FindAnyObjectByType<CameraController>();
        animator = GetComponent<Animator>();
    }

    void StartSwitchWeapon()
    {
        animator.SetTrigger("SwitchWeapon");
        
    }

    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }


    private void moveStickUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;

    }

    private void aimStickUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
        if (aimInput.magnitude > 0)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }

    }

    Vector3 StickInputToWorldDir(Vector2 inputVal)
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return  rightDir * inputVal.x + upDir * inputVal.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        PerformMoveAndAim();
        UpdateCamera();
    }

    private void PerformMoveAndAim()
    {
        Vector3 moveDir = StickInputToWorldDir(moveInput);
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);
        UpdateAim(moveDir);

        // Dot(a,b) = |a| |b| cos(theta)

        float forword = Vector3.Dot( moveDir , transform.forward);
        float right = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("ForwardSpeed", forword);
        animator.SetFloat("RightSpeed", right);
    }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 aimDir = currentMoveDir;

        if (aimInput.magnitude != 0)
        {
            aimDir = StickInputToWorldDir(aimInput);
        }

        RotateTowards(aimDir);
    }

    private void UpdateCamera()
    {
        //if the player is moving and not aiming, rotate the camera with the player
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {

            
                cameraController.AddYawInput(moveInput.x);

            

        }
    }

    private void RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = 0;
        if (aimDir.magnitude != 0)
        {
            Quaternion prevRot = transform.rotation;
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerpAlpha);

            Quaternion currentRot = transform.rotation;

            float Dir = Vector3.Dot(aimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRot, currentRot) * Dir;
             currentTurnSpeed = rotationDelta / Time.deltaTime;
        }

        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime  * animTurnSpeed);
            animator.SetFloat("TurningSpeed", animatorTurnSpeed);
    }
}
