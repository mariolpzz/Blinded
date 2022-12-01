using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindController : MonoBehaviour
{

    public float speed = 12;
    private CharacterController characterController;
    private float rotationSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); 
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

        characterController.SimpleMove(velocity);

        if (movement.magnitude != 0)
        {
            var rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }
    }

}
