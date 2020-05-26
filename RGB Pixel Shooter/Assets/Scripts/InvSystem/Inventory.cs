using System;
using System.Collections.Generic;

[Serializable]
public class Inventory {
    // TODO: Inventory space limit!
    private List<Item> items;
    private Loadout loadout;

    public Inventory () {
        items = new List<Item>();
    }

    public Loadout GetLoadout () {
        return loadout;
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
