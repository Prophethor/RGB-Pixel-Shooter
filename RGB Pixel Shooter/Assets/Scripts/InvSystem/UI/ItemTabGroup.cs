using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTabGroup : MonoBehaviour {

    public InventoryView inventoryView;
    public List<ItemTabButton> tabButtons;

    private ItemTabButton selectedTab;

    public void SwitchToTab (ItemTabButton tabButton, int slotCount, List<ItemToken> items, ItemFilter filter = null) {
        ResetTabs();

        selectedTab = tabButton;
        inventoryView.SetItems(slotCount, items, filter);
    }

    private void ResetTabs () {
        foreach (ItemTabButton tabButton in tabButtons) {
            tabButton.ResetButton();
        }
    }
}
