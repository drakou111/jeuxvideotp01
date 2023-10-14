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

  private float gravity = 9.81f;
  private float jumpSpeed = 6f;
  private float vecticalMovement = 0f;
  private bool doubleJump = false;

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
    horizontal = Input.GetAxis("Horizontal");
    vertical = Input.GetAxis("Vertical");
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
    if (characterController.isGrounded)
      doubleJump = true;
    else
      vecticalMovement -= gravity * Time.deltaTime;

    if (Input.GetButtonDown("Jump"))
    {
      if (characterController.isGrounded || doubleJump)
        vecticalMovement = jumpSpeed;

      if (!characterController.isGrounded && doubleJump)
        doubleJump = false;
    }

    direction.y = vecticalMovement * Time.deltaTime;
  }
}
