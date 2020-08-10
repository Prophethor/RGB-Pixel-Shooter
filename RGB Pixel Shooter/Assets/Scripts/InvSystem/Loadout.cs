using System;
using System.Collections.Generic;

[Serializable]
public class Loadout {
    private const int minWeapons = 1;
    private const int maxWeapons = 2;
    private const int minGear = 0;
    private const int maxGear = 3;
    private const int minConsumables = 0;
    private const int maxConsumables = 3;

    // Minimum of 1 weapon for the Loadout to be valid
    private List<Weapon> weapons;
    private List<ItemToken> gearItems;
    private List<ItemToken> consumables;

    public Loadout () {
        weapons = new List<Weapon>();
        gearItems = new List<ItemToken>();
        consumables = new List<ItemToken>();
    }

    public bool IsValid () {
        if (weapons.Count < minWeapons || maxWeapons < weapons.Count) {
            return false;
        }

        if (gearItems.Count < minGear || maxGear < gearItems.Count) {
            return false;
        }

        if (consumables.Count < minConsumables || maxConsumables < consumables.Count) {
            return false;
        }

        return true;
    }

    public bool AddItem (ItemToken item) {
        if (item.HasTag("Weapon")) {
            return AddWeapon((Weapon) item);
        }
        else if (item.HasTag("Gear")) {
            return AddGear(item);
        }
        else {
            return AddConsumable(item);
        }
    }

    public bool RemoveItem (ItemToken item) {
        if (item.HasTag("Weapon")) {
            return RemoveWeapon((Weapon) item);
        }
        else if (item.HasTag("Gear")) {
            return RemoveGear(item);
        }
        else {
            return RemoveConsumable(item);
        }
    }

    public List<Weapon> GetWeapons () {
        return weapons;
    }

    public bool AddWeapon (Weapon weapon) {
        if (weapons.Count < maxWeapons) {
            weapons.Add(weapon);
            return true;
        }

        return false;
    }

    public bool RemoveWeapon (Weapon weapon) {
        if (weapons.Contains(weapon)) {
            return weapons.Remove(weapon);
        }

        return false;
    }
    public List<ItemToken> GetGear () {
        return gearItems;
    }

    public bool AddGear (ItemToken gear) {
        if (gearItems.Count < maxGear) {
            gearItems.Add(gear);
            return true;
        }

        return false;
    }

    public bool RemoveGear (ItemToken gear) {
        if (gearItems.Contains(gear)) {
            return gearItems.Remove(gear);
        }

        return false;
    }

    public List<ItemToken> GetConsumables () {
        return consumables;
    }

    public bool AddConsumable (ItemToken consumable) {
        if (consumables.Count < maxConsumables) {
            consumables.Add(consumable);
            return true;
        }

        return false;
    }

    public bool RemoveConsumable (ItemToken consumable) {
        if (consumables.Contains(consumable)) {
            return consumables.Remove(consumable);
        }

        return false;
    }

    public bool Contains (ItemToken item) {
        if (item is Weapon) {
            return weapons.Contains((Weapon) item);
        }
        //else if (item is Gear) {
        //    return weapons.Contains((Weapon) item);
        //}
        //else if (item is Consumable) {
        //    return weapons.Contains((Weapon) item);
        //}
        else {
            return false;
        }
    }
}
