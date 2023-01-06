using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerV6 : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject handsRadio;
    [SerializeField]
    private GameObject childBody;
    private GameObject objectOnHands;
    private GameObject TargetObject;
    public GameObject StonePrefab;
    private Collider[] itemsInside;
    private Vector3 boyDirection;

    private bool canThrowStone = true;


    private void Start()
    {
        objectOnHands = StonePrefab;
    }

    private void Update()
    {

        boyDirection = handsRadio.transform.position - childBody.transform.position;
        Debug.Log("Boy direction: " + boyDirection);

        //SETTING THE RAYCAST AND THE OVERLAPSPHERE TO GET ITEMS AROUND
        //=============================================================

        itemsInside = Physics.OverlapSphere(handsRadio.transform.position, 2.50f);
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
                        OutlineObject(TargetObject);
                    }
                    else
                    {
                        UndoOutlineObject(item.gameObject);
                    }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (objectOnHands != null)
            {
                LeaveObject(objectOnHands);
                ObjectInteraction(TargetObject);
            }
            else
            {
                ObjectInteraction(TargetObject);
            }

        }

        if (Input.GetKey(KeyCode.R))
        {
            LeaveObject(objectOnHands);

        }

        if (Input.GetMouseButtonDown(1))

        {
            if (objectOnHands == StonePrefab && canThrowStone)
            {
                GameObject stone = Instantiate(StonePrefab, handsRadio.transform.position, Quaternion.identity);
                ThrowStone(stone);
            }
            if (objectOnHands != StonePrefab)
            {
                ThrowObject(objectOnHands);
                
            }




        }

    }

    public void UndoOutlineObject(GameObject targetObject)
    {
        if (targetObject.GetComponent<Outline>() != null)
        {
            targetObject.GetComponent<Outline>().enabled = false;
        }

    }

    public void OutlineObject(GameObject targetObject)
    {
        if (targetObject.CompareTag("Movable") || targetObject.CompareTag("Pickable"))
        {
            if (targetObject.GetComponent<Outline>() == null)
            {
                targetObject.AddComponent<Outline>();
            }
            targetObject.GetComponent<Outline>().enabled = true;
        }
    }

    public void ObjectInteraction(GameObject targetObject)
    {
        if (targetObject.CompareTag("Movable"))
        {
            targetObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            objectOnHands = targetObject.transform.gameObject;
            objectOnHands.transform.position = handsRadio.transform.position;
            objectOnHands.transform.SetParent(handsRadio.gameObject.transform);
            objectOnHands.GetComponent<Rigidbody>().useGravity = false;
            objectOnHands.GetComponent<Rigidbody>().isKinematic = true;
        }
        
        if (targetObject.CompareTag("Pickable"))
        {
            Destroy(targetObject);
            objectOnHands = StonePrefab;
        }

    }

    public void LeaveObject(GameObject targetObject)
    {
        targetObject.transform.SetParent(null);
        targetObject.GetComponent<Rigidbody>().useGravity = true;
        targetObject.GetComponent<Rigidbody>().isKinematic = false;
        targetObject.gameObject.layer = LayerMask.NameToLayer("Default");
        objectOnHands = StonePrefab;

    }

    public void ThrowObject(GameObject targetObject)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(cam.transform.position, mousePos - transform.position, Color.red);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {


            Vector3 distanceToThrow = hit.point - objectOnHands.transform.position;
            distanceToThrow = distanceToThrow / 2;
            distanceToThrow.y += 5;
            LeaveObject(targetObject);

            targetObject.GetComponent<Rigidbody>().AddForce(distanceToThrow, ForceMode.VelocityChange);


        }
        

    }

    public void ThrowStone(GameObject targetObject)
    {
        canThrowStone = false;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(cam.transform.position, mousePos - transform.position, Color.red);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100))
        {

            Vector3 distanceToThrow = hit.point - handsRadio.transform.position;
            distanceToThrow = distanceToThrow / 2;
            distanceToThrow.y += 5;
            targetObject.GetComponent<Rigidbody>().AddForce(distanceToThrow, ForceMode.VelocityChange);
            targetObject.GetComponent<Rigidbody>().useGravity = true;
            targetObject.GetComponent<Rigidbody>().isKinematic = false;

        }
        Invoke(nameof(ResetCanThrowStone), 5);

    }

    public void ResetCanThrowStone()
    {
        canThrowStone = true;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PushablePullable"))
        {
            if (Input.GetKey(KeyCode.E)) 
            {
                other.attachedRigidbody.velocity = boyDirection;
            }
        }
    }
}
