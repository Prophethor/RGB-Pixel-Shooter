﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x+3, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
