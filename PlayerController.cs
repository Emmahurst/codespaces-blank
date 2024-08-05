using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; // 0: left, 1: middle, 2: right
    public float laneDistance = 4; // the distance between 2 lanes

    public float jumpForce = 10f;
    public float gravity = -20f;
    private Vector3 direction;
    public float moveSpeed = 5f;
    private bool isSliding = false;
    private float slideTimer = 0f;
    public float slideSpeed = 10f;
    public float slideDuration = 2f;
    private float currentSpeed;

    public bool isGrounded;

    private float originalHeight;
    private Vector3 originalCenter;
    public float slideHeight = 0.5f;
    public Vector3 slideCenter = new Vector3(0, 0.25f, 0);

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = forwardSpeed; // Initialize current speed

        // Save original height and center of the CharacterController
        originalHeight = controller.height;
        originalCenter = controller.center;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && direction.y < 0)
        {
            direction.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            StartSlide();
        }

        if (isSliding)
        {
            slideTimer += Time.deltaTime;
            if (slideTimer >= slideDuration)
            {
                EndSlide();
            }
        }

        // Gather the inputs on which lane we should be in
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane > 2)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane < 0)
                desiredLane = 0;
        }

        // Calculate the target position
        Vector3 targetPosition = transform.position.z * Vector3.forward + transform.position.y * Vector3.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        // Will nicely move towards the target lanes
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).x * moveSpeed;

        // Apply gravity
        if (!isGrounded)
        {
            direction.y += gravity * Time.deltaTime;
        }

        // Set the forward movement
        direction.z = currentSpeed;

        // Increase player speed
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
            if (forwardSpeed > maxSpeed)
            {
                forwardSpeed = maxSpeed;
            }
        }

        // Move the character
        controller.Move((moveVector + direction) * Time.deltaTime);
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTimer = 0f;
        currentSpeed = slideSpeed;

        // Change the character's height and center for sliding
        controller.height = slideHeight;
        controller.center = slideCenter;
    }

    private void EndSlide()
    {
        isSliding = false;
        currentSpeed = forwardSpeed;

        // Revert the character's height and center to original values
        controller.height = originalHeight;
        controller.center = originalCenter;
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }

}

