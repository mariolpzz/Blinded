using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnim;
    public float speed = 5;
    public float rotationSpeed = 1;
    private CharacterController characterController;
    //private bool lantern;
    private Light lantern;

    private bool isJumping;
    private bool isGrounded;
    public float jumpSpeed = 20;
    [SerializeField] private float ySpeed = 0;
    private float gravity = -10f;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private float jumpButtonGracePeriod = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        lantern = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.F))
        {
            lantern.enabled = !lantern.enabled;
        }

    }

    private void Move()
    {

        isGrounded = characterController.isGrounded;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        var velocity = movement * speed;

        if (isGrounded)
        {
            lastGroundedTime = Time.time;
            ySpeed = 0;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {

            playerAnim.SetBool("isGrounded", true);
            playerAnim.SetBool("isJumping", false);
            isJumping = false;
            playerAnim.SetBool("isFalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                playerAnim.SetBool("isJumping", true);
                isJumping = true;
            }
        } else
        {
            playerAnim.SetBool("isGrounded", false);
            
            if (isJumping && ySpeed < 0 || ySpeed < -1)
            {
                playerAnim.SetBool("isFalling", true);

            }
        }

        if (movement.magnitude != 0)
        {
            var rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }

        if (movement != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }


        if (Input.GetKey(KeyCode.LeftShift) && movement != Vector3.zero)
        {
            playerAnim.SetBool("isRunning", true);
            playerAnim.SetBool("isWalking", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerAnim.SetBool("isRunning", false);
        }

        ySpeed += gravity * Time.deltaTime;
        movement.y = ySpeed;
        characterController.Move(movement * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

