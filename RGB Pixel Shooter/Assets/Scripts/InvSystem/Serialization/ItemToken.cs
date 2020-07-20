using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as a descriptor for an Item ScriptableObject
[System.Serializable]
public abstract class ItemToken {

    private List<string> tags;

    public abstract void Read (ScriptableObject obj);
    public abstract ScriptableObject Instantiate ();

    public abstract string GetName ();
    public abstract Sprite GetIcon ();

    public List<string> GetTags () {
        return tags;
    }

    public bool HasTag (string tag) {
        return tags.Contains(tag);
    }
}
