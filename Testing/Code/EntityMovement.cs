using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

// EDITED BY AADITYA PUTTAGUNTA

[RequireComponent(typeof(CharacterController))]
public class EntityMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float playerSpeed;
    public float playerJumpPower;
    public float gravity;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public bool canMove = true;
    CharacterController characterController;

    public int jumps = 0;
    public int maxJumps;
    public int dashes = 0;
    public int maxDashes;
    private float delay = 0f;
    public float dashRechargeSpeed;
    public float dashPower;
    public float dashTime;
    Vector3 dashDirection = Vector3.zero;

    public int topCount; //top limit for camera bobbing
    public int bottomCount; //bottom limit for camera bobbing
    private int camCounter = 0; //counter for bobbing
    private bool goingUp = true; //bool for camera bobbing current direction
    public float bobRate; //The speed at which to bob camera at when grounded

    private bool sprint = false;
    public float sprintSpeed; // relative to the regular speed
    public float stamina;
    public float maxStamina;

    private bool cursorLocked = true;

    private float t;

    void Start()
    {
        t = Time.time;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {

        #region mouseLock
        if (Input.GetKeyUp(KeyCode.F))
        {
            cursorLocked = !cursorLocked;
            canMove = cursorLocked;
            Cursor.lockState = (cursorLocked) ? CursorLockMode.Locked : CursorLockMode.None;
        }
        #endregion

        #region Handles Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? playerSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? playerSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;

        sprint = Input.GetKey(KeyCode.LeftShift) ? true : false;    // change to getButtonDown later for more compatibility
        if (sprint && stamina > 1.5f)
        {
            moveDirection = (forward * curSpeedX) * sprintSpeed + (right * curSpeedY) * sprintSpeed;
            if (Time.time - t >= 1)
            {
                stamina -= 0.5f;
                t = 0f;
            }
        }
        else
        {
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            if (Time.time - t >= 1.5)
            {
                if (stamina < maxStamina)
                    stamina += 0.5f;
                t = 0f;
            }
        }



        #endregion

        #region Handles Jumping
        bool notMoving = true;
        if (Input.GetButtonDown("Jump") && canMove && jumps < maxJumps)
        {
            moveDirection.y = playerJumpPower;
            jumps++;
            notMoving = false;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            //print("Adding Downward");
        }
        else if (notMoving && jumps > 0)
        {
            jumps = 0;
        }

        #endregion

        characterController.Move(moveDirection * Time.deltaTime);

        #region Handles Dashing
        if (dashes > 0)
        {
            delay += Time.deltaTime;
            if (delay >= dashRechargeSpeed)
            {
                delay = 0;
                dashes = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && dashes < maxDashes)
        {
            dashes++;
            //This line is somewhat similar to multithreading in which you can run calculations without interfering
            //with the main code. Perfect for while loops.
            StartCoroutine(dash());
        }
        #endregion

        #region Handles Rotation
        if (canMove && playerCamera != null)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            #region Handles Character Bobbing
            if (characterController.isGrounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            {
                if (camCounter < topCount && goingUp)
                {
                    camCounter++;
                    playerCamera.transform.Translate(0, Time.deltaTime * bobRate, 0, Space.World);
                }
                else if (camCounter == topCount && goingUp)
                {
                    goingUp = false;
                }
                else if (camCounter > bottomCount && !goingUp)
                {
                    camCounter--;
                    playerCamera.transform.Translate(0, -Time.deltaTime * bobRate, 0, Space.World);
                }
                else if (camCounter == bottomCount && !goingUp)
                {
                    goingUp = true;
                }
            }
            #endregion

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion

        
    }

    //Dash function to run over a period of frames
    IEnumerator dash()
    {
        float start = Time.time;
        while (Time.time < start + dashTime)
        {
            characterController.Move(transform.TransformDirection(Vector3.forward) * dashPower * Time.deltaTime);
            yield return null; //It may be strange to have a return in a while loop but what this does is
            //return every action and continue the while loop at the next frame. This prevents it from
            //stopping the code without needing to run asynchnously.
        }
    }
}