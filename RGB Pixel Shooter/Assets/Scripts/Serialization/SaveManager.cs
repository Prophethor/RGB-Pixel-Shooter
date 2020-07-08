using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager {

    private static SaveManager instance;

    private SaveManager () { }

    public static SaveManager GetInstance () {
        if (instance == null) {
            instance = new SaveManager();
        }

        return instance;
    }

    private SaveState GenerateSaveState () {
        SaveState saveState = new SaveState {

            // TODO: Replace dummy values with real values
            inventory = InventoryManager.GetInstance().GetInventory(),
            coins = 46,
            unlockedLevels = 4
        };

        return saveState;
    }

    private void ApplySaveState (SaveState saveState) {
        InventoryManager.GetInstance().SetInventory(saveState.inventory);
        Debug.Log(saveState.coins);
    }

    public void Save () {
        SaveState saveState = GenerateSaveState();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.sav");
        bf.Serialize(file, saveState);
        file.Close();
    }

    public void Load () {
        SaveState saveState;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenRead(Application.persistentDataPath + "/gamesave.sav");
        saveState = (SaveState) bf.Deserialize(file);
        file.Close();

        ApplySaveState(saveState);
    }
}