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
    private List<Gear> gearItems;
    private List<Consumable> consumables;

    public Loadout () {
        weapons = new List<Weapon>();
        gearItems = new List<Gear>();
        consumables = new List<Consumable>();
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
    public List<Gear> GetGear () {
        return gearItems;
    }

    public bool AddGear (Gear gear) {
        if (gearItems.Count < maxGear) {
            gearItems.Add(gear);
            return true;
        }

        return false;
    }

    public bool RemoveGear (Gear gear) {
        if (gearItems.Contains(gear)) {
            return gearItems.Remove(gear);
        }

        return false;
    }

    public List<Consumable> GetConsumables () {
        return consumables;
    }

    public bool AddConsumables (Consumable consumable) {
        if (consumables.Count < maxConsumables) {
            consumables.Add(consumable);
            return true;
        }

        return false;
    }

    public bool RemoveConsumables (Consumable consumable) {
        if (consumables.Contains(consumable)) {
            return consumables.Remove(consumable);
        }

        return false;
    }
}
