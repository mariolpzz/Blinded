using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCotrollerV2 : MonoBehaviour
{
    public GameObject hands;
    private Collider nearestCollider = null;
    private Collider[] itemsInside;

    void FixedUpdate()
    {
        //USE OF AN OVERLAPSPHERE TO CALCULATE THE NEAREST OBJECT TO THE HANDS
        //====================================================================

        float distanceFromHand;
        float lastNearestDistance = 100f;
        itemsInside = Physics.OverlapSphere(this.transform.position, 2.50f);
        if (itemsInside.Length != 0)
        {
            foreach (var item in itemsInside)
            {
                distanceFromHand = Vector3.Distance(item.transform.position, this.transform.position);

                if (item.CompareTag("Movable") || item.CompareTag("Pickable"))
                {


                    if (distanceFromHand < lastNearestDistance)
                    {
                        lastNearestDistance = distanceFromHand;
                        nearestCollider = item;
                    }
                }

            }
            //Debug.Log("Collider cercano definitivo " + nearestCollider);
        }
        else {
            lastNearestDistance = 100f;
        }
        

    }

    private void OnTriggerStay(Collider other)
    {
        //ADDING OUTLINE COMPONENT TO THE NEAREST OBJECT
        //==============================================
        if (nearestCollider != null) 
        {
            if (nearestCollider.gameObject.GetComponent<Outline>() == null) 
            {
            nearestCollider.gameObject.AddComponent<Outline>();
            }
            

            //IF THE NEAREST OBJECT IS EQUALS TO ANY OBJECT INSIDE THE HANDS TRIGGER
            //======================================================================

            if (other.gameObject.Equals(nearestCollider.gameObject))
            {

                nearestCollider.gameObject.GetComponent<Outline>().enabled = true;

                //THIS IF IS TO AVOID PROBLEMS WITH THE HAND COLLISION ON THE CHARACTER CONTROLLER COLLIDER
                //AND ONLY INTERACT WITH OBJECTS WITH THE "MOVABLE/PICKABLE" LABEL
                //=========================================================================================
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

                            nearestCollider.gameObject.transform.SetParent(hands.gameObject.transform);
                            nearestCollider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                            nearestCollider.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                            Vector3 center = other.GetComponent<Renderer>().bounds.center;
                            nearestCollider.gameObject.transform.position = hands.transform.position;





                        }
                    }

                    if (Input.GetKey(KeyCode.R) && (PlayerController3D.getIsOnHand() == true))
                    {

                        nearestCollider.gameObject.GetComponent<Rigidbody>().useGravity = true;
                        nearestCollider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        nearestCollider.gameObject.transform.SetParent(null);

                        PlayerController3D.setIsOnHand(false);


                    }

                    if (Input.GetKey(KeyCode.T) && (PlayerController3D.getIsOnHand() == true))
                    {

                        nearestCollider.gameObject.GetComponent<Rigidbody>().useGravity = true;
                        nearestCollider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        nearestCollider.gameObject.transform.SetParent(null);
                        PlayerController3D.setIsOnHand(false);
                        nearestCollider.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * 1000);


                    }

                }

            }
            else
            {
                nearestCollider.gameObject.GetComponent<Outline>().enabled = false;
            }
        }
        


    }

    //WHEN ANY OBJECT LEAVES THE HANDS COLLIDER WE SET THE OUTLINE OFF
    //================================================================

    private void OnTriggerExit(Collider other)
    {

        nearestCollider.gameObject.GetComponent<Outline>().enabled = false;
    }

}
