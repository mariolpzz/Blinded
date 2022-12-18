using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightVision : MonoBehaviour
{

    private bool max = false;
    private Light vision;

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
            InvokeRepeating("IncreaseVision", .01f, .01f);

        }

    }



    void IncreaseVision()
    {
        if (vision.spotAngle < 150 && !max)
        {
            vision.spotAngle += 0.7f;
        } else if (!max)
        {
            max = true;
        } else if (vision.spotAngle > 60)
        {
            vision.spotAngle -= 1;
        }
    }

}
