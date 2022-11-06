using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeeThroughWallsCamera : MonoBehaviour
{

    public GameObject player;
    public Material transparent;
    public Material baseMaterial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();

        Debug.DrawRay(transform.position, player.transform.position - transform.position + new Vector3(0, 1, 0), Color.red);

        if (Physics.Raycast(transform.position, player.transform.position - transform.position + new Vector3(0, 1, 0), out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.tag);
            if (hit.collider.gameObject.tag != "Player")
            {
                baseMaterial = hit.transform.GameObject().GetComponent<MeshRenderer>().material;
                hit.transform.GameObject().GetComponent<MeshRenderer>().material = transparent;
            } else
            {
                hit.transform.GameObject().GetComponent<MeshRenderer>().material = baseMaterial;
            }
        }
    }
}
