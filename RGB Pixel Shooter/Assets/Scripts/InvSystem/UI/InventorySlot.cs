using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image itemIcon;

    public void SetItem (ItemToken item) {
        itemIcon.sprite = item.GetIcon();
        itemIcon.preserveAspect = true;
        itemIcon.gameObject.SetActive(true);
    }

    public bool IsFilled () {
        return itemIcon.enabled;
    }
}
