using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindController : MonoBehaviour
{

    public float speed = 12;
    private CharacterController characterController;
    private float rotationSpeed = 2;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); 
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        var velocity = movement * speed;

        //characterController.SimpleMove(velocity);

        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            characterController.SimpleMove(Vector3.zero);
        } else
        {
            characterController.SimpleMove(velocity);
        }

        if (movement.magnitude != 0 && !playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            var rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }

        if (movement != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            playerAnim.SetTrigger("attack");
        }
    }

}
