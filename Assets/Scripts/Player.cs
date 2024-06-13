using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;

    Vector2 moveInput;


    // Start is called before the first frame update
    void Start()
    {
        moveStick.OnStickValueUpdated += moveStickmoveStick;
    }

    private void moveStickmoveStick(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * moveSpeed); 
    }
}
