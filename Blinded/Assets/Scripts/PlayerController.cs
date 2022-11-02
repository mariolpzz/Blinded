using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnim;
    public float speed = 5;
    public float rotationSpeed = 15;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        //Esto hace que el personaje se pueda mover y este animado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        //Rota al personaje para apuntar a donde te mueves
        if (movement.magnitude != 0)
        {
            var rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }

        
        transform.Translate(velocity * Time.deltaTime);
        var velocity = movement * speed;
        playerAnim.SetFloat("Speed_f", velocity.magnitude);
    }
    */

    private void Update()
    {

    }

    void FixedUpdate()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        var velocity = movement * speed;

        playerRb.AddForce(velocity / Time.deltaTime);

        if (movement.magnitude != 0)
        {
            var rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }

        playerAnim.SetFloat("Speed_f", velocity.magnitude);
    }
}
