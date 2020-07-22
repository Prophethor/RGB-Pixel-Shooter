using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTabButton : MonoBehaviour {

    public ItemTabGroup itemTabGroup;

    public Image bgImage;

    public Color defaultColor;
    public Color selectedColor;

    // Use this to filter items to include into inventoryView
    // All items that have given tag will be included
    public string itemTag;

    public void ResetButton () {
        // Reset image sprite
        bgImage.color = defaultColor;
    }

    public void SelectTab () {
        SwitchToTab();
        // Change image sprite
        bgImage.color = selectedColor;
    }

    public void SwitchToTab () {
        itemTabGroup.SwitchToTab(this, 10, InventoryManager.GetInstance().GetInventory().GetItems(), (item) => {
            return item.HasTag(itemTag);
        });
    }
}
