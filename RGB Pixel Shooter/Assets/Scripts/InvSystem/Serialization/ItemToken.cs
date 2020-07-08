using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as a descriptor for an Item ScriptableObject
public interface ItemToken {
    void Read (ScriptableObject obj);
    ScriptableObject Instantiate ();

    string GetName ();
    Sprite GetIcon ();
}
