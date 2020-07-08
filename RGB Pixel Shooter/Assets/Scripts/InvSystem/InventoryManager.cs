using UnityEngine;
using System.Collections;

public class InventoryManager {

    private static InventoryManager instance;

    private Inventory inventory;

    private InventoryManager () {
        inventory = new Inventory();
    }

    public static InventoryManager GetInstance () {
        if (instance == null) {
            instance = new InventoryManager();
        }

        return instance;
    }

    public Inventory GetInventory () {
        return inventory;
    }

    public void SetInventory (Inventory inventory) {
        this.inventory = inventory;
    }

}
