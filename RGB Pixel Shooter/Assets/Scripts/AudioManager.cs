using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    private HashSet<AudioClip> audioClips;
    private AudioSource musicSource;

    private void Awake () {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        audioClips = new HashSet<AudioClip>();

        musicSource = GetComponent<AudioSource>();

        if (musicSource == null) {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySoundtrack (AudioClip soundtrack) {
        PlayLoopInternal(soundtrack);
    }

    public void PlaySound (AudioClip clip) {
        PlaySoundPitched(clip, 1f);
    }

    // pitchRange represents a value of 1+

    public void PlaySoundPitched (AudioClip clip, float pitchRange) {
        pitchRange = Mathf.Max(pitchRange, 1f);

        if (!audioClips.Contains(clip)) {
            PlayOneShotInternal(clip, pitchRange);
            audioClips.Add(clip);

            Tweener.Invoke(clip.length, () => {
                audioClips.Remove(clip);
            });
        }
    }

    private void PlayOneShotInternal (AudioClip clip, float pitchRange) {
        GameObject audioSourceObject = new GameObject("AudioSource");
        audioSourceObject.transform.parent = transform;

        AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(1f / pitchRange, pitchRange);
        audioSource.loop = false;
        audioSource.Play();
    }

    private void PlayLoopInternal (AudioClip clip) {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
