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
        SaveState saveState = new SaveState();

        // TODO: Replace dummy values with real values
        saveState.inventory = new Inventory();
        saveState.coins = 46;
        saveState.unlockedLevels = 4;

        return saveState;
    }

    private void ApplySaveState (SaveState saveState) {
        // TODO: Apply the SaveState to the running game
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

        Debug.Log(saveState.coins);
    }
}