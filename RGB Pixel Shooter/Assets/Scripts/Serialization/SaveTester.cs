using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveTester : MonoBehaviour {

}

[CustomEditor(typeof(SaveTester))]
public class SaveTesterEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        //This draws the default screen.  You don't need this if you want
        //to start from scratch, but I use this when I'm just adding a button or
        //some small addition and don't feel like recreating the whole inspector.
        if (GUILayout.Button("Save")) {
            SaveManager.GetInstance().Save();
        }
        if (GUILayout.Button("Load")) {
            SaveManager.GetInstance().Load();
        }
    }
}