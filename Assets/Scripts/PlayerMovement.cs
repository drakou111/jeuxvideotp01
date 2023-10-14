using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform mainCam;

    private CharacterController characterController;
    private Vector3 direction;
    private float rotationTime = 0.1f;
    private float rotationSpeed;

    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpSpeed = 6f;
    private float vecticalMovement = 0f;

    float horizontal, vertical, targetAngle, angle, tempSpeed, originalMovementMagnitude;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        BuildSurfaceMovement();
        BuildVerticalMovement();

        characterController.Move(direction);
    }

    private void BuildSurfaceMovement()
    {
        if (Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "")
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = Input.GetAxis("HorizontalJ");
            vertical = Input.GetAxis("VerticalJ");
        }
        direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude > 1.0f) direction = direction.normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;

            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            tempSpeed = speed;
            if (!characterController.isGrounded) tempSpeed /= 2;

            Vector3 directionWithCamera = (Quaternion.Euler(0f, angle, 0f) * Vector3.forward).normalized;
            originalMovementMagnitude = direction.magnitude;
            direction.x = directionWithCamera.x * tempSpeed * originalMovementMagnitude * Time.deltaTime;
            direction.z = directionWithCamera.z * tempSpeed * originalMovementMagnitude * Time.deltaTime;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void BuildVerticalMovement()
    {
        if (!characterController.isGrounded)
            vecticalMovement -= gravity * Time.deltaTime;

        if (((Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "") && Input.GetButtonDown("Jump")) || Input.GetJoystickNames().Length > 0 && Input.GetButtonDown("JumpJ")) {
            if (characterController.isGrounded)
                vecticalMovement = jumpSpeed;
        }

        direction.y = vecticalMovement * Time.deltaTime;
    }
}
