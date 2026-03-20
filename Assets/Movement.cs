using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    public float turnSpeed = 120f;
    public float lookSpeed = 120f;
    public float gravity = -20f;

    float xRotation = 0f;
    float verticalVelocity = 0f;

    Camera cameraChild;
    CharacterController controller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cameraChild = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // MOVIMENTO AVANTI / INDIETRO
        float moveForward = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveForward = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveForward = -1f;
        }

        // MOVIMENTO LATERALE (STRAFE)
        float moveSide = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveSide = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveSide = 1f;
        }

        Vector3 move = transform.forward * moveForward * speed
                     + transform.right * moveSide * speed;

        // GRAVITA'
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);

        // ROTAZIONE CON A / D
        float turnInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            turnInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnInput = 1f;
        }

        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);

        // GUARDARE SU / GIU' CON W / S
        float lookInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            lookInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            lookInput = -1f;
        }

        xRotation -= lookInput * lookSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraChild.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}