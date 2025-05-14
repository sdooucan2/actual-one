using System;
using System.Net.Http.Headers;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;


public class Player_Movement : MonoBehaviour
{

    [SerializeField]CharacterController player;
    [SerializeField]Camera playerCamera;
    public float speed = 5f;
    public float gravityforce = 5f;
    public float jumpHeigh = 2f;
    private Vector2 inputVector;
    private Vector3 playerVelocity;
    private float inputJump;
    private bool isJumpPressed;

    void Start()
    {

        player = GetComponent<CharacterController>();

        if(playerCamera == null)
        {
            playerCamera = Camera.main;
        }

    }
    void OnMove(InputValue Pos)
    {
        
        inputVector = Pos.Get<Vector2>();
    }

    void OnJump(InputValue jump)
    {
        isJumpPressed = jump.isPressed;
    }


    void Update()
    {

 
        if(!player.isGrounded)
        {   
           playerVelocity.y -= gravityforce * Time.deltaTime;
        }
        else
        {
            playerVelocity.y = -0.5f;
        }

        if(player.isGrounded && isJumpPressed)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeigh * gravityforce);
            isJumpPressed = false;
        }

        Vector3 cameraForwards = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        cameraForwards.y = 0;
        cameraRight.y = 0;
        cameraForwards.Normalize();
        cameraRight.Normalize();
        
        Vector3 move = cameraRight * inputVector.x + cameraForwards * inputVector.y;

        player.Move(move * speed * Time.deltaTime);
        player.Move(playerVelocity * Time.deltaTime);


    }



}
