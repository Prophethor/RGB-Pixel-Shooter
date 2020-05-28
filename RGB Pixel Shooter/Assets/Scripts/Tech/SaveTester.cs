using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveTester : MonoBehaviour {

}

#if UNITY_EDITOR

[CustomEditor(typeof(SaveTester))]
public class SaveTesterEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        if (GUILayout.Button("Save")) {
            SaveManager.GetInstance().Save();
        }
        if (GUILayout.Button("Load")) {
            SaveManager.GetInstance().Load();
        }
    }
}

#endif