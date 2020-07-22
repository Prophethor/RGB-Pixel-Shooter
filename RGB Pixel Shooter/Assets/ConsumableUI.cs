using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableUI : MonoBehaviour
{

    public Canvas dragCanvas;
    public GameObject dragItem;

    public void StartDrag () {
        dragItem = Instantiate(gameObject, Input.mousePosition, transform.rotation) as GameObject;
        dragItem.transform.SetParent(dragCanvas.transform);
        dragItem.GetComponent<Image>().SetNativeSize();
        dragItem.GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
        dragItem.transform.localScale = 0.8f * dragItem.transform.localScale;
    }

    public void Drag () {
        dragItem.transform.position = Input.mousePosition;
    }

    public void EndDrag() {
        Debug.Log(Camera.main.ScreenToWorldPoint(dragItem.transform.position));
        Destroy(dragItem);
    }
}
