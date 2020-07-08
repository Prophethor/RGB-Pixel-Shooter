using UnityEngine;

public interface Item {

    string GetName ();
    Sprite GetIcon ();

    ItemToken GetToken ();
}
