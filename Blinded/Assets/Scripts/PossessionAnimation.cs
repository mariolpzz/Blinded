using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 200.0f;
    public Transform pivote;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivote.transform.position, -Vector3.up, speed++ * Time.deltaTime);

    }
}
