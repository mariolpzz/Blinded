using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BlindVision : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public GameObject vision;
    private bool isFinished = true;

    // Start is called before the first frame update
    void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.positionCount = 2;

    }

    // Update is called once per frame
    void Update()
    {
        Vision2();
    }


    void Vision1()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //Destroy(lineRenderer, 3);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);

        }


        lineRenderer.SetPosition(0, lineRenderer.GetPosition(0) + new Vector3(-10, 0, 10) * Time.deltaTime);
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(1) + new Vector3(10, 0, 10) * Time.deltaTime);

    }

    void Vision2()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vision.SetActive(true);
            isFinished = false;
        }

        if (vision.transform.localScale.magnitude < new Vector3(40, 40, 40).magnitude && !isFinished)
        {
            vision.transform.localScale += new Vector3(30, 30, 30) * Time.deltaTime;
        }
        else
        {
            vision.SetActive(false);
            vision.transform.localScale = new Vector3(0, 0, 0);
            isFinished = true;
        }
    }


    void Vision3()
    {

    }
}
