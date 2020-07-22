using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InventoryUITest : MonoBehaviour {
    public GameObject inventoryPanelPrefab;

    public Weapon item1;
    public Weapon item2;

    public List<ItemToken> items;

    public void ShowInventory () {
        GameObject invPanel = Instantiate(inventoryPanelPrefab, FindObjectOfType<Canvas>().transform);

        items = InventoryManager.GetInstance().GetInventory().GetItems();

        invPanel.GetComponent<InventoryView>().SetItems(6, items);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(InventoryUITest))]
public class InventoryUITestEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        if (GUILayout.Button("Show inventory")) {
            ((InventoryUITest) target).ShowInventory();
        }
    }
}

#endif
