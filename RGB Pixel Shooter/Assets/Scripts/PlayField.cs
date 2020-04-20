using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField : MonoBehaviour
{
    private int rowCount = 3;
    private int columnCount = 8;

    private static List<List<Transform>> playGrid = new List<List<Transform>>();
    private void Awake()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        for(int i = 0; i < rowCount; i++)
        {
            playGrid.Add(new List<Transform>());
            for(int j = 0; j < columnCount; j++)
            {
                playGrid[i].Add(children[columnCount * i + j + 1]);
            }
        }
    }

    public static float GetLanePosition(int i)
    {
        return playGrid[i][i].position.y;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
            Debug.Log(hit.transform.name);
        }
    }
}
