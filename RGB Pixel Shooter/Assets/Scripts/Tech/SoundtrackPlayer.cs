using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for levelScene
public class SoundtrackPlayer : MonoBehaviour {

    public AudioClip soundtrack;
    public bool playOnStart;

    private void Start () {
        if (playOnStart) {
            PlaySoundtrack();
        }
    }

    public void PlaySoundtrack () {
        AudioManager.GetInstance().PlaySoundtrack(soundtrack);
    }
}
