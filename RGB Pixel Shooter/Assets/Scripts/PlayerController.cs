using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float offset = 1.0f;
    void Start()
    {
        transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x+50, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameBehaviour.levelLost && !GameBehaviour.levelWon)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Lane")
                {
                    if (transform.position.y > hit.collider.transform.position.y+offset) {
                        StartCoroutine(MovePlayer(new Vector3(0,-100,0)));
                    } else if (transform.position.y < hit.collider.transform.position.y-offset) {
                        StartCoroutine(MovePlayer(new Vector3(0, 100, 0)));
                    }
                    
                }
            }
        }
    }


    IEnumerator MovePlayer(Vector3 deltapos)
    {
        float i = 0;
        Vector3 pos = transform.position;
        while (i < 1.0f)
        {
            transform.position = Vector3.Lerp(transform.position, pos + deltapos, i);
            i += 0.1f;
            yield return null;
        }
    }
}
