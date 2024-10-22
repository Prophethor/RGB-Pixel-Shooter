﻿using System;
using System.Collections.Generic;

[Serializable]
public class Inventory {

    private List<Item> items;
    private List<Weapon> weapons;
    private List<Consumable> consumables;
    private List<Gear> gearItems;
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
