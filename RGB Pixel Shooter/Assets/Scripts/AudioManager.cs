using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    private HashSet<AudioClip> audioClips;

    private void Awake () {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        audioClips = new HashSet<AudioClip>();
    }

    public void PlaySound (AudioClip clip) {
        if (!audioClips.Contains(clip)) {
            PlayOneShot(clip, 2f);
            audioClips.Add(clip);

            Tweener.Invoke(clip.length, () => {
                audioClips.Remove(clip);
            });
        }
    }

    private void PlayOneShot (AudioClip clip, float pitchRange) {
        GameObject audioSourceObject = new GameObject("AudioSource");
        audioSourceObject.transform.parent = transform;

        AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(1f / pitchRange, 1f * pitchRange);
        audioSource.loop = false;
        audioSource.Play();
    }
}
