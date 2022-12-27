using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCotrollerV2 : MonoBehaviour
{
    public GameObject hands;
    private Collider nearestCollider = null;
    private Collider[] itemsInside;


    //GameObject colliderObject = item.gameObject;
    //colliderObject.AddComponent<Outline>();
    void Update()
    {

        float distanceFromHand;
        float lastNearestDistance = 100f;
        itemsInside = Physics.OverlapSphere(this.transform.position, 1f);
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
            Debug.Log("Collider cercano definitivo " + nearestCollider);
        }
        else {
            lastNearestDistance = 100f;
        }
        

    }

    private void OnTriggerStay(Collider other)
    {

        nearestCollider.gameObject.AddComponent<Outline>();
        if (other.gameObject.Equals(nearestCollider.gameObject))
        {
            
            nearestCollider.gameObject.GetComponent<Outline>().enabled = true;
            Debug.Log("Comprobacion son iguales " + other.gameObject.Equals(nearestCollider.gameObject));
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
                    nearestCollider.gameObject.gameObject.transform.SetParent(null);

                    PlayerController3D.setIsOnHand(false);

             
                }
            }

        }
        else 
        {
            nearestCollider.gameObject.GetComponent<Outline>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        nearestCollider.gameObject.GetComponent<Outline>().enabled = false;
    }

}
