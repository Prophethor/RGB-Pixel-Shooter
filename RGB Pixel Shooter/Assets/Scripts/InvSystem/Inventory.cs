using System;
using System.Collections.Generic;

[Serializable]
public class Inventory {
    // TODO: Inventory space limit!
    private List<Item> items;

    public Inventory () {
        items = new List<Item>();
    }

    public List<Item> GetItems () {
        return items;
    }

    public bool AddItem (Item item) {
        items.Add(item);
        return true;
    }

    public bool RemoveItem (Item item) {
        if (items.Contains(item)) {
            return items.Remove(item);
        }

        return false;
    }
}
