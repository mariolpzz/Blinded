using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnim;
    public float speed = 12;
    public float rotationSpeed = 15;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    void FixedUpdate()
    {

        
    }


    private void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        var velocity = movement * speed;

        characterController.SimpleMove(movement);

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


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerAnim.SetBool("isRunning", true);
            playerAnim.SetBool("isWalking", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerAnim.SetBool("isRunning", false);
        }
    }
}

