using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveTester : MonoBehaviour {
    [SerializeField, SerializeReference]
    public Weapon item;

    public void AddItem () {
        InventoryManager.GetInstance().GetInventory().AddItem(new Revolver());
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(SaveTester))]
public class SaveTesterEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        if (GUILayout.Button("Add item")) {
            ((SaveTester) target).AddItem();
        }

        if (GUILayout.Button("Save")) {
            SaveManager.GetInstance().Save();
        }
        if (GUILayout.Button("Load")) {
            SaveManager.GetInstance().Load();
        }
    }
}

#endif