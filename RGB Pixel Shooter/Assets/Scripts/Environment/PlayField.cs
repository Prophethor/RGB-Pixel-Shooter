using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField : MonoBehaviour {

    public GameObject lanePrefab;
    public int laneCount = 3;

    private RectTransform playField;

    private static List<Transform> laneList = new List<Transform>();
    private static float laneHeight;

    private void Start () {
        playField = GetComponent<RectTransform>();
        laneHeight = playField.rect.height / laneCount;
        Vector3 startPos = transform.position + new Vector3(0,laneHeight/2);
        for (int i = 0; i < laneCount; i++) {
            GameObject currSpace = Instantiate(lanePrefab, startPos + new Vector3(playField.rect.width/2, i * laneHeight), Quaternion.identity, transform);
            currSpace.transform.localScale = new Vector2(playField.rect.width, laneHeight);
            currSpace.name = "Lane[" + i + "]";
            currSpace.layer = LayerMask.NameToLayer("PlayField");
            laneList.Add(currSpace.transform);
        }

    }
    public static Vector2 GetLanePosition (int i) {
        return laneList[i].position;
    }

    public static float GetLaneHeight () {
        return laneHeight;
    }
}
