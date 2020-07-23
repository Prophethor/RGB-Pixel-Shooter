using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{

    public GameObject panel;

    public void Play () {
        Tweener.AddTween(
            () => panel.transform.position.y,
            (x) => { panel.transform.position = new Vector3(panel.transform.position.x, x, panel.transform.position.z); },
            Camera.main.WorldToScreenPoint(new Vector3(0, 20f, 0)).y,
            0.5f,
            TweenMethods.Ease,
            true);
    }
}
