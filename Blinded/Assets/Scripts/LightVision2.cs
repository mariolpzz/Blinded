using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVision2 : MonoBehaviour
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

        if (vision.range < 35 && !max)
        {
            vision.range += 0.7f;

        }
        else if (!max)
        {
            max = true;

        }
        else if (vision.range > 0)
        {
            vision.spotAngle -= 10;
        }
        else
        {
            running = false;
        }

    }
}
