using System;
using System.Collections.Generic;

[Serializable]
public class Inventory {

    private List<Item> items;
    private List<Weapon> weapons;
    private List<Consumable> consumables;
    private List<Gear> gearItems;
    //commented out for the sake of mental health (was unused and was gining a warning all the time)
    //private Loadout loadout;

    private int weaponMax = 6;
    private int consumableMax = 6;
    private int gearMax = 6;

    private Dictionary<Type, Func<Item, bool>> methodDict;

    public Inventory () {
        items = new List<Item>();
        weapons = new List<Weapon>();
        consumables = new List<Consumable>();
        gearItems = new List<Gear>();

        methodDict = new Dictionary<Type, Func<Item, bool>>();
    }

    private void InitMethodDict () {

    }

    /*public Loadout GetLoadout () {
        return loadout;
    }*/

    public List<Item> GetItems () {
        return items;
    }

    public List<Weapon> GetWeapons () {
        return weapons;
    }

    public List<Consumable> GetConsumables () {
        return consumables;
    }

    public List<Gear> GetGear () {
        return gearItems;
    }

    public bool AddItem (Item item) {
        items.Add(item);
        return true;
    }

    private bool AddWeapon (Weapon weapon) {
        if (weapons.Count >= weaponMax) {
            return false;
        }

        weapons.Add(weapon);
        return true;
    }

    private bool AddConsumable (Consumable consumable) {
        if (consumables.Count >= consumableMax) {
            return false;
        }

        consumables.Add(consumable);
        return true;
    }

    private bool AddGear (Gear gear) {
        if (gearItems.Count >= gearMax) {
            return false;
        }

        gearItems.Add(gear);
        return true;
    }

    public bool RemoveItem (Item item) {
        if (items.Contains(item)) {
            return items.Remove(item);
        }

        return false;
    }
}
