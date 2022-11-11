using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 myVector;
    public float speed = 200.0f;
    public Transform pivote;
    public Transform fantasma;
    private float posicion = -2.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (fantasma.transform.position.x == posicion)
        {
            posicion = posicion + 1f;
            myVector = new Vector3(posicion, transform.position.y, transform.position.z);
            fantasma.transform.position = myVector;
        }
        transform.RotateAround(pivote.transform.position, -Vector3.up, speed * Time.deltaTime);
      
            
    }
}
