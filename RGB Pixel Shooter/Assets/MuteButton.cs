using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Sprite unmuted, muted;

    public void SetMuted () {
        GetComponent<Image>().sprite = muted;
    }

    public void SetUnmuted () {
        GetComponent<Image>().sprite = unmuted;
    }
}
