using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

// Should not be manually added to GameObjects

[AddComponentMenu("")]
public class TouchManager : MonoBehaviour {
    #region Singleton

    private static TouchManager instance;

    public static TouchManager GetInstance () {
        if (instance != null) {
            return instance;
        }

        GameObject touchManagerObject = new GameObject("TouchManager");
        instance = touchManagerObject.AddComponent<TouchManager>();
        instance.listeners = new List<UnityAction<Touch>>();

        return instance;
    }

    #endregion

    private List<UnityAction<Touch>> listeners;

    public void AddListener (UnityAction<Touch> touchHandler) {
        listeners.Add(touchHandler);
    }

    public void RemoveListener (UnityAction<Touch> touchHandler) {
        listeners.Remove(touchHandler);
    }

    private void NotifyListeners (Touch touch) {
        foreach (UnityAction<Touch> action in listeners) {
            action(touch);
        }
    }

    private void Update () {
        foreach (Touch touch in Input.touches) {
            NotifyListeners(touch);
        }
    }
}
