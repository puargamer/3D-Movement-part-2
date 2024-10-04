using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    
    [Range(0,5)]
    public float speed = 2f;
    [Range(0, 5)]
    public float jumpHeight = 1f;
    public float gravityValue = -9.81f;

    private bool isGrounded;
    private Vector3 playerVelocity;

    public Transform forward;
    public bool moving;

    private Vector3 move;
    public float turnSmoothTime = 5f;
    public float turnSmoothVelocity;

    public AnimationStateController _AnimationStateController;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        moveCheck();
        jump();
        gravity();
    }

    //moves and rotates player
    void moveCheck()
    {
        move = forward.forward * Input.GetAxis("Vertical") + forward.right * Input.GetAxis("Horizontal");

        moving = move != Vector3.zero ? true: false;

        if (moving) 
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, move, 5 * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        if(moving) { _AnimationStateController.isRunning(true); }
        else { _AnimationStateController.isRunning(false); }

        //Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(move * Time.deltaTime * speed);
    }

    void jump()
    {
        if (isGrounded)
        {
            _AnimationStateController.isJumping(false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = jumpHeight * -gravityValue;

            _AnimationStateController.isJumping(true);
        }
    }

    void gravity()
    {
        if (isGrounded == false)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

}
