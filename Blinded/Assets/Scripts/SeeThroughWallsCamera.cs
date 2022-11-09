using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeeThroughWallsCamera : MonoBehaviour
{

    public GameObject player;
    public bool obstructed = false;
    private Material baseMaterial;
    public Renderer rend;
    private bool bigObject;
    List<Renderer> childs = new List<Renderer>();
    private Shader opaco = Shader.Find("Standard");

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Creo un RaycastHit que me devolvera info de donde apunta la camara
        RaycastHit hit = new RaycastHit();

        //Esto es para ver en el inspector el rayo que simula a donde esta apuntando la camara
        Debug.DrawRay(transform.position, player.transform.position - transform.position + new Vector3(0, 1, 0), Color.red);

        if (Physics.Raycast(transform.position, player.transform.position - transform.position + new Vector3(0, 1, 0), out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.tag);


            //Si no esta "viendo" al Player, bajo el componente "Alpha" del material que esta obstruyendo
            if (hit.collider.gameObject.tag != "Player")
            {
                bigObject = false;
                rend = hit.transform.GameObject().GetComponent<Renderer>();
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color colorAux = rend.material.color;
                colorAux.a = 0.3F;
                rend.material.color = colorAux;

                obstructed = true;
            }
            else
            {
                if (obstructed)
                { 
                    rend.material.shader = Shader.Find("Standard");
                    obstructed = false;
                }
            }
        }
    }
}

