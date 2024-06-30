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


    Camera mainCamera;
    CameraController cameraController;

    Vector2 moveInput;
    Vector2 aimInput;


    // Start is called before the first frame update
    void Start()
    {
        moveStick.OnStickValueUpdated += moveStickUpdated;
        aimStick.OnStickValueUpdated += aimStickUpdated;
        mainCamera = Camera.main;
        cameraController = FindAnyObjectByType<CameraController>();
    }

    private void moveStickUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    private void aimStickUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
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
        PerformMove();
        UpdateCamera();
    }

    private void PerformMove()
    {
        Vector3 moveDir = StickInputToWorldDir(moveInput);
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);


        Vector3 aimDir = moveDir;

        if (aimInput.magnitude != 0)
        {
            aimDir = StickInputToWorldDir(aimInput);
        }

        RotateTowards(aimDir);
        
    }

    private void UpdateCamera()
    {
        if (moveInput.magnitude != 0)
        {

            if (cameraController != null)
            {
                cameraController.AddYawInput(moveInput.x);

            }

        }
    }

    private void RotateTowards(Vector3 aimDir)
    {
        if (aimDir.magnitude != 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerpAlpha);
        }
    }
}
