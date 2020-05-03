[System.Serializable]
public class SaveState {

    // SaveState acts more like a struct than a class, hence the public fields
    public Inventory inventory;
    public int coins;
    public int unlockedLevels;

    public SaveState () { }

    public SaveState (Inventory inventory, int coins, int unlockedLevels) {
        this.inventory = inventory;
        this.coins = coins;
        this.unlockedLevels = unlockedLevels;
    }
}