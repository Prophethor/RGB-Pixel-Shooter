using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance;

    public static AudioManager GetInstance () {
        if (instance == null) {
            GameObject audioManagerObject = new GameObject("AudioManager");
            audioManagerObject.AddComponent<AudioManager>();
        }

        return instance;
    }

    private HashSet<AudioClip> audioClips;
    private List<AudioSource> sfxSources;
    private AudioSource musicSource;

    [Range(0f, 1f)]
    private float sfxVolumeValue = 1f;
    [Range(0f, 1f)]
    private float musicVolumeValue = 0.6f;

    public float SfxVolumeValue {
        get => sfxVolumeValue;
        set {
            sfxVolumeValue = Mathf.Clamp01(value);
            if (sfxSources != null) {
                foreach (AudioSource source in sfxSources) {
                    source.volume = value;
                }
            }
        }
    }

    public float MusicVolumeValue {
        get => musicVolumeValue;
        set {
            musicVolumeValue = Mathf.Clamp01(value);
            if (musicSource != null) {
                musicSource.volume = value;
            }
        }
    }

    private void Awake () {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        audioClips = new HashSet<AudioClip>();
        sfxSources = new List<AudioSource>();

        musicSource = GetComponent<AudioSource>();

        if (musicSource == null) {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySoundtrack (AudioClip soundtrack) {
        PlayLoopInternal(soundtrack);
    }

    // stavio sam opcionalni bool koji ako definises kao true, onda mozes da odes u metod koji dozvoljava da se zvuci stakuju
    public void PlaySound (AudioClip clip, bool overlappingSound = false) {
        PlaySoundPitched(clip, 1f, overlappingSound);
    }

    // pitchRange represents a value of 1+

    public void PlaySoundPitched (AudioClip clip, float pitch, bool overlappingSound = false) {
        if (!overlappingSound && audioClips.Contains(clip)) {
            return;
        }

        PlayOneShotInternal(clip, pitch);

        if (!overlappingSound) {
            audioClips.Add(clip);

            Tweener.Invoke(clip.length, () => {
                audioClips.Remove(clip);
            });
        }
    }

    private void PlayOneShotInternal (AudioClip clip, float pitch) {
        GameObject audioSourceObject = new GameObject("AudioSource");
        audioSourceObject.transform.parent = transform;

        AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.loop = false;
        audioSource.volume = SfxVolumeValue;
        audioSource.Play();

        sfxSources.Add(audioSource);

        // TODO: Replace creation and destruction of GameObject with object pooling pattern
        Tweener.Invoke(audioSource.clip.length, () => {
            sfxSources.Remove(audioSource);
            Destroy(audioSourceObject);
        });
    }

    private void PlayLoopInternal (AudioClip clip) {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = musicVolumeValue;
        musicSource.Play();
    }
}
