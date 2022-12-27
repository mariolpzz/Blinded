using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    private Animator playerAnim;
    public CharacterController controller;
    public float speed = 5;
    public float turnSmoothTime = 3;
    float turnSmoothVelocity;
    public Transform cam;
    private Light lantern;
    public static bool isOnHand = false;
    public float forceMagnitude = 1;






    void Start()
    {
        playerAnim = GetComponent<Animator>();
        lantern = GetComponentInChildren<Light>();

    }

    
    // Update is called once per frame
    void Update()
    {
         Movement();

        if (Input.GetKeyDown(KeyCode.F))
        {
            lantern.enabled = !lantern.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("jumpOver");
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;
        
        if(direction.magnitude != 0)
        {

            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);


            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            controller.Move(moveDir.normalized*speed*Time.deltaTime);
        }

        if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }


        if (Input.GetKey(KeyCode.LeftShift) && direction != Vector3.zero)
        {
            playerAnim.SetBool("isRunning", true);
            playerAnim.SetBool("isWalking", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerAnim.SetBool("isRunning", false);
        }
    }
    
    
    //Controller of the collisions of the body while walking
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody objectRB = hit.collider.attachedRigidbody;

        if (objectRB != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            objectRB.AddForceAtPosition(forceDirection * forceMagnitude, transform.position,ForceMode.Force);
        }
    }

    public static void setIsOnHand(bool state)
    {
        if (state == true)
        {
            isOnHand = true;
        }else
        {
            isOnHand = false;
        }
    }

    public static bool getIsOnHand()
    {
        return isOnHand;
    }



}
