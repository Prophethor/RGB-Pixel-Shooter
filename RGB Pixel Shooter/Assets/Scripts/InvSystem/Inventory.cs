using System;
using System.Collections.Generic;

[Serializable]
public class Inventory {

    private List<ItemToken> items;
    //commented out for the sake of mental health (was unused and was giving a warning all the time)
    //private Loadout loadout;

    private int weaponMax = 6;
    private int consumableMax = 6;
    private int gearMax = 6;

    public Inventory () {
        items = new List<ItemToken>();
    }

    /*public Loadout GetLoadout () {
        return loadout;
    }*/

    public List<ItemToken> GetItems () {
        return items;
    }

    //public List<Weapon> GetWeapons () {
    //    return weapons;
    //}

    //public List<Consumable> GetConsumables () {
    //    return consumables;
    //}

    //public List<Gear> GetGear () {
    //    return gearItems;
    //}

    public bool AddItem (ItemToken item) {
        items.Add(item);
        return true;
    }

    //private bool AddWeapon (Weapon weapon) {
    //    if (weapons.Count >= weaponMax) {
    //        return false;
    //    }

    //    weapons.Add(weapon);
    //    return true;
    //}

    //private bool AddConsumable (Consumable consumable) {
    //    if (consumables.Count >= consumableMax) {
    //        return false;
    //    }

    //    consumables.Add(consumable);
    //    return true;
    //}

    //private bool AddGear (Gear gear) {
    //    if (gearItems.Count >= gearMax) {
    //        return false;
    //    }

    //    gearItems.Add(gear);
    //    return true;
    //}

    public bool RemoveItem (ItemToken item) {
        if (items.Contains(item)) {
            return items.Remove(item);
        }

        return false;
    }
}
