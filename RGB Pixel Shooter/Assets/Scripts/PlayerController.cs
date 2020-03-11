using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Lane")
                {
                    if (transform.position.y > hit.collider.transform.position.y) {
                        transform.position = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
                    } else if (transform.position.y < hit.collider.transform.position.y) {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
                    }
                    
                }
            }
        }
    }
}
