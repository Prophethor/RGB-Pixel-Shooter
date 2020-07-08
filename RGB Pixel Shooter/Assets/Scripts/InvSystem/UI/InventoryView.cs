using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour {

    public GameObject slotPrefab;

    private List<ItemToken> items;
    private List<GameObject> slotObjects;

    private void ClearSlots () {
        if (slotObjects == null) {
            slotObjects = new List<GameObject>();
        }

        foreach (GameObject slotObject in slotObjects) {
            Destroy(slotObject);
        }

        slotObjects.Clear();
    }

    public void SetItems (int slotCount, List<ItemToken> items) {
        this.items = items;
        ClearSlots();

        // Set up InventorySlots
        for (int i = 0; i < slotCount; i++) {
            GameObject newSlot = Instantiate(slotPrefab);
            newSlot.transform.SetParent(transform);
            slotObjects.Add(newSlot);
        }

        for (int i = 0; i < items.Count; i++) {
            slotObjects[i].GetComponent<InventorySlot>().SetItem(items[i]);
        }
    }

    public void FillSlot (ItemToken item) {
        if (slotObjects == null) {
            Debug.LogError("slots is null");
            return;
        }

        foreach (GameObject slotObject in slotObjects) {
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            if (!slot.IsFilled()) {
                slot.SetItem(item);
                return;
            }
        }
    }
}
