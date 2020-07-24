using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{

    public LevelInfo level;
    [Header("Audio Clips")]
    public AudioClip levelSelectSFX;

    private void Awake () {
        if (level != null) {
            GetComponent<Button>().onClick.AddListener(() => {
                AudioManager.GetInstance().PlaySound(levelSelectSFX);
                SceneLoader.GetInstance().SetLevel(level);
                SceneLoader.GetInstance().LoadLevel();
            });
        }
        else {
            GetComponent<Button>().onClick.AddListener(() => {
                AudioManager.GetInstance().PlaySound(levelSelectSFX);
                SceneLoader.GetInstance().LoadScene("Tutorial");
            });
        }
    }
}
