using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(LevelInfo))]
public class LevelInfoEditor : Editor {

    ReorderableList waveList;

    private void OnEnable () {
        SerializedProperty waves = serializedObject.FindProperty("waves");

        waveList = new ReorderableList(serializedObject, waves, true, true, true, true);
        //waveList.elementHeight = 7f * EditorGUIUtility.singleLineHeight + 2f;

        waveList.elementHeightCallback = (int index) => {
            float height = 7f;

            var element = new SerializedObject(waveList.serializedProperty.GetArrayElementAtIndex(index).objectReferenceValue);

            SerializedProperty enemyList = element.FindProperty("enemyPool");
            if (enemyList.isExpanded) {
                // Plus 1 for the "Size" field
                height += enemyList.arraySize + 1;
            }

            SerializedProperty typeDistrList = element.FindProperty("typeDistr");
            if (typeDistrList.isExpanded) {
                // Plus 1 for the "Size" field
                height += typeDistrList.arraySize + 1;
            }

            return height * (EditorGUIUtility.singleLineHeight + 2f);
        };


        waveList.onAddCallback += (ReorderableList list) => {
            int index = waves.arraySize++;
            list.index = index;

            SerializedProperty newElement = waves.GetArrayElementAtIndex(index);

            // WaveInfo objects must be added to the LevelInfo asset
            WaveInfo newWave = ScriptableObject.CreateInstance<WaveInfo>();
            newWave.name = "Wave " + (list.index + 1);
            AssetDatabase.AddObjectToAsset(newWave, serializedObject.targetObject);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(serializedObject.targetObject));

            newElement.objectReferenceValue = newWave;
        };

        waveList.onRemoveCallback += (ReorderableList list) => {
            // WaveInfo objects must also be deleted from the corresponding LevelInfo asset
            SerializedProperty selectedItem = waves.GetArrayElementAtIndex(list.index);
            AssetDatabase.RemoveObjectFromAsset(selectedItem.objectReferenceValue);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(serializedObject.targetObject));

            // When deleting an element from the array, it is first set to null.
            // Then, if it is null, it is removed and the array is shrunk
            // Hence we must sometimes call the Delete method twice
            if (waves.GetArrayElementAtIndex(list.index) != null) {
                waves.DeleteArrayElementAtIndex(list.index);
            }
            waves.DeleteArrayElementAtIndex(list.index);

            list.index = Mathf.Min(list.index, waves.arraySize - 1);
        };

        waveList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), new GUIContent("Wave List"));
        };

        waveList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            if (waveList.serializedProperty.GetArrayElementAtIndex(index).objectReferenceValue == null) {
                Debug.Log("This shit null");
            }
            var element = new SerializedObject(waveList.serializedProperty.GetArrayElementAtIndex(index).objectReferenceValue);

            if (element == null) {
                Debug.Log("Element is null");
            }
            else if (element.FindProperty("pointPool") == null) {
                Debug.Log("PointPool is null");
            }

            float currentHeight = 0f;

            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight + 2f), new GUIContent("Wave " + (index + 1)));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 2f, rect.width, EditorGUIUtility.singleLineHeight + 2f),
                element.FindProperty("pointPool"));
            currentHeight = 2f * (EditorGUIUtility.singleLineHeight + 2f);

            float enemyPoolHeight = 1f;
            if (element.FindProperty("enemyPool").isExpanded) {
                enemyPoolHeight += element.FindProperty("enemyPool").arraySize + 1f;
            }
            enemyPoolHeight *= (EditorGUIUtility.singleLineHeight + 2f);

            EditorGUI.PropertyField(new Rect(rect.x, rect.y + currentHeight, rect.width, enemyPoolHeight),
                element.FindProperty("enemyPool"), true);
            currentHeight += enemyPoolHeight;

            float typeDistrHeight = 1f;
            if (element.FindProperty("typeDistr").isExpanded) {
                typeDistrHeight += element.FindProperty("typeDistr").arraySize + 1f;
            }
            typeDistrHeight *= (EditorGUIUtility.singleLineHeight + 2f);

            EditorGUI.PropertyField(new Rect(rect.x, rect.y + currentHeight, rect.width, typeDistrHeight),
                element.FindProperty("typeDistr"), true);
            currentHeight += typeDistrHeight;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y + currentHeight, rect.width, EditorGUIUtility.singleLineHeight + 2f),
                element.FindProperty("redDistribution"));
            currentHeight += EditorGUIUtility.singleLineHeight + 2f;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y + currentHeight, rect.width, EditorGUIUtility.singleLineHeight + 2f),
                element.FindProperty("greenDistribution"));
            currentHeight += EditorGUIUtility.singleLineHeight + 2f;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y + currentHeight, rect.width, EditorGUIUtility.singleLineHeight + 2f),
                element.FindProperty("blueDistribution"));
            currentHeight += EditorGUIUtility.singleLineHeight + 2f;

            element.ApplyModifiedProperties();
        };
    }

    public override void OnInspectorGUI () {
        waveList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
