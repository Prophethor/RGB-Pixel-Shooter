using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField : MonoBehaviour
{

    public GameObject playSpace;
    public Material otherMat;
    public int rowCount = 3;
    private int columnCount = 8;

    private float spaceDim;
    private RectTransform rect;

    private static List<List<Transform>> playGrid = new List<List<Transform>>();

    private void Start()
    {
        bool even = false;
        rect = GetComponent<RectTransform>();
        spaceDim = rect.sizeDelta.y / rowCount;
        columnCount = Mathf.FloorToInt(rect.sizeDelta.x/spaceDim);
        Vector3 startPos = transform.position + new Vector3(spaceDim / 2, spaceDim / 2);
        for (int i = 0; i < rowCount; i++)
        {
            playGrid.Add(new List<Transform>());
            for (int j = 0; j < columnCount; j++)
            {
                GameObject currSpace = Instantiate(playSpace, startPos + new Vector3(j * spaceDim, i * spaceDim), Quaternion.identity, transform);
                currSpace.transform.localScale = new Vector2(spaceDim, spaceDim);
                if (even)
                {
                    currSpace.GetComponent<MeshRenderer>().material = otherMat;
                }
                even = !even;

                currSpace.name = "[" + i + ", " + j + "]";
                playGrid[i].Add(currSpace.transform);
            }
            if (columnCount % 2 == 0) even = !even;
        }
                
    }
    public static float GetLanePosition(int i)
    {
        return playGrid[i][0].position.y;
    }
}
