using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnim;
    public float speed = 12;
    public float rotationSpeed = 3;
    private CharacterController characterController;
    //private bool lantern;
    private Light lantern;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("jumpOver");
        }

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


        if (Input.GetKey(KeyCode.LeftShift) && movement != Vector3.zero)
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

