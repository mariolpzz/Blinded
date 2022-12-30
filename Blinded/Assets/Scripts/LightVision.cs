using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightVision : MonoBehaviour
{

    private bool max = false;
    private bool running = false;
    private Light vision;
    private float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        vision = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            max = false;
            if (!running)
            {
                running = true;
                CancelInvoke("IncreaseVision");
                InvokeRepeating("IncreaseVision", 0, 0.01f);
            }

        }

    }



    void IncreaseVision()
    {

        running = true;

        if (vision.spotAngle < 150 && !max)
        {
            vision.spotAngle += 0.7f;

        } else if (!max)
        {
            max = true;

        } else if (vision.spotAngle > 60)
        {
            vision.spotAngle -= 10;
        } else
        {
            running = false;
        }

    }

}