using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField : MonoBehaviour {

    public GameObject playSpace;
    public int rowCount = 3;
    private int columnCount = 8;

    private RectTransform rect;

    private static List<List<Transform>> playGrid = new List<List<Transform>>();
    private static float spaceDim;

    private void Start () {
        rect = GetComponent<RectTransform>();
        spaceDim = rect.sizeDelta.y / rowCount;
        columnCount = Mathf.FloorToInt(rect.sizeDelta.x / spaceDim);
        Vector3 startPos = transform.position + new Vector3(spaceDim / 2, spaceDim / 2);
        for (int i = 0; i < rowCount; i++) {
            playGrid.Add(new List<Transform>());
            for (int j = 0; j < columnCount; j++) {
                GameObject currSpace = Instantiate(playSpace, startPos + new Vector3(j * spaceDim, i * spaceDim), Quaternion.identity, transform);
                currSpace.transform.localScale = new Vector2(spaceDim, spaceDim);
                currSpace.name = "[" + i + ", " + j + "]";
                currSpace.layer = LayerMask.NameToLayer("PlayField");
                playGrid[i].Add(currSpace.transform);
            }
        }

    }
    public static Vector2 GetSpacePosition (int i, int j) {
        return playGrid[i][j].position;
    }

    public static float GetSpaceHeight () {
        return spaceDim;
    }
}
