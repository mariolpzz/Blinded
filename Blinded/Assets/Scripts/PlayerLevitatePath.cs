using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevitatePath : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2;
    private int index = 0;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (index <= waypoints.Length - 1)
        {

            rb.AddForce(waypoints[index].transform.position - transform.position);

            if (transform.position == waypoints[index].transform.position)
            {
                index++;
            }
        }
    }
}
