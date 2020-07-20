using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate bool ItemFilter (ItemToken item);

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

    public void SetItems (int slotCount, List<ItemToken> items, ItemFilter filter = null) {
        this.items = items;
        ClearSlots();

        // Set up InventorySlots
        for (int i = 0; i < slotCount; i++) {
            GameObject newSlot = Instantiate(slotPrefab);
            newSlot.transform.SetParent(transform);
            newSlot.GetComponent<InventorySlot>().IsSelectable = false;
            slotObjects.Add(newSlot);
        }
        
        foreach (ItemToken item in items) {
            if (filter == null || filter(item)) {
                FillSlot(item);
            }
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
                slot.IsSelectable = true;
                return;
            }
        }
    }
}
