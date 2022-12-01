using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorReaction : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer mesh;
    void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("hay trigger");
        if (other.CompareTag("vision"))
        {
            mesh.enabled = true;
            StartCoroutine(ExampleCoroutine());
            
        }
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        mesh.enabled = false;

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
