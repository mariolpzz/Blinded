using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerV4 : MonoBehaviour
{

    public Camera cam;
    public GameObject hands;
    private GameObject objectOnHands;
    private GameObject TargetObject;
    private Collider[] itemsInside;
    private Vector3 TransformBeforeOnHands = new Vector3(0,0,0);


    private void Update()
    {

        //SETTING THE RAYCAST AND THE OVERLAPSPHERE TO GET ITEMS AROUND
        //=============================================================

        itemsInside = Physics.OverlapSphere(hands.transform.position, 2.50f);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(cam.transform.position, mousePos - transform.position, Color.blue);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {

            //CHECK IF ANY ITEM AROUND IS BEING POINTED BY THE RAYCAST
            //========================================================

            foreach (var item in itemsInside)
            {
                if (item.gameObject.Equals(hit.transform.gameObject))
                {
                    TargetObject = hit.transform.gameObject;
                }
                else 
                {
                    UndoOutlineObject(item.gameObject);
                }
            }

            //INTERACTIONS WHEN SAME OBJECT
            //=============================

            if (TargetObject == hit.transform.gameObject) 
            {
                OutlineObject(TargetObject);

                if (Input.GetMouseButtonDown(0))
                {
                    
                        GrabObject(TargetObject);
                    
                    
                }
            }
        }
    }


    public void UndoOutlineObject(GameObject targetObject)
    {
        if (targetObject.GetComponent<Outline>() == null)
        {
            targetObject.AddComponent<Outline>();
        }
        targetObject.GetComponent<Outline>().enabled = false;
    }

    public void OutlineObject(GameObject targetObject)
    {
        if (targetObject.GetComponent<Outline>() == null)
        {
            targetObject.AddComponent<Outline>();
        }
        targetObject.GetComponent<Outline>().enabled = true;
    }

    public void GrabObject(GameObject targetObject)
    {

        if (objectOnHands != null)
        {
            if (hands.GetComponent<Renderer>().bounds.Contains(TransformBeforeOnHands))
            {
                objectOnHands.transform.SetParent(null);
                objectOnHands.transform.position = TransformBeforeOnHands;
                TransformBeforeOnHands = targetObject.transform.position;
                objectOnHands = targetObject.transform.gameObject;
                objectOnHands.transform.position = hands.transform.position;
                objectOnHands.transform.SetParent(hands.gameObject.transform);
                objectOnHands.GetComponent<Rigidbody>().useGravity = false;
                objectOnHands.GetComponent<Rigidbody>().isKinematic = true;
                objectOnHands.transform.position = hands.transform.position;

            }
        }
        else 
        {
            TransformBeforeOnHands = targetObject.transform.position;
            objectOnHands = targetObject.transform.gameObject;
            objectOnHands.transform.position = hands.transform.position;
            objectOnHands.transform.SetParent(hands.gameObject.transform);
            objectOnHands.GetComponent<Rigidbody>().useGravity = false;
            objectOnHands.GetComponent<Rigidbody>().isKinematic = true;
            objectOnHands.transform.position = hands.transform.position;
        }
    }

    public void LeaveObject(GameObject targetObject)
    {
        if (hands.GetComponent<Renderer>().bounds.Contains(TransformBeforeOnHands))
        {
            Debug.Log("Contiene la posicion: " + hands.GetComponent<Renderer>().bounds.Contains(TransformBeforeOnHands));
            targetObject.transform.SetParent(null);
            targetObject.transform.position = TransformBeforeOnHands;
            targetObject.GetComponent<Rigidbody>().useGravity = true;
            targetObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        else 
        {
            targetObject.transform.SetParent(null);
            targetObject.GetComponent<Rigidbody>().useGravity = true;
            targetObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        
    }
}
