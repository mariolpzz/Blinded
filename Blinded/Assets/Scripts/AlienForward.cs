using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienForward : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-5, 0, 0) * Time.deltaTime;
    }
}
