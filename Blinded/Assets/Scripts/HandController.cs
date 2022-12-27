using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    public GameObject hands;

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) 
        {
            if (other.gameObject.CompareTag("Pickable"))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    Destroy(other);

                }

            }

            if (other.gameObject.CompareTag("Movable"))
            {

                if (Input.GetKey(KeyCode.E) && PlayerController3D.getIsOnHand() == false)
                {
                    PlayerController3D.setIsOnHand(true);

                    other.gameObject.transform.SetParent(hands.gameObject.transform);
                    other.GetComponent<Rigidbody>().useGravity = false;
                    other.GetComponent<Rigidbody>().isKinematic = true;

                    Vector3 center = other.GetComponent<Renderer>().bounds.center;
                    other.transform.position = hands.transform.position;





                }
            }

            if (Input.GetKey(KeyCode.R) && (PlayerController3D.getIsOnHand() == true))
            {

                other.GetComponent<Rigidbody>().useGravity = true;
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.transform.SetParent(null);

                PlayerController3D.setIsOnHand(false);


            }
        }
        
    }


}
