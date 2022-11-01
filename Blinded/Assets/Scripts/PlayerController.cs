using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnim;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Esto hace que el personaje se pueda mover y este animado
        var velocity = Vector3.forward * Input.GetAxis("Vertical") * speed;
        transform.Translate(velocity * Time.deltaTime);
        playerAnim.SetFloat("Speed_f", velocity.magnitude);
    }
}
