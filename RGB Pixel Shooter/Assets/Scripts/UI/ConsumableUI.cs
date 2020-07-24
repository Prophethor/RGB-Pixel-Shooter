using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableUI : MonoBehaviour {

    public Canvas dragCanvas;
    [HideInInspector]
    public GameObject dragItem;
    public GameObject blinds;
    public Consumable consumable; // For now, this is the ScriptableObject

    public void StartDrag () {
        
        dragItem = Instantiate(gameObject, Input.mousePosition, transform.rotation) as GameObject;
        dragItem.transform.SetParent(dragCanvas.transform);
        dragItem.GetComponent<Image>().SetNativeSize();
        dragItem.GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
        dragItem.transform.localScale = 0.8f * dragItem.transform.localScale;
        AudioManager.GetInstance().PlaySound(consumable.GetPickupAudio(), true);
    }

    public void Drag () {
        dragItem.transform.position = Input.mousePosition;
    }

    public void EndDrag () {
        if (PlayField.OnField(Camera.main.ScreenToWorldPoint(dragItem.transform.position))) {
            consumable.Use(Camera.main.ScreenToWorldPoint(dragItem.transform.position));
            Tweener.AddTween(() => blinds.GetComponent<RectTransform>().sizeDelta.y, (x) => {
                blinds.GetComponent<RectTransform>().sizeDelta = new Vector2(blinds.GetComponent<RectTransform>().sizeDelta.x, x);
            }, 230, 0.3f, true);
            
            Destroy(gameObject);
        }
        Destroy(dragItem);
        
    }
}
