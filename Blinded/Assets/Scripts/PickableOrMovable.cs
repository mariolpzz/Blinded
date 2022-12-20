using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableOrMovable : MonoBehaviour
{
    public GameObject item;

    private bool isPicked = false;

    

    // Update is called once per frame
    void Update()
    {
        if (isPicked)
        {
            if (Input.GetKey(KeyCode.R))
            {
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<Rigidbody>().isKinematic = true;

                item.gameObject.transform.SetParent(null);
                isPicked = false;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && item.gameObject.CompareTag("Pickable"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(item);

            }
        }

        if (other.gameObject.CompareTag("Player") && item.gameObject.CompareTag("Movable"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                isPicked = true;
                item.GetComponent<Rigidbody>().useGravity = false;
                item.GetComponent<Rigidbody>().isKinematic = false;
                item.transform.position = (other.transform.position) + new Vector3(-10* Time.deltaTime, 0 * Time.deltaTime, 1);
                item.gameObject.transform.SetParent(other.gameObject.transform);



            }
        }
    }
}
