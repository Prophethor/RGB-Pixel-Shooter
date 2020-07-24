using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Helper class to connect EventTrigger and play a sound effect
public class SoundEffectTrigger : MonoBehaviour {

    public AudioClip soundEffect;
    public bool overlapping;

    public void Play () {
        AudioManager.GetInstance().PlaySound(soundEffect, overlapping);
    }
}
