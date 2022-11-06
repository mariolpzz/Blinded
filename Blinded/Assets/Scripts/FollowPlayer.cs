using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset = new Vector3(0, 6, -6);

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(35, 0, 0));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
