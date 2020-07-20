using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventorySlot : MonoBehaviour {

    public Image itemIcon;
    public Image selectedIcon;

    private ItemToken item;

    private Button button;

    public bool IsSelectable {
        get {
            return button.IsActive();
        }
        set {
            button.enabled = value;
        }
    }

    private bool isSelected;

    private void Awake () {
        button = GetComponent<Button>();

        // TODO: Replace with custom listener determined by InventoryView
        button.onClick.AddListener(() => {
            if (isSelected) {
                InventoryManager.GetInstance().GetInventory().GetLoadout().RemoveItem(item);
                SetSelection(false);
            }
            else {
                if (InventoryManager.GetInstance().GetInventory().GetLoadout().AddItem(item)) {
                    SetSelection(true);
                }
            }
        });
    }

    public void SetSelection (bool selected) {
        isSelected = selected;
        selectedIcon.gameObject.SetActive(selected);
    }

    public void SetItem (ItemToken item) {
        this.item = item;

        itemIcon.sprite = item.GetIcon();
        itemIcon.preserveAspect = true;
        itemIcon.gameObject.SetActive(true);

        if (InventoryManager.GetInstance().GetInventory().GetLoadout().Contains(item)) {
            SetSelection(true);
        }
    }

    public bool IsFilled () {
        return itemIcon.gameObject.activeInHierarchy;
    }
}
